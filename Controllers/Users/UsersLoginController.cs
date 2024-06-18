using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.Users;
using BaobabBackEndService.DTOs;

namespace BaobabBackEndService.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UsersLoginController : ControllerBase
{
    private readonly IUsersServices _usersService;

    public UsersLoginController(IUsersServices usersService)
    {
        _usersService = usersService;
    }
    [HttpPost("login")]
    public async Task<ActionResult<ResponseUtils<MarketingUser>>> Login([FromBody] UserLoginDTO user)
    {
        var response = await _usersService.UserLoginAsync(user);
        if(!response.Status)
        {
            return StatusCode(response.Code, response);
        }
        return Ok(response);
    }
}