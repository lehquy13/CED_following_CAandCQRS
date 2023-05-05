using CED.Contracts.Authentication;
using MediatR;
namespace CED.Application.Services.Authentication.Queries.ManageAccount;

public record ManageAccountQuery
(
   string Token
    ) : IRequest<AuthenticationResult>;

