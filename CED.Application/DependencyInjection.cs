using CED.Application.Common.Behaviors;
using CED.Application.Services.Authentication.Commands.Register;
using CED.Application.Services.Authentication.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CED.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddScoped<
                IPipelineBehavior<RegisterCommand, AuthenticationResult>,
                ValidatorRegisterCommandBehavior
                >();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
