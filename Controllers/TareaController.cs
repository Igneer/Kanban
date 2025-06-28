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
    [ServiceFilter(typeof(AccesoATableroFilter))]
    public IActionResult IrACrearTarea(int id)
    {
        CrearTareaViewModel model = new CrearTareaViewModel()
        {
            IdTablero = id
        };

        return View(model);
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    [ServiceFilter(typeof(AccesoATableroFilter))]

    public IActionResult crearTarea(CrearTareaViewModel crearTareaViewModel)
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
                Nombre = crearTareaViewModel.Nombre,
                Descripcion = crearTareaViewModel.Descripcion,
                Estado = (EstadoTarea)crearTareaViewModel.Estado,
                IdTablero = crearTareaViewModel.IdTablero,
                Color = "rojo",
                IdUsuarioAsignado = null,
            };

            _tareaRepository.crearTarea(tarea, crearTareaViewModel.IdTablero);

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

    [HttpGet("Tarea/IrAModificarTarea/{idTarea}")]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    [ServiceFilter(typeof(AccesoATareaFilter))]
    public IActionResult IrAModificarTarea(int idTarea)
    {
        Tarea tarea = _tareaRepository.obtenerTarea(idTarea);

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
    [ServiceFilter(typeof(AccesoATareaFilter))]

    public IActionResult modificarTarea(ModificarTareaViewModel modificarTareaViewModel)
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
                Id = modificarTareaViewModel.Id,
                Nombre = modificarTareaViewModel.Nombre,
                Descripcion = modificarTareaViewModel.Descripcion
            };

            _tareaRepository.modificarTarea(modificarTareaViewModel.Id, tarea);

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
    [ServiceFilter(typeof(AccesoATareaFilter))]

    public IActionResult eliminarTarea(int idTarea)
    {
        try
        {
            _tareaRepository.eliminarTarea(idTarea);

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
    [ServiceFilter(typeof(CambiarEstadoTareaFilter))]

    public IActionResult cambiarEstadoTarea(int idTarea, int estado, int direccion)
    {
        try
        {
            _tareaRepository.cambiarEstadoTarea(idTarea, estado, direccion);

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
    [ServiceFilter(typeof(AccesoATareaFilter))]
    public IActionResult IrAsignarTareas(int idTarea)
    {
        AsignarTareasViewModel model = new AsignarTareasViewModel()
        {
            Tarea = _fachadaTarea.obtenerTarea(idTarea),
            Usuarios = _fachadaTarea.obtenerUsuarios()
        };

        return View(model);
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    [ServiceFilter(typeof(AccesoATareaFilter))]
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