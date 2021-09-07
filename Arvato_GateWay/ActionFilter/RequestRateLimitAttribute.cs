using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace GateWayApi.DAL.ActionFilter
{
    public class RequestRateLimitAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
        public int Seconds { get; set; }
        private static MemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions());

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var ipAddress = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress;
            var memoryCacheKey = $"{Name}-{ipAddress}";

            if (!Cache.TryGetValue(memoryCacheKey, out bool entry))
            {
                var cacheEntryPoint = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(Seconds));
                Cache.Set(memoryCacheKey, true, cacheEntryPoint);
            }
            else
            {
                context.Result = new ContentResult()
                {
                    Content = $"Request are Limited to 1 request every {Seconds} seconds.",
                    StatusCode  = StatusCodes.Status403Forbidden
                };

                context.HttpContext.Request.Scheme = ((int)HttpStatusCode.TooManyRequests).ToString();

            }

            base.OnActionExecuted(context);
        }
    }
}
