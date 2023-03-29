using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.UsersInformations.Admin.Commands.ChangeInfo;

public record ConfirmTutorInfoCommand
(
    TutorDto TutorDto
    ) : IRequest<bool>;

