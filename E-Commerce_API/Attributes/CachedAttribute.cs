using E_Commerce.Service.Services.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_Commerce_API.Attributes
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int expireTime;

        public CachedAttribute(int expireTime)
        {
            this.expireTime = expireTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await cacheService.GetCacheKeyAsync(cacheKey);
            if (cacheResponse != null)
            {
                var contentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }
            // To execute the action if the cache is empty
            var executedContext = await next();
            if (executedContext.Result is OkObjectResult objectResult)
            {
                await cacheService.SetCacheKeyAsync(cacheKey, objectResult.Value, TimeSpan.FromSeconds(expireTime));
            }
        }
        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var cacheKey = string.Empty;
            // To Get Request Path like /api/products
            cacheKey += request.Path;
            // OrderBy is used to make sure the query string is always in the same order
            // Like /api/products?name=product1&type=type1 is the same as /api/products?type=type1&name=product1
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                cacheKey += $"|{key}-{value}";
            }
            return cacheKey;
        }
    }
}
