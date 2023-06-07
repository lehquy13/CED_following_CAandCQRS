﻿using CED.Contracts.Authentication;
using CED.Domain.Interfaces.Authentication;
using CED.Domain.Users;
using MapsterMapper;
using MediatR;

namespace CED.Application.Services.Authentication.Customer.Commands.Register;

public class CustomerRegisterCommandHandler 
    : IRequestHandler<CustomerRegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IValidator _validator;

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public CustomerRegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IValidator validator,
        IUserRepository userRepository, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<AuthenticationResult> Handle(CustomerRegisterCommand command, CancellationToken cancellationToken)
    {

        //Check if the user existed

        if (await _userRepository.GetUserByEmail(command.Email) is not null)
        {
            //  return new AuthenticationResult(false, "User has already existed");
            return new AuthenticationResult(null, "",false,"User with an email has already existed");

            throw new Exception("User with an email has already existed");
        }
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password =   _validator.HashPassword(command.Password)
        };

        await _userRepository.Insert(user);

        //Create jwt token
        var token = _jwtTokenGenerator.GenerateToken(
            user.Id,
            command.FirstName,
            command.LastName);


        return new AuthenticationResult(_mapper.Map<UserLoginDto>(user), token,true,"Register successfully");
    }
}

