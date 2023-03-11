using CED.Domain.Entities.User;

namespace CED.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token
);

