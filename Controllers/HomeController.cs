using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.ViewModels;
using Microsoft.Data.Sqlite;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITableroRepository _tableroRepository;
    private readonly ITareaRepository _tareaRepository;
    public HomeController(IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository, ITareaRepository tareaRepository, ILogger<HomeController> logger)
    {
        _usuarioRepository = usuarioRepository;
        _tableroRepository = tableroRepository;
        _tareaRepository = tareaRepository;
        _logger = logger;
    }

   [HttpGet]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult Index()
    {
        try
        {
            int id = HttpContext.Session.GetInt32("Id").Value;
            string nombre = HttpContext.Session.GetString("Nombre");
            string rol = HttpContext.Session.GetString("Rol");

            var tableros = _tableroRepository.listarTablerosPorID(id);
            var tareasPorTablero = new Dictionary<int, List<Tarea>>();

            foreach(var tablero in tableros)
            {
                var tareas = _tareaRepository.obtenerTareasXTablero(tablero.Id);
                tareasPorTablero[tablero.Id] = tareas;
            }

            HomeViewModel model = new HomeViewModel()
            {
                Id = id,
                Nombre = nombre,
                Rol = rol,
                Tableros = _tableroRepository.listarTablerosPorID(id),
                TareasPorTablero = tareasPorTablero
            };

            return View(model);

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
