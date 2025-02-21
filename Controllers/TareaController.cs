using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.ViewModels;
using Microsoft.Data.Sqlite;

public class TareaController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private readonly ITareaRepository _tareaRepository;
    private readonly IFachadaTarea _fachadaTarea;
    public TareaController(ITareaRepository tareaRepository, IFachadaTarea fachadaTarea, ILogger<TableroController> logger)
    {
        _tareaRepository = tareaRepository;
        _fachadaTarea = fachadaTarea;
        _logger = logger;
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult IrACrearTarea(int idTablero)
    {
        CrearTareaViewModel model = new CrearTareaViewModel()
        {
            IdTablero = idTablero
        };

        return View(model);
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult crearTarea(CrearTareaViewModel model)
    {
        if(!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Por favor, revisa los campos ingresados. Algunos datos no son válidos.";
            return RedirectToAction("Index", "Error"); 
        }

        try
        {
            Tarea tarea = new Tarea
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Estado = (EstadoTarea)model.Estado,
                IdTablero = model.IdTablero,
                Color = "rojo",
                IdUsuarioAsignado = null,
            };

            _tareaRepository.crearTarea(tarea, model.IdTablero);

            _logger.LogInformation("Tarea creada con éxito");

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
    public IActionResult IrAModificarTarea(int id)
    {
        Tarea tarea = _tareaRepository.obtenerTarea(id);

        ModificarTareaViewModel model = new ModificarTareaViewModel()
        {
            Id = tarea.Id,
            Nombre = tarea.Nombre,
            Descripcion = tarea.Descripcion
        };

        return View(model);
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult modificarTarea(ModificarTareaViewModel model)
    {
        if(!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Por favor, revisa los campos ingresados. Algunos datos no son válidos.";
            return RedirectToAction("Index", "Error"); 
        }
        
        try
        {
            Tarea tarea = new Tarea()
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion
            };

            _tareaRepository.modificarTarea(model.Id, tarea);

            _logger.LogInformation("Tarea modificada con éxito");

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

    [HttpPost]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult eliminarTarea(int id)
    {
        try
        {
            _tareaRepository.eliminarTarea(id);

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

    [HttpPost]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult cambiarEstadoTarea(int id, int estado, int direccion)
    {
        try
        {
            _tareaRepository.cambiarEstadoTarea(id, estado, direccion);

            _logger.LogInformation("Cambio de estado de tarea exitoso");

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
    public IActionResult IrAsignarTareas(int idTablero)
    {
        AsignarTareasViewModel model = new AsignarTareasViewModel()
        {
            Tareas = _fachadaTarea.obtenerTareasXTablero(idTablero),
            Usuarios = _fachadaTarea.obtenerUsuarios()
        };

        return View(model);
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult asignarTarea(int idUsuario, int idTarea)
    {
        try
        {
            _fachadaTarea.asignarTarea(idUsuario, idTarea);
            _logger.LogInformation("Tarea asignada con éxito");
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
}