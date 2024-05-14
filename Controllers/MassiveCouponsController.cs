using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using Microsoft.EntityFrameworkCore;
using BaobabBackEndService.Utils;

namespace BaobabBackEndSerice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MassiveCouponsController : Controller
    {
        private readonly BaobabDataBaseContext _context;

        public MassiveCouponsController(BaobabDataBaseContext context)
        {
            _context = context;
        }


        [HttpGet]


        public async Task<ActionResult<ResponseUtils<MassiveCoupon>>> GetMassiveCoupons()
        {
            try
            {
                var result = await _context.MassiveCoupons.ToListAsync();
                return new ResponseUtils<MassiveCoupon>(true, result, null, "todo oki");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<MassiveCoupon>(false, null, null, $"Error: {ex.Message}"));
            }
        }
    }
}
