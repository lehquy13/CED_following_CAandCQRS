using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.Authentication.Admin.Commands.ChangePassword;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.ClassInformations.Tutor.Queries;
using CED.Application.Services.Users.Admin.Commands;
using CED.Contracts.Authentication;
using CED.Contracts.ClassInformations.Dtos;
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

    private void PackStaticListToView()
    {
        ViewData["Roles"] = EnumProvider.Roles;
        ViewData["Genders"] = EnumProvider.Genders;
        ViewData["AcademicLevels"] = EnumProvider.AcademicLevels;

        //ViewData["Addresses"] = _addressService.GetAddresses();
    }

    [HttpGet("")]
    public async Task<IActionResult> Profile()
    {
        PackStaticListToView();

        var identity = HttpContext.User.Identities.First();

        var query = new GetObjectQuery<UserDto>()
        {
            Guid = new Guid(identity.Claims.FirstOrDefault()?.Value ?? "")
        };

        var loginResult = await _mediator.Send(query);
       

        if (loginResult != null )
        {
            var changePasswordRequest = _mapper.Map<ChangePasswordRequest>(loginResult);

            var viewModelResult = new ProfileViewModel
            {
                UserDto = loginResult,
                ChangePasswordRequest = changePasswordRequest
            };
            if (loginResult.Role == UserRole.Tutor)
            {
                var query1 = new GetTeachingClassInformationsOfTutorQuery()
                {
                    Guid = loginResult.Id
                };
                var loginResult1 = await _mediator.Send(query1);

                viewModelResult.ClassInformationDtos = loginResult1;
            }
            return View(viewModelResult);
        }

        return RedirectToAction("Login", "Authentication", new LoginRequest("", ""));
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
    public async Task<IActionResult> Edit(UserDto userDto, IFormFile? formFile)
    {
        
        PackStaticListToView();

        if (ModelState.IsValid)
        {
            try
            {
                bool result = false;
                var filePath = string.Empty;
                if (formFile != null)
                {
                    filePath = await Helper.SaveFiles(formFile, _webHostEnvironment.WebRootPath);
                }

                if (userDto.Role == UserRole.Tutor)
                {
                    result = await _mediator.Send(new CreateUpdateUserCommand(userDto,filePath));
                }
                else
                {
                    result = await _mediator.Send(new CreateUpdateUserCommand(userDto,filePath));

                }

                ViewBag.Updated = result;
                Helper.ClearTempFile(_webHostEnvironment.WebRootPath);
                var viewModelResult = new ProfileViewModel
                {
                    UserDto = userDto,
                    ChangePasswordRequest = _mapper.Map<ChangePasswordRequest>(userDto),
                    IsPartialLoad = true
                };
                if (result == true)
                {
                    HttpContext.Response.Cookies.Append("name", userDto.FirstName + userDto.LastName);
                    HttpContext.Response.Cookies.Append("image", userDto.Image);
                    var query1 = new GetTeachingClassInformationsOfTutorQuery()
                    {
                        Guid = userDto.Id
                    };
                    var loginResult1 = await _mediator.Send(query1);

                    viewModelResult.ClassInformationDtos = loginResult1;
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
    [Route("{id}")]
    public async Task<IActionResult> TeachingClassDetail(Guid id)
    {
        var query = new GetObjectQuery<Result<RequestGettingClassDto>>()
        {
            Guid = id
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

   
}