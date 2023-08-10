using CED.Contracts.Users;
using CED.Contracts.Users.Tutors;
using Mapster;

namespace CED.Web.CustomerSide.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<LearnerDto, TutorForDetailDto>()
            .Map(dest => dest, src => src);
       

    }
}

