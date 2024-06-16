using Microsoft.AspNetCore.Mvc;
using BaobabBackEndService.DTOs;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.ExternalServices.Jwt;

namespace BaobabBackEndService.Authentication
{
    [ApiController]
    [Route("api/login")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO userLoginDto)
        {
            var user = new MarketingUser
            {
                Username = "User",
                Email = "usuario@ejemplo.com"
            };

            var token = _jwtService.GenerateJwtToken(user);
            

            return Ok(new ResponseUtils<object>(true,new List<object>{new{ Token = token }}));
        }
    }
}
