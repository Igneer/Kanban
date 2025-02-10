using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.ViewModels;

public class UsuarioController : Controller
{
    IUsuarioRepository _usuarioRepository = new UsuarioRepository();
    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult IrACrearUsuario()
    {
        return View();
    }

    [HttpPost]
    public IActionResult crearUsuario(CrearUsuarioViewModel model)
    {
        Usuario usuario = new Usuario(){
            NombreUsuario = model.Nombre,
            Password = model.Password,
            Rol = model.Rol
        };

        _usuarioRepository.crearUsuario(usuario);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
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

    public IActionResult modificarUsuario(ModificarUsuarioViewModel model)
    {
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
    public IActionResult listarUsuarios()
    {
        ListarUsuariosViewModel listadoUsuarios  = new ListarUsuariosViewModel()
        {
            Usuarios = _usuarioRepository.listarUsuarios()
        };

        return View(listadoUsuarios);
    }

    [HttpPost]
    public IActionResult eliminarUsuario(int id)
    {
        _usuarioRepository.eliminarUsuario(id);

        return RedirectToAction("Index", "Login");
    }

}