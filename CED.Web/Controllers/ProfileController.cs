using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.Authentication.Admin.Commands.ChangePassword;
using CED.Application.Services.Users.Admin.Commands;
using CED.Contracts.Authentication;
using CED.Contracts.Users;
using CED.Domain.Shared;
using CED.Web.Models;
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

            if (loginResult is not null)
            {
                var changePasswordRequest = _mapper.Map<ChangePasswordRequest>(loginResult);
                return View(new ProfileViewModel
                {
                    UserDto = loginResult,
                    ChangePasswordRequest = changePasswordRequest
                });
            }

            return RedirectToAction("Index", "Authentication", new LoginRequest("", ""));
        }

        [HttpPost("ChoosePicture")]
        public async Task<IActionResult> ChoosePicture(IFormFile? formFile)
        {
            if (formFile == null)
            {
                return Json(false);
            }

            var image = await Helper.SaveFiles(formFile, _webHostEnvironment.WebRootPath);
           
            return Json(new { res = true, image = "temp\\" +  Path.GetFileName(image) });
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
                    var filePath = string.Empty;
                    if (formFile != null)
                    {
                        filePath = await Helper.SaveFiles(formFile, _webHostEnvironment.WebRootPath);
                    }
                    var query = new CreateUpdateUserCommand(userDto,filePath);

                    var result = await _mediator.Send(query);
                    ViewBag.Updated = result;
                    Helper.ClearTempFile(_webHostEnvironment.WebRootPath);

                    if (result == true)
                    {
                        HttpContext.Response.Cookies.Append("name", query.UserDto.FirstName + query.UserDto.LastName);
                        HttpContext.Response.Cookies.Append("image", query.UserDto.Image);
                    }
                    return Helper.RenderRazorViewToString(this, "Profile", new ProfileViewModel
                    {
                        UserDto = userDto,
                        ChangePasswordRequest = _mapper.Map<ChangePasswordRequest>(userDto)
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
                        return Helper.RenderRazorViewToString(this, "_ChangePassword", new ChangePasswordRequest{Id = changePasswordRequest.Id});
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

            return Helper.RenderRazorViewToString(this, "_ChangePassword", changePasswordRequest,true);

        }
    }
}