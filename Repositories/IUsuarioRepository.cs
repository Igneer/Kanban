public interface IUsuarioRepository
{
    Usuario crearUsuario(Usuario usuario);
    Usuario obtenerUsuario(int id);
    void modificarUsuario(int id, Usuario usuario);
    List<Usuario> listarUsuarios();
    void cambiarContraseña(int id, string password);
    void eliminarUsuario(int id);
}