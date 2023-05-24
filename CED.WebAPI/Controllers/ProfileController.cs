using System.Net;
using CED.Application.Services.Authentication.Admin.Commands.ChangePassword;
using CED.Application.Services.Users.Admin.Commands;
using CED.Application.Services.Users.Queries;
using CED.Application.Services.Users.Student.Commands;
using CED.Application.Services.Users.Tutor.Commands.ChangeInfo;
using CED.Contracts.Authentication;
using CED.Contracts.Users;
using CED.Domain.Shared;
using CED.Domain.Shared.ClassInformationConsts;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CED.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ProfileController> _logger;


        public ProfileController(ISender mediator, IMapper mapper, ILogger<ProfileController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet("")]
        public async Task<IActionResult> Profile(Guid id)
        {
            var query = new GetUserByIdQuery<UserDto>()
            {
                Id = id
            };

            var loginResult = await _mediator.Send(query);

            if (loginResult is not null)
            {
                return Ok(loginResult);
            }

            return NotFound();
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IRequest<bool> query;
                    if (userDto.Role == UserRole.Tutor)
                    {
                        query= new TutorInfoChangingCommand(userDto);
                    }
                    else 
                    {
                        query= new StudentInfoChangingCommand(userDto);

                    }

                    var result = await _mediator.Send(query);

                    if (result)
                    {
                        return Ok(result);
                    }

                    return Conflict(result);
                }
                catch (Exception ex)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists, " + ex.Message +
                                                 "see your system administrator.");
                }
            }

            return BadRequest(userDto);
        }
    }
}