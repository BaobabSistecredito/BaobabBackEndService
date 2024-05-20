using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using Microsoft.EntityFrameworkCore;

namespace BaobabBackEndService.Repository.Coupons
{
    public class CouponsRepository : ICouponsRepository
    {
        private readonly BaobabDataBaseContext _context;

        public CouponsRepository(BaobabDataBaseContext context)
        {
            _context = context;
        }
        /*
        Parte 3:

        Hola,

        En esta parte, nos comunicamos directamente con la base de datos a través de CouponsRepository.
        CouponsRepository es el único responsable de interactuar con la base de datos, 
        liberando al controlador  y el businessLogic de consultas a la DB.

        - El objetivo es mantener el controlador limpio y enfocado únicamente en manejar las solicitudes HTTP.
        - Aquí, en el repositorio, realizamos todas las consultas necesarias a la base de datos.

        Para ver los detalles de la interfaz que define los métodos del repositorio, consulta el archivo Service/Coupons/ICouponsRepository.
        Continuemos con el siguiente paso Alli en este archivo...
        */

        public Coupon GetCoupon(string id)
        {
            return _context.Coupons.Find(id);
        }

        public async Task<Coupon> GetCouponByCouponCodeAsync(string cuponCode)
        {
            return await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == cuponCode);
        }

        public async Task<Coupon> GetCouponByTitleAsync(string title)
        {
            return await _context.Coupons.FirstOrDefaultAsync(c => c.Title == title);
        }

        public IEnumerable<Coupon> GetCoupons()
        {
            return _context.Coupons.ToList();
        }
        /*
        Parte 5:

        ¡Atención! Si no has leído la Parte 3, por favor, regresa y léela antes de continuar con esta parte.

        Ahora, procederemos a crear nuestra llamada a la base de datos. Por ejemplo, el método para crear un cupón. 
        Este método realiza el proceso de creación sin ningún tipo de validación
        siguiendo el principio de responsabilidad única (SRP).
        El método tiene una única responsabilidad: crear un cupón en la base de datos.

        Aquí está el método:

        public Coupon CreateCoupon(Coupon coupon){
            _context.Coupons.Add(coupon);
            _context.SaveChanges();
            return coupon;
        }

        Este método:
        1. Añade el nuevo cupón a la colección de cupones en el contexto de la base de datos.
        2. Guarda los cambios realizados en el contexto para persistir el nuevo cupón en la base de datos.
        3. Retorna el cupón recién creado.

        A continuación, explicaremos los otros principios SOLID, como el de abierto/cerrado, en futuras partes.

        Para finalizar esta guía, nos dirigiremos al archivo `program.cs` en la carpeta raíz para nuestra Parte 5.

        Continuemos con el siguiente paso Alli en este archivo...
        */

        public Coupon CreateCoupon(Coupon coupon)
        {
            _context.Coupons.Add(coupon);
            _context.SaveChanges();
            return coupon;
        }

    }
}