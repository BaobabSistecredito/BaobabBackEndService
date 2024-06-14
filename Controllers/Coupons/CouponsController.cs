using Microsoft.AspNetCore.Mvc;
using BaobabBackEndSerice.Data;
using BaobabBackEndSerice.Models;
using BaobabBackEndService.Utils;
using System.Collections.Generic;
using BaobabBackEndService.Services.Coupons;

namespace BaobabBackEndSerice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
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

        public CouponsController(ICouponsServices couponsService)
        {
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
        public async Task<ActionResult<ResponseUtils<Coupon>>> GetCoupons()
        {
            try
            {
                var result = _couponsService.GetCoupons();
                return new ResponseUtils<Coupon>(true, new List<Coupon>(result), null, "todo oki");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, $"Errors: {ex.Message}"));
            }
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
                return new ResponseUtils<Coupon>(false, null, null, $"Error: {ex.Message}");
            }
        }

        //Buscador y search
        [HttpGet("{Search}")]
        public async Task<ActionResult<ResponseUtils<Coupon>>> SearchFilter(string Search)
        {
            try
            {

                var SearchResult = await _couponsService.FilterSearch(Search);
                if (!SearchResult.Status)
                {
                    return StatusCode(400, SearchResult);
                }

                return Ok(SearchResult);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseUtils<Category>(false, null, null, $"Errors: {ex.Message}"));
            }
        }

    }
}



