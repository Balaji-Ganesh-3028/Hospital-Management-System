using System.Net;
using System.Text.Json;

namespace backend.Middleware
{
    public class ExceptionHandler
    {
       private readonly RequestDelegate _requestDelegate;

       public ExceptionHandler(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            
            try
            {
                await _requestDelegate(context);
            }
            catch(Exception ex) {
                var result = exceptionMessage(context, ex);
                context.Response.StatusCode = Convert.ToInt16(HttpStatusCode.Unauthorized);
                context.Response.ContentType = result;
            }
        }

        private string exceptionMessage(HttpContext context, Exception ex)
        {
            Console.WriteLine(ex.Message);
            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                ArgumentNullException => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = $"There is an error: {ex.Message}",
            };

            var json = JsonSerializer.Serialize(response);
            return json;
        }
    }
}
