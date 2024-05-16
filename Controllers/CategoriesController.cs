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

        [HttpGet("{number}")]
        public async Task<ActionResult<ResponseUtils<Category>>> GetCategories(string number)
        {   
            try
            {   
                

                /*  */
                if(!int.TryParse(number, out int ParseNumber)){
                    return BadRequest(new ResponseUtils<Category>(false, null, null, "SkormJF disapproved"));
                }

                var result = await _context.Categories.ToListAsync();
                if(ParseNumber != 1){
                        result = result.Where(c => c.Status == "Inactivo").ToList();
                    }
                    else{
                        result = result.Where(c => c.Status == "Activo").ToList();
                    }
                return new ResponseUtils<Category>(true, result, null, "SkormJF approved");               
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, $"Error: {ex.Message}"));
            }

        }
    }
}
