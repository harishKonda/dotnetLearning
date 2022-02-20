using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.User;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IAuthRepository _authRepositoryService; 
        public UserController(IAuthRepository authRepository)
        {
            _authRepositoryService = authRepository;
        }
        [HttpPost("registerUser")]
        public async Task<ActionResult> RegisterUser(UserRegisterDto user)
        {
            var response = await _authRepositoryService.Register(
                new User(){ UserName = user.UserName}, password: user.Password
                );
            if (response.Success == true)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto user)
        {
            var response = await _authRepositoryService.Login(user.Username, user.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}