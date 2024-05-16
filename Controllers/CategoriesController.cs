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


        //creacion de categorias
        [HttpPost]
        public async Task<ActionResult<ResponseUtils<Category>>> CreateCategoty(Category category)
        {
            try
            {
                var ValidateCategory =_context.Categories.FirstOrDefault(c =>  c.CategoryName.ToLower() ==category.CategoryName.ToLower());

                if(ValidateCategory == null && (category.Status == "Inactivo" || category.Status == "Activo")){
                    
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();
                    var Categoria = CreatedAtAction("GetCategory",new{id = category.Id},category);
                    return new ResponseUtils<Category>(true, new List<Category> { category }, null, "Todo oki");

                }
                
                if(category.Status == "Inactivo" ||category.Status == "Activo"){

                }else {
                    return BadRequest(new ResponseUtils<Category>(false,null, null, "El estado ingresado no es Valido"));
                }

                if(ValidateCategory != null)
                {
                    return BadRequest(new ResponseUtils<Category>(false,null, null, "El Nombre de la Categoria ya existe"));
                }
                else{
                    return BadRequest(new ResponseUtils<Category>(false,null, null, "Error al almacenar en la base de datos"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, $"Error: {ex.Message}"));
            }
        }
        
    }
}
