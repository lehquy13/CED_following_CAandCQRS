using CED.Contracts.Users;
using CED.Domain.Users;
using Mapster;

namespace CED.Application.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<StudentDto, TutorDto>();
            
        config.NewConfig<TutorDto, User>();

    }
}

