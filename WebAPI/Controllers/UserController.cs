using System.Threading.Tasks;
using BLL.Services;
using Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AuthService _authService;

        public UserController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Register(UserCredentialsDto userCredentials)
        {
            await _authService.CreateUser(userCredentials);
            return Ok("User created successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserCredentialsDto userCredentials)
        {
            return Ok(new { Token = await _authService.LoginUser(userCredentials) });
        }
    }
}