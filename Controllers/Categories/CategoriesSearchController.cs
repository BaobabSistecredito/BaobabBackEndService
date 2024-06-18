using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using BaobabBackEndService.Services.categories;
using BaobabBackEndService.ExternalServices.SlackNotificationService;

namespace BaobabBackEndService.Controllers.Categories
{

    [ApiController]
    [Route("/api/categories")]
    public class CategoriesSearchController : ControllerBase
    {
        private readonly ICategoriesServices _categoryService;
        private readonly SlackNotificationService _slackNotificationService;

        public CategoriesSearchController(ICategoriesServices categoryService,SlackNotificationService slackNotificationService)
        {
            _slackNotificationService = slackNotificationService;
            _categoryService = categoryService;
        }
        // ----------------------- SEARCH ACTION:
        [HttpGet("search/{category?}")]
        public async Task<ActionResult<ResponseUtils<Category>>> SearchCategory(string? category)
        {
            try
            {
                var response = await _categoryService.SearchCategory(category);
                if (!response.IsSuccessful)
                {
                    return StatusCode(400, response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new ResponseUtils<Category>(false, null, 400, $"Error: {ex.Message}");
            }

        }

    }
}