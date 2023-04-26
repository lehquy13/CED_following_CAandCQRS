using CED.Contracts.Interfaces.Authentication;
using MediatR;

namespace CED.Application.Services.Authentication.Queries.ValidateToken;

public class ValidateTokenQueryHandler
    : IRequestHandler<ValidateTokenQuery, bool>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    public ValidateTokenQueryHandler(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<bool> Handle(ValidateTokenQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _jwtTokenGenerator.ValidateToken(query.validateToken);
    }
}

