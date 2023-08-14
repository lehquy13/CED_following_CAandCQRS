using CED.WebAPI.Mapping;

namespace CED.WebAPI;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        return services;
    }
}

