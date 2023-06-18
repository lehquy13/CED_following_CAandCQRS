using MediatR;

namespace CED.Application.Services.SubscribeRegistration.Commands;

public record EmailUnSubscriptionCommand(string Mail) : IRequest<bool>;
