using Mottu.Locacao.Motos.Api.Response;
using System.Net;

namespace Mottu.Locacao.Motos.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ExceptionHandlerMiddleware : {0}", ex));

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(
                    new ResponseRequest((int)HttpStatusCode.InternalServerError, false, null, new[] { "Ocorreu um erro inesperado, por favor conte o administrador do sistema ou tente mais tarde" }));
            }
        }
    }
}
