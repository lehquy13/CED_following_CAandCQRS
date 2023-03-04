using CED.Application.Common.Authentication;
using CED.Application.Common.Persistence;
using CED.Application.Interfaces;
using CED.Domain.Entities;

namespace CED.Application.Services;


public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this._jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Regiser(string firstName, string lastName, string email, string password)
    {
        //Check if the user existed
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            //return new AuthenticationResult(false, "User has already existed");
            throw new Exception("User with an email has already existed");
        }
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.Add(user);

        //Create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user.Id, firstName, lastName);


        return new AuthenticationResult(user.Id, user.FirstName, user.LastName, user.Email, token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        //1. Check if user exist
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            //return new AuthenticationResult(false, "User has already existed");
            throw new Exception("User with an email hasn't already existed");
        }

        //2. Check if logining with right password

        if (user.Password != password)
        {
            throw new Exception("Wrong password");
        }
        //3. Generate token
        var loginToken = _jwtTokenGenerator.GenerateToken(
            user.Id,
            user.FirstName,
            user.LastName);

        return new AuthenticationResult(user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            loginToken
        );
    }
}