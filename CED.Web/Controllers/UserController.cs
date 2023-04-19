using CED.Application.Services.UsersInformations.Queries;
using CED.Application.Services.UsersInformations.Commands;
using CED.Contracts.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CED.Application.Services.Subjects.Commands;
using Microsoft.EntityFrameworkCore;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Web.Models;
using System.Diagnostics;
using CED.Application.Common.Services;

namespace CED.Web.Controllers
{
    [Route("[controller]")]

    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;

        public List<UserDto> _userDtos { get; set; } = new List<UserDto>();

        //static list
        private static List<string>? _roles, _academics, _genders;
        

        public UserController(ILogger<UserController> logger, ISender sender, IMapper mapper,IDateTimeProvider dateTimeProvider)
        {
            _logger = logger;
            _mediator = sender;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
            if(_roles == null || _roles.Count == 0 || _academics == null || _academics.Count == 0 || _genders == null || _genders.Count == 0)
            {
               SetUpStaticList();
            }
           
        }

        private void SetUpStaticList()
        {
            _roles = Enum.GetNames(typeof(UserRole)).AsEnumerable()
                                 .Where(x => x != "All" && x != "Undefined")
                                .ToList();

            _genders = Enum.GetNames(typeof(Gender)).AsEnumerable()
                                  .Where(x => x != "None")
                                  .ToList();
            _academics = Enum.GetNames(typeof(AcademicLevel)).AsEnumerable()
                                  .ToList();
        }

        private void PackStaticListToView()
        {
            ViewData["Roles"] = _roles;
            ViewData["Genders"] = _genders;
            ViewData["AcademicLevel"] = _academics;
        }


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
           
            var query = new GetUserByIdQuery<UserDto>()
            {
                Id = Id
            };
            var result = await _mediator.Send(query);

            return View(result);
        }

        [HttpPost("Edit")]
        //[ValidateAntiForgeryToken]
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
                    return await Edit(Id);

                    
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
            return View();
        }


        [HttpPost("Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDto userDto) // cant use userdto
        {
            userDto.CreationTime = _dateTimeProvider.UtcNow;
            var query = new CreateUserCommand() { UserDto = userDto };
            var result = await _mediator.Send(query);

            return View(result);
        }
    }
}
