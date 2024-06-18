using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.Users;

namespace BaobabBackEndService.Controllers.Users;

[ApiController]
[Route("api/users")]
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
        try{
            var response = await _usersService.CreateUser(newUser);
            if(!response.IsSuccessful)
            {
                return StatusCode(422, response);
            }
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(422, new ResponseUtils<MarketingUser>(false, null, 422, $"Errors: {ex.Message}"));
        }
        
    }
}