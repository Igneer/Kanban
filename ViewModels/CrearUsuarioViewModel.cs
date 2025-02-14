using System.ComponentModel.DataAnnotations;
using Kanban.Models;

namespace Kanban.ViewModels
{
    public class CrearUsuarioViewModel
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        public RolUsuario Rol { get; set; }
    }
}
