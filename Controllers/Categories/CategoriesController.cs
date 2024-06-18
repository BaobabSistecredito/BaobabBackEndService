using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.categories;
using BaobabBackEndService.ExternalServices.SlackNotificationService;

namespace BaobabBackEndSerice.Controllers
{

    [ApiController]
    [Route("/api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesServices _categoryService;
        private readonly SlackNotificationService _slackNotificationService;


        public CategoriesController(ICategoriesServices categoryService, SlackNotificationService slackNotificationService)
        {
            _slackNotificationService = slackNotificationService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public ResponseUtils<Category> GetAllCategories()
        {
            try
            {

                throw new InvalidOperationException("Esta es una pruba con el fin de probar el slack");
                return _categoryService.GetAllCategories();
            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new ResponseUtils<Category>(false, null, 500, $"Error: {ex.Message}");
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
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new ResponseUtils<Category>(false, null, 500, $"Error: {ex.Message}");
            }
        }
    }
}
