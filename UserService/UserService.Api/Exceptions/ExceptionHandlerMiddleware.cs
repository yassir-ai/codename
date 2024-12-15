using System.Net;
using Serilog;

namespace UserService.Exceptions {
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case EntityNotFoundException:
                    Log.Error(exception, "Entity not found: {Message}", exception.Message);
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case DatabaseSavingException:
                    Log.Error(exception, "Database error: {Message}", exception.Message);
                    break;
                default:
                    Log.Error(exception, "An unexpected error occurred: {Message}", exception.Message);
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new { exception.Message, StatusCode = (int)statusCode};

            return context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}