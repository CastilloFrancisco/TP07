using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP24234.Models;
using Newtonsoft.Json;

namespace TP24234.Models
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
        public string Fecha { get; set; }
        [JsonProperty]
        public string Finalizada { get; set; }
        [JsonProperty]
        public string IdUsuario { get; set; }
    
        public Tarea()
        {
    
        }

        
    }
}