using Microsoft.AspNetCore.Mvc;
public class TareaController : Controller
{
    TareaRepository repoTarea = new TareaRepository();

    [HttpGet]
    public IActionResult IrACrearTarea()
    {
        return View();
    }

    [HttpPost]
    public IActionResult crearTarea(Tarea tarea, int idTablero)
    {
        repoTarea.crearTarea(tarea, idTablero);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult IrAModificarTarea(int id)
    {
        Tarea tarea = new Tarea();

        tarea = repoTarea.obtenerTarea(id);

        return View(tarea);
    }

    [HttpPost]
    public IActionResult modificarTarea(int id, Tarea tarea)
    {
        repoTarea.modificarTarea(id, tarea);

        return RedirectToAction("Index", "Login");
    }
}