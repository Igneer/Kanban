using Kanban.Models;

namespace Kanban.ViewModels
{
    public class IndexViewModel
    {
        public List<Tablero> Tableros { get; set; }
        public Dictionary<int, List<Tarea>> TareasPorTablero { get; set; }
    }
}