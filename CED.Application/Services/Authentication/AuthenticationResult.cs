using CED.Domain.User;

namespace CED.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token
);

