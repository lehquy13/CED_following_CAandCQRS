using CED.Application.Services.Authentication.Commands.ChangePassword;
using CED.Application.Services.Authentication.Commands.Register;
using CED.Application.Services.Authentication.Queries.Login;
using CED.Contracts.Authentication;
using CED.Contracts.Users;
using CED.Domain.Users;
using Mapster;

namespace CED.Application.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserLoginDto>()
            .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}")
            .Map(dest => dest, src => src);
        
    }
}

