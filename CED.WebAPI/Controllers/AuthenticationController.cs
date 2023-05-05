using CED.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CED.Application.Services.Authentication.Commands.Register;
using CED.Application.Services.Authentication.Queries.Login;
using MapsterMapper;

namespace CED.WebAPI.Controllers;

[ApiController]
[Route("Auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

     public AuthenticationController(IMediator mediator, IMapper mapper)
    {
        _mediator= mediator;
        _mapper= mapper;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
       
        var registerResult = await _mediator.Send(command);
      
        return Ok(registerResult);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var loginResult = await _mediator.Send(query);

        return Ok(loginResult);

    }

}