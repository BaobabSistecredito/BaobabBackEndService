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
            // Lógica de negocio para obtener todos los cupones

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

        //redencion de cupon
        public async Task<ResponseUtils<MassiveCoupon>> RedeemCoupon(RedeemRequest redeemRequest)
        {

            bool Validate = true;

            if(Validate == true)
            {
                var CuponValido = _couponsRepository.CuponCode(redeemRequest.CodeCoupon);


                //validar si el cupon es null
                if(CuponValido == null)
                {
                    return new ResponseUtils<MassiveCoupon>(false, message: "El cupon no existe en la base de datos");                    
                }


                if(CuponValido.TypeUsability == "limitada" && CuponValido.NumberOfAvailableUses >0)
                {

                    //traer todos los datos del cupon para actualizarlo

                    var title = CuponValido.Title;

                    var Description = CuponValido.Description;

                    var CreationDate = CuponValido.CreationDate;

                    var StarDate = CuponValido.StartDate;
                    
                    var ExpiryDate = CuponValido.ExpiryDate;

                    var ValueDiscount = CuponValido.ValueDiscount;

                    var TypeDiscount = CuponValido.TypeDiscount;

                    int NumberOfAvailableUses = CuponValido.NumberOfAvailableUses;

                    var Menos = NumberOfAvailableUses-1;

                    var TypeUsability = CuponValido.TypeUsability;

                    var StatusCoupon = CuponValido.StatusCoupon;

                    var MinPurchaseRange = CuponValido.MinPurchaseRange;

                    var MaxPurchaseRange = CuponValido.MaxPurchaseRange;

                    var CouponCode = CuponValido.CouponCode;

                    var CategotyId = CuponValido.CategoryId;

                    var MarketingUserId = CuponValido.MarketingUserId;
                    

                    Console.WriteLine(NumberOfAvailableUses);
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine(Menos);



                    //resto 1 numero de uso 
                    //NumberOfAvailableUses = NumberOfAvailableUses-1; 


                    var Cupon = new Coupon
                    {
                        Title = title,

                        Description = Description,

                        CreationDate = CreationDate,

                        StartDate = StarDate,

                        ExpiryDate= ExpiryDate,

                        ValueDiscount = ValueDiscount,

                        TypeDiscount = TypeDiscount,

                        NumberOfAvailableUses = Menos ,

                        TypeUsability = TypeUsability,

                        StatusCoupon = StatusCoupon,

                        MinPurchaseRange=MinPurchaseRange,

                        MaxPurchaseRange = MaxPurchaseRange,

                        CouponCode = CouponCode,

                        CategoryId = CategotyId,

                        MarketingUserId = MarketingUserId    
                    };

                    await _couponsRepository.RedencionCupon(Cupon);
                    
                }

                MassiveCoupon massiveCoupon= new MassiveCoupon
                {
                    MassiveCouponCode = redeemRequest.CodeCoupon+1,//agregar funcion
                    CouponId = CuponValido.Id,
                    UserEmail = redeemRequest.UserEmail,
                    RedemptionDate = DateTime.Now,
                    PurcharseId = redeemRequest.PurcharseId,
                    PurchaseValue = redeemRequest.PurchaseValue


                };

                /* massiveCoupon.MassiveCouponCode = redeemRequest.CodeCoupon;

                massiveCoupon.CouponId = CuponValido.Id;

                massiveCoupon.UserEmail = redeemRequest.UserEmail;

                massiveCoupon.RedemptionDate = DateTime.Now;

                massiveCoupon.PurchaseId = redeemRequest.PurchaseId;

                massiveCoupon.purchaseValue = redeemRequest.PurchaseValue; */



                 return new ResponseUtils<MassiveCoupon>(true, new List<MassiveCoupon> { _couponsRepository.CrearPoll(massiveCoupon) }, null, message: "Todo oki");

            

            }else{
                return new ResponseUtils<MassiveCoupon>(false, message: "El cupon no es valido");                    
            }

            

            

        }




    }
}