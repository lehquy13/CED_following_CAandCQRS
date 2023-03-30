using CED.Domain.Users;
using CED.Domain.Shared.ClassInformationConsts;
using MapsterMapper;
using CED.Application.Common.Services.CommandHandlers;
using CED.Domain.ClassInformations;

namespace CED.Application.Services.UsersInformations.Tutor.Commands.ApplyClass;

public class ApplyClassCommandHandler : CreateUpdateCommandHandler<TutorInfoChangingCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IClassInformationRepository _classInformationRepository;
    public ApplyClassCommandHandler(IUserRepository userRepository, IClassInformationRepository classInformationRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
        _classInformationRepository = classInformationRepository;
    }
    public override async Task<bool> Handle(TutorInfoChangingCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        var user = await _userRepository.GetById(command.TutorGuid);
        if (user is null)
        {
            throw new Exception("User doesn't exist");
        }
        if (user.Role != UserRole.Tutor) return false;

        var classInfor = await _classInformationRepository.GetById(command.ClassGuid);
        if (classInfor is null)
        {
            throw new Exception("Class doesn't exist");
        }
        if(classInfor.IsDeleted is true || !classInfor.Status.Equals(Status.Waiting))
        {
            return false;
        }
        //im doing here
        //user.UpdateTutorInformation(_mapper.Map<User>(command));

        classInfor.TutorId = command.TutorGuid;

        var afterUpdatedUser = _userRepository.Update(user);

        if (afterUpdatedUser is null) { return false; }

        return true;
    }
}

