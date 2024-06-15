using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Services.Coupons;
using BaobabBackEndService.Utils;
using Microsoft.AspNetCore.Mvc;
using BaobabBackEndService.ExternalServices.SlackNotificationService;
using BaobabBackEndService.DTOs;

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
        [HttpPut("{marketinUserId}/{couponid}")]
        public async Task<ActionResult<ResponseUtils<CouponUpdateDTO>>> EditCoupon(int marketinUserId, int couponid, [FromBody] CouponUpdateDTO coupon)
        {
            try
            {
                var response = await _couponsService.EditCoupon(marketinUserId, couponid, coupon);
                return StatusCode(response.Code, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}