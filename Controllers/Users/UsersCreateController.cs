using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.Users;

namespace BaobabBackEndService.Controllers.Users;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersCreateController : ControllerBase
{
    private readonly IUsersServices _usersService;

    public UsersCreateController(IUsersServices usersService)
    {
        _usersService = usersService;
    }
    // ------------------------ ADD NEW USER:
    [HttpPost]
    public async Task<ActionResult<ResponseUtils<MarketingUser>>> CreateUser([FromBody] MarketingUser newUser)
    {
        var response = await _usersService.CreateUser(newUser);
        if(!response.Status)
        {
            return StatusCode(422, response);
        }
        return Ok(response);
    }
}