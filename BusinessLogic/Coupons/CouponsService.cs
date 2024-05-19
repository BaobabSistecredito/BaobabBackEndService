using BaobabBackEndSerice.Models;
using BaobabBackEndService.Services.Coupons;

namespace BaobabBackEndService.BusinessLogic
{
    public class ICouponsService
    {
        private readonly ICouponsRepository _couponsRepository;

        public ICouponsService(ICouponsRepository couponsRepository)
        {
            _couponsRepository = couponsRepository;
        }
        /*
        Parte 2:

        En esta sección, implementamos toda la lógica del negocio. Este servicio actúa como intermediario entre el `CouponsRepository` y el controlador.

        - La lógica del negocio se define aquí, separando las responsabilidades de acceso a datos y manejo de solicitudes HTTP.
        - Este servicio toma las solicitudes del controlador, aplica cualquier lógica necesaria y luego interactúa con el `CouponsRepository` para acceder a los datos.

        El objetivo es mantener el código organizado y adherirse al principio de separación de responsabilidades, asegurando que cada componente del sistema tenga una única responsabilidad bien definida.

        Para ver mas consulta el archivo Service/Coupons/CouponsRepository.
        Continuemos con el siguiente paso Alli en este archivo...
        */

        public IEnumerable<Coupon> GetAllCoupons()
        {
            // Lógica de negocio para obtener todos los cupones
            return _couponsRepository.GetCoupons();
        }

        public Coupon GetCouponById(string id)
        {
            return _couponsRepository.GetCoupon(id);
        }
    }

}
