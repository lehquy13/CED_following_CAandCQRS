using Azure.Core;
using CED.Application.Services.Authentication.Commands.ChangePassword;
using CED.Application.Services.Authentication.Commands.Register;
using CED.Application.Services.Authentication.Commands.SaveToken;
using CED.Application.Services.Authentication.Queries.Login;
using CED.Application.Services.Authentication.Queries.ValidateToken;
using CED.Application.Services.Subjects.Commands;
using CED.Application.Services.Users.Queries;
using CED.Contracts.Authentication;
using CED.Contracts.Users;
using CED.Domain.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CED.Web.Controllers;

public class AuthenticationController : Controller
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(ISender mediator, IMapper mapper, ILogger<AuthenticationController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    private void PackStaticListToView()
    {
        ViewData["Roles"] = CEDEnumProvider.Roles;
        ViewData["Genders"] = CEDEnumProvider.Genders;
        ViewData["AcademicLevels"] = CEDEnumProvider.AcademicLevels;
    }

    [Route("")]
    public async Task<IActionResult> Index(string? returnUrl)
    {
        TempData["ReturnUrl"] = returnUrl;
        string validateToken = HttpContext.Request.Cookies["access_token"] ?? "";
        if (validateToken is "")
        {
            return View("Login", new LoginRequest("", ""));
        }
        var query = new ValidateTokenQuery(validateToken);

        var loginResult = await _mediator.Send(query);

        if (loginResult is true)
        {
            return RedirectToAction("Index", "Home");
        }

        return View("Login", new LoginRequest("", ""));
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var loginResult = await _mediator.Send(query);


        if (loginResult.IsSuccess is false)
        {
            ViewBag.isFail = true;
            return View("Login", new LoginRequest("", ""));
        }
        // Store the JWT token in a cookie
        var command = new SaveTokenCommand(loginResult.Token, HttpContext);
        var result = await _mediator.Send(command);
        if (result is false) { return View("Index", this.ModelState); }

        var returnUrl = TempData["ReturnUrl"] as string;
        if (returnUrl is null)
            return RedirectToAction("Index", "Home");
        _logger.Log(LogLevel.Debug, returnUrl);

        return Redirect(returnUrl);
    }

    [Authorize]
    [HttpGet("Profile")]
    public async Task<IActionResult> Profile()
    {
        PackStaticListToView();
        string validateToken = HttpContext.Request.Cookies["access_token"] ?? "";
        if (HttpContext.User.Identity is null || HttpContext.User.Identity.IsAuthenticated is false )
        {
            return View("Login", new LoginRequest("", ""));
        }
        var identity = HttpContext.User.Identities.FirstOrDefault();
        
        if (identity == null) { return View("Login", new LoginRequest("", "")); }

        var query = new GetUserByIdQuery<UserDto>() {
            Id = new Guid(identity.Claims.FirstOrDefault()?.Value ?? "")
        };

        var loginResult = await _mediator.Send(query);

        if (loginResult is not null)
        {
            return View(loginResult);
        }

        return View("Login", new LoginRequest("", ""));
    }

    [Authorize]
    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid Id, UserDto userDto)
    {
        if (Id != userDto.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                var query = new CreateUserCommand()
                {
                    UserDto = userDto
                };
                var result = await _mediator.Send(query);
                ViewBag.Updated = true;
                return View(userDto);



            }
            catch (Exception ex)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " + ex.Message +
                    "see your system administrator.");
            }
        }
        return View(userDto);
    }
    [Authorize]
    [HttpPost("ChangePassword")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
    {
        if (changePasswordRequest.ConfirmedPassword != changePasswordRequest.NewPassword)
        {
            return View("Profile",ModelState);
        }
        if (ModelState.IsValid)
        {
            try
            {
                var query = _mapper.Map<ChangePasswordCommand>(changePasswordRequest);

                var loginResult = await _mediator.Send(query);

                if(loginResult.IsSuccess)
                {
                    ViewBag.Updated = true;

                    return Json(true);
                }

            }
            catch (Exception ex)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " + ex.Message +
                    "see your system administrator.");
            }
        }
        return View("Profile", ModelState);

    }



    [Authorize]
    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("access_token");
       
        return View("Login", new LoginRequest("", ""));
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var query = new ForgotPasswordCommand(email);

        var loginResult = await _mediator.Send(query);

        return RedirectToAction("Index", "Home");
    }
}
