using CED.Contracts.Users;
using CED.Contracts.Users.Tutors;
using Mapster;

namespace CED.WebAPI.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<LearnerDto, TutorForDetailDto>()
            .Map(dest => dest, src => src);
        config.NewConfig<UserDto, TutorForDetailDto>();


    }
}

