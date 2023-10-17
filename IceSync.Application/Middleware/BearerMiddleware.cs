using IceSync.Domain.Constants;
using IceSync.Domain.Models.Caching;
using IceSync.Infrastructure.ExternalApis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace IceSync.Application.Middleware
{
    public class BearerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UniversalLoaderClient _universalLoaderClient;
        private readonly IMemoryCache _memoryCache;

        public BearerMiddleware(RequestDelegate next,
            UniversalLoaderClient universalLoaderClient,
            IMemoryCache memoryCache)
        {
            _next = next;
            _universalLoaderClient = universalLoaderClient;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!_memoryCache.TryGetValue(Constants.BearerKey, out BearerCacheData? bearer))
            {
                bearer = await _universalLoaderClient.Authenticate();
                _memoryCache.Set(Constants.BearerKey, bearer);
            }
            else if (bearer.ExpirationDate < DateTimeOffset.UtcNow)
            {
                bearer = await _universalLoaderClient.Authenticate();
                _memoryCache.Set(Constants.BearerKey, bearer);
            }

            context.Items.Add(Constants.BearerKey, bearer);

            await _next(context);
        }
    }
}