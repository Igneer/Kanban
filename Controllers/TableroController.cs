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
            return View("IrACrearTablero");
        }

        Tablero tablero = new Tablero()
        {
            IdUsuarioPropietario = crearTableroViewModel.IdUsuarioPropietario,
            Nombre = crearTableroViewModel.Nombre,
            Descripcion = crearTableroViewModel.Descripcion
        };

        _tableroRepository.crearTablero(tablero, crearTableroViewModel.IdUsuarioPropietario);

        return RedirectToAction("Index", "Home");
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
            return View("IrAModificarTablero");
        }

        Tablero tablero = new Tablero()
        {
            Id = modificarTableroViewModel.Id,
            Nombre = modificarTableroViewModel.Nombre,
            Descripcion = modificarTableroViewModel.Descripcion
        };

        _tableroRepository.modificarTablero(modificarTableroViewModel.Id, tablero);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult listarTableros()
    {
        ListarTablerosViewModel listarTablerosViewModel = new ListarTablerosViewModel()
        {
            Tableros = _tableroRepository.listarTableros()
        };
    
        return View(listarTablerosViewModel);
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult listarTablerosPorID(int idUsuario)
    {
        ListarTablerosViewModel listarTablerosViewModel = new ListarTablerosViewModel()
        {
            Tableros = _tableroRepository.listarTablerosPorID(idUsuario)
        };

        return View(listarTablerosViewModel);
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