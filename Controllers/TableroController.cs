using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;

public class TableroController : Controller
{
    TableroRepository repoTablero = new TableroRepository();

    [HttpGet]
    public IActionResult IrACrearTablero()
    {
        return View();
    }

    [HttpPost]
    public IActionResult crearTablero(Tablero tablero, int IdUsuarioPropietario)
    {
        repoTablero.crearTablero(tablero, IdUsuarioPropietario);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult IrAModificarTablero(int id)
    {
        Tablero tablero = new Tablero(); 
        tablero = repoTablero.obtenerTablero(id);

        return View(tablero);
    }

    [HttpPost]
    public IActionResult modificarTablero(int id, Tablero tablero)
    {
        repoTablero.modificarTablero(id, tablero);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult listarTableros()
    {
        List<Tablero> tableros = repoTablero.listarTableros();
    
        return View(tableros);
    }

    [HttpGet]
    public IActionResult listarTablerosPorID(int idUsuario)
    {
        List<Tablero> tableros = repoTablero.listarTablerosPorID(idUsuario);

        return View(tableros);
    }   
}