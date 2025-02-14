using System.Data.Common;
using Microsoft.Data.Sqlite;
using Kanban.Models;
public class TareaRepository : ITareaRepository
{
    string connectionString = "Data Source=DB/Kanban.db;Cache=Shared";
    public Tarea crearTarea(Tarea tarea, int idTablero)
    {
        string queryString = @"INSERT INTO tarea (id_tablero, nombre, estado, descripcion, color) VALUES (@idTablero, @nombre, @estado, @descripcion, @color)";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@idTablero", idTablero);
            command.Parameters.AddWithValue("@nombre", tarea.Nombre);
            command.Parameters.AddWithValue("@estado", tarea.Estado);
            command.Parameters.AddWithValue("@descripcion", tarea.Descripcion);
            command.Parameters.AddWithValue("@color", tarea.Color);

            command.ExecuteNonQuery();

            connection.Close();
        }
    

        return tarea;
    }
    public Tarea obtenerTarea(int id)
    {
        Tarea tarea = new Tarea();
        string queryString = @"SELECT * FROM tarea WHERE id = @id";
        
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@id", id);
            
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                tarea.Id = Convert.ToInt32(reader["id"]);
                tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                tarea.Nombre = reader["nombre"].ToString();
                tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                tarea.Descripcion = reader["descripcion"].ToString();
                tarea.Color = reader["color"].ToString();

                if(reader["id_usuario_asignado"] == DBNull.Value)
                {
                    tarea.IdUsuarioAsignado = null;
                }else
                {
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                }
            }
            connection.Close();
        }

        return tarea;
    }
    public void modificarTarea(int id, Tarea tarea)
    {
        string queryString = @"UPDATE tarea SET nombre = @nombre, descripcion = @descripcion WHERE id = @id";
            
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@nombre", tarea.Nombre);
            command.Parameters.AddWithValue("@descripcion", tarea.Descripcion);

            command.ExecuteNonQuery();

            connection.Close();

        }
    }
    public List<Tarea> obtenerTareasXUsuario(int idUsuario)
    {
        List<Tarea> tareas = new List<Tarea>();
        string queryString = @"SELECT * FROM tarea WHERE id_usuario_asignado = @idUsuario";
        
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@idUsuario", idUsuario);
            
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Tarea tarea = new Tarea();
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    
                    tareas.Add(tarea);
                }
            }
            connection.Close();
        }

        return tareas;
    }
    public List<Tarea> obtenerTareasXTablero(int idTablero)
    {
        List<Tarea> tareas = new List<Tarea>();
        string queryString = @"SELECT * FROM tarea WHERE id_tablero = @idTablero";
        
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@idTablero", idTablero);
            
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Tarea tarea = new Tarea();
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    if(reader["id_usuario_asignado"] == DBNull.Value)
                    {
                        tarea.IdUsuarioAsignado = null;
                    }else
                    {
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                    tareas.Add(tarea);
                }
            }
            connection.Close();
        }

        return tareas;
    }
    public void eliminarTarea(int idTarea)
    {
        string queryString = @"DELETE FROM tarea WHERE id = @idTarea";
        
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@idTarea", idTarea);

            command.ExecuteNonQuery();

            connection.Close();

        }
    }
    public void asignarTarea(int idUsuario, int idTarea)
    {
        string queryString = @"UPDATE tarea SET id_usuario_asignado = @idUsuario WHERE id = @idTarea";
        
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@idUsuario", idUsuario);
            command.Parameters.AddWithValue("@idTarea", idTarea);

            command.ExecuteNonQuery();

            connection.Close();

        }       
    } 
}