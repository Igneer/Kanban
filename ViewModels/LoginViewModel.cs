using System.ComponentModel.DataAnnotations;
namespace Kanban.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre {get; set;}
        
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password {get; set;}
    }
}