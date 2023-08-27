using CED.Application.Services.Authentication;
using CED.Application.Services.Authentication.Admin.Commands.ChangePassword;
using CED.Application.Services.Authentication.Admin.Commands.Register;
using CED.Application.Services.Authentication.Admin.Queries.Login;
using CED.Application.Services.Authentication.Commands.Register;
using CED.Contracts.Authentication;
using CED.Contracts.Users;
using CED.Domain.Shared.ClassInformationConsts;
using Mapster;

namespace CED.Web.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>()
            .Map(des => des, src => (src.Gender == "Male")?Gender.Male: Gender.Female)
            .Map(des => des, src => src);
        config.NewConfig<ChangePasswordRequest, ChangePasswordCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<UserDto, ChangePasswordRequest>()
            .Map(dest => dest.Id, src => src.Id);
        
    }
}

