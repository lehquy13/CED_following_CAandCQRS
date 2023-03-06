using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CED.WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class SubjectController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllSubjects()
    {
        return Ok(Array.Empty<string>());
    }
}

