using CED.Contracts.Users;
using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;

public record CreateUpdateTutorCommand
(
    TutorDto TutorDto,
    List<Guid> SubjectId
) : IRequest<bool>;