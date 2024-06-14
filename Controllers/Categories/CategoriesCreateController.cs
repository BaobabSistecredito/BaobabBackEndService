using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BaobabBackEndService.Services.categories;
using BaobabBackEndService.Utils;
using BaobabBackEndSerice.Models;
using System.Globalization;



namespace BaobabBackEndService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class CategoriesCreateController : ControllerBase
    {
        private readonly ICategoriesServices _categoryService;

        public CategoriesCreateController(ICategoriesServices categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseUtils<Category>>> CrearCategory(CategoryRequest request)
        {
            try
            {

                var Category = new Category
                {
                    CategoryName = request.CategoryName,
                    Status = request.Status
                };

                var respuesta = await _categoryService.CreateCategoria(Category);

                if (!respuesta.Status)
                {
                    return StatusCode(422, respuesta);
                }


                return Ok(respuesta);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Coupon>(false, message: "Ocurrió un error al crear la categoria: " + ex.Message));

            }


        }








    }

    public interface IActionResult<T>
    {
    }
}