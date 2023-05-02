namespace CED.Domain.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId, string firstName, string lastName);
        bool ValidateToken(string token);
    }
}
