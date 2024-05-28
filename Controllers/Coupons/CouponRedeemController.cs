using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using System.Collections.Generic;
using BaobabBackEndService.Services.Coupons;
using System.Globalization;

namespace BaobabBackEndService.Controllers.Coupons
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponRedeemController : Controller
    {
        private readonly ICouponsServices _couponsService;

        public CouponRedeemController(ICouponsServices couponsService)
        {
            _couponsService = couponsService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseUtils<MassiveCoupon>>> redeemCoupon([FromBody]RedeemRequest redeemRequest)
        {
             try
            {
                var Redimir = await _couponsService.RedeemCoupon(redeemRequest);
                return Ok(Redimir);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<MassiveCoupon>(false, null, null, $"Errors: {ex.Message}"));
            }
        }
       
    }
}