using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.categories;

namespace BaobabBackEndService.Controllers.Categories
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesSearchController : ControllerBase
    {
        private readonly ICategoriesServices _categoryService;
        public CategoriesSearchController(ICategoriesServices categoryService)
        {
            _categoryService = categoryService;
        }
        // ----------------------- SEARCH ACTION:
        [HttpGet("{category?}")]
        public async Task<ActionResult<ResponseUtils<Category>>> SearchCategory(string? category)
        {
            var response = await _categoryService.SearchCategory(category);
            if (!response.Status)
            {
                return StatusCode(400, response);
            }

            return Ok(response);
        }

    }
}