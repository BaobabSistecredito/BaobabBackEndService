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

        [HttpGet("{SearchType}/{Value}")]
        public async Task<ActionResult<ResponseUtils<Coupon>>> GetCoupon(string SearchType, string Value){
            try
            {
                if(!int.TryParse(SearchType, out int ParseSearchType)){
                    return BadRequest(new ResponseUtils<Coupon>(false, null, null, "SkormJF disapproved"));
                }

                IQueryable<Coupon> CuponQuery = _context.Coupons;
                switch (ParseSearchType){
                    case 1:
                        if (!int.TryParse(Value, out int couponId)) {
                            return BadRequest(new ResponseUtils<Coupon>(false, null, null, "Invalid coupon ID"));
                        }
                        CuponQuery = CuponQuery.Where(c => c.Id == couponId);
                break;
                    case 2:
                        CuponQuery = CuponQuery.Where(c => c.Title.StartsWith(Value));
                        break;
                    case 3:
                        CuponQuery = CuponQuery.Where(c => c.CouponCode.StartsWith(Value));
                        break;
                    default:
                        return BadRequest(new ResponseUtils<Coupon>(false, null, null, "SkormJF disapproved"));
                }
                var result = await CuponQuery.ToListAsync();
                if (result.Count == 0) {
                    return NotFound(new ResponseUtils<Coupon>(false, null, null, "No coupons found"));
                }
                return new ResponseUtils<Coupon>(true, result, null, "SkormJF approved");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, $"Errors: {ex.Message}"));
            }
        }
    }
}
