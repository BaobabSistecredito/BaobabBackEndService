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
        public async Task<ActionResult<ResponseUtils<MassiveCoupon>>> redeemCoupon([FromBody] RedeemDTO request)
        {
            try
            {
                var redimir = await _couponsService.RedeemCoupon(request);
                return StatusCode(redimir.Code, redimir);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<MassiveCoupon>(false,code:500, message: $"Errors: {ex.Message}"));
            }
        }

    }
}