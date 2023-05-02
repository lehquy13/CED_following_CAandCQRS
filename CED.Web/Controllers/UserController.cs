using CED.Application.Services.Users.Queries;
using CED.Application.Services.Users.Commands;
using CED.Contracts.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CED.Application.Services.Subjects.Commands;
using CED.Contracts.Interfaces.Services;
using CED.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using CED.Domain.Users;
using Newtonsoft.Json;
using CED.Web.Utilities;

namespace CED.Web.Controllers;

[Route("[controller]")]

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;

    public List<UserDto> _userDtos { get; set; } = new List<UserDto>();
  
    public UserController(ILogger<UserController> logger, ISender sender, IMapper mapper,IDateTimeProvider dateTimeProvider)
    {
        _logger = logger;
        _mediator = sender;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
        


    }

    private void PackStaticListToView()
    {
        ViewData["Roles"] = CEDEnumProvider.Roles;
        ViewData["Genders"] = CEDEnumProvider.Genders;
        ViewData["AcademicLevels"] = CEDEnumProvider.AcademicLevels;
    }

    #region basic user management

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var query = new GetUsersQuery<UserDto>();
        _userDtos = await _mediator.Send(query);

        return View(_userDtos);
    }

    [HttpGet("Edit")]
    public async Task<IActionResult> Edit(Guid Id)
    {
        PackStaticListToView();
        var query = new GetUserByIdQuery<UserDto>()
        {
            Id = Id
        };
        var result = await _mediator.Send(query);

        return View(result);
    }

    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid Id, UserDto userDto)
    {
        if (Id != userDto.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                var query = new CreateUserCommand()
                {
                    UserDto = userDto
                };
                var result = await _mediator.Send(query);
                ViewBag.Updated = true;
                //return await Edit(Id);
                return Json(new {res = true});

                
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

    [HttpGet("Create")]
    public IActionResult Create()
    {
        PackStaticListToView();
        return View(new UserDto());
    }


    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserDto userDto) // cant use userdto
    {
        userDto.LastModificationTime = _dateTimeProvider.UtcNow;
        var query = new CreateUserCommand() { UserDto = userDto };
        var result = await _mediator.Send(query);

        return View(result);
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var query = new GetUserByIdQuery<UserDto>() {Id= (Guid)id };
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return NotFound();
        }

        return Json(new
        {
            html = Helper.RenderRazorViewToString(this, "Delete", result)

        });
       // return View(result);
    }

    [HttpPost("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(Guid? id)
    {
        if (id == null || id.Equals(Guid.Empty))
        {
            return NotFound();
        }

        var query = new DeleteUserCommand((Guid)id);
        var result = await _mediator.Send(query);

        if (result is true)
        {
            return RedirectToAction("Index");

        }
        return RedirectToAction("Error", "Home");
    }

    [HttpGet("Detail")]
    public async Task<IActionResult> Detail(Guid? id) 
    {
        if (id == null || id.Equals(Guid.Empty))
        {
            return NotFound();
        }

        var query = new GetUserByIdQuery<UserDto>() { Id= (Guid)id };
        var result = await _mediator.Send(query);

        if (result is not null)
        {
            return View(result);

        }
        return RedirectToAction("Error", "Home");
    }
    #endregion

    [HttpGet("Student")]

    public async Task<IActionResult> Student()
    {
        var query = new GetUsersQuery<StudentDto>();
        var studentDtos = await _mediator.Send(query);
        var userDtos = _mapper.Map<List<UserDto>>(studentDtos);
        return View("Index", userDtos);
    }

    [HttpGet("Tutor")]
    public async Task<IActionResult> Tutor()
    {
        var query = new GetUsersQuery<TutorDto>();
        var tutorDtos = await _mediator.Send(query);
        var userDtos = _mapper.Map<List<UserDto>>(tutorDtos);
        return View(tutorDtos);
    }
}
