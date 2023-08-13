using CED.Web.Mapping;
using CED.Web.Utilities;

namespace CED.Web;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings();
        services.AddSingleton<ILocalStorageService, LocalStorageServiceService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
}

