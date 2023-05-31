using CED.Application.Services.Authentication.Admin.Queries.ValidateToken;
using CED.Application.Services.Authentication.Customer.Commands.Register;
using CED.Application.Services.Authentication.Customer.Queries.Login;
using CED.Contracts.Authentication;
using Mapster;

namespace CED.Web.CustomerSide.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<RegisterRequest, CustomerRegisterCommand>();
        config.NewConfig<LoginRequest, CustomerLoginQuery>();
     
        config.NewConfig<string, ValidateTokenQuery>()
            .Map(dest => dest.ValidateToken, src => src);
    }
}

