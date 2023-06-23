using CED.Application.Services.Authentication.Admin.Commands.ChangePassword;
using CED.Application.Services.Authentication.Admin.Commands.Register;
using CED.Application.Services.Authentication.Admin.Queries.ValidateToken;
using CED.Application.Services.Authentication.Commands.Register;
using CED.Application.Services.Authentication.Customer.Commands.ChangePassword;
using CED.Application.Services.Authentication.Customer.Commands.Register;
using CED.Application.Services.Authentication.Customer.Queries.Login;
using CED.Contracts.Authentication;
using CED.Web.CustomerSide.Utilities;
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

        if (loginResult && HttpContext.Session.GetString("email") != null)
        {
            return RedirectToAction("Index", "Home");
        }

        return await Logout();
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var query = _mapper.Map<CustomerRegisterCommand>(registerRequest);
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
        {
            await StoreCookie(result);
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

        await StoreCookie(loginResult);

        var returnUrl = TempData["ReturnUrl"] as string;
        if (returnUrl is null)
            return RedirectToAction("Index", "Home");

        var value = HttpContext.Session.GetString("Value");
        _logger.Log(LogLevel.Debug, returnUrl);
        if (value != null)
        {
            HttpContext.Session.Remove("Value");
            await HttpContext.Session.CommitAsync();
            return RedirectToAction("Detail", "ClassInformation", new { id = new Guid(value) });
        }

        return Redirect(returnUrl);
    }


    private async Task StoreCookie(AuthenticationResult loginResult)
    {
        // Store the JWT token in a cookie

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            IsEssential = true,
            Expires = DateTime.UtcNow.AddHours(16),
            //Domain = "yourdomain.com",
        };
        if (loginResult.User != null)
        {
             HttpContext.Response.Cookies.Append("access_token", loginResult.Token, cookieOptions);
            // HttpContext.Response.Cookies.Append("name", loginResult.User.FullName, cookieOptions);
            // HttpContext.Response.Cookies.Append("image", loginResult.User.Image, cookieOptions);
            // HttpContext.Response.Cookies.Append("email", loginResult.User.Email, cookieOptions);
            
            //HttpContext.Session.SetString("access_token",loginResult.Token);
            HttpContext.Session.SetString("name",loginResult.User.FullName);
            HttpContext.Session.SetString("image",loginResult.User.Image);
            HttpContext.Session.SetString("email",loginResult.User.Email);
            HttpContext.Session.SetString("role",loginResult.User.Role.ToString());
            await HttpContext.Session.CommitAsync();

        }
    }

    [Authorize]
    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Response.Cookies.Delete("access_token");
        HttpContext.Session.Remove("name");
        HttpContext.Session.Remove("image");
        HttpContext.Session.Remove("email");
        await HttpContext.Session.CommitAsync();

        return RedirectToAction("Index", "Home");
    }
    [HttpGet("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword()
    {
        return View();
    }
    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword( string email)
    {
        var query = new ForgotPasswordCommand(email);

        var loginResult = await _mediator.Send(query);

        return View("SuccessPage", "If you have registered with us, we have sent an email to your registered email.");
    }
    [HttpGet]
    [Route("ChangePassword/{id}")]
    public async Task<IActionResult> ChangePassword(string id)
    {
        return View("ChangePassword",id);
    }
    [HttpPost("ChangePassword1")]
    public async Task<IActionResult> ChangePassword1(string id, string password)
    {
        var query = new ChangePasswordWhenForgotCommand(new Guid(id), password);

        var loginResult = await _mediator.Send(query);

        if (loginResult)
        {
            return RedirectToAction("Index");
        }
        return RedirectToAction("FailPage","Home");
    }
    
    

   
    
}