using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoJose.Modelo
{
   public class TrabajadorEpi
    {
        public int IdTrabajador { get; set; }
        public Trabajador Trabajador { get; set; }
        public int IdEpi { get; set; }
        public Epi Epi { get; set; }
        public string FechaEpi { get; set; }
    }
}
