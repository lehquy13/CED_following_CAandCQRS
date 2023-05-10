using CED.Contracts.Authentication;
using MediatR;

namespace CED.Application.Services.Authentication.Customer.Queries.Login;

public record CustomerLoginQuery
(
    string Email,
    string Password) : IRequest<AuthenticationResult>;

