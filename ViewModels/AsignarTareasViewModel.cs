using Kanban.Models;

namespace Kanban.ViewModels
{
    public class AsignarTareasViewModel
    {   
        public Tarea Tarea { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}