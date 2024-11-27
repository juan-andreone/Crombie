using bibliotecaLAST.Logging;

namespace bibliotecaLAST.Middlewares
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly FileLogger _logger;

        public ErrorLoggingMiddleware(RequestDelegate next, FileLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Procesar la solicitud
            }
            catch (Exception ex)
            {
                // Registrar el error en el archivo
                _logger.LogError($"Error procesando la solicitud: " +
                                 $"{context.Request.Method} {context.Request.Path}", ex);

                // Enviar una respuesta de error al cliente
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Ocurrió un error interno del servidor.");
            }
        }
    }
}





