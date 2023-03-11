using CED.Contracts.Entities.Subject;
using CED.Domain.Entities.Subject;
using Mapster;

namespace CED.Application.Mapping;

public class SubjectMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Subject, SubjectDto>();

    }
}

