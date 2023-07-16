using Microsoft.AspNetCore.Mvc;

namespace CED.Web.Controllers;

/// <summary>
/// A navigate controller
/// </summary>
[Route("[controller]")]
public class RequestGettingClassController : Controller
{
    [Route("Detail")]
    public IActionResult Detail(string? id)
    {
        return RedirectToAction("Edit", "ClassInformation", new { id = id });
    }
}