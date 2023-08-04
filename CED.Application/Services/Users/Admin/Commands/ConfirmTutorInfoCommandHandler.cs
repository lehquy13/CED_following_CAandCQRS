using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Users.Admin.Commands;

public class ConfirmTutorInfoCommandHandler : CreateUpdateCommandHandler<ConfirmTutorInfoCommand>
{
    private readonly ITutorRepository _userRepository;

    public ConfirmTutorInfoCommandHandler(ITutorRepository userRepository,
        ILogger<ConfirmTutorInfoCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork, IPublisher publisher,
        IAppCache cache) : base(logger,mapper, unitOfWork, cache, publisher)
    {
        _userRepository = userRepository;
    }
    public override async Task<Result<bool>> Handle(ConfirmTutorInfoCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        var user = await _userRepository.GetUserByEmail(command.TutorDto.Email);
        if (user is null)
        {
            throw new Exception("Tutor with an email doesn't exist");
        }
        //if (user.Role != UserRole.Tutor) return false;

        command.TutorDto.IsVerified = true;
        user.UpdateTutorInformation(_mapper.Map<Domain.Users.Tutor>(command.TutorDto));

        var afterUpdatedUser = _userRepository.Update(user);

        if (afterUpdatedUser is null) { return false; }

        return true;
    }
}

