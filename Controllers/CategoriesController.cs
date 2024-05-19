using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using Microsoft.EntityFrameworkCore;
using BaobabBackEndService.Utils;

namespace BaobabBackEndSerice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly BaobabDataBaseContext _context;

        public CategoriesController(BaobabDataBaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseUtils<Category>>> GetCategory()
        {
            try
            {
                var result = await _context.Categories.ToListAsync();
                return new ResponseUtils<Category>(true, result, null, "todo oki");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, $"Error: {ex.Message}"));
            }

        }
        // ----------------------- SEARCH ACTION:
        [HttpGet]
        [Route("Search/{category?}")]
        public async Task<ActionResult<ResponseUtils<Category>>> SearchCategory(string category)
        {
            // Se trae la información de la entidad 'Categories':
            var categories = _context.Categories.AsQueryable();
            try
            {
                // Se confirma que el parámetro 'category' no esté vacío:
                if(!string.IsNullOrEmpty(category))
                {
                    // Se convierte la búsqueda a minúscula para hacerla case-insensitive:
                    var loweredCategory = category.ToLower();
                    // Se inicializa variable con los datos que coincidan con algún campo de la entidad 'Categories':
                    var categoriesFiltered = categories.Where(c => c.CategoryName.ToLower().Contains(loweredCategory) || c.Status.ToLower().Contains(loweredCategory));
                    // Se inicializa variable con la información filtrada:
                    var categoriesList = await categoriesFiltered.ToListAsync();
                    // Se confirma si se han encontrado datos que coincidan con el filtrado:
                    if(categoriesList.Any())
                    {
                        // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                        return Ok(new ResponseUtils<Category>(true, categoriesList, null, "¡Búsqueda éxitosa!"));
                    }
                    else
                    {
                        // Retorno de la respuesta fallida con la estructura de la clase 'ResponseUtils':
                        return StatusCode(404, new ResponseUtils<Category>(false, null, null, "¡Dato no encontrado!"));
                    }
                }
                else
                {
                    // Retorno de la respuesta fallida con la estructura de la clase 'ResponseUtils':
                    return StatusCode(422, new ResponseUtils<Category>(false, null, null, "¡No se han ingresado datos!"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, new ResponseUtils<Category>(false, null, null, $"Error: {ex.Message}"));
            }
        }


    }
}
