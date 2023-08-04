using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CED.Application.Services.Users.Admin.Commands;
using CED.Application.Services.Users.Queries.CustomerQueries;
using CED.Contracts;
using CED.Contracts.Subjects;
using CED.Domain.Shared;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Web.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CED.Web.Controllers;

[Route("[controller]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    private readonly ISender _mediator;
    private readonly IMapper _mapper;


    public UserController(ILogger<UserController> logger, ISender sender, IMapper mapper)
    {
        _logger = logger;
        _mediator = sender;
        _mapper = mapper;
    }

    private void PackStaticListToView()
    {
        ViewData["Roles"] = EnumProvider.Roles;
        ViewData["Genders"] = EnumProvider.Genders;
        ViewData["AcademicLevels"] = EnumProvider.AcademicLevels;
    }

    #region basic user management

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var query = new GetObjectQuery<PaginatedList<UserDto>>()
        {
            PageSize = 200
        };
        var userDtos = await _mediator.Send(query);

        return View(userDtos);
    }

    [HttpGet("Edit")]
    public async Task<IActionResult> Edit(Guid Id)
    {
        PackStaticListToView();
        var query = new GetObjectQuery<UserDto>()
        {
            ObjectId = Id
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
                var query = new CreateUpdateUserCommand(userDto,"");


                var result = await _mediator.Send(query);

                if (!result)
                {
                    _logger.LogError("Create user failed!");
                    throw new DbUpdateException("Create fail!");
                }

                PackStaticListToView();

                return Helper.RenderRazorViewToString(
                    this,
                    "Edit",
                    userDto
                );
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
        userDto.LastModificationTime = DateTime.UtcNow;
        var command = new CreateUpdateUserCommand(userDto,"");


        var result = await _mediator.Send(command);
        if (result)
        {
            return RedirectToAction("Index");
        }

        return View("Create", userDto);
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var query = new GetObjectQuery<UserDto>() { ObjectId = (Guid)id };
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

        if (result)
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

        var query = new GetObjectQuery<UserDto>() { ObjectId = (Guid)id };
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
        var query = new GetObjectQuery<PaginatedList<LearnerDto>>
        {
            PageSize = 200
        };
        var studentDtos = await _mediator.Send(query);

        return View(studentDtos);
    }

  
   
}