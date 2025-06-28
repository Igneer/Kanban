using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.ViewModels;
public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;
    public LoginController(IUsuarioRepository usuarioRepository, ILogger<LoginController> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult IrAIniciarSesion()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult IniciarSesion(LoginViewModel model)
    {
        if(!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Por favor, revisa los campos ingresados. Algunos datos no son válidos.";
            return RedirectToAction("Index", "Error"); 
        }

        try
        {
            var usuarios = _usuarioRepository.listarUsuarios();
            var usuario = usuarios.FirstOrDefault(u => u.NombreUsuario == model.Nombre && u.Password == model.Password);
            
            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos.");
                _logger.LogError("Intento de acceso invalido - Usuario:"+model.Nombre+" Clave ingresada: "+model.Password);
                return View("IrAIniciarSesion");  
            } 

            HttpContext.Session.SetInt32("Id",usuario.Id);
            HttpContext.Session.SetString("Nombre",usuario.NombreUsuario);
            HttpContext.Session.SetString("Rol",usuario.Rol.ToString());

            _logger.LogInformation("El usuario "+usuario.NombreUsuario+" ingreso correctamente");

            return RedirectToAction("Index", "Home");
        }catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ExceptionMessage"] = ex.Message;
            return RedirectToAction("Index", "Error");
        }
    }

    [HttpGet]
    public IActionResult CerrarSesion()
    {
        Response.Cookies.Delete("AuthCookie");
        HttpContext.Session.Clear();
        return RedirectToAction("IrAIniciarSesion");
    }

}
