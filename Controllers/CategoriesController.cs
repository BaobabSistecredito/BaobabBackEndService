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

                //Valida y el nombre de la categoria es null o esta vacio
                if(string.IsNullOrWhiteSpace(category.CategoryName) || category.CategoryName == null){

                    return BadRequest(new ResponseUtils<Category>(false,null, null, "el nombre es un campo obligatorio."));

                }else if (ValidateCategory != null){//valida si el nombre de la categoria ya existe en la base de datos

                    return BadRequest(new ResponseUtils<Category>(false,null, null, "El Nombre de la Categoria ya existe"));

                }else{
                    //valida si el estado es null o es diferente a Inactivo o Activo
                    if(category.Status == "Inactivo" ||category.Status == "Activo"){

                    }else {

                        return BadRequest(new ResponseUtils<Category>(false,null, null, "El estado ingresado no es Valido"));
                    }

                    //Crear Categoria
                    if(ValidateCategory == null && (category.Status == "Inactivo" || category.Status == "Activo")){

                        //poner en minuscula los datos de la base de datos
                        category.Status = category.Status.ToLower();
                        category.CategoryName = category.CategoryName.ToLower();
                        
                        _context.Categories.Add(category);
                        await _context.SaveChangesAsync();
                        var Categoria = CreatedAtAction("GetCategory",new{id = category.Id},category);
                        return new ResponseUtils<Category>(true, new List<Category> { category }, null, "Todo oki");
                    }else{
                        return BadRequest(new ResponseUtils<Category>(false,null, null, "Error al almacenar en la base de datos"));
                    }

                }
            
                


              /*   if(ValidateCategory != null)
                {
                    return BadRequest(new ResponseUtils<Category>(false,null, null, "El Nombre de la Categoria ya existe"));
                }
                else if(string.IsNullOrWhiteSpace(category.CategoryName)){

                    return BadRequest(new ResponseUtils<Category>(false,null, null, "el nombre es un campo obligatorio."));

                }
                else{

                    return BadRequest(new ResponseUtils<Category>(false,null, null, "Error al almacenar en la base de datos"));
                } */
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, $"Error: {ex.Message}"));
            }
        }
        
    }
}
