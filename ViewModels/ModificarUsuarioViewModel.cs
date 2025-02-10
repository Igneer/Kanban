using Kanban.Models;

namespace Kanban.ViewModels
{
    public class ModificarUsuarioViewModel
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public RolUsuario Rol { get; set; }
    }
}
