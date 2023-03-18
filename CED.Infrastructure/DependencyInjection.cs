using CED.Application.Common.Authentication;
using CED.Application.Common.Services;

using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using CED.Domain.User;

using CED.Infrastructure.Authentication;
using CED.Infrastructure.Persistence;
using CED.Infrastructure.Persistence.Repository;
using CED.Infrastructure.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace CED.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration
            )
        {
            services.AddAuth(configuration);
            services.AddDbContext<CEDDBContext>(options =>
                options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection")
                    )

            ) ;
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddDatabaseDeveloperPageExceptionFilter();
            // Dependency Injection for repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IClassInformationRepository, ClassInformationRepository>();
            return services;
        }

        public static IServiceCollection AddAuth(
           this IServiceCollection services,
           ConfigurationManager configuration
           )
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings._SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings._SectionName));

            services
                .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secrect)
                        ),
                });

            return services;
        }
    }
}
