using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Services.Coupons
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

        Coupon GetCoupon(string id);

        //ResponseUtils<Coupon> AddCoupon(Coupon coupon);

        //ResponseUtils<Coupon> UpdateCoupon(stirng id,Coupon coupon);

        //ResponseUtils<Coupon> DeleteCoupon(string id); // en este caso no se elimina el coupon solo se cambia de estado

    }
}