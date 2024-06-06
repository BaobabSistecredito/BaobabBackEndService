using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndService.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BaobabBackEndService.Dto;
using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using Microsoft.EntityFrameworkCore;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.MassiveCoupons;
using BaobabBackEndService.Services.User;

namespace BaobabBackEndService.Controllers.User
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Valid")]
        public async Task<ActionResult<ResponseUtils<AuthResponse>>> Login(AuthResponse authResponse)
        {
            try
            {
                var result =  await _userService.CreateToken(authResponse);
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                return new ResponseUtils<AuthResponse>(false, null, null, $"Error: {ex.Message}");
                
            }

        }
      
    }
}