using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.Coupons;
using BaobabBackEndService.ExternalServices.SlackNotificationService;

namespace BaobabBackEndService.Controllers.Coupons
{

    [ApiController]
    [Route("/api/coupons")]
    public class CouponsValidateController : Controller
    {
        private readonly ICouponsServices _couponService;
        private readonly SlackNotificationService _slackNotificationService;
        public CouponsValidateController(ICouponsServices couponService,SlackNotificationService slackNotificationService)
        {
            _slackNotificationService = slackNotificationService;
            _couponService = couponService;
        }
        // ----------------------- VALIDATE ACTION:
        [HttpGet("validate")]
        public async Task<ActionResult<ResponseUtils<Coupon>>> ValidateCoupon([FromBody] CouponValidationRequest request)
        {
            try
            {
                var response = await _couponService.ValidateCoupon(request.CouponCode, request.PurchaseValue);
                if (!response.IsSuccessful)
                {
                    return StatusCode(409, response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(422, new ResponseUtils<Coupon>(false, null, 422, $"Errors: {ex.Message}"));
            }

        }
    }
}