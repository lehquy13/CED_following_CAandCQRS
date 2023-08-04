using CED.Contracts.Users;
using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;
/// <summary>
/// This command currently is not used
/// </summary>
/// <param name="TutorDto"></param>
public record ConfirmTutorInfoCommand
(
    TutorDto TutorDto
) : IRequest<Result<bool>>;

