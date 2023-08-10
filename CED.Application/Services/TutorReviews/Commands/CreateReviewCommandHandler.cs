using CED.Application.Common.Errors.Users;
using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.TutorReviews.Queries;
using CED.Contracts;
using CED.Contracts.TutorReview;
using CED.Contracts.Users.Tutors;
using CED.Domain.ClassInformations;
using CED.Domain.Common.Models;
using CED.Domain.Repository;
using CED.Domain.Review;
using CED.Domain.Shared.NotificationConsts;
using CED.Domain.Users;
using FluentResults;
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

    public CreateReviewCommandHandler(IRepository<TutorReview> tutorReviewRepository, IAppCache cache,
        ILogger<CreateReviewCommandHandler> logger, IMapper mapper, ITutorRepository tutorRepository,
        IClassInformationRepository classInformationRepository,
        IPublisher publisher, IUnitOfWork unitOfWork) : base(logger, mapper, unitOfWork,cache, publisher)
    {
        _tutorReviewRepository = tutorReviewRepository;
        _tutorRepository = tutorRepository;
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<Result<bool>> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var review = await _tutorReviewRepository.GetById(command.ReviewDto.Id);
            var tutor = await _tutorRepository.GetUserByEmail(command.TutorEmail);
            if (tutor is  null)
            {
                return Result.Fail(new NonExistUserError());
            }
            //Check if the review existed
            if (review is not null)
            {
                review = _mapper.Map<TutorReview>(command.ReviewDto);
                review.LastModificationTime = DateTime.Now;
            }
            else
            {
                command.ReviewDto.TutorId = tutor.Id;
                review = _mapper.Map<TutorReview>(command.ReviewDto);
                await _tutorReviewRepository.Insert(review);
            }

            if (await _unitOfWork.SaveChangesAsync() > 0)
            {
                var message = "New tutor review for " + command.TutorEmail + " at " + review.CreationTime.ToLongDateString();
                await _publisher.Publish(new NewObjectCreatedEvent(review.Id, message, NotificationEnum.ReviewClass), cancellationToken);
            }
            
            //Update tutor's rate
            var reviews = await _tutorRepository.GetReviewsOfTutor(tutor.Id);
           
            tutor.Rate = (short)reviews.Average(x => x.Rate);
            
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return Result.Fail("Fail to update tutor's rate");
            }
            
            var defaultRequest = new GetObjectQuery<TutorForDetailDto>();
            _cache.Remove(defaultRequest.GetType() + JsonConvert.SerializeObject(defaultRequest));
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error happens when review is adding or updating." + ex.Message);
        }
    }
}