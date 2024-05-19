using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using Microsoft.EntityFrameworkCore;
using BaobabBackEndService.Utils;

namespace BaobabBackEndSerice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponsController : Controller
    {
        private readonly BaobabDataBaseContext _context;

        public CouponsController(BaobabDataBaseContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<ResponseUtils<Coupon>>> GetCoupons()
        {
            try
            {
                var result = await _context.Coupons.ToListAsync();
                return new ResponseUtils<Coupon>(true, result, null, "todo oki");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, $"Errors: {ex.Message}"));
            }
        }

        // ----------------------- VALIDATE ACTION:
        [HttpGet]
        [Route("validate/{couponcode?}/{purchasevalue?}")]
        public async Task<ActionResult<ResponseUtils<Coupon>>> ValidateCoupon(string couponCode, float purchaseValue)
        {
            try
            {
                // Se trae la información de la entidad 'Coupons':
                var coupons = _context.Coupons.AsQueryable();
                // Consulta LINQ para confirmar si el cupón existe en la base de datos:
                var existCoupon = await coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
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
                                            return Ok(new ResponseUtils<Category>(true, null, validatedCoupon, "¡Cupón válido!"));
                                        }
                                        else
                                        {
                                            // Retorno de la respuesta con la estructura de la clase 'ResponseUtils':
                                            return StatusCode(406, new ResponseUtils<Coupon>(false, null, null, "¡El rango de la compra no cumple los requerimientos!"));
                                        }
                                    }
                                    else
                                    {
                                        // Se confirma el rango del valor comprado:
                                        if(purchaseValue >= existCoupon.MinPurchaseRange)
                                        {
                                            var validatedCoupon = existCoupon.CouponCode;
                                            // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                                            return Ok(new ResponseUtils<Category>(true, null, validatedCoupon, "¡Cupón válido!"));
                                        }
                                        else
                                        {
                                            // Retorno de la respuesta con la estructura de la clase 'ResponseUtils':
                                            return StatusCode(406, new ResponseUtils<Coupon>(false, null, null, "¡El rango de la compra no cumple los requerimientos!"));
                                        }
                                    }
                                }
                                else
                                {
                                    // Retorno de la respuesta con la estructura de la clase 'ResponseUtils':
                                    return StatusCode(406, new ResponseUtils<Coupon>(false, null, null, "¡El cupón ha expirado!"));
                                }
                            }
                            else
                            {
                                // Retorno de la respuesta con la estructura de la clase 'ResponseUtils':
                                return StatusCode(406, new ResponseUtils<Coupon>(false, null, null, "¡Cupón sin usos disponibles!"));
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
                                        return Ok(new ResponseUtils<Category>(true, null, validatedCoupon, "¡Cupón válido!"));
                                    }
                                    else
                                    {
                                        // Retorno de la respuesta con la estructura de la clase 'ResponseUtils':
                                        return StatusCode(406, new ResponseUtils<Coupon>(false, null, null, "¡El rango de la compra no cumple los requerimientos!"));
                                    }
                                }
                                else
                                {
                                    // Se confirma el rango del valor comprado:
                                    if(purchaseValue >= existCoupon.MinPurchaseRange)
                                    {
                                        var validatedCoupon = existCoupon.CouponCode;
                                        // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                                        return Ok(new ResponseUtils<Category>(true, null, validatedCoupon, "¡Cupón válido!"));
                                    }
                                    else
                                    {
                                        // Retorno de la respuesta con la estructura de la clase 'ResponseUtils':
                                        return StatusCode(406, new ResponseUtils<Coupon>(false, null, null, "¡El rango de la compra no cumple los requerimientos!"));
                                    }
                                }
                            }
                            else
                            {
                                // Retorno de la respuesta con la estructura de la clase 'ResponseUtils':
                                return StatusCode(406, new ResponseUtils<Coupon>(false, null, null, "¡El cupón ha expirado!"));
                            }
                        }
                    }
                    else
                    {
                        // Retorno de la respuesta con la estructura de la clase 'ResponseUtils':
                        return StatusCode(406, new ResponseUtils<Coupon>(false, null, null, "¡Cupón no activo!"));
                    }
                }
                else
                {
                    // Retorno de la respuesta con la estructura de la clase 'ResponseUtils':
                    return StatusCode(404, new ResponseUtils<Coupon>(false, null, null, "¡Cupón no encontrado!"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, new ResponseUtils<Category>(false, null, null, $"Error: {ex.Message}"));
            }
        }
        // ----------------------------------------------
    }
}
