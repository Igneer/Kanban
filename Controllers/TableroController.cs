using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;

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
    public IActionResult crearTablero(Tablero tablero, int IdUsuarioPropietario)
    {
        _tableroRepository.crearTablero(tablero, IdUsuarioPropietario);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult IrAModificarTablero(int id)
    {
        Tablero tablero = new Tablero(); 
        tablero = _tableroRepository.obtenerTablero(id);

        return View(tablero);
    }

    [HttpPost]
    public IActionResult modificarTablero(int id, Tablero tablero)
    {
        _tableroRepository.modificarTablero(id, tablero);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult listarTableros()
    {
        List<Tablero> tableros = _tableroRepository.listarTableros();
    
        return View(tableros);
    }

    [HttpGet]
    public IActionResult listarTablerosPorID(int idUsuario)
    {
        List<Tablero> tableros = _tableroRepository.listarTablerosPorID(idUsuario);

        return View(tableros);
    }   

    [HttpPost]
    public IActionResult eliminarTablero(int id)
    {
        _tableroRepository.eliminarTablero(id);

        return RedirectToAction("Index", "Login");
    }
}