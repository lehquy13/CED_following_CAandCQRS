using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Commands;

public class UpdateTutorActivityCommand : IRequest<Result<bool>>
{
    public Guid UserId { get; set; }
}