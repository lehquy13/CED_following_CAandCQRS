using CED.Application.Services.Authentication.Commands.Register;
using CED.Contracts.Entities.Subject;
using Mapster;

namespace CED.WebAPI.Mapping;

public class SubjectMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<CreateUpdateSubjectDto, CreateSubjectCommand>()
            .Map(dest => dest.SubjectDto, src => src);

    }
}

