using Microsoft.AspNetCore.Mvc;
using BaobabBackEndService.DTOs;
using BaobabBackEndService.Services;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;

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
            // Aquí deberías validar las credenciales del usuario.
            // Por simplicidad, vamos a suponer que siempre son válidas.

            var user = new MarketingUser
            {
                Username = userLoginDto.Username,
                Email = "usuario@ejemplo.com"  // Deberías obtener el email del usuario desde la base de datos
            };

            var token = _jwtService.GenerateJwtToken(user);
            

            return Ok(new ResponseUtils<object>(true,new List<object>{new{ Token = token }}));
        }
    }
}
