using Kanban.Models;

namespace Kanban.ViewModels
{
    public class CrearUsuarioViewModel
    {
        public string Nombre { get; set; }
        public string Password { get; set; }
        public RolUsuario Rol { get; set; }
    }
}
