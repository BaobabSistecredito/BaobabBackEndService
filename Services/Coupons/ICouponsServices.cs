using BaobabBackEndSerice.Models;
using BaobabBackEndService.DTOs;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Services.Coupons
{
    public interface ICouponsServices
    {
        IEnumerable<Coupon> GetCoupons();
        Task<Coupon> GetCoupon(string id);
        Task<ResponseUtils<Coupon>> GetCouponsAsync(string searchType, string value);
        Task<ResponseUtils<Coupon>> CreateCoupon(CouponDTO coupon);
        // -------------------------- VALIDATE FUNCTION:
        Task<ResponseUtils<Coupon>> ValidateCoupon(string couponCode, float purchaseValue);
        // ----------------------- EDIT ACTION:
        Task<ResponseUtils<CouponUpdateDTO>> EditCoupon(int marketingUserId, int couponId, CouponUpdateDTO coupon);
        // -----------------------------------
        // ---------------------------------------------
        Task<ResponseUtils<Coupon>> FilterSearch(string Search);
        Task<ResponseUtils<Coupon>> EditCouponStatus(string id,string status);
        Task<ResponseUtils<MassiveCoupon>> RedeemCoupon(RedeemDTO redeemRequest);
        // ----------------------- GET COUPON & CATEGORY:
        Task<IEnumerable<Coupon>> GetCouponAndCategory();
    }
}