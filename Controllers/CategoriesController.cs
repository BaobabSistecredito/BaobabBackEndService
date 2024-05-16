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
        [Route("Search/{category}")]
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
                    // Se confirma que la información enviada coincida con algún campo de la entidad 'Categories':
                    categories = categories.Where(c => c.CategoryName.ToLower().Contains(loweredCategory) 
                    || c.Status.ToLower().Contains(loweredCategory));
                }
                // Se inicializa variable con la información filtrada:
                var categoriesList = await categories.ToListAsync();
                // Retorno de la respuesta éxitosa con la estructura de la clase 'ResponseUtils':
                return Ok(new ResponseUtils<Category>(true, categoriesList, null, "¡Búsqueda éxitosa!"));
            }
            catch (Exception ex)
            {
                // Se inicializa variable con la respuesta de error:
                var errorResponse = new ResponseUtils<Category>(false, null, null, $"Error: {ex.Message}");
                return StatusCode(400, errorResponse);
            }
        }
        
    }
}
