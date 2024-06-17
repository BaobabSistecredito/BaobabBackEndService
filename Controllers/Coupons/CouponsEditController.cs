using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
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
        /* Definición de variables las cuales se inicializarán luego con las dependencias */
        private readonly ICouponsServices _couponsService;
        private readonly SlackNotificationService _slackNotificationService;
        // Inyección de dependencias:
        public CouponsEditController(ICouponsServices couponsService, SlackNotificationService slackNotificationService)
        {
            _couponsService = couponsService;
            _slackNotificationService = slackNotificationService; // Inicializa la clase 'SlackNotificationService'
        }
        // ----------------------- EDIT COUPON STATUS:
        [HttpPut("Status/{id}/{status}")]
        public async Task<ResponseUtils<Coupon>> EditCouponStatus(string id, string status)
        {
            return await _couponsService.EditCouponStatus(id, status);
        }
        // ----------------------- EDIT COUPON:
        [HttpPut("{marketingUserId}/{couponid}")]
        public async Task<ActionResult<ResponseUtils<CouponUpdateDTO>>> EditCoupon(int marketingUserId, int couponid, [FromBody] CouponUpdateDTO coupon)
        {
            try
            {
                var response = await _couponsService.EditCoupon(marketingUserId, couponid, coupon);
                if(!response.Status)
                {
                    return StatusCode(response.Code, response);
                }
                else
                {
                    return StatusCode(response.Code, response);
                }
            }
            catch (Exception ex)
            {
                /* En caso de error, se llama a la clase '_slackNotification' y su método 'SendNotification()' en el cual se le envía el mensaje de error generado por el sistema */
                await _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema:: {ex.Message}");
                return StatusCode(500, ex);
            }
        }
    }
}