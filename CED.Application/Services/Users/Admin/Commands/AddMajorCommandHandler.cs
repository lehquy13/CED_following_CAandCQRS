using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Interfaces.Logger;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Admin.Commands;

public class AddMajorCommandHandler : CreateUpdateCommandHandler<AddMajorCommand>
{

    private readonly IUserRepository _userRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly IAppLogger<AddMajorCommandHandler> _logger;

    public AddMajorCommandHandler(IUserRepository userRepository, IRepository<TutorMajor> tutorMajorRepository , IAppLogger<AddMajorCommandHandler> logger, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
        _tutorMajorRepository = tutorMajorRepository;
        _logger = logger;
    }

    public override async Task<bool> Handle(AddMajorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetById(command.TutorId);
            //Check if the subject existed
            _logger.LogInformation("Checking tutor...");
            if (user is not null && user.Role == UserRole.Tutor)
            {
                _logger.LogInformation("Checked! Updating user's major...");

                await _tutorMajorRepository.Insert( new TutorMajor()
                {
                    TutorId = command.TutorId,
                    SubjectId = command.SubjectId
                });

                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            throw new Exception("Error happens when adding or updating tutor's majors." + ex.Message);
        }
    }
}

