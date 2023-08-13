using CED.Domain.Users;

namespace CED.Domain.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        bool ValidateToken(string token);
    }
}
