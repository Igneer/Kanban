using Microsoft.AspNetCore.Mvc;
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
    public IActionResult crearTarea(Tarea tarea, int idTablero)
    {
        _tareaRepository.crearTarea(tarea, idTablero);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult IrAModificarTarea(int id)
    {
        Tarea tarea = new Tarea();

        tarea = _tareaRepository.obtenerTarea(id);

        return View(tarea);
    }

    [HttpPost]
    public IActionResult modificarTarea(int id, Tarea tarea)
    {
        _tareaRepository.modificarTarea(id, tarea);

        return RedirectToAction("Index", "Login");
    }

    [HttpPost]
    public IActionResult eliminarTarea(int id)
    {
        _tareaRepository.eliminarTarea(id);

        return RedirectToAction("Index", "Login");
    }
}