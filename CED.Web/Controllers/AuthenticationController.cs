using CED.Application.Services.Authentication.Admin.Commands.ForgotPassword;
using CED.Application.Services.Authentication.Admin.Queries.Login;
using CED.Application.Services.Authentication.ValidateToken;
using CED.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;


namespace CED.Web.Controllers;

[Route("[controller]")]
[Route("")]
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
        string? validateToken = Request.Headers[HeaderNames.Authorization];
        if (validateToken is null)
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


    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var loginResult = await _mediator.Send(query);


        if (loginResult.IsSuccess is false || loginResult.User is null)
        {
            ViewBag.isFail = true;
            return View("Login", new LoginRequest("", ""));
        }
        // Store the JWT token in a cookie
      
        //store token into session
        HttpContext.Session.SetString("access_token", loginResult.Token);
        HttpContext.Session.SetString("name", loginResult.User.FullName);
        HttpContext.Session.SetString("image", loginResult.User.Image);

        var returnUrl = TempData["ReturnUrl"] as string;
        if (returnUrl is null)
            return RedirectToAction("Index", "Home");

        return Redirect(returnUrl);
    }

   



    [Authorize(Policy = "RequireAdministratorRole")]
    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("access_token");
        HttpContext.Response.Cookies.Delete("name");
        HttpContext.Response.Cookies.Delete("image");

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
