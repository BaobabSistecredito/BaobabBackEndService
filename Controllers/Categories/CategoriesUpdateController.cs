using Microsoft.AspNetCore.Mvc;
using BaobabBackEndService.DTOs;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using Microsoft.AspNetCore.Authorization;
using BaobabBackEndService.Services.categories;
using BaobabBackEndService.ExternalServices.SlackNotificationService;

namespace BaobabBackEndSerice.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoriesUpdateController : ControllerBase
    {
        private readonly ICategoriesServices _categoryService;
        private readonly SlackNotificationService _slackNotificationService;

        public CategoriesUpdateController(ICategoriesServices categoryService,SlackNotificationService slackNotificationService)
        {
            _slackNotificationService = slackNotificationService;
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
                if (!response.IsSuccessful)
                {
                    return StatusCode(409, response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(422, new ResponseUtils<Category>(false, message: "Ocurrió un error al actualizar la categoría: " + ex.Message));
            }
        }
    }
}
