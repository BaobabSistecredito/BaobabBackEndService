using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using System.Collections.Generic;
using BaobabBackEndService.Services.Coupons;
using System.Globalization;
using BaobabBackEndService.DTOs;
using BaobabBackEndService.ExternalServices.SlackNotificationService;

namespace BaobabBackEndService.Controllers.Coupons
{

    [ApiController]
    [Route("/api/coupons")]
    public class CouponRedeemController : Controller
    {
        private readonly ICouponsServices _couponsService;
        private readonly SlackNotificationService _slackNotificationService;

        public CouponRedeemController(ICouponsServices couponsService,SlackNotificationService slackNotificationService)
        {
            _slackNotificationService = slackNotificationService;
            _couponsService = couponsService;
        }

        [HttpPost("redeem")]
        public async Task<ActionResult<ResponseUtils<MassiveCoupon>>> redeemCoupon([FromBody] RedeemDTO request)
        {
            try
            {
                var redimir = await _couponsService.RedeemCoupon(request);
                return StatusCode(redimir.StatusCode, redimir);
            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, new ResponseUtils<MassiveCoupon>(false, null, 500, $"Errors: {ex.Message}"));
            }
        }

    }
}