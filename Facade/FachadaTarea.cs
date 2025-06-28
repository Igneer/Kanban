using Kanban.Models;
public class FachadaTarea : IFachadaTarea
{
    private readonly ITareaRepository _tareaRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public FachadaTarea(ITareaRepository tareaRepository, IUsuarioRepository usuarioRepository)
    {
        _tareaRepository = tareaRepository;
        _usuarioRepository = usuarioRepository;
    }

    public Tarea obtenerTarea(int id)
    {
        return _tareaRepository.obtenerTarea(id);
    }

    public List<Usuario> obtenerUsuarios()
    {
        return _usuarioRepository.listarUsuarios();
    }

    public void asignarTarea(int idUsuario, int idTarea)
    {
        _tareaRepository.asignarTarea(idUsuario, idTarea);
    }
}