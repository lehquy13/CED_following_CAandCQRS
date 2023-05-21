using LazyCache;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CED.Application.Common.Caching;

public class CachingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly IAppCache _cache;
    private readonly ILogger _logger;
    //private readonly CacheSettings _settings;
    public CachingBehavior(IAppCache cache, ILogger<TResponse> logger)
    {
        _cache = cache;
        _logger = logger;
        //_settings = settings.Value;
    }
   

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string key = GenerateCacheKey(request);
        //big problems
        //return next();

        return _cache.GetOrAddAsync(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
            return await next();
        });
    }
    private string GenerateCacheKey(TRequest request)
    {
        // Implement your logic to generate a unique cache key based on the request
        // You can concatenate request properties or serialize the request object, depending on your requirements
        // Return a string representing the cache key

        return request.GetType().AssemblyQualifiedName + JsonConvert.SerializeObject(request);
    }
}