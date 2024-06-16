using Microsoft.AspNetCore.Mvc;
using BaobabBackEndService.DTOs;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using Microsoft.AspNetCore.Authorization;
using BaobabBackEndService.Services.categories;

namespace BaobabBackEndSerice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesUpdateController : ControllerBase
    {
        private readonly ICategoriesServices _categoryService;

        public CategoriesUpdateController(ICategoriesServices categoryService)
        {
            _categoryService = categoryService;
        }


        //filtrar y Search categorias
        [HttpPut("{id}")]
        [Authorize("TheAdmin")]
        public async Task<ActionResult<ResponseUtils<Category>>> UpdateCategory(string id, CategoryDTO category)
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
