using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.ClassInformations.Queries;
using CED.Contracts.ClassInformations;
using Mapster;

namespace CED.WebAPI.Mapping;

public class ClassInformationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Guid, GetClassInformationQuery>()
           .Map(dest => dest.Id, src => src);
        config.NewConfig<Guid, DeleteClassInformationCommand>()
            .Map(dest => dest.id, src => src);
        
        config.NewConfig<CreateUpdateClassInformationDto, CreateUpdateClassInformationCommand>()
            .Map(dest => dest.ClassInformationDto, src => src);

    }
}

