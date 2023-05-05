namespace CED.Contracts.Authentication;

public record AuthenticationResult(
    UserLoginDto? User,
    string Token,
    bool IsSuccess,
    string Message
);

