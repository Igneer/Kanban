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
        var rol = context.HttpContext.Session.GetString("Rol");
        if (rol != "Administrador")
        {
            _logger.LogInformation("Usuario sin permisos suficientes");
            context.HttpContext.Items["ErrorMessage"] = "No tienes permisos suficientes para acceder a esta página.";
            context.Result = new RedirectToActionResult("Index", "Error", null);
        }
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {

    }

}
