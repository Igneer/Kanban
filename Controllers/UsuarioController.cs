using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.ViewModels;
using Microsoft.Data.Sqlite;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioController(IUsuarioRepository usuarioRepository, ILogger<UsuarioController> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
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
            TempData["ErrorMessage"] = "Por favor, revisa los campos ingresados. Algunos datos no son válidos.";
            return RedirectToAction("Index", "Error"); 
        }

        try
        {
            Usuario usuario = new Usuario(){
                NombreUsuario = model.Nombre,
                Password = model.Password,
                Rol = model.Rol
            };

            _usuarioRepository.crearUsuario(usuario);
            _logger.LogInformation("Usuario creado con éxito");
            return RedirectToAction("Index", "Home");
        }catch(SqliteException ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "Ocurrió un error en la base de datos. Por favor, intentelo nuevamente";
            return RedirectToAction("Index", "Error");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "Ocurrio un error inesperado. Por favor, intentelo de nuevo";
            return RedirectToAction("Index", "Error");
        }
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
            TempData["ErrorMessage"] = "Por favor, revisa los campos ingresados. Algunos datos no son válidos.";
            return RedirectToAction("Index", "Error"); 
        }

        try
        {
            Usuario usuario = new Usuario()
            {
                Id = model.Id,
                NombreUsuario = model.NombreUsuario,
                Rol = model.Rol
            };

            _usuarioRepository.modificarUsuario(model.Id, usuario);
            _logger.LogInformation("Usuario modificado con éxito");
            return RedirectToAction("Index", "Home");
        }catch(SqliteException ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "Ocurrió un error en la base de datos. Por favor, intentelo nuevamente";
            return RedirectToAction("Index", "Error");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "Ocurrio un error inesperado. Por favor, intentelo de nuevo";
            return RedirectToAction("Index", "Error");
        }

    }

    [HttpGet]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    [ServiceFilter(typeof(AdministradorAuthorizeUserFilter))]

    public IActionResult listarUsuarios()
    {
        try
        {
            ListarUsuariosViewModel listadoUsuarios  = new ListarUsuariosViewModel()
            {
                Usuarios = _usuarioRepository.listarUsuarios()
            };
            _logger.LogInformation("Listado de usuarios obtenidos exitosamente");
            return View(listadoUsuarios);
        }catch(SqliteException ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "Ocurrió un error en la base de datos. Por favor, intentelo nuevamente";
            return RedirectToAction("Index", "Error");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "Ocurrio un error inesperado. Por favor, intentelo de nuevo";
            return RedirectToAction("Index", "Error");
        }
    }

    [HttpPost]
    [ServiceFilter(typeof(AdministradorAuthorizeUserFilter))]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult eliminarUsuario(int id)
    {
        try
        {
            _usuarioRepository.eliminarUsuario(id);
            _logger.LogInformation("Usuario eliminado exitosamente");
            return RedirectToAction("Index", "Home");
        }catch(SqliteException ex) when(ex.SqliteErrorCode == 19)
        {
            _logger.LogError(ex.ToString());

            TempData["ErrorMessage"] = "No se puede eliminar el usuario porque posee tableros. Asegurese de eliminar los tableros primero.";

            return RedirectToAction("Index", "Error");
        }catch(SqliteException ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "Ocurrió un error en la base de datos. Por favor, intentelo nuevamente";
            return RedirectToAction("Index", "Error");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "Ocurrio un error inesperado. Por favor, intentelo de nuevo";
            return RedirectToAction("Index", "Error");
        }
    }

}