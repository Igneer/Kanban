using Microsoft.Data.Sqlite;
public class TableroRepository : ITableroRepository
{
    string connectionString = "Data Source=DB/Kanban.db;Cache=Shared";

    public Tablero crearTablero(Tablero tablero, int idUsarioPropietario)
    {
        string queryString = @"INSERT INTO tablero (id_usuario_propietario, nombre, descripcion) VALUES (@idUsuario, @nombre, @descripcion)";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@idUsuario", idUsarioPropietario);
            command.Parameters.AddWithValue("@nombre", tablero.Nombre);
            command.Parameters.AddWithValue("@descripcion", tablero.Descripcion);

            command.ExecuteNonQuery();

            connection.Close();
        }

        return tablero;
    }
    public Tablero obtenerTablero(int id)
    {
        Tablero tablero = new Tablero();
        string queryString = @"SELECT * FROM tablero WHERE id = @id";
        
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@id", id);
            
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                tablero.Id = id;
                tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                tablero.Nombre = reader["nombre"].ToString();
                tablero.Descripcion = reader["descripcion"].ToString();
            }
            connection.Close();
        }

        return tablero;
    }
    public void modificarTablero(int idTablero, Tablero tablero)
    {
        string queryString = @"UPDATE tablero SET nombre = @nombre, descripcion = @descripcion WHERE id = @id";
        
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@id", idTablero);
            command.Parameters.AddWithValue("@nombre", tablero.Nombre);
            command.Parameters.AddWithValue("@descripcion", tablero.Descripcion);

            command.ExecuteNonQuery();

            connection.Close();

        }
    }
    public List<Tablero> listarTableros()
    {
        List<Tablero> tableros = new List<Tablero>();
        string queryString = @"SELECT * FROM tablero";
        
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(queryString, connection);
            
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Tablero tablero = new Tablero();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    
                    tableros.Add(tablero);
                }
            }
            connection.Close();

        }

        return tableros;
    }
    public List<Tablero> listarTablerosPorID(int idUsuario)
    {
        List<Tablero> tableros = new List<Tablero>();
        string queryString = @"SELECT * FROM tablero WHERE id_usuario_propietario = @idUsuario";
        
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(queryString, connection);
            command.Parameters.AddWithValue("@idUsuario", idUsuario);
            
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Tablero tablero = new Tablero();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    
                    tableros.Add(tablero);
                }
            }
            connection.Close();

        }

        return tableros;
    }
    public void eliminarTablero(int id)
    {
        string queryString = @"DELETE FROM tablero WHERE id = @id";
        
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(queryString, connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            
            connection.Close();
        }
    }
}