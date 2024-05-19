using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;

namespace BaobabBackEndService.BusinessLogic.Coupons
{
    public interface ICouponsService
    {
        IEnumerable<Coupon> GetCoupons();

        Coupon GetCoupon(string id);
    }
}