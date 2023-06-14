using CED.Application.Services.Users.Queries;
using CED.Application.Services.Users.Student.Commands;
using CED.Application.Services.Users.Tutor.ChangeInfo;
using CED.Contracts.Users;

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
        public async Task<IActionResult> Edit(LearnerDto userDto, string filePath = "")
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var query = new LearnerInfoChangingCommand(userDto,filePath);


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
        [HttpPost("EditTutorInfor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTutorInfor(TutorMainInfoDto userDto, List<Guid> subjectIds, List<string> filePaths)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var query = new TutorInfoChangingCommand(userDto,subjectIds,filePaths);

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