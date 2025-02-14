using System.ComponentModel.DataAnnotations;

namespace Kanban.ViewModels
{
    public class CrearTableroViewModel
    {
        public int IdUsuarioPropietario {get; set;}
        
        [Required]
        [StringLength(15, ErrorMessage = "El nombre no puede tener más de 10 caracteres")]
        public string Nombre {get; set;}

        [Required]        
        [StringLength(320, ErrorMessage = "La descripcion debe de tener menos de 320 caracteres")]

        public string Descripcion {get; set;}
    }
}