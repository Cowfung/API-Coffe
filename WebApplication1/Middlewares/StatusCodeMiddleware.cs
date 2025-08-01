using System.Net;
using System.Text.Json;

namespace WebApplication1.Middlewares
{
    public class StatusCodeMiddleware
    {
        private readonly RequestDelegate _next;

        public StatusCodeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new
                {
                    success = false,
                    message = "Unauthorized - Token missing or invalid"
                });
                await context.Response.WriteAsync(result);
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new
                {
                    success = false,
                    message = "Forbidden - You do not have permission to access this resource"
                });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
