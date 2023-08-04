using CED.Contracts.Authentication;
using MediatR;

namespace CED.Application.Services.Authentication.ManageAccount;

public record ManageAccountQuery
(
   string Token
    ) : IRequest<AuthenticationResult>;

