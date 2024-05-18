using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.Categories;

namespace BaobabBackEndSerice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseUtils<Category>>> GetCategories()
        {
            try
            {
                var result = _categoriesRepository.GetCategories();
                return new ResponseUtils<Category>(true, new List<Category>(result), null, "todo oki");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, $"Error: {ex.Message}"));
            }

        }
    }
}
