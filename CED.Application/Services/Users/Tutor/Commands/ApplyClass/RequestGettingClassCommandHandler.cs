﻿using CED.Domain.Users;
using CED.Domain.Shared.ClassInformationConsts;
using MapsterMapper;
using CED.Domain.ClassInformations;
using CED.Application.Services.Abstractions.CommandHandlers;

namespace CED.Application.Services.Users.Tutor.Commands.ApplyClass;

public class RequestGettingClassCommandHandler : CreateUpdateCommandHandler<RequestGettingClassCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IClassInformationRepository _classInformationRepository;
    public RequestGettingClassCommandHandler(IUserRepository userRepository, IClassInformationRepository classInformationRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
        _classInformationRepository = classInformationRepository;
    }
    public override async Task<bool> Handle(RequestGettingClassCommand command, CancellationToken cancellationToken)
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

        var afterUpdatedUser = _classInformationRepository.Update(classInfor);

        if (afterUpdatedUser is null) { return false; }

        return true;
    }
}

