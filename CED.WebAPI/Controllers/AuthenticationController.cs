using CED.Contracts.Authorization;
using Microsoft.AspNetCore.Mvc;
using CED.Application.Interfaces;

namespace CED.WebAPI.Controllers;

[ApiController]
[Route("Auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Register")]
    public IActionResult Register(RegisterRequest request)
    {
        var registerResult = _authenticationService.Regiser(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
            );

        var respone = new AuthenticationResponse(
            registerResult.Id,
            registerResult.FirstName,
            registerResult.LastName,
            registerResult.Email,
            registerResult.Token
            );
        return Ok(respone);
    }

    [HttpPost("Login")]
    public IActionResult Login(LoginRequest request)
    {
        var loginResult = _authenticationService.Login(
            request.Email,
            request.Password
            );

        var respone = new AuthenticationResponse(
            loginResult.Id,
            loginResult.FirstName,
            loginResult.LastName,
            loginResult.Email,
            loginResult.Token
            );
        return Ok(respone);
    }

}