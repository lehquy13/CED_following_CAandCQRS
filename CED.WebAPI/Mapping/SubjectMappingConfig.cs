using CED.Application.Services.Authentication.Commands.Register;
using CED.Application.Services.Authentication.Queries.Login;
using CED.Contracts.Entities.Subject;
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

