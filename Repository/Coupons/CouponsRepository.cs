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

        public async Task<Coupon> GetCouponAsync(int id)
        {
            return await _context.Coupons.FindAsync(id);
        }

        public async Task<IEnumerable<Coupon>> GetCouponByIdAsync(int couponId)
        {
            return await _context.Coupons.Where(c => c.Id == couponId).ToListAsync();
        }

        public async Task<IEnumerable<Coupon>> GetCouponByTitleSearchAsync(string value)
        {
            return await _context.Coupons.Where(c => c.Title.StartsWith(value)).ToListAsync();
        }

        public async Task<IEnumerable<Coupon>> GetCouponByCouponCodeSearchAsync(string value)
        {
            return await _context.Coupons.Where(c => c.CouponCode.StartsWith(value)).ToListAsync();
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
        // ------------------- GET MassiveCoupon:
        public async Task<MassiveCoupon> GetMassiveCouponByCouponId(Coupon coupon)
        {
            return await _context.MassiveCoupons.FirstOrDefaultAsync(mc => mc.CouponId == coupon.Id);
        }
        // ---------------------------------------

        public async Task<IEnumerable<Coupon>> GetCouponsAsync()
        {
            return await _context.Coupons.ToListAsync();
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

        //buscar cupon por codigo de cupon

        public Coupon CuponCode(string CodeCoupon)
        {
            return _context.Coupons.FirstOrDefault(c => c.CouponCode == CodeCoupon);
        }

        //Crear poll
        public async Task<MassiveCoupon> CrearPoll(MassiveCoupon massiveCoupon)
        {
            _context.MassiveCoupons.Add(massiveCoupon);
            await _context.SaveChangesAsync();
            return massiveCoupon;
        }

        //actualizar cupon
        public async Task<Coupon> RedencionCupon(Coupon coupon)
        {
            _context.Coupons.Update(coupon);
            await _context.SaveChangesAsync();
            return coupon;
        }
        

        // --------------------- UPDATE COUPON:
        public async Task<Coupon> UpdateCoupon(Coupon coupon)
        {
            _context.Coupons.Update(coupon);
            await _context.SaveChangesAsync();
            return coupon;
        }
        // --------------------- ADD NEW CHANGE:
        public async Task<ChangeHistory> AddNewChange(ChangeHistory newChange)
        {
            _context.ChangesHistory.Add(newChange);
            await _context.SaveChangesAsync();
            return newChange;
        }
        
        public async Task<Coupon> SearchCouponsByCategoryAsync(int categoryid)
        {
            return await _context.Coupons.FirstOrDefaultAsync(c => c.CategoryId == categoryid && c.StatusCoupon == "Activo");
        }

        public async Task UpdateStatusCouponAsync(Coupon coupons)
        {
            _context.Entry(coupons).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}