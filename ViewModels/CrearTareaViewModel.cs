using Kanban.Models;

namespace Kanban.ViewModels
{
    public class CrearTareaViewModel
    {
        public int IdTablero { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
    }
}