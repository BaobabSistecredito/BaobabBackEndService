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

        //function para actualizar una categoria se le agg el id al httpPut para indicar que este recibe uno como param
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseUtils<Category>>> UpdateCategory(int id, Category category)
        {
            try
            {
                // Validaciones de entrada por id
                if (id <= 0)
                {
                    return BadRequest(new ResponseUtils<Category>(false, null, null, "ID de categoría no válido."));
                }

                // Validaciones de entrada por campos de cat
                if (category == null)
                {
                    return BadRequest(new ResponseUtils<Category>(false, null, null, "El objeto de categoría es nulo."));
                }

                // Validaciones de entrada por campos que no sean vacios 
                if (string.IsNullOrWhiteSpace(category.CategoryName))
                {
                    return BadRequest(new ResponseUtils<Category>(false, null, null, "El nombre de categoría es requerido."));
                }

                // Validaciones de entrada por campos que no sean vacios 
                if (string.IsNullOrWhiteSpace(category.Status))
                {
                    return BadRequest(new ResponseUtils<Category>(false, null, null, "El estado de la categoría es requerido."));
                }
        

                // Buscar la categoría en la base de datos
                var result = await _context.Categories.FindAsync(id);
                if (result == null)
                {
                    return NotFound(new ResponseUtils<Category>(true, null, null, "No se encontró la categoría para actualizar."));
                }

                // Actualizar la categoría
                result.CategoryName = category.CategoryName;
                result.Status = category.Status;

                _context.Entry(result).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new ResponseUtils<Category>(true, new List<Category> { result }, null, "La categoría se actualizó correctamente."));
            }
            catch (DbUpdateException ex)
            {
                // Error de base de datos con el innerExepcion 
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, "Error al actualizar la categoría en la base de datos: " + ex.InnerException.Message));
            }
            catch (Exception ex)
            {
                // Otro tipo de erorres pero no me acuerdo como lo dijo el inge
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, "Ocurrió un error al actualizar la categoría: " + ex.Message));
            }
        }
    }
}
