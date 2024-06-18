using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using System.Collections.Generic;
using BaobabBackEndService.Services.Coupons;
using System.Globalization;
using BaobabBackEndService.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace BaobabBackEndService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/coupons")]
    public class CouponCreateController : ControllerBase
    {
        private readonly ICouponsServices _couponsService;

        public CouponCreateController(ICouponsServices couponsService)
        {
            _couponsService = couponsService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseUtils<Coupon>>> CreateCoupon(CouponDTO request)
        {
            /* if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseUtils<Coupon>(false, message: $"{ModelState}"));
            } */
            try
            {
                var response = await _couponsService.CreateCoupon(request);
                if (!response.Status)
                {
                    return StatusCode(422, response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Coupon>(false, message: "Ocurrió un error al actualizar el cupón: " + ex.Message));
            }
        }
    }

}
