using Kanban.Models;
public interface ITareaRepository
{
    Tarea crearTarea(Tarea tarea, int idTablero);
    Tarea obtenerTarea(int id);
    void modificarTarea(int id, Tarea tarea);
    List<Tarea> obtenerTareasXUsuario(int idUsuario);
    List<Tarea> obtenerTareasXTablero(int idTablero);
    void eliminarTarea(int idTarea);
    void asignarTarea(int idUsuario, int idTarea);
    void cambiarEstadoTarea(int idTarea, int estado, int direccion);
}