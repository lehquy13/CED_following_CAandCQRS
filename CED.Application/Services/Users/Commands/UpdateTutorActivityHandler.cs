using CED.Domain.Users;
using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Commands;

public class UpdateTutorActivityCommandyHandler : IRequestHandler<UpdateTutorActivityCommand,Result<bool>>
{
    private readonly IUserRepository _userRepository;

    public UpdateTutorActivityCommandyHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<Result<bool>> Handle(UpdateTutorActivityCommand request, CancellationToken cancellationToken)
    {
        //TODO: Add last update attribute to user
        throw new NotImplementedException();
    }
}