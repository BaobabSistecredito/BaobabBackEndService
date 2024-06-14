using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.categories;
using Microsoft.AspNetCore.Authorization;

namespace BaobabBackEndSerice.Controllers
{
    [Authorize]
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
        public ResponseUtils<Category> GetAllCategories()
        {
            try
            {

              /*   var respuesta = _categoryService.GetAllCategories();
                 
                 if (respuesta)
                {
                    return new ResponseUtils<Category>(false, null, null, $"Usuario no Authorizado");
                } */
                return _categoryService.GetAllCategories();
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Category>(false, null, null, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{number}")]
        public async Task<ResponseUtils<Category>> GetCategories(string number)
        {
            try
            {
                var result = await _categoryService.GetCategoriesAsync(number);
                return result;
            }
            catch (Exception ex)
            {
                return new ResponseUtils<Category>(false, null, null, $"Error: {ex.Message}");
            }
        }

    }
}
