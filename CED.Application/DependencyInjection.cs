using CED.Application.Interfaces;
using CED.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CED.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
