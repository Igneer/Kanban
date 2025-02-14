using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.ViewModels;


namespace Kanban.Controllers;

public class LoginController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;
    
    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult IrAIniciarSesion()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult IniciarSesion(LoginViewModel model)
    {
        var usuarios = _usuarioRepository.listarUsuarios();
        var usuario = usuarios.FirstOrDefault(u => u.NombreUsuario == model.Nombre && u.Password == model.Password);
        
        if (usuario == null)
        {
            ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos.");
            return View("IrAIniciarSesion");  
        } 

        HttpContext.Session.SetInt32("Id", usuario.Id);
        HttpContext.Session.SetString("Nombre", usuario.NombreUsuario);
        HttpContext.Session.SetString("Rol", usuario.Rol.ToString());

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult CerrarSesion()
    {
        Response.Cookies.Delete("AuthCookie");
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult Index()
    {

        return View();
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
