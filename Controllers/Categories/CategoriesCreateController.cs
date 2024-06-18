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
using BaobabBackEndService.DTOs;
using BaobabBackEndService.ExternalServices.SlackNotificationService;



namespace BaobabBackEndService.Controllers
{
    [ApiController]
    [Route("/api/categories")]

    public class CategoriesCreateController : ControllerBase
    {
        private readonly ICategoriesServices _categoryService;
        private readonly SlackNotificationService _slackNotificationService;

        public CategoriesCreateController(ICategoriesServices categoryService,SlackNotificationService slackNotificationService)
        {   
            _slackNotificationService = slackNotificationService;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseUtils<Category>>> CrearCategory(CategoryDTO request)
        {
            try
            {
                var respuesta = await _categoryService.CreateCategoria(request);
                return StatusCode(respuesta.StatusCode, respuesta);
            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(422, new ResponseUtils<Coupon>(false, message: "Ocurrió un error al crear la categoria: " + ex.Message));
            }
        }








    }

}