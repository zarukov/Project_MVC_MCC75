using Microsoft.AspNetCore.Mvc;
using Project_MVC_MCC75.Models;
using Project_MVC_MCC75.Repositories;
using Project_MVC_MCC75.ViewModels;

namespace Project_MVC_MCC75.Controllers;

public class ErrorController : Controller
{

    [Route("Unauthorized")]
    public IActionResult Unauthorized()
    {
        return View();
    }

    [Route("Forbidden")]
    public IActionResult Forbidden()
    {
        return View();
    }
}
