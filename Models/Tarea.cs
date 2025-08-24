using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP07.Models;
using Newtonsoft.Json;

namespace TP07.Models
{
    public class Tarea
    {
        [JsonProperty]
        public int ID { get; set; }
        [JsonProperty]
        public string Titulo { get; set; }
        [JsonProperty]
        public string Descripcion { get; set; }
        [JsonProperty]
        public DateTime Fecha { get; set; }
        [JsonProperty]
        public bool Finalizada { get; set; }
        [JsonProperty]
        public int IdUsuario { get; set; }
        [JsonProperty]
        public DateTime FechaCreación { get; set; }
        [JsonProperty]
        public DateTime? FechaModificación { get; set; }
        [JsonProperty]
        public DateTime? FechaEliminación { get; set; }
        [JsonProperty]
        public bool Eliminado { get; set; }

        public Tarea()
        {

        }
        public Tarea(int pID, string pTitulo, string pDescripcion, DateTime pFecha, bool pFinalizada, int IdU)
        {
            ID = pID;
            Titulo = pTitulo;
            Descripcion = pDescripcion;
            Fecha = pFecha;
            Finalizada = pFinalizada;
            IdUsuario = IdU;
            FechaCreación = DateTime.Now;
            Eliminado = false;

        }

        public Tarea(string pTitulo, string pDescripcion, DateTime pFecha, bool pFinalizada, int IdU)
        {
            Titulo = pTitulo;
            Descripcion = pDescripcion;
            Fecha = pFecha;
            Finalizada = pFinalizada;
            IdUsuario = IdU;
            FechaCreación = DateTime.Now;
            Eliminado = false;


        }
    }
}