using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoJose.Modelo
{
   public class TrabajadorCurso
    {
        public int IdTrabajador { get; set; }
        public Trabajador Trabajador { get; set; }
        public int IdCurso { get; set; }
        public Curso Curso { get; set; }
    }
}
