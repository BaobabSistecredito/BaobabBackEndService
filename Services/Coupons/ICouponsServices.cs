using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;

namespace BaobabBackEndService.Services.Coupons
{
    public interface ICouponsServices
    {
        IEnumerable<Coupon> GetCoupons();

        Coupon GetCoupon(string id);
    }
}