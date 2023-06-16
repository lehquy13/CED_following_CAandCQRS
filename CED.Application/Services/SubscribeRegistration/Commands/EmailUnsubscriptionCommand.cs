using MediatR;

namespace CED.Application.Services.SubscribeRegistration.Commands;

public record EmailUnsubscriptionCommand(Guid Id) : IRequest<bool>;
