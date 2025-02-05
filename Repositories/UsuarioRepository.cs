using Microsoft.Data.Sqlite;
public class UsuarioRepository  : IUsuarioRepository
{
    string connectionString = "Data Source=DB/Kanban.db;Cache=Shared";
    public Usuario crearUsuario(Usuario usuario)
    {
        string queryString = @"INSERT INTO usuario (nombre_usuario, password, rolusuario) VALUES (@nombre, @password, @rol)";

        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@nombre", usuario.NombreUsuario);
            command.Parameters.AddWithValue("@password", usuario.Password);
            command.Parameters.AddWithValue("@rol", usuario.Rol);

            command.ExecuteNonQuery();

            connection.Close();
        }

        return usuario;
    }
    public Usuario obtenerUsuario(int id)
    {
        Usuario usuario = new Usuario();
        string queryString = @"SELECT * FROM usuario WHERE id = @id";
        
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@id", id);
            
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                usuario.Id = Convert.ToInt32(reader["id"]);
                usuario.NombreUsuario = reader["nombre_usuario"].ToString();
                usuario.Password = reader["password"].ToString();
                usuario.Rol = (RolUsario)Convert.ToInt32(reader["rolusuario"]);
            }
            connection.Close();
        }

        return usuario;
    }
    public void modificarUsuario(int id, Usuario usuario)
    {
        string queryString = @"UPDATE usuario SET nombre_usuario = @nombre, password = @password, rolusuario = @rolusuario WHERE id = @id";
            
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@nombre", usuario.NombreUsuario);
            command.Parameters.AddWithValue("@password", usuario.Password);
            command.Parameters.AddWithValue("@rolusuario", Convert.ToInt32(usuario.Rol));

            command.ExecuteNonQuery();

            connection.Close();

        }

    } 
    public List<Usuario> listarUsuarios()
    {
        List<Usuario> usuarios = new List<Usuario>();
        string queryString = @"SELECT * FROM usuario";
        
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(queryString, connection);
            
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreUsuario = reader["nombre_usuario"].ToString();
                    usuario.Password = reader["password"].ToString();
                    usuario.Rol = (RolUsario)Convert.ToInt32(reader["rolusuario"]);
                    
                    usuarios.Add(usuario);
                }
            }
            connection.Close();
        }

        return usuarios;
    }
    public void cambiarContraseña(int id, string password)
    {
        string queryString = @"UPDATE usuario SET password = @password WHERE id = @id";
            
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(queryString, connection);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@password", password);

            command.ExecuteNonQuery();

            connection.Close();

        }
    }
    public void eliminarUsuario(int id)
    {
        string queryString = @"DELETE FROM usuario WHERE id = @id";
            
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