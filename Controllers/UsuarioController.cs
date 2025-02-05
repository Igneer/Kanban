using Microsoft.AspNetCore.Mvc;

public class UsuarioController : Controller
{
    IUsuarioRepository _usuarioRepository = new UsuarioRepository();
    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult IrACrearUsuario()
    {
        return View();
    }

    [HttpPost]
    public IActionResult crearUsuario(Usuario usuario)
    {
        _usuarioRepository.crearUsuario(usuario);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]

    public IActionResult IrAModificarUsuario(int id)
    {
        Usuario usuario = new Usuario();
        usuario = _usuarioRepository.obtenerUsuario(id);

        return View(usuario);
    }

    [HttpPost]

    public IActionResult modificarUsuario(int id, Usuario usuario)
    {
        _usuarioRepository.modificarUsuario(id, usuario);

        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult listarUsuarios()
    {
        List<Usuario> usuarios = _usuarioRepository.listarUsuarios();

        return View(usuarios);
    }

    [HttpPost]
    public IActionResult eliminarUsuario(int id)
    {
        _usuarioRepository.eliminarUsuario(id);

        return RedirectToAction("Index", "Login");
    }

}