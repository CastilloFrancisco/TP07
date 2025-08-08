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


        public static void CrearTarea (Tarea t)
        {using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = " INSERT INTO Tareas (Titulo, Descripcion, Fecha, Finalizada, IdUsuario) VALUES ('Estudiar para el examen', 'Repasar temas de base de datos', '2025-08-10', 0, 1)";

                connection.Execute(query, new{ nombre, usuario, contraseña, frase, hobby, profeFav, peliculaFav, IDgrupo, foto });
            }
           
        }


        /*public void EliminarTarea (int IDtarea)
        {
            return
        }*/

        /*public Tarea TraerTarea (int IDtarea)
        {
            return
        }*/

        /*public void ActualizarTarea (Tarea t)
        {
            return
        }*/

        /*public void FinalizarTarea (int IDtarea)
        {
            return
        }*/

        /*public void ActualizarFechaLogin (int IDusuario)
        {
            return
        }*/


    }
}