using MediatR;
namespace CED.Application.Services.Authentication.Admin.Queries.ValidateToken;

public record ValidateTokenQuery
(
    string ValidateToken
    ) : IRequest<bool>;

