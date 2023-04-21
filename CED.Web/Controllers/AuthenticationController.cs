using CED.Application.Services.Authentication.Queries.Login;
using CED.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CED.Web.Controllers
{
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

        public IActionResult Index()
        {
            return View("Login", new LoginRequest("",""));
        }

      

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);

            var loginResult = await _mediator.Send(query);

            //return Ok(_mapper.Map<AuthenticationResponse>(loginResult));


            return RedirectToAction("Index", "Home");
        }
    }
}
