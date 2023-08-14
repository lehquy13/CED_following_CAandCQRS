using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.Users.Student.Commands;
using CED.Application.Services.Users.Tutor.ChangeInfo;
using CED.Contracts.Users;
using CED.Contracts.Users.Tutors;
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
            var query = new GetObjectQuery<UserDto>()
            {
                ObjectId = id
            };

            var loginResult = await _mediator.Send(query);

            if (loginResult.IsSuccess)
            {
                return Ok(loginResult.Value);
            }
            //return not found if login failed along with errors
            return NotFound(loginResult.Errors);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(LearnerDto userDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var query = new LearnerInfoChangingCommand(userDto, null);
                    var result = await _mediator.Send(query);

                    if (result.IsSuccess)
                    {
                        return Ok(result.Value);
                    }

                    return Conflict(result.Errors);
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
        
        [HttpPost("EditTutorInformation")]
        public async Task<IActionResult> EditTutorInformation(TutorMainInfoDto userDto, List<Guid> subjectIds, List<string> filePaths)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var query = new TutorInfoChangingCommand(userDto,subjectIds,filePaths);

                    var result = await _mediator.Send(query);

                    if (result.IsSuccess)
                    {
                        return Ok(result.Value);
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