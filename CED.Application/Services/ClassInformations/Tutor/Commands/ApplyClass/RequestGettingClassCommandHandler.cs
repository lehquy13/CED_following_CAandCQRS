using CED.Application.Common.Errors.ClassInformations;
using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using FluentResults;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.ClassInformations.Tutor.Commands.ApplyClass;

public class RequestGettingClassCommandHandler : NewCreateUpdateCommandHandler<RequestGettingClassCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ITutorRepository _tutorRepository;
    private readonly IRepository<RequestGettingClass> _requestGettingClassRepository;
    private readonly IClassInformationRepository _classInformationRepository;

    public RequestGettingClassCommandHandler(IUserRepository userRepository, ITutorRepository tutorRepository,
        IClassInformationRepository classInformationRepository, ILogger<RequestGettingClassCommandHandler> logger,
        IRepository<RequestGettingClass> requestGettingClassRepository,
        IMapper mapper) : base(logger,mapper)
    {
        _userRepository = userRepository;
        _classInformationRepository = classInformationRepository;
        _tutorRepository = tutorRepository;
        _requestGettingClassRepository = requestGettingClassRepository;
    }

    public override async Task<Result<bool>> Handle(RequestGettingClassCommand command, CancellationToken cancellationToken)
    {
        try
        {
            //Check if the user existed
            var user = await _userRepository.GetUserByEmail(command.Email);
            var tutor = await _tutorRepository.GetUserByEmail(command.Email);
            if (user is null || tutor is null)
            {
                throw new Exception("User doesn't exist");
            }

            if (user.Role != UserRole.Tutor)
            {
                throw new Exception("User has not register as a tutor!");
            }

            var classInfor = await _classInformationRepository.GetById(command.ClassGuid);
            if (classInfor is null)
            {
                throw new Exception("Class doesn't exist.");
            }

            if (classInfor.IsDeleted is true || !classInfor.Status.Equals(Status.Available))
            {
                throw new Exception("Class is deleted or is being verified.");
            }

            var checkIfTheTutorAlreadyRequest = _requestGettingClassRepository.GetAll()
                .FirstOrDefault(x => x.ClassInformationId.Equals(command.ClassGuid) && x.TutorId.Equals(user.Id));
            if (checkIfTheTutorAlreadyRequest is not null)
            {
                _logger.LogError("Tutor has already requested");
                return Result.Fail<bool>(new RequestedClassError());
            }
            //im doing here
            var request = new RequestGettingClass()
            {
                ClassInformationId = classInfor.Id,
                TutorId = tutor.Id
            };

            await _requestGettingClassRepository.Insert(request);
            
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Result.Fail(e.Message);
        }
    }
}