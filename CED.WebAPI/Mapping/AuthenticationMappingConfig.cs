using CED.Application.Services.Authentication;
using CED.Application.Services.Authentication.Commands.Register;
using CED.Application.Services.Authentication.Queries.Login;
using CED.Application.Services.Authentication.Queries.ValidateToken;
using CED.Contracts.Authentication;
using Mapster;

namespace CED.WebAPI.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User);

        config.NewConfig<string, ValidateTokenQuery>()
            .Map(dest => dest.validateToken, src => src);
    }
}

