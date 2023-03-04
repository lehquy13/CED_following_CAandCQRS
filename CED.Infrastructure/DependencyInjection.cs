using CED.Application.Common.Authentication;
using CED.Application.Common.Persistence;
using CED.Application.Common.Services;
using CED.Infrastructure.Authentication;
using CED.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CED.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration
            )
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings._SectionName));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            // Dependency Injection for repository
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
 