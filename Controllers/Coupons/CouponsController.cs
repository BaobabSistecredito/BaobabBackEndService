using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using System.Collections.Generic;
using BaobabBackEndService.Services.Coupons;
using BaobabBackEndService.ExternalServices.SlackNotificationService;

namespace BaobabBackEndSerice.Controllers
{
    [ApiController]
    [Route("/api/coupons")]
    public class CouponsController : ControllerBase
    {
        /*
        Parte 0:

        En esta sección, declaramos nuestra interfaz y la llamamos, similar a cómo llamamos al contexto de la base de datos.
        Este paso solo se realiza una vez por archivo.

        ¿Por qué hacemos esto?
        Establecer la interfaz en el archivo permite definir los métodos que necesitamos implementar más adelante. 
        De esta manera, podemos trabajar con una estructura clara y definida.

        ¿Cuándo lo hacemos?
        Más adelante, proporcionaré una guía paso a paso sobre cómo y cuándo realizar cada acción. 
        Por ahora, asegúrate de seguir este patrón para cada archivo donde necesites implementar una interfaz.

        Continuemos con el siguiente paso...
        */
        private readonly ICouponsServices _couponsService;
        private readonly SlackNotificationService _slackNotificationService;


        public CouponsController(ICouponsServices couponsService,SlackNotificationService slackNotificationService)
        {
            _slackNotificationService = slackNotificationService;
            _couponsService = couponsService;
        }


        /*
        Parte 1:

        Esta es la entrada de nuestro servicio. Todo lo que enviemos desde herramientas como Postman u otros clientes HTTP llegará aquí.
        En este caso, redirigimos la información que enviamos o solicitamos al repositorio correspondiente.

        - En este ejemplo, estamos manejando una solicitud GET.
        - Solicitamos información y almacenamos el resultado en la variable 'result'.
        - En este caso, estamos solicitando cupones.

        El flujo de la información es el siguiente:
        1. El cliente (por ejemplo, Postman) envía una solicitud al servicio.
        2. El servicio recibe la solicitud y la procesa.
        3. El servicio redirige la solicitud al repositorio correspondiente.
        4. El repositorio maneja la lógica de acceso a datos (en este caso, recupera los cupones).

        Para ver la implementación del repositorio, consulta el archivo BusinessLogic/CouponsRepository.
        Continuemos con el siguiente paso Alli en este archivo...
        */

        [HttpGet]
        public IActionResult GetCoupons([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = _couponsService.GetCoupons(pageNumber, pageSize);
            return Ok(response);
        }
        // ----------------------- GET COUPON & CATEGORY:
        //REVISAR DE QUIEN ES ESTO PLEASE
        [HttpGet]
        [Route("couponsAndCategories")]
        public async Task<ActionResult<ResponseUtils<Coupon>>> GetCouponsAndCategory()
        {
            var result = await _couponsService.GetCouponAndCategory();
            return new ResponseUtils<Coupon>(true, new List<Coupon>(result), 200, "¡Listado de cupones!");
        }

        [HttpGet("{searchType}/{value}")]
        public async Task<ResponseUtils<Coupon>> GetCoupon(string searchType, string value)
        {
            try
            {
                var result = await _couponsService.GetCouponsAsync(searchType, value);
                return result;
            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new ResponseUtils<Coupon>(false, null, 500, $"Error: {ex.Message}");
            }
        }

        //Buscador y search
        [HttpGet("{search}")]
        public async Task<ActionResult<ResponseUtils<Coupon>>> SearchFilter(string search)
        {
            try
            {

                var searchResult = await _couponsService.FilterSearch(search);
                if (!searchResult.IsSuccessful)
                {
                    return StatusCode(400, searchResult);
                }

                return Ok(searchResult);

            }
            catch (Exception ex)
            {
                _slackNotificationService.SendNotification($"Ha ocurrido un error en el sistema: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, new ResponseUtils<Category>(false, null, 500, $"Errors: {ex.Message}"));
            }
        }

    }
}



