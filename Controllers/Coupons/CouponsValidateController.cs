using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.Coupons;

namespace BaobabBackEndService.Controllers.Coupons
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponsValidateController : Controller
    {
        private readonly ICouponsServices _couponService;
        public CouponsValidateController(ICouponsServices couponService)
        {
            _couponService = couponService;
        }
        // ----------------------- VALIDATE ACTION:
        [HttpGet]
        public async Task<ActionResult<ResponseUtils<Coupon>>> ValidateCoupon([FromBody] CouponValidationRequest request)
        {
            var response = await _couponService.ValidateCoupon(request.CouponCode, request.PurchaseValue);
            if (!response.Status)
            {
                return StatusCode(422, response);
            }
            return Ok(response);
        }
    }
}