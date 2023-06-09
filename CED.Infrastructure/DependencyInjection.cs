using CED.Domain.Interfaces.Authentication;
using CED.Domain.Interfaces.Services;

using CED.Domain.ClassInformations;
using CED.Domain.Subjects;
using CED.Domain.Users;

using CED.Infrastructure.Authentication;
using CED.Infrastructure.Persistence.Repository;
using CED.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CED.Domain.Interfaces.Logger;
using CED.Domain.Repository;
using CED.Infrastructure.Entity_Framework_Core;
using CED.Infrastructure.Logging;
using CED.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace CED.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration
            )
        {
            // Authentication configuration using jwt bearer
            services.AddAuth(configuration);

            services.AddDbContext<CEDDBContext>(options =>
                options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection")
                    )

            );
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));


            // Dependency Injection for repository
            services.AddScoped( typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITutorRepository, TutorRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IClassInformationRepository, ClassInformationRepository>();
            
            // set configuration settings to cloudinarySettings and turn it into Singleton
            var cloudinary = new CloudinarySetting();
            configuration.Bind(CloudinarySetting._SectionName, cloudinary);
            services.AddSingleton(Options.Create(cloudinary));
            services.AddScoped<ICloudinaryFile, CloudinaryFile>();
            
            return services;
        }

        public static IServiceCollection AddAuth(
           this IServiceCollection services,
           ConfigurationManager configuration
           )
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IValidator, Validator>();

            // set configuration settings to jwtSettings and turn it into Singleton
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings._SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));

            //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings._SectionName));
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(scheme =>
                    {
                        scheme.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        scheme.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    })
                    .AddCookie(options =>
                    {
                        options.Cookie.Name = "access_token";
                        options.Cookie.HttpOnly = true;
                        options.Cookie.SameSite = SameSiteMode.Strict;
                        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                        //options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(90);
                    
                        options.LoginPath = "/Authentication";
                        options.LogoutPath = "/Logout";
                        options.AccessDeniedPath = "/";
                      
                    
                        
                    
                    })
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtSettings.Issuer,
                            ValidAudience = jwtSettings.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secrect)),
                            
                        };
                        
                        options.Events = new JwtBearerEvents()
                        {
                            OnMessageReceived = context =>
                            {
                                context.Token = context.Request.Cookies["access_token"];
                                return Task.CompletedTask;
                            },
                            
                            
                            OnForbidden = context =>
                            {
                                //context.HttpContext.Response.Redirect("/");
                                context.Response.Redirect("/Authentication");
                                return Task.CompletedTask;

                            },
                            OnAuthenticationFailed = context =>
                            {
                                //context.HttpContext.Response.Redirect("/");
                                context.Response.Cookies.Delete("access_token");
                                context.Response.Cookies.Delete("name");
                                context.Response.Cookies.Delete("image");
                                context.Response.Cookies.Delete("email");
                                context.Response.Redirect("/");

                                return Task.CompletedTask;

                            }
                        };
                        
                       
                    });


            return services;
        }
    }
}
