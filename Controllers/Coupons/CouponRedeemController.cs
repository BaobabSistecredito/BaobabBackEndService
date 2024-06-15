using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using System.Collections.Generic;
using BaobabBackEndService.Services.Coupons;
using System.Globalization;
using BaobabBackEndService.DTOs;

namespace BaobabBackEndService.Controllers.Coupons
{
    
    [ApiController]
    [Route("/api/coupons")]
    public class CouponRedeemController : Controller
    {
        private readonly ICouponsServices _couponsService;

        public CouponRedeemController(ICouponsServices couponsService)
        {
            _couponsService = couponsService;
        }

        [HttpPost("redeem")]
        public async Task<ActionResult<ResponseUtils<MassiveCoupon>>> redeemCoupon([FromBody]RedeemDTO request)
        {
            try
            {
                var Redimir = await _couponsService.RedeemCoupon(request);
                return Ok(Redimir);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<MassiveCoupon>(false, null, null, $"Errors: {ex.Message}"));
            }
        }
    
    }
}