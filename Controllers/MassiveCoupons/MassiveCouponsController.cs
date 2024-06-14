using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using Microsoft.EntityFrameworkCore;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.MassiveCoupons;

namespace BaobabBackEndSerice.Controllers
{
    [ApiController]
    public class MassiveCouponsController : Controller
    {
        private readonly IMassiveCouponsServices _massivecouponService;
        public MassiveCouponsController(IMassiveCouponsServices massivecouponService)
        {
            _massivecouponService = massivecouponService;
        }

        [HttpGet("/api/massivecoupons")]
        public ResponseUtils<MassiveCoupon> GetAllMassiveCoupons()
        {
            try
            {
                return _massivecouponService.GetAllMassiveCoupons();
            }
            catch (Exception ex)
            {
                return new ResponseUtils<MassiveCoupon>(false, null, null, $"Error: {ex.Message}");
            }
        }

        [HttpGet("/api/massivecoupons/{searchType}/{value}")]
        public async Task<ResponseUtils<MassiveCoupon>> GetMassiveCouponsAsync(string searchType, string value)
        {
            try
            {
                var result = await _massivecouponService.GetMassiveCouponsAsync(searchType, value);
                return result;
            }
            catch (Exception ex)
            {
                return new ResponseUtils<MassiveCoupon>(false, null, null, $"Error: {ex.Message}");
            }
        }
    }
}
