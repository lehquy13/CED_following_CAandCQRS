using CED.Application.Services.Subjects.Commands;
using CED.Application.Services.Subjects.Queries;
using CED.Contracts.Subjects;
using Mapster;

namespace CED.WebAPI.Mapping;

public class SubjectMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Guid, GetSubjectQuery>()
            .Map(dest => dest.id, src => src);
        config.NewConfig<Guid, DeleteSubjectCommand>()
            .Map(dest => dest.id, src => src);
        
        config.NewConfig<CreateUpdateSubjectDto, CreateUpdateSubjectCommand>()
            .Map(dest => dest.SubjectDto, src => src);

    }
}

