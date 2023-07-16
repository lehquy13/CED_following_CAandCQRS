using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.TutorReviews.Queries;
using CED.Contracts;
using CED.Contracts.TutorReview;
using CED.Contracts.Users;
using CED.Domain.ClassInformations;
using CED.Domain.Common.Models;
using CED.Domain.Repository;
using CED.Domain.Review;
using CED.Domain.Shared.NotificationConsts;
using CED.Domain.Users;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CED.Application.Services.TutorReviews.Commands;

public class CreateReviewCommandHandler : CreateUpdateCommandHandler<CreateReviewCommand>
{
    private readonly IRepository<TutorReview> _tutorReviewRepository;
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly ITutorRepository _tutorRepository;
    private readonly IAppCache _cache;
    private readonly IPublisher _publisher;

    public CreateReviewCommandHandler(IRepository<TutorReview> tutorReviewRepository, IAppCache cache,
        ILogger<CreateReviewCommandHandler> logger, IMapper mapper, ITutorRepository tutorRepository, IClassInformationRepository classInformationRepository,
        IPublisher publisher
        ) : base(logger, mapper)
    {
        _tutorReviewRepository = tutorReviewRepository;
        _cache = cache;
        _publisher = publisher;
        _tutorRepository = tutorRepository;
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<bool> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var review = await _tutorReviewRepository.GetById(command.ReviewDto.Id);
            var tutor = await _tutorRepository.GetUserByEmail(command.TutorEmail);
            if (tutor is  null)
            {
                return false;
            }
            //Check if the subject existed
            if (review is not null)
            {
                review.LastModificationTime = DateTime.Now;
                review.Description = command.ReviewDto.Description;
                review.Rate = command.ReviewDto.Rate;
                _tutorReviewRepository.Update(review);
            }
            else
            {
            
               
                command.ReviewDto.TutorId = tutor.Id;

                review = _mapper.Map<TutorReview>(command.ReviewDto);
                review.CreationTime = DateTime.Now;
                var entity = await _tutorReviewRepository.Insert(review);
                var message = "New tutor review for " + command.TutorEmail + " at " + entity.CreationTime.ToLongDateString();
                await _publisher.Publish(new NewObjectCreatedEvent(entity.Id, message, NotificationEnum.ReviewClass), cancellationToken);
            }
            var teachingClasses =
                _classInformationRepository.GetAll().Where(x => x.TutorId.Equals(tutor.Id)).ToList();
            var reviews =  (await _tutorReviewRepository.GetAllList()).Join(
                teachingClasses,
                rev => rev.ClassInformationId,
                cl => cl.Id,
                (rev,cl) => new
                {
                    review = rev
                }).Select(x => x.review.Rate)
                .ToList();
            tutor.Rate = (short)reviews.Average(x => x);
            _tutorRepository.Update(tutor);
            
            var defaultRequest = new GetObjectQuery<TutorDto>();
            _cache.Remove(defaultRequest.GetType() + JsonConvert.SerializeObject(defaultRequest));
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error happens when review is adding or updating." + ex.Message);
        }
    }
}