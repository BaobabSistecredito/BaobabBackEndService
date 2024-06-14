using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Services.Coupons;
using BaobabBackEndService.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BaobabBackEndService.Controllers.Coupons
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CouponsEditController : ControllerBase
    {
        private readonly ICouponsServices _couponsService;
        public CouponsEditController(ICouponsServices couponsService)
        {
            _couponsService = couponsService;
        }

        [HttpPut("Status/{id}/{status}")]
        public async Task<ResponseUtils<Coupon>> EditCouponStatus(string id, string status)
        {
            return await _couponsService.EditCouponStatus(id, status);
        }
        // ----------------------- EDIT ACTION:
        [HttpPut("{marketinUserId}")]
        public async Task<ActionResult<ResponseUtils<Coupon>>> EditCoupon(int marketinUserId, [FromBody] Coupon coupon)
        {
            var response = await _couponsService.EditCoupon(marketinUserId, coupon);
            if(!response.Status)
            {
                return StatusCode(422, response);
            }
            return Ok(response);
        }
    }
}