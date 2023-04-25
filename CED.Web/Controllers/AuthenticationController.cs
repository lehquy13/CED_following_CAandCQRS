using CED.Application.Services.Authentication.Queries.Login;
using CED.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Dynamic.Core.Tokenizer;

namespace CED.Web.Controllers;

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
    public IActionResult Index()
    {
        return View("Login", new LoginRequest("",""));
    }
  

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var loginResult = await _mediator.Send(query);

        // Store the JWT token in a cookie
        var token = loginResult.Token;
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTime.UtcNow.AddDays(1),
           // Domain = "yourdomain.com",
        };
        HttpContext.Response.Cookies.Append("access_token", token,cookieOptions);
        
        return RedirectToAction("Index", "Home");
    }
}
