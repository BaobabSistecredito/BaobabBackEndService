using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Repository.Coupons
{
  public interface ICouponsRepository
  {
    /*
    Parte 4:

    Wenaaaas,

    En esta sección, declaramos las funciones de nuestro `CouponRepository`. 
    Es aquí donde definimos los métodos necesarios para agregar, editar o eliminar cupones.

    - Si necesitamos implementar nuevas funcionalidades, primero declaramos los métodos aquí.
    - He comentado algunos métodos como ejemplos, para que sepan cómo deberían lucir. 
      Si necesitan implementar alguno de estos métodos, simplemente descoméntenlos y completen la implementación.

    Por ejemplo:
    - Agregar un cupón (`addCoupon`)
    - Editar un cupón (`editCoupon`)
    - Eliminar un cupón (`deleteCoupon`)

    Volvemos a la ruta `Service/Coupons/CouponsRepository` para continuar con la implementación detallada de estos métodos.
    Continuemos con el siguiente paso Alli en este archivo...
    */

    IEnumerable<Coupon> GetCoupons();
    Task<Coupon> GetCouponAsync(int id);
    Coupon CreateCoupon(Coupon coupon);
    Task<Coupon> GetCouponByCouponCodeAsync(string couponCode);
    Task<Coupon> GetCouponByTitleAsync(string title);
    // ------------------- GET MassiveCoupon:
    Task<MassiveCoupon> GetMassiveCouponByCouponId(Coupon coupon);
    // --------------------- UPDATE COUPON:
    Task<Coupon> UpdateCoupon(Coupon coupon);
    // --------------------- CREATE RECORD IN 'ChangesHistory':
    Task<ChangeHistory> AddNewChange(ChangeHistory newChange);
    Task<IEnumerable<Coupon>> GetCouponByIdAsync(int couponId);
    Task<IEnumerable<Coupon>> GetCouponByTitleSearchAsync(string value);
    Task<IEnumerable<Coupon>> GetCouponByCouponCodeSearchAsync(string value);
    Task<IEnumerable<Coupon>> GetCouponsAsync();
    Task<Coupon> SearchCouponsByCategoryAsync(int categoryid);
    Task UpdateStatusCouponAsync(Coupon coupons);
    Coupon CuponCode(string CodeCoupon);
    Task<MassiveCoupon> CrearPoll(MassiveCoupon massiveCoupon);
    Task<Coupon> RedencionCupon(Coupon coupon);
  }
}