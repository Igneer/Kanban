using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.ViewModels;
public class LoginController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;
    public LoginController(IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository, ITareaRepository tareaRepository)
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

        return RedirectToAction("Home", "Home");
    }

    [HttpGet]
    public IActionResult CerrarSesion()
    {
        Response.Cookies.Delete("AuthCookie");
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

}
