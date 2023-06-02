using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TimeSheet_Backend.Models.Data;
using TimeSheet_Backend.Models.DTOs;

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

        public AuthenticationController(
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            DatabaseContext databaseContext, 
            IConfiguration configuration, 
            TokenValidationParameters tokenValidationParameters,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _databaseContext = databaseContext;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
            _mapper = mapper;
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
                    return Ok("User created.");
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
                return Ok(tokenValue);
            }

            return Unauthorized();
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
            
            if(rToken != null)
            {
                var rTokenResponse = new AuthResultDTO { Token = jwtToken, ExpiresAt = token.ValidTo, RefreshToken = rToken.Token };
                return rTokenResponse;
            }

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

            var response = new AuthResultDTO()
            {
                Token = jwtToken,
                ExpiresAt = token.ValidTo,
                RefreshToken = refreshToken.Token
            };

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
