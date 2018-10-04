using CalculadorDeJurosApi.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace CalculadorDeJurosApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}