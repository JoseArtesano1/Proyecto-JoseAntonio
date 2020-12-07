using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoJose.Modelo
{
  public  class Periodo
    {
        [Key]
        public int IdPeriodo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }

        public int IdTrabajador { get; set; }

        public Trabajador Trabajador { get; set; }

        public int IdTipoDia { get; set; }

        public TipoDia TipoDia { get; set; }

    }
}
