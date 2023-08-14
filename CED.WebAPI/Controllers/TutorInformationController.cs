
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.TutorReviews.Commands;
using CED.Application.Services.Users.Queries.CustomerQueries;
using CED.Application.Services.Users.Tutor.Registers;
using CED.Contracts.TutorReview;
using CED.Contracts.Users.Tutors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CED.WebAPI.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TutorInformationController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly int _pageSize = 10;

    public TutorInformationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    // Query
    // GET: api/<TutorInformationController>
    [HttpGet]
    [Route("GetAllTutors")]
    public async Task<IActionResult> GetAllTutors(int pageIndex, string subjectName )
    {
        var query = new GetAllTutorInformationsAdvancedQuery()
        {
            PageIndex = pageIndex,
            PageSize = _pageSize
        };
        var tutorDtos = await _mediator.Send(query);
        if(tutorDtos.IsSuccess)
            return Ok(tutorDtos);
        return BadRequest(tutorDtos.Errors);
    }
    
    // GET api/<TutorInformationController>/5
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Detail(Guid id)
    {
        var query = new GetObjectQuery<TutorForDetailDto>()
        {
            ObjectId = id
        };
        var tutorDto = await _mediator.Send(query);
        if(tutorDto.IsSuccess)
            return Ok(tutorDto.Value);
        return BadRequest(tutorDto.Errors);
    }
    
    // POST api/<TutorInformationController>/TutorRegistration
    [Authorize]
    [HttpPost]
    [Route("TutorRegistration")]
    public async Task<IActionResult> TutorRegistration(TutorForRegistrationDto tutorForRegistrationDto)
    {
        var command = new TutorRegistrationCommand(tutorForRegistrationDto);

        var result = await _mediator.Send(command);
        
        if (result.IsSuccess)
        {
            return CreatedAtRoute("Profile", new { controller = "Profile", Iid = tutorForRegistrationDto.Id }, tutorForRegistrationDto);
        }
        return BadRequest(result.Errors);
    }
    

    [Authorize]
    [HttpPost]
    [Route("ReviewTutor")]
    public async Task<IActionResult> ReviewTutor(TutorReviewRequestDto tutorDto)
    {
        var command = new CreateReviewCommand
        {
            ReviewDto = new TutorReviewDto()
            {
                Rate = tutorDto.Rate,
                Description = tutorDto.Description,
                Id = tutorDto.Id,
                ClassInformationId = new Guid(tutorDto.ClassId)
            },
            LearnerEmail = HttpContext.Session.GetString("email") ?? "",
            TutorEmail = tutorDto.TutorEmail,
        };

        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return NoContent(); //implement
        }
        return BadRequest(result.Errors);
    }

   
}
