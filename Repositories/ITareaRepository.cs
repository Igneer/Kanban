public interface ITareaRepository
{
    Tarea crearTarea(Tarea tarea, int idTablero);
    Tarea obtenerTarea(int id);
    void modificarTarea(int id, Tarea tarea);
    List<Tarea> obtenerTareasXUsario(int idUsuario);
    void eliminarTarea(int idTarea);
    void asignarTarea(int idUsuario, int idTarea);
}