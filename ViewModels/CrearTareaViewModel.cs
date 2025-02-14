using System.ComponentModel.DataAnnotations;
using Kanban.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.ViewModels
{
    public class CrearTareaViewModel
    {
        public int IdTablero { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "El nombre no puede tener más de 10 caracteres")]
        public string Nombre { get; set; }
        
        [Required]
        [StringLength(320, ErrorMessage = "La descripcion debe tener menos de 320 caracteres")]
        public string Descripcion { get; set; }
        public int Estado { get; set; }
    }
}