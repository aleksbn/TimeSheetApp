using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TimeSheet_Backend.Models.Data;
using TimeSheet_Backend.Models.DTOs;
using TimeSheet_Backend.Warehouse;

namespace TimeSheet_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationController(
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            DatabaseContext databaseContext, 
            IConfiguration configuration, 
            TokenValidationParameters tokenValidationParameters,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _databaseContext = databaseContext;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide accurate fields.");
            }

            var userExists = await _userManager.FindByEmailAsync(userDTO.Email);

            if (userExists != null)
            {
                return BadRequest($"The user {userDTO.Email} has already been used.");
            }

            AppUser appUser = (AppUser)_mapper.Map(userDTO, typeof(RegisterUserDTO), typeof(AppUser));
            appUser.UserName = appUser.Email;

            var roleExists = await _roleManager.RoleExistsAsync(userDTO.Role);

            if (roleExists)
            {
                var result = await _userManager.CreateAsync(appUser, userDTO.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, userDTO.Role);
                    var userId = _userManager.FindByEmailAsync(appUser.Email);
                    return Ok(userId);
                }

                return BadRequest("User could not be created.");
            }

            return BadRequest("The role does not exist.");
        }
        
        [HttpPost("login-user")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide accurate fields.");
            }

            var userExists = await _userManager.FindByEmailAsync(userDTO.Email);
            if (userExists != null && await _userManager.CheckPasswordAsync(userExists, userDTO.Password))
            {
                var tokenValue = await GenerateJWTTokenAsync(userExists, null);
                return Ok(new { userExists.Id, tokenValue});
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPut("edit-user")]
        public async Task<IActionResult> EditUser([FromBody] EditUserDTO editUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide accurate fields");
            }

            var user = await _userManager.FindByEmailAsync(editUserDTO.OldEmail);

            if (user == null)
            {
                return NotFound("There is no user under that email address.");
            }

            if (user.Id != GetUserId())
            {
                return Unauthorized("You are not allowed to edit an account you're not logged into.");
            }

            if (await _userManager.CheckPasswordAsync(user, editUserDTO.OldPassword) == false)
            {
                return NotFound("Wrong old password");
            }

            if (editUserDTO.NewEmail != null)
            {
                var emailExists = await _userManager.FindByEmailAsync(editUserDTO.NewEmail);
                if (emailExists != null)
                {
                    return BadRequest("There is a user with that email already. Pick another one.");
                }
                user.Email = editUserDTO.NewEmail;
                user.NormalizedEmail = editUserDTO.NewEmail.ToUpper();
                user.UserName = editUserDTO.NewEmail;
                user.NormalizedUserName = editUserDTO.NewEmail.ToUpper();
            }

            if (editUserDTO.NewPassword != "")
            {
                await _userManager.ChangePasswordAsync(user, editUserDTO.OldPassword, editUserDTO.NewPassword);
            }

            if (editUserDTO.FirstName != "")
            {
                user.FirstName = editUserDTO.FirstName;
            }

            if (editUserDTO.LastName != "")
            {
                user.LastName = editUserDTO.LastName;
            }

            await _userManager.UpdateAsync(user);

            return Ok("User data changed");
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser([FromBody] LoginUserDTO deleteUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide accurate data fields");
            }

            var user = await _userManager.FindByEmailAsync(deleteUser.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, deleteUser.Password))
            {
                var companies = await _unitOfWork.CompanyRepository.GetAll(c => c.CompanyManagerId == user.Id);
                foreach (var company in companies)
                {
                    company.CompanyManagerId = "";
                }
                await _unitOfWork.Save();
                await _userManager.DeleteAsync(user);
                return Ok("User has been deleted.");
            }

            return BadRequest("Email or password are wrong");
        }

        private async Task<AuthResultDTO> VerifyAndGenerateTokenAsync(TokenRequestDTO request)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var storedToken = await _databaseContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == request.RefreshToken);
            var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);

            try
            {
                var tokenResults = jwtTokenHandler.ValidateToken(request.Token, _tokenValidationParameters, out var validatedToken);
                return await GenerateJWTTokenAsync(dbUser, storedToken);
            }
            catch (SecurityTokenExpiredException)
            {
                if (storedToken.DateExpired > DateTime.UtcNow)
                {
                    return await GenerateJWTTokenAsync(dbUser, storedToken);
                }
                else
                {
                    return await GenerateJWTTokenAsync(dbUser, null);
                }
            }
        }

        private async Task<AuthResultDTO> GenerateJWTTokenAsync(AppUser user, RefreshToken rToken)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(15),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            if (rToken != null)
            {
                var rTokenResponse = new AuthResultDTO { Token = jwtToken, ExpiresAt = token.ValidTo, RefreshToken = rToken.Token };
                return rTokenResponse;
            }

            var response = new AuthResultDTO()
            {
                Token = jwtToken,
                ExpiresAt = token.ValidTo,
                RefreshToken = null
            };

            var rTokensFromDb = await _databaseContext.RefreshTokens.Where(rt => rt.UserId == user.Id).OrderByDescending(rt => rt.DateExpired).ToListAsync();

            if (rTokensFromDb != null && rTokensFromDb[0].DateExpired <= DateTime.UtcNow)
            {
                var refreshToken = new RefreshToken()
                {
                    JwtId = token.Id,
                    IsRevoked = false,
                    UserId = user.Id,
                    DateAdded = DateTime.UtcNow,
                    DateExpired = DateTime.UtcNow.AddMonths(6),
                    Token = Guid.NewGuid().ToString() + Guid.NewGuid().ToString()
                };
                await _databaseContext.RefreshTokens.AddAsync(refreshToken);
                await _databaseContext.SaveChangesAsync();

                response.Token = refreshToken.Token;
            }
            else
            {
                response.RefreshToken = rTokensFromDb[0].Token;
            }

            return response;
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDTO tokenRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all required fields");
            }

            var result = await VerifyAndGenerateTokenAsync(tokenRequestDTO);
            return Ok(result);
        }
    }
}
