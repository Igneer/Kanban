using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.ViewModels;
public class ErrorController : Controller
{
    [HttpGet]

    public IActionResult Index()
    {
        string? errorMessage = TempData["ErrorMessage"]?.ToString();
        
        ErrorViewModel model = new ErrorViewModel()
        {
            Message = errorMessage  
        };

        return View(model);
    }    

}
