using MediatR;

namespace CED.Application.Services.SubscribeRegistration.Commands;

public record EmailSubscriptionCommand(string Mail) : IRequest<bool>;
