﻿using CED.Application.Services.Authentication.Commands.ChangePassword;
using CED.Contracts.Authentication;
using CED.Domain.Interfaces.Authentication;
using CED.Domain.Users;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Authentication.Customer.Commands.ChangePassword;

public class CustomerChangePasswordCommandHandler : IRequestHandler<CustomerChangePasswordCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    

    ILogger<CustomerChangePasswordCommandHandler> _logger;
    public CustomerChangePasswordCommandHandler(IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository, ILogger<CustomerChangePasswordCommandHandler> logger, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<AuthenticationResult> Handle(CustomerChangePasswordCommand command, CancellationToken cancellationToken)
    {

        //Check if the user existed


        if (await _userRepository.GetById(command.Id) is not User user  )
        {
            _logger.LogError("Can not change password. User doesn't exist.", command);
            return new AuthenticationResult(null,"",false, "User doesn't exist");
        }
        if( command.NewPassword != command.ConfirmedPassword)
        {
            _logger.LogError("Can not change password. Password doesn't match.", command);
            return new AuthenticationResult(null, "", false, "Password doesn't match.");
        }
        
        user.Password = command.NewPassword;

        var newUser = _userRepository.Update(user);
        
        return new AuthenticationResult(_mapper.Map<UserLoginDto>(user), "" ,true, "Password changed.");
    }
}

