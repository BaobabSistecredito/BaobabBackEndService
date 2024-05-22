using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Repository.Coupons;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Services.Coupons
{
    public class CouponsServices : ICouponsServices
    {
        private readonly ICouponsRepository _couponsRepository;

        public CouponsServices(ICouponsRepository couponsRepository)
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

        public IEnumerable<Coupon> GetCoupons()
        {
            // Lógica de negocio para obtene rtodos los cupones

            return _couponsRepository.GetCoupons();
        }

        public Coupon GetCoupon(string id)
        {
            return _couponsRepository.GetCoupon(id);
        }

        public async Task<ResponseUtils<Coupon>> CreateCoupon(Coupon coupon)
        {
            var existingCodeCoupon = await _couponsRepository.GetCouponByCouponCodeAsync(coupon.CouponCode);
            var existingTitleCoupon = await _couponsRepository.GetCouponByTitleAsync(coupon.Title);
            if (existingCodeCoupon != null)
            {
                return new ResponseUtils<Coupon>(false, message: "El codigo Del cupon ya existe");
            }
            if (existingTitleCoupon != null)
            {
                return new ResponseUtils<Coupon>(false, message: "El Titulo Del cupon ya existe");
            }

            coupon.CreationDate = DateTime.Now;
            coupon.StatusCoupon = "Creado";

            return new ResponseUtils<Coupon>(true, new List<Coupon> { _couponsRepository.CreateCoupon(coupon) }, null, message: "Todo oki");
        }

        //funcion para buscar, Filtrar o mostrar cuponen
        public async Task<ResponseUtils<Coupon>> FilterSearch(string Search)
        {

            var Cupones = await _couponsRepository.GetCouponsAsync();

            if(Search == "Activo" || Search == "Inactivo" || Search == "Creado" || Search == "Vencido" || Search == "Agotado")
            {

                Cupones = Cupones.Where(x => x.StatusCoupon == Search).ToList();
            return new ResponseUtils<Coupon>(true, new List<Coupon>(Cupones), null, message: "Todo 2");

            }else{
                //buscador
                if(!string.IsNullOrEmpty(Search))
                {
                    Cupones = Cupones.Where(x => x.StatusCoupon == Search || x.CouponCode == Search).ToList();
                }else{
                    return new ResponseUtils<Coupon>(false, message: "El parametro no se encuentra en la base de datos");
                }

            }
            return new ResponseUtils<Coupon>(true, new List<Coupon>(Cupones), null, message: "Todo oki");
        }


    }
}