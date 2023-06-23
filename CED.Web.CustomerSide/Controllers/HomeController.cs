using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CED.Web.CustomerSide.Models;

namespace CED.Web.CustomerSide.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    } 
    public IActionResult Contact()
    {
        return View();
    }
    public IActionResult SuccessPage(string? message)
    {
        return View(message?? "We have received your request.");
    } 
    public IActionResult FailPage()
    {
        return View();
    }
    public IActionResult SuccessRequestPage()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}