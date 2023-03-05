﻿using CED.Application.Common.Authentication;
using CED.Application.Common.Persistence;
using CED.Application.Services.Authentication.Common;
using CED.Domain.Entities;
using MediatR;

namespace CED.Application.Services.Authentication.Commands.Register;

public class RegisterCommandHandler 
    : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        //Check if the user existed
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            //  return new AuthenticationResult(false, "User has already existed");
            throw new Exception("User with an email has already existed");
        }
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        _userRepository.Add(user);

        //Create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user.Id, command.FirstName, command.LastName);


        return new AuthenticationResult(user, token);
    }
}

