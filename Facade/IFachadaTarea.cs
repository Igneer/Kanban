using Kanban.Models;
public interface IFachadaTarea
{
    Tarea obtenerTarea(int id);
    List<Usuario> obtenerUsuarios();
    void asignarTarea(int idTarea, int idUsuario); 
}