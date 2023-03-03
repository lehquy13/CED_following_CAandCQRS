using CED.Application.Interfaces;

namespace CED.Application.Services;


public class AuthenticationService : IAuthenticationService
{
    public AuthenticationResult Regiser(string firstName, string lastName, string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), "register", "", "", "token");
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), "Login", "", "", "tokenlogin");
    }
}