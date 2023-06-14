using CED.Contracts.Users;
using Mapster;

namespace CED.Web.CustomerSide.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<LearnerDto, TutorDto>()
            .Map(dest => dest, src => src);
       

    }
}

