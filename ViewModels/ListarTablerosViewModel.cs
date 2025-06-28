using Kanban.Models;

namespace Kanban.ViewModels
{
    public class ListarTablerosViewModel
    {
        public List<Tablero> Tableros { get; set; }
        public Dictionary<int, List<Tarea>> TareasPorTablero { get; set; }
    }
}