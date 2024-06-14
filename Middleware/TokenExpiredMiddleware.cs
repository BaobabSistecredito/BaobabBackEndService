using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Threading.Tasks;

namespace BaobabBackEndService.Middleware
{
    public class TokenExpiredMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenExpiredMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SecurityTokenExpiredException)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(new { Message = "Token Expirado. Por favor, genere un nuevo." }.ToString());
                }
            }
            catch (Exception ex)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(new { Message = ex.Message }.ToString());
                }
                else
                {
                    throw;
                }
            }
        }
    }

    public static class TokenExpiredMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenExpiredMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenExpiredMiddleware>();
        }
    }
}
