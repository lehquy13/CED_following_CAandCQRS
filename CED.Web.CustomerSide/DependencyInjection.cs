using CED.Web.CustomerSide.Mapping;

namespace CED.Web.CustomerSide;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings();
        //services.AddTransient<GlobalErrorHandlingMiddleWare>();
        return services;
    }
}

