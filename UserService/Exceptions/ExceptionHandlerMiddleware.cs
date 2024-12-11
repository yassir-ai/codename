using System.Net;

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

            // TODO: add logs
            switch (exception)
            {
                case EntityNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case DatabaseSavingException:
                    break;
                default:
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