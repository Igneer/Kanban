using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.ViewModels;
public class HomeController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITableroRepository _tableroRepository;
    private readonly ITareaRepository _tareaRepository;
    public HomeController(IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository, ITareaRepository tareaRepository)
    {
        _usuarioRepository = usuarioRepository;
        _tableroRepository = tableroRepository;
        _tareaRepository = tareaRepository;

    }

   [HttpGet]
    [ServiceFilter(typeof(AuthorizeUserFilter))]
    public IActionResult Index()
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
    }

}
