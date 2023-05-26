using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Interfaces.Logger;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Admin.Commands;

public class CreateUpdateTutorCommandHandler : CreateUpdateCommandHandler<CreateUpdateTutorCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly IAppLogger<CreateUpdateTutorCommandHandler> _logger;

    public CreateUpdateTutorCommandHandler(IUserRepository userRepository, IRepository<TutorMajor> tutorMajorRepository,
        IAppLogger<CreateUpdateTutorCommandHandler> logger, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
        _logger = logger;
        _tutorMajorRepository = tutorMajorRepository;
    }

    public override async Task<bool> Handle(CreateUpdateTutorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserByEmail(command.UserDto.Email);
            var newMajorUpdate = command.SubjectId.DistinctBy(x => x).ToList();
            //Check if the subject existed
            if (user is not null && user.Role == UserRole.Tutor)
            {
                var currentMajor = _tutorMajorRepository.GetAll().Where(x => x.TutorId.Equals(command.UserDto.Id));
                // check the subject changes
                foreach (var major in currentMajor)
                {
                    if (!newMajorUpdate.Contains(major.SubjectId))
                    {
                        await _tutorMajorRepository.DeleteById(major.Id);
                        _logger.LogDebug("Remove subject {0} from tutor's major", major.SubjectId);
                    }
                    else
                    {
                        var removeResult = newMajorUpdate.Remove(major.SubjectId);
                        if (removeResult)
                        {
                            _logger.LogDebug("Remove subject {0} from newMajorUpdateList", major.SubjectId);
                        }
                        else
                        {
                            _logger.LogError("fail to remove the subject {0} from newMajorUpdateList", major.SubjectId);
                        }
                    }
                }
                foreach (var newMU in newMajorUpdate)
                {
                    await _tutorMajorRepository.Insert(new TutorMajor()
                    {
                        TutorId = user.Id,
                        SubjectId = newMU
                    });
                }

                user.UpdateUserInformation(_mapper.Map<User>(command.UserDto));
                _logger.LogDebug("ready for updating!");
                _userRepository.Update(user);

                return true;
            }
            _logger.LogDebug("ready for creating!");

            user = _mapper.Map<User>(command.UserDto);
            await _userRepository.Insert(user);

            // add new subjects

            foreach (var newMU in newMajorUpdate)
            {
                await _tutorMajorRepository.Insert(new TutorMajor()
                {
                    TutorId = user.Id,
                    SubjectId = newMU
                });
            }




            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error happens when user is adding or updating." + ex.Message);
        }
    }
}