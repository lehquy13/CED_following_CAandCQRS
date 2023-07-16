using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;

public record RemoveTutorReviewCommand(Guid Guid) : IRequest<bool>;
