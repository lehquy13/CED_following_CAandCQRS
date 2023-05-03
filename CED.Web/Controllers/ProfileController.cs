﻿using CED.Application.Services.Authentication.Commands.ChangePassword;
using CED.Application.Services.Subjects.Commands;
using CED.Application.Services.Users.Queries;
using CED.Contracts.Authentication;
using CED.Contracts.Users;
using CED.Domain.Shared;
using CED.Web.Utilities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CED.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ProfileController : Controller
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        private readonly ILogger<AuthenticationController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileController(ISender mediator, IMapper mapper, ILogger<AuthenticationController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        private void PackStaticListToView()
        {
            ViewData["Roles"] = CEDEnumProvider.Roles;
            ViewData["Genders"] = CEDEnumProvider.Genders;
            ViewData["AcademicLevels"] = CEDEnumProvider.AcademicLevels;
        }
        [HttpGet("")]
        public async Task<IActionResult> Profile()
        {
            PackStaticListToView();
            string validateToken = HttpContext.Request.Cookies["access_token"] ?? "";
            if (HttpContext.User.Identity is null || HttpContext.User.Identity.IsAuthenticated is false)
            {
                return RedirectToAction("Login", "Authentication", new LoginRequest("", ""));
            }
            var identity = HttpContext.User.Identities.FirstOrDefault();

            if (identity == null) { return View("Login", new LoginRequest("", "")); }

            var query = new GetUserByIdQuery<UserDto>()
            {
                Id = new Guid(identity.Claims.FirstOrDefault()?.Value ?? "")
            };

            var loginResult = await _mediator.Send(query);

            if (loginResult is not null)
            {
                return View(loginResult);
            }

            return RedirectToAction("Login", "Authentication", new LoginRequest("", ""));

        }

        [HttpPost("ChoosePicture")]
        public async Task<IActionResult> ChoosePicture(IFormFile formFile)
        {
            if (formFile == null)
            {
                return Json(false);
            }
            var image = await Helper.SaveFiles(formFile, _webHostEnvironment.WebRootPath);

            return Json(new { res = true, image = "avatar\\" + image });

        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid Id, UserDto userDto, IFormFile formFile)
        {
            if (Id != userDto.Id)
            {
                return NotFound();
            }

            if (formFile != null)
            {
                userDto.Image = await Helper.SaveFiles(formFile, _webHostEnvironment.WebRootPath);
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var query = new CreateUserCommand()
                    {
                        UserDto = userDto
                    };
                    //var query = new UserInfoChangingCommand(
                    //    userDto
                    //);
                    var result = await _mediator.Send(query);
                    ViewBag.Updated = result;

                    return Json(new
                    {
                        res = true,
                        viewName = "Profile",
                        partialView = Helper.RenderRazorViewToString(this, "Profile", userDto)
                    });



                }
                catch (Exception ex)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " + ex.Message +
                        "see your system administrator.");
                }
            }
            return View(userDto);
        }

        [HttpPost("ChangePassword")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            if (changePasswordRequest.ConfirmedPassword != changePasswordRequest.NewPassword)
            {
                return View("Profile", ModelState);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var query = _mapper.Map<ChangePasswordCommand>(changePasswordRequest);

                    var loginResult = await _mediator.Send(query);

                    if (loginResult.IsSuccess)
                    {
                        ViewBag.Updated = true;

                        return Json(true);
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
            return View("Profile", ModelState);

        }
    }
}
