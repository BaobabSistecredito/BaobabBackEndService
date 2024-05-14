using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using Microsoft.EntityFrameworkCore;
using BaobabBackEndService.Utils;

namespace BaobabBackEndSerice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponsController : Controller
    {
        private readonly BaobabDataBaseContext _context;

        public CouponsController(BaobabDataBaseContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<ResponseUtils<Coupon>>> GetCoupons()
        {
            try
            {
                var result = await _context.Coupons.ToListAsync();
                return new ResponseUtils<Coupon>(true, result, null, "todo oki");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, $"Errors: {ex.Message}"));
            }
        }
    }
}
