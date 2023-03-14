using CED.Contracts.ClassInformations;
using CED.Domain.Entities.ClassInformations;
using Mapster;

namespace CED.Application.Mapping;

public class ClassInformationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<ClassInformationDto, ClassInformation>();
        config.NewConfig<ClassInformation, ClassInformationDto>();

    }
}

