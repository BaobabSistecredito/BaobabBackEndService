using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using Microsoft.EntityFrameworkCore;

namespace BaobabBackEndService.Repository.MassiveCoupons
{
    public class MassiveCouponsRepository : IMassiveCouponsRepository
    {
        private readonly BaobabDataBaseContext _context;

        public MassiveCouponsRepository(BaobabDataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MassiveCoupon>> GetMassiveCouponByIdAsync(int couponId)
        {
            return await _context.MassiveCoupons.Where(c => c.Id == couponId).ToListAsync();
        }

        public async Task<IEnumerable<MassiveCoupon>> GetMassiveCouponByMassiveCouponCodeSearchAsync(string value)
        {
            return await _context.MassiveCoupons.Where(c => c.MassiveCouponCode.StartsWith(value)).ToListAsync();
        }

        public async Task<IEnumerable<MassiveCoupon>> GetMassiveCouponByCouponIdSearchAsync(int value)
        {
            return await _context.MassiveCoupons.Where(c => c.CouponId == value).ToListAsync();
        }

        public IEnumerable<MassiveCoupon> GetAllMassiveCoupons()
        {
            return _context.MassiveCoupons.ToList();
        }
    }
}