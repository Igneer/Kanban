using Kanban.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


public class AccesoATableroFilter : IActionFilter
{
    private readonly ITableroRepository _tableroRepository;
    private readonly ILogger<AuthorizeUserFilter> _logger;
    public AccesoATableroFilter(ILogger<AuthorizeUserFilter> logger, ITableroRepository tableroRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        try
        {
            int idTablero; 
            var idUsuario = context.HttpContext.Session.GetInt32("Id");
            var rol = context.HttpContext.Session.GetString("Rol");
            
            if(context.ActionArguments.ContainsKey("modificarTableroViewModel"))
            {
                var model = (ModificarTableroViewModel)context.ActionArguments["modificarTableroViewModel"];
                idTablero = model.Id;
            }else
            {
                idTablero = (int)context.ActionArguments["id"];
            }
            
            var tablero = _tableroRepository.obtenerTablero(idTablero);

            if (tablero == null || (rol != "Administrador" && tablero.IdUsuarioPropietario != idUsuario))
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
