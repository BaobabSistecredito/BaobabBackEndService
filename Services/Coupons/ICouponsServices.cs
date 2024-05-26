using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Services.Coupons
{
    public interface ICouponsServices
    {
        IEnumerable<Coupon> GetCoupons();

        Coupon GetCoupon(string id);

        Task<ResponseUtils<Coupon>> GetCouponsAsync(string searchType, string value);

        Task<ResponseUtils<Coupon>> CreateCoupon(Coupon coupon);
        // -------------------------- VALIDATE FUNCTION:
        Task<ResponseUtils<Coupon>> ValidateCoupon(string couponCode, float purchaseValue);
        // ---------------------------------------------
        Task<ResponseUtils<Coupon>> FilterSearch(string Search);
    }
}