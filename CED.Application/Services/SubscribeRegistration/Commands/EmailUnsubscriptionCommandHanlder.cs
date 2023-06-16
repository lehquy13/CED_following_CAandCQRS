
using CED.Domain.Repository;
using CED.Domain.Subscriber;
using MediatR;

namespace CED.Application.Services.SubscribeRegistration.Commands;

public record EmailUnsubscriptionCommandHanlder : IRequestHandler<EmailSubscriptionCommand, bool>
{

    private readonly IRepository<Subscriber> _subscriberRepository;
    public EmailUnsubscriptionCommandHanlder(IRepository<Subscriber> subscriberRepository)
    {
        _subscriberRepository = subscriberRepository;
    }
    
    public async Task<bool> Handle(EmailSubscriptionCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var subscriber = _subscriberRepository.GetAll()
            .FirstOrDefault(x => x.TutorId.Equals(command.Equals(command.Id)));
        if (subscriber != null)
        {
            _subscriberRepository.Delete(subscriber);
            return true;
        }

        return false;
    }
}
