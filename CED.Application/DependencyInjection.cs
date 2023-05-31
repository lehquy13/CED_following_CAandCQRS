﻿using CED.Application.Common.Behaviors;
using CED.Application.Mapping;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CED.Application.Common.Caching;
using CED.Application.Services;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.Subjects.Queries;
using CED.Contracts;
using CED.Contracts.Interfaces.Services;
using CED.Contracts.Subjects;

namespace CED.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddApplicationMappings();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddLazyCache();


            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>)); 
            services.AddScoped(typeof(IPipelineBehavior<GetObjectQuery<PaginatedList<SubjectDto>>,PaginatedList<SubjectDto>>), 
                typeof(CachingBehavior<GetObjectQuery<PaginatedList<SubjectDto>>,PaginatedList<SubjectDto>>)); 
            
            // services.AddScoped(typeof(IPipelineBehavior<GetAllClassInformationsQuery,List<ClassInformationDto>>), 
            //     typeof(CachingBehavior<GetAllClassInformationsQuery,List<ClassInformationDto>>)); 
            
            services.AddScoped(typeof(IAddressService), typeof(AddressService));
         

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
