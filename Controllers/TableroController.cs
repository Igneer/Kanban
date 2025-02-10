using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.ViewModels;

public class TableroController : Controller
{
    private readonly ITableroRepository _tableroRepository;
    public TableroController(ITableroRepository tableroRepository)
    {
        _tableroRepository = tableroRepository;
    }

    [HttpGet]
    public IActionResult IrACrearTablero()
    {
        return View();
    }

    [HttpPost]
    public IActionResult crearTablero(CrearTableroViewModel crearTableroViewModel)
    {
        Tablero tablero = new Tablero()
        {
            IdUsuarioPropietario = crearTableroViewModel.IdUsuarioPropietario,
            Nombre = crearTableroViewModel.Nombre,
            Descripcion = crearTableroViewModel.Descripcion
        };

        _tableroRepository.crearTablero(tablero, crearTableroViewModel.IdUsuarioPropietario);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
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
    public IActionResult modificarTablero(ModificarTableroViewModel modificarTableroViewModel)
    {
        Tablero tablero = new Tablero()
        {
            Id = modificarTableroViewModel.Id,
            Nombre = modificarTableroViewModel.Nombre,
            Descripcion = modificarTableroViewModel.Descripcion
        };

        _tableroRepository.modificarTablero(modificarTableroViewModel.Id, tablero);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult listarTableros()
    {
        ListarTablerosViewModel listarTablerosViewModel = new ListarTablerosViewModel()
        {
            Tableros = _tableroRepository.listarTableros()
        };
    
        return View(listarTablerosViewModel);
    }

    [HttpGet]
    public IActionResult listarTablerosPorID(int idUsuario)
    {
        ListarTablerosViewModel listarTablerosViewModel = new ListarTablerosViewModel()
        {
            Tableros = _tableroRepository.listarTablerosPorID(idUsuario)
        };

        return View(listarTablerosViewModel);
    }   

    [HttpPost]
    public IActionResult eliminarTablero(int id)
    {
        _tableroRepository.eliminarTablero(id);

        return RedirectToAction("Index", "Login");
    }
}