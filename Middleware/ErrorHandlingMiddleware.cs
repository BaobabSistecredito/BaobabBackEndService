using BaobabBackEndService.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
namespace BaobabBackEndService.Middleware
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;
    using BaobabBackEndService.Utils;

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException)
            {
                var response = new ResponseUtils<object>(false, message: "No está autorizado para acceder a este recurso.");
                await WriteResponse(context, response, StatusCodes.Status401Unauthorized);
            }
            catch (SecurityTokenExpiredException)
            {
                var response = new ResponseUtils<object>(false, message: "El token ha expirado.");
                await WriteResponse(context, response, StatusCodes.Status403Forbidden);
            }
        }

        private async Task WriteResponse(HttpContext context, ResponseUtils<object> response, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }

}

