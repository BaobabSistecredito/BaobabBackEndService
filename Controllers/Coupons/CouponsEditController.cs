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
    [Route("/api/coupons")]
    public class CouponsEditController : ControllerBase
    {
        private readonly ICouponsServices _couponsService;
        public CouponsEditController(ICouponsServices couponsService)
        {
            _couponsService = couponsService;
        }

        [HttpPut("{id}/{status}")]
        public async Task<ActionResult<ResponseUtils<Coupon>>> EditCouponStatus(string id, string status)
        {
            try{
                return await _couponsService.EditCouponStatus(id, status);
            }
            catch (Exception ex)
            {
                return StatusCode(422, new ResponseUtils<Coupon>(false, null, 422, $"Errors: {ex.Message}"));
            }
        }
        // ----------------------- EDIT ACTION:
        //ERROR EN POSTMAN POR NO AFECTAR COLUMNAS
        [HttpPut("{marketinUserId}")]
        public async Task<ActionResult<ResponseUtils<Coupon>>> EditCoupon(int marketinUserId, [FromBody] Coupon coupon)
        {
            try{
                var response = await _couponsService.EditCoupon(marketinUserId, coupon);
            if(!response.Status)
            {
                return StatusCode(422, response);
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