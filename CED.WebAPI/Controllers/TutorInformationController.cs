using System.Globalization;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.TutorReviews.Commands;
using CED.Application.Services.Users.Queries.CustomerQueries;
using CED.Application.Services.Users.Tutor.Registers;
using CED.Contracts.TutorReview;
using CED.Contracts.Users.Tutors;
using CED.Domain.Shared.ClassInformationConsts;
using CED.WebAPI.Models;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CED.WebAPI.Controllers;

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
    public async Task<IActionResult> GetAllTutors([FromQuery] TutorParams tutorParams)
    {
        var query = new GetAllTutorInformationsAdvancedQuery()
        {
            PageIndex = 1,
            PageSize = _pageSize
        };
        if (tutorParams != null)
        {
            query.PageIndex = tutorParams.PageIndex;
            query.SubjectName = tutorParams.SubjectName ?? "";
            query.Gender = (Gender)Enum.Parse(typeof(Gender), !string.IsNullOrEmpty(tutorParams.Gender)
                ? tutorParams.Gender : "None" , ignoreCase:
            true);
            query.BirthYear = tutorParams.BirthYear ?? 0;
            query.Academic = (AcademicLevel)Enum.Parse(typeof(AcademicLevel),
                (!string.IsNullOrWhiteSpace(tutorParams.AcademicLevel)) ? tutorParams.AcademicLevel : "Optional", ignoreCase: true);
            query.Address = tutorParams.Address ?? "";
        }


        var tutorDtos = await _mediator.Send(query);
        if (tutorDtos.IsSuccess)
            return Ok(tutorDtos);
        return BadRequest(tutorDtos.Errors);
    }


    // GET api/<TutorInformationController>/5
    [HttpGet]
    [Route("GetTutor/{id}")]
    public async Task<IActionResult> GetTutor(Guid id)
    {
        var query = new GetObjectQuery<TutorForDetailDto>()
        {
            ObjectId = id
        };
        var tutorDto = await _mediator.Send(query);
        if (tutorDto.IsSuccess)
        {
            var result = _mapper.Map<TutorForDetailDto1>(tutorDto.Value);
            return Ok(result);
        }

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
            return CreatedAtRoute("Profile", new { controller = "Profile", Iid = tutorForRegistrationDto.Id },
                tutorForRegistrationDto);
        }

        return BadRequest(result.Errors);
    }


    [Authorize]
    [HttpPost]
    [Route("ReviewTutor/{id}")]
    public async Task<IActionResult> ReviewTutor(string id,[FromBody]TutorReviewRequestDto tutorDto)
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
            LearnerEmail = id,
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