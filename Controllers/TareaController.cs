using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.ViewModels;

public class TareaController : Controller
{
    private readonly ITareaRepository _tareaRepository;
    public TareaController(ITareaRepository tareaRepository)
    {
        _tareaRepository = tareaRepository;
    }

    [HttpGet]
    public IActionResult IrACrearTarea()
    {
        return View();
    }

    [HttpPost]
    public IActionResult crearTarea(CrearTareaViewModel model)
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

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
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
    public IActionResult modificarTarea(ModificarTareaViewModel model)
    {
        Tarea tarea = new Tarea()
        {
            Id = model.Id,
            Nombre = model.Nombre,
            Descripcion = model.Descripcion
        };

        _tareaRepository.modificarTarea(model.Id, tarea);

        return RedirectToAction("Index", "Login");
    }

    [HttpPost]
    public IActionResult eliminarTarea(int id)
    {
        _tareaRepository.eliminarTarea(id);

        return RedirectToAction("Index", "Login");
    }
}