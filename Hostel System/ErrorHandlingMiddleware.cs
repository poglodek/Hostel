using Hostel_System.Core.Exception;

namespace Hostel_System
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;

        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }

            catch (ForbiddenException ex)
            {
                _logger.LogError(ex.Message, ex);
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("something goes wrong.");
            }
        }
    }
}
