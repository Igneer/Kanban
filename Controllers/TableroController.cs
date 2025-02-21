using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.ViewModels;
using Microsoft.Data.Sqlite;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private readonly ITableroRepository _tableroRepository;
    public TableroController(ITableroRepository tableroRepository, ILogger<TableroController> logger)
    {
        _tableroRepository = tableroRepository;
        _logger = logger;
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult IrACrearTablero()
    {
        int IdUsuarioPropietario = HttpContext.Session.GetInt32("Id").Value;

        var model = new CrearTableroViewModel()
        {
            IdUsuarioPropietario = IdUsuarioPropietario
        };

        return View(model);
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult crearTablero(CrearTableroViewModel crearTableroViewModel)
    {
        if(!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Por favor, revisa los campos ingresados. Algunos datos no son válidos.";
            return RedirectToAction("Index", "Error"); 
        }

        try
        {
            Tablero tablero = new Tablero()
            {
                IdUsuarioPropietario = crearTableroViewModel.IdUsuarioPropietario,
                Nombre = crearTableroViewModel.Nombre,
                Descripcion = crearTableroViewModel.Descripcion
            };

            _tableroRepository.crearTablero(tablero, crearTableroViewModel.IdUsuarioPropietario);

            _logger.LogInformation("Tablero creado con éxito");

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
    public IActionResult IrAModificarTablero(int id)
    {
        Tablero tablero = new Tablero(); 
        tablero = _tableroRepository.obtenerTablero(id);

        ModificarTableroViewModel modificarTableroViewModel = new ModificarTableroViewModel()
        {
            Id = tablero.Id,
            Nombre = tablero.Nombre,
            Descripcion = tablero.Descripcion
        };

        return View(modificarTableroViewModel);
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult modificarTablero(ModificarTableroViewModel modificarTableroViewModel)
    {
        if(!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Por favor, revisa los campos ingresados. Algunos datos no son válidos.";
            return RedirectToAction("Index", "Error"); 
        }

        try
        {
            Tablero tablero = new Tablero()
            {
                Id = modificarTableroViewModel.Id,
                Nombre = modificarTableroViewModel.Nombre,
                Descripcion = modificarTableroViewModel.Descripcion
            };

            _tableroRepository.modificarTablero(modificarTableroViewModel.Id, tablero);

            _logger.LogInformation("Tablero modificado con éxito");

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
    public IActionResult listarTableros()
    {
        try
        {
            ListarTablerosViewModel listarTablerosViewModel = new ListarTablerosViewModel()
            {
                Tableros = _tableroRepository.listarTableros()
            };

            _logger.LogInformation("Listado de tableros obtenido con éxito");
        
            return View(listarTablerosViewModel);

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
    public IActionResult listarTablerosPorID(int idUsuario)
    {
        try
        {
            ListarTablerosViewModel listarTablerosViewModel = new ListarTablerosViewModel()
            {
                Tableros = _tableroRepository.listarTablerosPorID(idUsuario)
            };

            _logger.LogInformation("Listado de tableros obtenido con éxito");

            return View(listarTablerosViewModel);
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
    public IActionResult eliminarTablero(int id)
    {
        try
        {
            _tableroRepository.eliminarTablero(id);

            return RedirectToAction("Index", "Home");
        
        }catch(SqliteException ex) when(ex.SqliteErrorCode == 19)
        {
            _logger.LogError(ex.ToString());

            TempData["ErrorMessage"] = "No se puede eliminar el tablero porque contiene tareas. Asegurese de eliminar las tareas primero.";

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