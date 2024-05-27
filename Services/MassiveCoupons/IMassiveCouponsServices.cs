using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;

namespace BaobabBackEndService.Services.MassiveCoupons
{
    public interface IMassiveCouponsServices
    {
        Task<ResponseUtils<MassiveCoupon>> GetMassiveCouponsAsync(string searchType, string value);
        ResponseUtils<MassiveCoupon> GetAllMassiveCoupons();
    }
}