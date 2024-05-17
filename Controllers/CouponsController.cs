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

        //Filtrado y Buscador de Cupones

          [HttpGet("{status}")]
         public async Task<ActionResult<ResponseUtils<Coupon>>> GetCoupons(string status)
        {
            try
            {
                var Cupon = await _context.Coupons.ToListAsync();
                
                //Filtrado 
                if(status == "Activo" ||status == "Inactivo" ||status == "Creado" || status == "Vencido" || status == "Agotado"){
                    Cupon = Cupon.Where(x => x.StatusCoupon == status).ToList();
                }else{
                    //Buscador
                    if (!string.IsNullOrEmpty(status))
                    {
                        Cupon = Cupon.Where(x => x.StatusCoupon == status || x.CouponCode  == status ).ToList();
                    }else{
                        return BadRequest(new ResponseUtils<Coupon>(false,null,null,"El Parametro ingresado no es Valido"));
                    }
                }
                return new ResponseUtils<Coupon>(true, Cupon, null, "todo oki");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, $"Errors: {ex.Message}"));
            }
        }
    }
}
