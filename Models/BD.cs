using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace TP24234.Models
{
    public static class BD
    {
        private static string _connectionString = "Server=localhost;Database=TP6_Introducciónabasededatos;Integrated Security=True;TrustServerCertificate=True;";

        public static Usuario Login(string user)
        {
            Usuario u = new Usuario();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE Username = @pUser";
                u = connection.QueryFirstOrDefault<Usuario>(query, new { pUser = user });
            }

            return u;

        }

        public static bool Registro(string user, string contraseña)
        {
            Usuario usuario = Login(user);
            if (usuario == null) return false;

            bool logeado = false;
            if (usuario.Pass == contraseña)
            {
                logeado = true;
            }

            return logeado;
        }


        public static List<Usuario> DevolverTareas(int IDusuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tareas WHERE IdUsuario = @idUsuario";

                return connection.Query<Usuario>(query, new { idUsuario = IDusuario }).ToList();
            }
        }


        public static void CrearTarea(Tarea t)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = " INSERT INTO Tareas (Titulo, Descripcion, Fecha, Finalizada, IdUsuario) VALUES (@t.Titulo, @t.Descripcion, @t.Fecha, @t.Finalizada, @t.IdUsuario)";

                connection.Execute(query, new { t.Titulo, t.Descripcion, t.Fecha, t.Finalizada, t.IdUsuario });
            }

        }


        public static void EliminarTarea (int IDtarea)   
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = " DELETE FROM Tareas WHERE ID = @IDtarea";

                connection.Execute(query, new { IDtarea });
            }
        }

        public static Tarea TraerTarea (int IDtarea)
        {
            Tarea t = new Tarea();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tareas WHERE ID = @idtarea";
                t = connection.QueryFirstOrDefault<Tarea>(query, new { idtarea = IDtarea });
            }

            return t;

        }

        public static void ActualizarTarea (Tarea t)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Tareas SET Titulo = @t.Titulo, Descripcion = @t.Descripcion, Fecha = @t.Fecha WHERE ID = t.ID";
                 connection.Execute(query, new {  t.ID, t.Titulo, t.Descripcion, t.Fecha });
            }

        }

        public static void FinalizarTarea (int IDtarea)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Tareas SET Finalizada = 1 WHERE ID = @IDtarea";
                 connection.Execute(query, new { IDtarea });
            }
        }

        public static void ActualizarFechaLogin (int IDusuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Usuarios SET UltimoLogin = GETDATE() WHERE ID = @IDusuario";
                 connection.Execute(query, new { IDusuario });
            }
        }


    }
}