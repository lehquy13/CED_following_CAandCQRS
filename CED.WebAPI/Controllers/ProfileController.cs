using System.Security.Claims;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.Authentication.Admin.Commands.ChangePassword;
using CED.Application.Services.Authentication.Customer.Queries.Login;
using CED.Application.Services.Authentication.RefreshToken;
using CED.Application.Services.ClassInformations.Tutor.Queries.GetAllRequestGettingClassOfTutor;
using CED.Application.Services.ClassInformations.Tutor.Queries.GetTeachingClassDetailQuery;
using CED.Application.Services.Users.Student.Commands;
using CED.Application.Services.Users.Tutor.ChangeInfo;
using CED.Contracts.Authentication;
using CED.Contracts.ClassInformations.Dtos;
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
        public async Task<IActionResult> Profile()
        {
            var identity = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            var query = new GetObjectQuery<UserDto>()
            {
                ObjectId = new Guid(identity ?? "")
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
            try
            {
                var result = await _mediator.Send(new LearnerInfoChangingCommand(userDto, null));

                if (result.IsSuccess)
                {
                    var query = new RefreshTokenQuery(userDto.Email);
                    var loginResult = await _mediator.Send(query);
                    if (loginResult.IsSuccess)
                        return Ok(loginResult.Value);
                }

            }
            catch (Exception ex)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists, " + ex.Message +
                                             "see your system administrator.");
            }


            return BadRequest(userDto);
        }

        [HttpPost("EditTutorInformation")]
        public async Task<IActionResult> EditTutorInformation(TutorMainInfoDto userDto, List<Guid> subjectIds,
            List<string> filePaths)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var query = new TutorInfoChangingCommand(userDto, subjectIds, filePaths);

                    var result = await _mediator.Send(query);

                    if (result.IsSuccess)
                    {
                        return Ok(result.Value);
                    }

                    return BadRequest(result.Errors);
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

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var query = _mapper.Map<ChangePasswordCommand>(changePasswordRequest);

                    var loginResult = await _mediator.Send(query);

                    if (loginResult.IsSuccess)
                    {
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists, " + ex.Message +
                                                 "see your system administrator.");
                }
            }

            return BadRequest(changePasswordRequest);
        }

        [Authorize(Policy = "RequireTutorRole")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> TeachingClassDetail(Guid requestId, Guid classId)
        {
            var query = new GetTeachingClassDetailQuery
            {
                ObjectId = requestId,
                ClassInformationId = classId
            };
            var classInformation = await _mediator.Send(query);
            if (classInformation.IsSuccess)
            {
                return Ok(classInformation.Value);
            }

            return NotFound(classInformation.Errors);
        }

        [HttpGet]
        [Route("GetLearningClass")]
        public async Task<IActionResult> GetLearningClass(Guid id)
        {
            var query = new GetObjectQuery<ClassInformationForDetailDto>()
            {
                ObjectId = id
            };
            var classInformation = await _mediator.Send(query);
            if (classInformation.IsSuccess)
            {
                return Ok(classInformation.Value);
            }

            return NotFound(classInformation.Errors);
        }
    }
}