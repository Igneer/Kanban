using Kanban.Models;
public interface IFachadaTarea
{
    List<Tarea> obtenerTareasXTablero(int id);
    List<Usuario> obtenerUsuarios();
    void asignarTarea(int idTarea, int idUsuario); 
}