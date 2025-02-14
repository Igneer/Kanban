using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


public class AdministradorAuthorizeUserFilter : IActionFilter
{
    private readonly ILogger<AuthorizeUserFilter> _logger;
    public AdministradorAuthorizeUserFilter(ILogger<AuthorizeUserFilter> logger)
    {
        _logger = logger;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var usuario = context.HttpContext.Session.GetString("Rol");
        if (usuario != "Administrador")
        {
            _logger.LogInformation("Usuario sin permisos suficients");
            context.Result = new RedirectToActionResult("Index", "Login", null);
        }
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Accion ejecutada");
    }

}
