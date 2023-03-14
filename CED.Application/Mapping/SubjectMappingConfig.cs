using CED.Contracts.Subjects;
using CED.Domain.Entities.Subjects;
using Mapster;

namespace CED.Application.Mapping;

public class SubjectMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Subject, SubjectDto>();

    }
}

