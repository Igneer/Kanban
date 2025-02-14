using System.ComponentModel.DataAnnotations;
using Kanban.Models;

namespace Kanban.ViewModels
{
    public class ModificarUsuarioViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(10, ErrorMessage = "El nombre no puede tener más de 10 caracteres")]
        public string NombreUsuario { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "La contraseña no puede tener más de 10 caracteres")]
        public string Password { get; set; }

        [Required]
        public RolUsuario Rol { get; set; }
    }
}
