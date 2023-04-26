using MediatR;
namespace CED.Application.Services.Authentication.Queries.ValidateToken;

public record ValidateTokenQuery
(
    string validateToken
    ) : IRequest<bool>;

