public enum EstadoTarea{
    Ideas = 1,
    ToDo = 2,
    Doing = 3,
    Review = 4,
    Done = 5
}
public class Tarea
{
    int id;
    int idTablero;
    string nombre;
    string descripcion;
    string color;
    EstadoTarea estado;
    int? idUsuarioAsignado;

    public int Id { get => id; set => id = value; }
    public int IdTablero { get => idTablero; set => idTablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
}