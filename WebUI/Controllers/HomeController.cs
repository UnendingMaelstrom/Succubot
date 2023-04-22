using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IClientRunnerService _runner;

    public HomeController(IClientRunnerService runner)
    {
        _runner = runner;
    }
    // GET
    public IActionResult Index()
    { 
        _runner.RunAsync().GetAwaiter().GetResult();
        return View();
    }
}