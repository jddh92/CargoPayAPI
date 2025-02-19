using CargoPayAPI.Service.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CargoPayAPI.Controllers
{
    [ApiController]
    [Route("api/auth")] 
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = _authService.Authenticate(request.Username, request.Password);
            if (token == null)
                return Unauthorized("Invalid username or password");

            return Ok(new { Token = token });
        }

    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
