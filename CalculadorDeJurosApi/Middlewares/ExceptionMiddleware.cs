using System;
using System.Net;
using System.Threading.Tasks;
using CalculadorDeJurosApi.Models;
using CalculadorDeJurosApi.Wrappers;
using Microsoft.AspNetCore.Http;

namespace CalculadorDeJurosApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;
    
    
        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }
    
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
    
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var excecaoPersonalizada = new ExcecaoPersonalizada()
            {
                StatusCode = context.Response.StatusCode,
                Mensagem = exception.Message,
                StackTrace = exception.StackTrace
            };
            return context.Response.WriteAsync(excecaoPersonalizada.ToString());
        }
    }
}