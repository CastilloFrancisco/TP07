using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace TP07.Models
{
    public static class BD
    {
        private static string _connectionString = "Server=localhost;Database=ToDoList;Integrated Security=True;TrustServerCertificate=True;";

        public static Usuario TraerUsuario(string user)
        {
            Usuario u = new Usuario();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE Username = @pUser";
                u = connection.QueryFirstOrDefault<Usuario>(query, new { pUser = user });
            }

            return u;

        }

        public static bool LogIn(string user, string contraseña)
        {
            Usuario usuario = TraerUsuario(user);
            if (usuario == null) return false;

            bool logeado = false;
            if (usuario.Pass == contraseña)
            {
                logeado = true;
            }

            return logeado;
        }

        public static Usuario TraerUsuarioPorId(int ID)
        {
            Usuario u = new Usuario();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE ID = @pId";
                u = connection.QueryFirstOrDefault<Usuario>(query, new { pId = ID });
            }

            return u;
        }

        public static void RegistrarUsuario(Usuario u)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Usuarios (Username, Pass, Nombre, Apellido, Foto, UltimoLogin) VALUES (@Username, @Pass, @Nombre, @Apellido, @Foto, @UltimoLogin)";

                connection.Execute(query, new { u.Username, u.Pass, u.Nombre, u.Apellido, u.Foto, u.UltimoLogin });
            }
        }

        public static List<Tarea> DevolverTareas(int IDusuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tareas WHERE IdUsuario = @idUsuario AND Eliminado = 0";

                return connection.Query<Tarea>(query, new { idUsuario = IDusuario }).ToList();
            }
        }

        public static List<Usuario> DevolverUsuarios()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios";

                return connection.Query<Usuario>(query).ToList();
            }
        }


        public static List<Tarea> DevolverTareasEliminadas(int IDusuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tareas WHERE IdUsuario = @idUsuario AND Eliminado = 1";

                return connection.Query<Tarea>(query, new { idUsuario = IDusuario }).ToList();
            }
        }

        public static void CrearTarea(Tarea t)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Tareas (Titulo, Descripcion, Fecha, Finalizada, IdUsuario, FechaCreacion, Eliminado) VALUES (@Titulo, @Descripcion, @Fecha, @Finalizada, @IdUsuario, GETDATE(), 0)";
                connection.Execute(query, new { t.Titulo, t.Descripcion, t.Fecha, t.Finalizada, t.IdUsuario });
            }
        }


        public static void EliminarTarea(int IDtarea)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Tareas SET Eliminado = 1, FechaEliminacion = GETDATE() WHERE ID = @IDtarea";

                connection.Execute(query, new { IDtarea });
            }
        }

        public static Tarea TraerTarea(int IDtarea)
        {
            Tarea t = new Tarea();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tareas WHERE ID = @idtarea";
                t = connection.QueryFirstOrDefault<Tarea>(query, new { idtarea = IDtarea });
            }

            return t;

        }

        public static void ActualizarTarea(Tarea t)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Tareas SET Titulo = @Titulo, Descripcion = @Descripcion, Fecha = @Fecha, FechaModificacion = GETDATE() WHERE ID = @ID";

                connection.Execute(query, new { ID = t.ID, Titulo = t.Titulo, Descripcion = t.Descripcion, Fecha = t.Fecha });
            }
        }

        public static void FinalizarTarea(int IDtarea)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Tareas SET Finalizada = 1 WHERE ID = @IDtarea";
                connection.Execute(query, new { IDtarea });
            }
        }

        public static void ActualizarFechaLogin(int IDusuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Usuarios SET UltimoLogin = GETDATE() WHERE ID = @IDusuario";
                connection.Execute(query, new { IDusuario });
            }
        }

        public static void RestaurarTarea(int IDtarea)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Tareas SET Eliminado = 0, FechaEliminacion = NULL WHERE ID = @IDtarea";
                connection.Execute(query, new { IDtarea });
            }
        }
        public static List<Usuario> DevolverOtrosUsuarios(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE ID <> @idUsuario";

                return connection.Query<Usuario>(query, new { idUsuario = ID }).ToList();
            }
        }

    }
}