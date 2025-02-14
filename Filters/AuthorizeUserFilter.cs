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
        if (usuario == null)
        {
            _logger.LogInformation("Usuario no autenticado");
            context.Result = new RedirectToActionResult("IrAIniciarSesion", "Login", null);
        }
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Accion ejecutada");
    }

}
