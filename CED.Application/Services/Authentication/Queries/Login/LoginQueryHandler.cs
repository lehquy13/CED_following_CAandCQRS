using CED.Application.Common.Authentication;
using CED.Application.Common.Persistence;
using CED.Application.Services.Authentication.Common;
using CED.Domain.Entities;
using MediatR;

namespace CED.Application.Services.Authentication.Queries.Login;

public class LoginQueryHandler
    : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        //1. Check if user exist
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            //return new AuthenticationResult(false, "User has already existed");
            throw new Exception("User with an email doesn't exist");
        }

        //2. Check if logining with right password

        if (user.Password != query.Password)
        {
            throw new Exception("Wrong password");
        }
        //3. Generate token
        var loginToken = _jwtTokenGenerator.GenerateToken(
            user.Id,
            user.FirstName,
            user.LastName);

        return new AuthenticationResult(user, loginToken);
    }
}

