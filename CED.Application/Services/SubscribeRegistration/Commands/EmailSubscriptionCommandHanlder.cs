using CED.Domain.Repository;
using CED.Domain.Subscriber;
using MediatR;

namespace CED.Application.Services.SubscribeRegistration.Commands;

public record EmailSubscriptionCommandHanler : IRequestHandler<EmailSubscriptionCommand, bool>
{
    private readonly IRepository<Subscriber> _subscriberRepository;
    public EmailSubscriptionCommandHanler(IRepository<Subscriber> subscriberRepository)
    {
        _subscriberRepository = subscriberRepository;
    }
    
    public async Task<bool> Handle(EmailSubscriptionCommand command, CancellationToken cancellationToken)
    {
        if (command.Id != Guid.Empty)
        {
            await _subscriberRepository.Insert(
                new Subscriber()
                {
                    TutorId = command.Id
                }
            );
            return true;
        }

        return false;

    }
}
