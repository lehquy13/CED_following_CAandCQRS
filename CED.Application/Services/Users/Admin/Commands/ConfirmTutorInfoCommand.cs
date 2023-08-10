using CED.Contracts.Users.Tutors;
using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;
/// <summary>
/// This command currently is not used
/// </summary>
/// <param name="TutorForDetailDto"></param>
public record ConfirmTutorInfoCommand
(
    TutorForDetailDto TutorForDetailDto
) : IRequest<Result<bool>>;

