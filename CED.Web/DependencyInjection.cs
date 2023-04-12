using CED.Web.Mapping;

namespace CED.Web;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings();
        return services;
    }
}

