using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.Users;
using BaobabBackEndService.DTOs;

namespace BaobabBackEndService.Controllers.Users;

[Route("api/users")]
[ApiController]
public class UsersLoginController : ControllerBase
{
    private readonly IUsersServices _usersService;

    public UsersLoginController(IUsersServices usersService)
    {
        _usersService = usersService;
    }
    // ------------------------ ADD NEW USER:
    [HttpPost("login")]
    public async Task<ActionResult<ResponseUtils<MarketingUser>>> Login([FromBody] UserLoginDTO user)
    {
        var response = await _usersService.UserLoginAsync(user);
        if(!response.Status)
        {
            return StatusCode((int)response.Code, response);
        }
        return Ok(response);
    }
}