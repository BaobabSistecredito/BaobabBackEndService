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
        // ----------------------- VALIDATE ACTION:
        public async Task<ResponseUtils<Coupon>> ValidateCoupon(string couponCode, float purchaseValue)
        {
            try
            {
                // Se inicializa variable confirmando si el cupón existe en la base de datos:
                var existCoupon = await _couponsRepository.GetCouponByCouponCodeAsync(couponCode);
                // Se confirma si se ha encontrado el cupón:
                if(existCoupon != null)
                {
                    // Se confirma si el estado del cupón es 'Activo':
                    if(existCoupon.StatusCoupon == "Activo")
                    {
                        // Se confirma el tipo de usabilidad del cupón:
                        if(existCoupon.TypeUsability == "Limitada")
                        {
                            // Se confirma si el cupón tiene usos disponibles:
                            if(existCoupon.NumberOfAvailableUses > 0)
                            {
                                // Se confirma la fecha de expiración del cupón:
                                var currentDate = DateTime.Now;
                                if(existCoupon.ExpiryDate >= currentDate)
                                {
                                    // Se confirma el tipo de cupón 'Porcentual' o 'Neto':
                                    if(existCoupon.TypeDiscount == "Porcentual")
                                    {
                                        // Se confirma el rango del valor comprado:
                                        if(purchaseValue >= existCoupon.MinPurchaseRange && purchaseValue <= existCoupon.MaxPurchaseRange)
                                        {
                                            var validatedCoupon = existCoupon.CouponCode;
                                            // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                                            return new ResponseUtils<Coupon>(true, null, validatedCoupon, message: "¡Cupón válido!");
                                        }
                                        else
                                        {
                                            return new ResponseUtils<Coupon>(false, null, 406, message: "¡El rango de la compra no cumple los requerimientos!");
                                        }
                                    }
                                    else
                                    {
                                        // Se confirma el rango del valor comprado:
                                        if(purchaseValue >= existCoupon.MinPurchaseRange)
                                        {
                                            var validatedCoupon = existCoupon.CouponCode;
                                            // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                                            return new ResponseUtils<Coupon>(true, null, validatedCoupon, message: "¡Cupón válido!");
                                        }
                                        else
                                        {
                                            return new ResponseUtils<Coupon>(false, null, 406, message: "¡El rango de la compra no cumple los requerimientos!");
                                        }
                                    }
                                }
                                else
                                {
                                    return new ResponseUtils<Coupon>(false, null, 406, message: "¡El cupón ha expirado!");
                                }
                            }
                            else
                            {
                                return new ResponseUtils<Coupon>(false, null, 406, message: "Cupón sin usos disponibles!");
                            }
                        }
                        else
                        {
                            // Se confirma la fecha de expiración del cupón:
                            var currentDate = DateTime.Now;
                            if(existCoupon.ExpiryDate >= currentDate)
                            {
                                // Se confirma el tipo de cupón 'Porcentual' o 'Neto':
                                if(existCoupon.TypeDiscount == "Porcentual")
                                {
                                    // Se confirma el rango del valor comprado:
                                    if(purchaseValue >= existCoupon.MinPurchaseRange && purchaseValue <= existCoupon.MaxPurchaseRange)
                                    {
                                        var validatedCoupon = existCoupon.CouponCode;
                                        // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                                        return new ResponseUtils<Coupon>(true, null, validatedCoupon, message: "¡Cupón válido!");
                                    }
                                    else
                                    {
                                        return new ResponseUtils<Coupon>(false, null, 406, message: "¡El rango de la compra no cumple los requerimientos!");
                                    }
                                }
                                else
                                {
                                    // Se confirma el rango del valor comprado:
                                    if(purchaseValue >= existCoupon.MinPurchaseRange)
                                    {
                                        var validatedCoupon = existCoupon.CouponCode;
                                        // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                                        return new ResponseUtils<Coupon>(true, null, validatedCoupon, message: "¡Cupón válido!");
                                    }
                                    else
                                    {
                                        return new ResponseUtils<Coupon>(false, null, 406, message: "¡El rango de la compra no cumple los requerimientos!");
                                    }
                                }
                            }
                            else
                            {
                                return new ResponseUtils<Coupon>(false, null, 406, message: "¡El cupón ha expirado!");
                            }
                        }
                    }
                    else
                    {
                        return new ResponseUtils<Coupon>(false, null, 406, message: "¡Cupón no activo!");
                    }
                }
                else
                {
                    return new ResponseUtils<Coupon>(false, null, 404, message: "¡Cupón no encontrado!");
                }
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Coupon>(false, null, 400, $"Error: {ex.Message}");
            }
        }
    }
}