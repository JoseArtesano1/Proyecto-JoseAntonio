using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoJose.Modelo
{
   public class Trabajador
    {
        [Key]
        public int IdTrabajador { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public long Nseguridads { get; set; }

        public string FechaAlta { get; set; }
        public string FechaMedico {get; set; }

        public string FechaDni { get; set; }

        public List<TrabajadorCurso> TrabajadoresCurso { get; set; }
        public List<Periodo> Periodos { get; set; }

        public List<TrabajadorEpi> TrabajadoresEpi { get; set; }

    }
}
