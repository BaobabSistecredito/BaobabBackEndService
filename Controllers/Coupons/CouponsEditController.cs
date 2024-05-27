using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.Coupons;

namespace BaobabBackEndService.Controllers.Coupons
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponsEditController : ControllerBase
    {
        private readonly ICouponsServices _couponService;
        public CouponsEditController(ICouponsServices couponService)
        {
            _couponService = couponService;
        }
        // ----------------------- EDIT ACTION:

    }
}