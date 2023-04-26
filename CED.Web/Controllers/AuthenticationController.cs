using CED.Application.Services.Authentication.Commands.Register;
using CED.Application.Services.Authentication.Commands.SaveToken;
using CED.Application.Services.Authentication.Queries.Login;
using CED.Application.Services.Authentication.Queries.ValidateToken;
using CED.Contracts.Authentication;
using MapsterMapper;
using MediatR;
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
    public async Task<IActionResult> Index()
    {
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

        
        if(loginResult.IsSuccess is false)
        {
            ViewBag.isFail = true;
            return View("Login", new LoginRequest("", ""));
        }
        // Store the JWT token in a cookie
        var command = new SaveTokenCommand(loginResult.Token,HttpContext);
        var result = await _mediator.Send(command);
        if (result is false) { return View("Index", this.ModelState); }

        return RedirectToAction("Index", "Home");
    }
    [HttpPost("Logout")]
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
