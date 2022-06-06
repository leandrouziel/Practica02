using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeandroPea02.Models
{
    public class UsuarioEntity
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public bool Estado { get; set; }
        public string Observacion { get; set; }
    }
}