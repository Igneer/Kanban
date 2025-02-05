public enum RolUsario{
    Administrador = 1,
    Operador = 2
}
/* 
Sirve para obtener datos de una base de datos y facilitar la conversion
Por ejemplo:
int rolUsuarioDB = 1 Un valor obtenido de la base de datos
RolUsuario rol = (RolUsuario)rolUsuarioDB Con esto se hace la conversion facilmente
*/
public class Usuario
{
    int id;
    string nombreUsuario;
    string password;
    RolUsario rol;

    public int Id { get => id; set => id = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    public string Password { get => password; set => password = value; }
    public RolUsario Rol { get => rol; set => rol = value; }
}