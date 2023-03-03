namespace CED.Contracts.Authorization;

public record LoginRequest(
    string Email,
    string Password
);

