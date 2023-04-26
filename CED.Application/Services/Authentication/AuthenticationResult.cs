using CED.Domain.Users;

namespace CED.Application.Services.Authentication;

public record AuthenticationResult(
    User? User,
    string Token,
    bool IsSuccess,
    string Message
);

