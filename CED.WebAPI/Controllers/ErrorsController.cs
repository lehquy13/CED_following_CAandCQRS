using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace CED.WebAPI.Controllers;

public class ErrorsController : ControllerBase
{
    [HttpGet("/error")]
    public IActionResult Error()
    {
        Exception? exception = 
            HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem( title:exception?.Message, statusCode: 400);
    }
}