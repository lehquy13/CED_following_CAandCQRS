using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;

public record RemoveTutorVerificationCommand(Guid Guid) : IRequest<bool>;
