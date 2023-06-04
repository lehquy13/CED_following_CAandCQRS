﻿using CED.Application.Services.Authentication.Admin.Commands.Register;
using CED.Application.Services.Authentication.Admin.Queries.ValidateToken;
using CED.Application.Services.Authentication.Commands.Register;
using CED.Application.Services.Authentication.Customer.Commands.Register;
using CED.Application.Services.Authentication.Customer.Queries.Login;
using CED.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CED.Web.CustomerSide.Controllers;

[Route("[controller]")]
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

        if (loginResult)
        {
            return RedirectToAction("Index", "Home");
        }

        return View("Login", new LoginRequest("", ""));
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var query = _mapper.Map<CustomerRegisterCommand>(registerRequest);
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
        {
            
            StoreCookie(result);
            return RedirectToAction("Index", "Home");
        }
        ViewBag.isFail = true;
        return View(registerRequest);
    }

    [HttpGet]
    [Route("Register")]
    public IActionResult Register()
    {
        return View();
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<CustomerLoginQuery>(request);

        var loginResult = await _mediator.Send(query);


        if (loginResult.IsSuccess is false || loginResult.User is null)
        {
            ViewBag.isFail = true;
            return View("Login", new LoginRequest("", ""));
        }

        StoreCookie(loginResult);

        var returnUrl = TempData["ReturnUrl"] as string;
        if (returnUrl is null)
            return RedirectToAction("Index", "Home");

        _logger.Log(LogLevel.Debug, returnUrl);

        return Redirect(returnUrl);
    }


    void StoreCookie(AuthenticationResult loginResult)
    {
        // Store the JWT token in a cookie

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            IsEssential = true,
            Expires = DateTime.UtcNow.AddDays(1),
            //Domain = "yourdomain.com",
        };
        HttpContext.Response.Cookies.Append("access_token", loginResult.Token, cookieOptions);
        HttpContext.Response.Cookies.Append("name", loginResult.User.FullName);
        HttpContext.Response.Cookies.Append("image", loginResult.User.Image);
        HttpContext.Response.Cookies.Append("email", loginResult.User.Email);
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