using System.ComponentModel.DataAnnotations;
using Kanban.Models;

namespace Kanban.ViewModels
{
    public class CrearUsuarioViewModel
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(10, ErrorMessage = "El nombre no puede tener más de 10 caracteres")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(10, ErrorMessage = "La contraseña no puede tener más de 10 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        public RolUsuario Rol { get; set; }
    }
}
