using CED.Application.Services.Authentication.Queries.Login;
using CED.Contracts.Authentication;
using CED.Domain.Interfaces.Authentication;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using MapsterMapper;
using MediatR;

namespace CED.Application.Services.Authentication.Customer.Queries.Login;

public class CustomerLoginQueryHandler
    : IRequestHandler<CustomerLoginQuery, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public CustomerLoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<AuthenticationResult> Handle(CustomerLoginQuery query, CancellationToken cancellationToken)
    {
        //await Task.CompletedTask;
        //1. Check if user exist
        if (await _userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return new AuthenticationResult(null,"",false, "User has already existed");
            //throw new Exception("User with an email doesn't exist");
        }

        //2. Check if logining with right password
        
        if (user.Password != query.Password || user.Role == UserRole.Admin)
        {
            return new AuthenticationResult(null, "", false, "Wrong password");

        }
        //3. Generate token
        var loginToken = _jwtTokenGenerator.GenerateToken(
            user.Id,
            user.FirstName,
            user.LastName);

        return new AuthenticationResult( _mapper.Map<UserLoginDto>(user),loginToken, true , "Login successfully");
    }
}

