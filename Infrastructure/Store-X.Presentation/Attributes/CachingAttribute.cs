using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Store_X.Services_Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Presentation.Attributes
{
    public class CachingAttribute(int timeInSeconds) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // logic
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;

            var key = GetCacheKey(context.HttpContext.Request);

            var cacheResult = await cacheService.GetAsync(key);

            //if (string.IsNullOrEmpty(cacheResult))
            //{
            //    await cacheService.SetAsync(key, next(), TimeSpan.FromSeconds(timeInSeconds));
            //}

            if (!string.IsNullOrEmpty(cacheResult))
            {
                var response = new ContentResult()
                {
                    Content = cacheResult,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = response;
                return;
            }

            var actionContext = await next.Invoke();
            if (actionContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.SetAsync(key, okObjectResult.Value, TimeSpan.FromSeconds(timeInSeconds));
            }
        }

        private string GetCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);

            foreach (var item in request.Query)
            {
                key.Append($"|{item.Key}-{item.Value}");
            }

            return key.ToString();
        }
    }
}
