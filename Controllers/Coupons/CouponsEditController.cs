using BaobabBackEndSerice.Models;
using BaobabBackEndService.Services.Coupons;
using BaobabBackEndService.Utils;
using Microsoft.AspNetCore.Mvc;
using BaobabBackEndService.ExternalServices.SlackNotificationService;
using BaobabBackEndService.DTOs;

namespace BaobabBackEndService.Controllers.Coupons
{
    [ApiController]
    [Route("/api/coupons")]
    public class CouponsEditController : ControllerBase
    {
        /* Definición de variables las cuales se inicializarán luego con las dependencias */
        private readonly ICouponsServices _couponsService;
        private readonly SlackNotificationService _slackNotificationService;
        // Inyección de dependencias:
        public CouponsEditController(ICouponsServices couponsService, SlackNotificationService slackNotificationService)
        {
            _couponsService = couponsService;
            _slackNotificationService = slackNotificationService; // Inicializa la clase 'SlackNotificationService'
        }

        [HttpPut("status/{id}/{status}")]
        public async Task<ActionResult<ResponseUtils<Coupon>>> EditCouponStatus(string id, string status)
        {
            try
            {
                return await _couponsService.EditCouponStatus(id, status);
            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, new ResponseUtils<Coupon>(false, code: 500, message: $"Errors: {ex.Message}"));
            }
        }
        // ----------------------- EDIT COUPON:
        [HttpPut("{marketingUserId}/{couponid}")]
        public async Task<ActionResult<ResponseUtils<CouponUpdateDTO>>> EditCoupon(int marketingUserId, int couponid, [FromBody] CouponUpdateDTO coupon)
        {

            try
            {
                var response = await _couponsService.EditCoupon(marketingUserId, couponid, coupon);
                return StatusCode(response.StatusCode, response);

            }
            catch (Exception ex)
            {
                /* En caso de error, se llama a la clase '_slackNotification' y su método 'SendNotification()' en el cual se le envía el mensaje de error generado por el sistema */
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, ex);
            }
        }
    }
}