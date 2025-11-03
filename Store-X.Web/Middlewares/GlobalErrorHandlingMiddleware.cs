using Store_X.Domain.Exceptions;
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
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "aplication/json";
                    var response = new ErrorDetails()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        ErrorMessage = $"End Point {context.Request.Path} is not found!"
                    };

                    await context.Response.WriteAsJsonAsync(response);
                }

            }
            catch (Exception ex)
            {
                // Log Exception
                _logger.LogError(ex, ex.Message);

                // 1. Set Status Code For Response
                // 2. Set Content Type For Response
                // 3. Response Body
                // 4. Return Response

                //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var responce = new ErrorDetails()
                {

                    ErrorMessage = ex.Message
                };

                responce.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };
                context.Response.StatusCode = responce.StatusCode;


                await context.Response.WriteAsJsonAsync(responce);

            }
        }
    }
}
