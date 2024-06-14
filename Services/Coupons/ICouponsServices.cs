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
        Task<Coupon> GetCoupon(string id);
        Task<ResponseUtils<Coupon>> GetCouponsAsync(string searchType, string value);
        Task<ResponseUtils<Coupon>> CreateCoupon(CouponRequest coupon);
        // -------------------------- VALIDATE FUNCTION:
        Task<ResponseUtils<Coupon>> ValidateCoupon(string couponCode, float purchaseValue);
        // ----------------------- EDIT ACTION:
        Task<ResponseUtils<Coupon>> EditCoupon(int marketingUserId, Coupon coupon);
        // -----------------------------------
        // ---------------------------------------------
        Task<ResponseUtils<Coupon>> FilterSearch(string Search);
        Task<ResponseUtils<Coupon>> EditCouponStatus(string id,string status);
        Task<ResponseUtils<MassiveCoupon>> RedeemCoupon(RedeemRequest redeemRequest);
    }
}