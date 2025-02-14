using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.ViewModels;

public class UsuarioController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    [ServiceFilter(typeof(AdministradorAuthorizeUserFilter))]
    public IActionResult IrACrearUsuario()
    {
        return View();
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    [ServiceFilter(typeof(AdministradorAuthorizeUserFilter))]
    public IActionResult crearUsuario(CrearUsuarioViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View("IrACrearUsuario");
        }

        Usuario usuario = new Usuario(){
            NombreUsuario = model.Nombre,
            Password = model.Password,
            Rol = model.Rol
        };

        _usuarioRepository.crearUsuario(usuario);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    [ServiceFilter(typeof(AdministradorAuthorizeUserFilter))]
    public IActionResult IrAModificarUsuario(int id)
    {
        Usuario usuario = new Usuario();
        usuario = _usuarioRepository.obtenerUsuario(id);

        var model = new ModificarUsuarioViewModel()
        {
            Id = usuario.Id,
            NombreUsuario = usuario.NombreUsuario,
            Password = usuario.Password,
            Rol = usuario.Rol
        };

        return View(model);
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    [ServiceFilter(typeof(AdministradorAuthorizeUserFilter))]
    public IActionResult modificarUsuario(ModificarUsuarioViewModel model)
    {
        if(!ModelState.IsValid)
        {
            return View("IrAModificarUsuario");
        }

        Usuario usuario = new Usuario()
        {
            Id = model.Id,
            NombreUsuario = model.NombreUsuario,
            Password = model.Password,
            Rol = model.Rol
        };

        _usuarioRepository.modificarUsuario(model.Id, usuario);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult listarUsuarios()
    {
        ListarUsuariosViewModel listadoUsuarios  = new ListarUsuariosViewModel()
        {
            Usuarios = _usuarioRepository.listarUsuarios()
        };

        return View(listadoUsuarios);
    }

    [HttpPost]
    [ServiceFilter(typeof(AdministradorAuthorizeUserFilter))]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult eliminarUsuario(int id)
    {
        _usuarioRepository.eliminarUsuario(id);

        return RedirectToAction("Index", "Login");
    }

}