using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using System.Collections.Generic;
using BaobabBackEndService.Services.Coupons;

namespace BaobabBackEndService.Controllers.Coupons
{
    [Route("[controller]")]
    public class CouponGetSearchController : Controller
    {
         private readonly ICouponsServices _couponsService;

        public CouponGetSearchController(ICouponsServices couponsService)
        {
            _couponsService = couponsService;
        }

        [HttpGet("{Search}")]
         public async Task<ActionResult<ResponseUtils<Coupon>>> SearchFilter(string Search)
        {
            try
            {
                var SearchResult = await _couponsService.FilterSearch(Search);
                 if (!SearchResult.Status)
                {
                    return StatusCode(422, SearchResult);
                }

                return Ok(SearchResult);
            
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, $"Errors: {ex.Message}"));
            }
        }


    }
}