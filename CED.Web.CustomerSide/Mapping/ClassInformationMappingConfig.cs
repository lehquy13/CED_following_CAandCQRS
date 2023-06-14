﻿using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.ClassInformations.Tutor.Commands.ApplyClass;
using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using CED.Contracts.Users;
using Mapster;

namespace CED.Web.CustomerSide.Mapping;

public class ClassInformationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Guid, GetClassInformationQuery>()
           .Map(dest => dest.Id, src => src);
        config.NewConfig<Guid, DeleteClassInformationCommand>()
            .Map(dest => dest.id, src => src);
        
        config.NewConfig<LearnerDto, CreateClassInformationByCustomer>()
            .Map(dest => dest.ContactNumber, src => src.PhoneNumber)
            .Map(dest => dest.Address, src => src.Address)
            .Map(dest => dest.StudentGender, src => src.Gender)
            .Ignore(dest => dest.Description);
        config.NewConfig<CreateClassInformationByCustomer, CreateUpdateClassInformationCommand>()
            .Map(dest => dest.ClassInformationDto, src => src);
        config.NewConfig<RequestGettingClassRequest, RequestGettingClassCommand>()
            .Map(dest => dest.ClassGuid, src => src.ClassId)
            .Map(dest => dest.Email, src => src.Email);
        
    }
}

