using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.ClassInformations.Queries.GetClassInformation;
using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using Mapster;

namespace CED.Web.Mapping;

public class ClassInformationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Guid, GetClassInformationQuery>()
           .Map(dest => dest.Id, src => src);
        config.NewConfig<Guid, DeleteClassInformationCommand>()
            .Map(dest => dest.Guid, src => src);
        
        config.NewConfig<CreateUpdateClassInformationDto, CreateUpdateClassInformationCommand>()
            .Map(dest => dest.ClassInformationDto, src => src);

    }
}

