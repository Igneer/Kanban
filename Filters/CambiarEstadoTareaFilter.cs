using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Kanban.ViewModels;


public class CambiarEstadoTareaFilter : IActionFilter
{
    private readonly ITableroRepository _tableroRepository;
    private readonly ITareaRepository _tareaRepository;
    private readonly ILogger<AuthorizeUserFilter> _logger;
    public CambiarEstadoTareaFilter(ILogger<AuthorizeUserFilter> logger, ITableroRepository tableroRepository, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _tareaRepository = tareaRepository;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        try
        {
            var idUsuario = context.HttpContext.Session.GetInt32("Id").Value;
            var rol = context.HttpContext.Session.GetString("Rol");
            int idTarea = (int)context.ActionArguments["idTarea"];
            
            
            var tarea = _tareaRepository.obtenerTarea(idTarea);
            var tablero = _tableroRepository.obtenerTablero(tarea.IdTablero);

            if (tablero == null || (rol != "Administrador" && tarea.IdUsuarioAsignado != idUsuario && tablero.IdUsuarioPropietario != idUsuario))
            {
                _logger.LogInformation("Usuario sin permisos suficientes");
                var controller = (Controller)context.Controller;
                controller.TempData["ErrorMessage"] = "No tienes permisos suficientes para acceder a esta página.";
                context.Result = new RedirectToActionResult("Index", "Error", null);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al verificar permisos");
            var controller = (Controller)context.Controller;
            controller.TempData["ErrorMessage"] = "Ocurrió un error al verificar permisos. Por favor, inténtelo de nuevo.";
            context.Result = new RedirectToActionResult("Index", "Error", null);   
        }
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }

}
