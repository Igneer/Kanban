using Microsoft.AspNetCore.Mvc;

public class UsuarioController : Controller
{
    UsuarioRepository repoUsuario = new UsuarioRepository();

    [HttpGet]
    public IActionResult IrACrearUsuario()
    {
        return View();
    }

    [HttpPost]
    public IActionResult crearUsuario(Usuario usuario)
    {
        repoUsuario.crearUsuario(usuario);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]

    public IActionResult IrAModificarUsuario(int id)
    {
        Usuario usuario = new Usuario();
        usuario = repoUsuario.obtenerUsuario(id);

        return View(usuario);
    }

    [HttpPost]

    public IActionResult modificarUsuario(int id, Usuario usuario)
    {
        repoUsuario.modificarUsuario(id, usuario);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult listarUsuarios()
    {
        List<Usuario> usuarios = repoUsuario.listarUsuarios();

        return View(usuarios);
    }

}