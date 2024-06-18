using AutoMapper;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.ExternalServices.SlackNotificationService;
using BaobabBackEndService.Repository.MassiveCoupons;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Services.MassiveCoupons
{
    public class MassiveCouponsServices : IMassiveCouponsServices
    {
        private readonly SlackNotificationService _slackNotificationService;
        private readonly IMassiveCouponsRepository _massivecouponsRepository;
        private readonly IMapper _mapper;
        public MassiveCouponsServices(IMassiveCouponsRepository massivecouponsRepository, IMapper mapper,SlackNotificationService slackNotificationService)
        {
            _slackNotificationService = slackNotificationService;
            _massivecouponsRepository = massivecouponsRepository;
            _mapper = mapper;
        }

        public ResponseUtils<MassiveCoupon> GetAllMassiveCoupons()
        {
            try
            {
                var result = _massivecouponsRepository.GetAllMassiveCoupons();
                return new ResponseUtils<MassiveCoupon>(true, new List<MassiveCoupon>(result), 200, message: "Se encontraron los cupones correctamente.");
            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new ResponseUtils<MassiveCoupon>(false, null, 404, message: "Error al buscar los cupones en la base de datos: " + ex.InnerException.Message);
            }

        }

        public async Task<ResponseUtils<MassiveCoupon>> GetMassiveCouponsAsync(string searchType, string value)
        {
            try
            {
                if (!int.TryParse(searchType, out int parseSearchType))
                {
                    return new ResponseUtils<MassiveCoupon>(false, null, 400, message: "Dato ingresado no es valido.");
                }

                List<MassiveCoupon> massiveCoupons;
                //voi aki en la lista de cambios
                switch (parseSearchType)
                {
                    case 1:
                        if (!int.TryParse(value, out int massiveCouponId))
                        {
                            return new ResponseUtils<MassiveCoupon>(false, null, 404, message: "No se encontraron coincidencias en la base de datos.");
                        }
                        massiveCoupons = new List<MassiveCoupon>(await _massivecouponsRepository.GetMassiveCouponByIdAsync(massiveCouponId));
                        break;
                    case 2:
                        massiveCoupons = new List<MassiveCoupon>(await _massivecouponsRepository.GetMassiveCouponByMassiveCouponCodeSearchAsync(value));
                        break;
                    case 3:
                        if (!int.TryParse(value, out int couponId))
                        {
                            return new ResponseUtils<MassiveCoupon>(false, null, 404, message: "No se encontraron coincidencias en la base de datos.");
                        }
                        massiveCoupons = new List<MassiveCoupon>(await _massivecouponsRepository.GetMassiveCouponByCouponIdSearchAsync(couponId));
                        break;
                    default:
                        return new ResponseUtils<MassiveCoupon>(false, null, 400, message: "Dato ingresado no es válido.");
                }

                if (massiveCoupons == null || !massiveCoupons.Any())
                {
                    return new ResponseUtils<MassiveCoupon>(false, null, 404, message: "No se encontraron cupones con los criterios de búsqueda proporcionados.");
                }

                return new ResponseUtils<MassiveCoupon>(true, massiveCoupons, 200, message: "Se encontraron los cupones correctamente.");

            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new ResponseUtils<MassiveCoupon>(false, null, 500, message: "Error buscar el cupon en la base de datos: " + ex.InnerException.Message);
            }

        }
    }
}