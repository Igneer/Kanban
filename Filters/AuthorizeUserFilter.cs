using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


public class AuthorizeUserFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var usuario = context.HttpContext.Session.GetString("Nombre");
        if (usuario == null)
        {
            context.Result = new RedirectToActionResult("IrAIniciarSesion", "Login", null);
        }
    }
}
