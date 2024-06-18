using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using System.Collections.Generic;
using BaobabBackEndService.Services.Coupons;
using System.Globalization;
using BaobabBackEndService.DTOs;
using Microsoft.AspNetCore.Authorization;
using BaobabBackEndService.ExternalServices.SlackNotificationService;

namespace BaobabBackEndService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/coupons")]
    public class CouponCreateController : ControllerBase
    {
        private readonly ICouponsServices _couponsService;
        private readonly SlackNotificationService _slackNotificationService;

        public CouponCreateController(ICouponsServices couponsService,SlackNotificationService slackNotificationService)
        {
            _slackNotificationService = slackNotificationService;
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
                if (!response.IsSuccessful)
                {
                    return StatusCode(422, response);
                }
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(422, new ResponseUtils<Coupon>(false, message: "Ocurrió un error al crear el cupón: " + ex.Message));
            }
        }
    }

}
