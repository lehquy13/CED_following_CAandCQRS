using CED.Domain.Entities;

namespace CED.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);

