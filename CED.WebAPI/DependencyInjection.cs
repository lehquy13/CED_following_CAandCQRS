using CED.WebAPI.Mapping;

namespace CED.WebAPI;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings();
        return services;
    }
}

