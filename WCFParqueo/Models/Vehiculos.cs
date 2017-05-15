using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFParqueo.Models
{
    public class Vehiculos
    {
        public string noPlaca { get; set; }
        public string tipo { get; set; }
        public string color { get; set; }
        public string propietario { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public byte[] imagen { get; set; }
        

    }
}