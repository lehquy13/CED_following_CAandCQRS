using CED.Application.Services.Authentication.Common;
using CED.Contracts.Authentication;
using Mapster;

namespace CED.WebAPI.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest.Token, src => src.User);


    }
}

