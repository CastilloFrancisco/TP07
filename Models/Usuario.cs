using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP07.Models;
using Newtonsoft.Json;

namespace TP07.Models
{
    public class Usuario
    {
        [JsonProperty]
        public int ID { get; set; }
        [JsonProperty]
        public string Username { get; set; }
        [JsonProperty]
        public string Pass { get; set; }
        [JsonProperty]
        public string Nombre { get; set; }
        [JsonProperty]
        public string Apellido { get; set; }
        [JsonProperty]
        public string Foto { get; set; }
        [JsonProperty]
        public DateOnly UltimoLogin { get; set; }
    
        public Usuario()
        {

        }
        

        
    }
}