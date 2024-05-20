using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.categories;

namespace BaobabBackEndSerice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesServices _categoryService;

        public CategoriesController(ICategoriesServices categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseUtils<Category>>> GetCategories()
        {
            try
            {
                var result = _categoryService.GetCategories();
                return new ResponseUtils<Category>(true, new List<Category>(result), null, "todo oki");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, $"Error: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseUtils<Category>>> UpdateCategory(string id, Category category)
        {
            try
            {
                var response = await _categoryService.UpdateCategory(id, category);
                if (!response.Status)
                {
                    return StatusCode(422, response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, message: "Ocurrió un error al actualizar la categoría: " + ex.Message));
            }
        }
    }
}
