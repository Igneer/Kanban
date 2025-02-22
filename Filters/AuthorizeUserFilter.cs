using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


public class AuthorizeUserFilter : IActionFilter
{
    private readonly ILogger<AuthorizeUserFilter> _logger;
    public AuthorizeUserFilter(ILogger<AuthorizeUserFilter> logger)
    {
        _logger = logger;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var usuario = context.HttpContext.Session.GetString("Nombre");
        if (usuario == null )
        {
            _logger.LogInformation("Usuario sin permisos suficientes");
            context.HttpContext.Items["ErrorMessage"] = "Usuario no logueado";
            context.Result = new RedirectToActionResult("Index", "Error", null);
        }
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

}
