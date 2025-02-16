using Kanban.Models;

namespace Kanban.ViewModels
{
    public class HomeViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public List<Tablero> Tableros { get; set; }
        public Dictionary<int, List<Tarea>> TareasPorTablero { get; set; }
    }
}