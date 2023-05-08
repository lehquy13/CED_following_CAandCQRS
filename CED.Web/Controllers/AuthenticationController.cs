using CED.Application.Services.Authentication.Commands.Register;
using CED.Application.Services.Authentication.Queries.Login;
using CED.Application.Services.Authentication.Queries.ValidateToken;
using CED.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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


        if (loginResult.IsSuccess is false || loginResult.User is null)
        {
            ViewBag.isFail = true;
            return View("Login", new LoginRequest("", ""));
        }
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

        var returnUrl = TempData["ReturnUrl"] as string;
        if (returnUrl is null)
            return RedirectToAction("Index", "Home");

        _logger.Log(LogLevel.Debug, returnUrl);

        return Redirect(returnUrl);
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
