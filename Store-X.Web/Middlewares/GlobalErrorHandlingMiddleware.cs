using Store_X.Shared.ErrorModels;

namespace Store_X.Web.Middlewares
{
    public class GlobalErrorHandlingMiddleware(RequestDelegate _next, ILogger<GlobalErrorHandlingMiddleware> _logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                // Log Exception
                _logger.LogError(ex, ex.Message);

                // 1. Set Status Code For Response
                // 2. Set Content Type For Response
                // 3. Response Body
                // 4. Return Response

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var responce = new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                };


                await context.Response.WriteAsJsonAsync(responce);

            }
        }
    }
}
