using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;

namespace BaobabBackEndService.Repository.MassiveCoupons
{
    public interface IMassiveCouponsRepository
    {
        Task<IEnumerable<MassiveCoupon>> GetMassiveCouponByIdAsync(int couponId);
        Task<IEnumerable<MassiveCoupon>> GetMassiveCouponByMassiveCouponCodeSearchAsync(string value);
        Task<IEnumerable<MassiveCoupon>> GetMassiveCouponByCouponIdSearchAsync(int value);
        IEnumerable<MassiveCoupon> GetAllMassiveCoupons();

    }
}