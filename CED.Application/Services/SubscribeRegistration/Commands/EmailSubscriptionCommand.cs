using MediatR;

namespace CED.Application.Services.SubscribeRegistration.Commands;

public record EmailSubscriptionCommand(Guid Id) : IRequest<bool>;
