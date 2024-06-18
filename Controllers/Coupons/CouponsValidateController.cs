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
    
    [ApiController]
    [Route("/api/coupons")]
    public class CouponsValidateController : Controller
    {
        private readonly ICouponsServices _couponService;
        public CouponsValidateController(ICouponsServices couponService)
        {
            _couponService = couponService;
        }
        // ----------------------- VALIDATE ACTION:
        [HttpGet("validate")]
        public async Task<ActionResult<ResponseUtils<Coupon>>> ValidateCoupon([FromBody] CouponValidationRequest request)
        {
            try{
                var response = await _couponService.ValidateCoupon(request.CouponCode, request.PurchaseValue);
                if (!response.IsSuccessful)
                {
                    return StatusCode(409, response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(422, new ResponseUtils<Coupon>(false, null, 422, $"Errors: {ex.Message}"));
            }
            
        }
    }
}