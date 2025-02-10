using Kanban.Models;
public interface ITableroRepository
{
    Tablero crearTablero(Tablero tablero, int idUsarioPropietario);
    Tablero obtenerTablero(int id);
    void modificarTablero(int id, Tablero tablero);
    List<Tablero> listarTableros();
    List<Tablero> listarTablerosPorID(int idUsuario);
    void eliminarTablero(int idTablero);
}