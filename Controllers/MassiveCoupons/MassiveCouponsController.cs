using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using Microsoft.EntityFrameworkCore;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.MassiveCoupons;
using BaobabBackEndService.ExternalServices.SlackNotificationService;

namespace BaobabBackEndSerice.Controllers
{
    [ApiController]
    [Route("/api/massivecoupons")]

    public class MassiveCouponsController : Controller
    {
        private readonly IMassiveCouponsServices _massivecouponService;
        private readonly SlackNotificationService _slackNotificationService;
        public MassiveCouponsController(IMassiveCouponsServices massivecouponService,SlackNotificationService slackNotificationService)
        {
            _slackNotificationService = slackNotificationService; 
            _massivecouponService = massivecouponService;
        }

        [HttpGet]
        public ResponseUtils<MassiveCoupon> GetAllMassiveCoupons()
        {
            try
            {
                return _massivecouponService.GetAllMassiveCoupons();
            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new ResponseUtils<MassiveCoupon>(false, null, 500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{searchType}/{value}")]
        public async Task<ResponseUtils<MassiveCoupon>> GetMassiveCouponsAsync(string searchType, string value)
        {
            try
            {
                var result = await _massivecouponService.GetMassiveCouponsAsync(searchType, value);
                return result;
            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new ResponseUtils<MassiveCoupon>(false, null, 500, $"Error: {ex.Message}");
            }
        }
    }
}
