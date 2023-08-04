using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.Authentication.Admin.Commands.ChangePassword;
using CED.Application.Services.ClassInformations.Tutor.Queries;
using CED.Application.Services.ClassInformations.Tutor.Queries.GetAllRequestGettingClassOfTutor;
using CED.Application.Services.ClassInformations.Tutor.Queries.GetTeachingClassDetailQuery;
using CED.Application.Services.Users.Admin.Commands;
using CED.Application.Services.Users.Queries;
using CED.Application.Services.Users.Queries.GetTutorProfile;
using CED.Application.Services.Users.Student.Commands;
using CED.Application.Services.Users.Tutor.ChangeInfo;
using CED.Contracts;
using CED.Contracts.Authentication;
using CED.Contracts.ClassInformations.Dtos;
using CED.Contracts.Subjects;
using CED.Contracts.Users;
using CED.Domain.Shared;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Web.CustomerSide.Models;
using CED.Web.CustomerSide.Utilities;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CED.Web.CustomerSide.Controllers;

[Authorize]
[Route("[controller]")]
public class ProfileController : Controller
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<ProfileController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProfileController(ISender mediator, IMapper mapper, ILogger<ProfileController> logger,
        IWebHostEnvironment webHostEnvironment)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    private async Task PackStaticListToView()
    {
        ViewData["Roles"] = EnumProvider.Roles;
        ViewData["Genders"] = EnumProvider.Genders;
        ViewData["AcademicLevels"] = EnumProvider.AcademicLevels;
        ViewData["Subjects"] = await _mediator.Send(new GetObjectQuery<PaginatedList<SubjectDto>>());

    }

    [HttpGet("")]
    public async Task<IActionResult> Profile()
    {
        try
        {
            await PackStaticListToView();

            var identity = HttpContext.User.Identities.First();

            var query = new GetObjectQuery<LearnerDto>()
            {
                ObjectId = new Guid(identity.Claims.FirstOrDefault()?.Value ?? "")
            };

            var loginResult = await _mediator.Send(query);


            if (loginResult != null)
            {
                var changePasswordRequest = _mapper.Map<ChangePasswordRequest>(loginResult);

                var viewModelResult = new ProfileViewModel
                {
                    UserDto = loginResult,
                    ChangePasswordRequest = changePasswordRequest
                };
                if (loginResult.Role == UserRole.Tutor)
                {

                    var query1 = new GetTutorProfileQuery()
                    {
                        ObjectId = loginResult.Id
                    };
                    var loginResult1 = await _mediator.Send(query1);

                    viewModelResult.RequestGettingClassDtos = loginResult1.RequestGettingClassDtos;
                    viewModelResult.TutorDto = loginResult1.TutorMainInfoDto;
                }

                return View(viewModelResult);
            }

            return RedirectToAction("Login", "Authentication", new LoginRequest("", ""));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return View("Error");

        }
    }

    [HttpPost("ChooseProfilePictures")]
    public async Task<IActionResult> ChooseProfilePictures(List<IFormFile?> formFiles)
    {
        if (formFiles.Count <= 0)
        {
            return Json(false);
        }

        var vals = new List<string>();
        foreach (var i in formFiles)
        {
            var image = await Helper.SaveFiles(i, _webHostEnvironment.WebRootPath);
            vals.Add("\\temp\\"+ Path.GetFileName(image));
        }

        return Json(new { res = true, images = vals });
    }
    [HttpPost("ChoosePicture")]
    public async Task<IActionResult> ChoosePicture(IFormFile? formFile)
    {
        if (formFile == null)
        {
            return Json(false);
        }

        var image = await Helper.SaveFiles(formFile, _webHostEnvironment.WebRootPath);

        return Json(new { res = true, image = "temp\\"+ Path.GetFileName(image) });
    }

    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(LearnerDto userDto, IFormFile? formFile)
    {
        
        await PackStaticListToView();

        if (ModelState.IsValid)
        {
            try
            {
                
                var filePath = string.Empty;
                if (formFile != null)
                {
                    filePath = await Helper.SaveFiles(formFile, _webHostEnvironment.WebRootPath);
                }

               
                bool result = await _mediator.Send(new LearnerInfoChangingCommand(userDto,filePath));

                ViewBag.Updated = result;
                Helper.ClearTempFile(_webHostEnvironment.WebRootPath);
                var viewModelResult = new ProfileViewModel
                {
                    UserDto = userDto,
                    ChangePasswordRequest = _mapper.Map<ChangePasswordRequest>(userDto),
                    IsPartialLoad = true
                };
                if (result)
                {
                    HttpContext.Response.Cookies.Append("name", userDto.FirstName + userDto.LastName);
                    HttpContext.Response.Cookies.Append("image", userDto.Image);
                    var query1 = new GetAllRequestGettingClassOfTutorQuery()
                    {
                        ObjectId = userDto.Id
                    };
                    var loginResult1 = await _mediator.Send(query1);

                    viewModelResult.RequestGettingClassDtos = loginResult1.Value;
                }
              
                
                return Helper.RenderRazorViewToString(this, "Profile", viewModelResult);
            }
            catch (Exception ex)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists, " + ex.Message +
                                             "see your system administrator.");
            }
        }

        return Helper.RenderRazorViewToString(this, "_ProfileEdit",
            userDto,
            true
        );
    }

    [HttpPost("EditTutorInformation")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditTutorInformation(TutorMainInfoDto userDto, List<IFormFile?>? files,List<Guid> subjectId)
    {
        
        await PackStaticListToView();

        if (ModelState.IsValid)
        {
            try
            {
                bool result;
                var filePath = new List<string>();
                if (files != null)
                {
                    foreach (var i in files)
                    {
                        if(i is not null)
                            filePath.Add(await Helper.SaveFiles(i, _webHostEnvironment.WebRootPath));
                    }
                }

                if (userDto.Role == UserRole.Tutor)
                {
                    result = await _mediator.Send(new TutorInfoChangingCommand(userDto,subjectId,filePath));
                }
                else
                {
                    throw new Exception("User has not registered as Tutor.");

                }

                ViewBag.Updated = result;
                Helper.ClearTempFile(_webHostEnvironment.WebRootPath);
               
                return Helper.RenderRazorViewToString(this, "_ProfileTutorEdit", userDto);

              
                
            }
            catch (Exception ex)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists, " + ex.Message +
                                             "see your system administrator.");
            }
        }

        return Helper.RenderRazorViewToString(this, "_ProfileEdit",
            userDto,
            true
        );
    }
    [HttpPost("ChangePassword")]
    //[ValidateAntiForgeryToken]
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
                    return Helper.RenderRazorViewToString(this, "_ChangePassword",
                        new ChangePasswordRequest { Id = changePasswordRequest.Id });
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

        return Helper.RenderRazorViewToString(this, "_ChangePassword", changePasswordRequest, true);
    }
    [HttpGet]
    [Route("{ObjectId}")]
    public async Task<IActionResult> TeachingClassDetail(Guid requestId , Guid classId)
    {
        var query = new GetTeachingClassDetailQuery
        {
            ObjectId = requestId,
            ClassInformationId = classId
        };
        var classInformation = await _mediator.Send(query);
        if (classInformation.IsSuccess)
        {
            return Helper.RenderRazorViewToString(this, "_TeachingClassDetail", classInformation.Value);
        }

        return View("Error", new ErrorViewModel()
        {
            RequestId = classInformation.Reasons.First().Message
        });

    }
    [HttpGet]
    [Route("GetLearningClass")]
    public async Task<IActionResult> GetLearningClass(Guid id)
    {
        var query = new GetObjectQuery<ClassInformationDto>()
        {
            ObjectId = id
        };
        var classInformation = await _mediator.Send(query);
        if (classInformation != null)
        {
            return Helper.RenderRazorViewToString(this, "_LearningClassDetail", classInformation);
        }

        return View("Error", new ErrorViewModel()
        {
            
        });

    }

    
}