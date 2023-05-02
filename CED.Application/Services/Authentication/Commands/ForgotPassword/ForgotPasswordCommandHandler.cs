using CED.Domain.Interfaces.Authentication;
using CED.Domain.Users;
using MediatR;

namespace CED.Application.Services.Authentication.Commands.Register;

public class ForgotPasswordCommandHandler
    : IRequestHandler<ForgotPasswordCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public ForgotPasswordCommandHandler(IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<AuthenticationResult> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return new AuthenticationResult(null, "",false,"");
    }
}

