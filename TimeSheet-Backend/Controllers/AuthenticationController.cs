using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
                return Ok();
            }

            return Unauthorized();
        }
    }
}
