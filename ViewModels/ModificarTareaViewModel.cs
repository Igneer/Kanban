using System.ComponentModel.DataAnnotations;

namespace Kanban.ViewModels
{
    public class ModificarTareaViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(10, ErrorMessage = "El nombre no puede tener más de 10 caracteres")]
        public string Nombre { get; set; }
        
        [Required]
        [StringLength(320, ErrorMessage = "La descripcion debe tener menos de 320 caracteres")]
        public string Descripcion { get; set; }
    }
}