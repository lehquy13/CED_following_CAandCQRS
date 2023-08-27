using CED.Contracts.Authentication;
using CED.Domain.Shared.ClassInformationConsts;
using MediatR;

namespace CED.Application.Services.Authentication.Customer.Commands.Register;

public record CustomerRegisterCommand
(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber,
    string Address,
    int BirthYear,
    Gender Gender
) : IRequest<AuthenticationResult>;