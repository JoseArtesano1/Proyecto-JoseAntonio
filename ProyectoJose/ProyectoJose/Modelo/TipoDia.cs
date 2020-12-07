using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoJose.Modelo
{
  public  class TipoDia
    {
        [Key]
        public int IdTipoDia { get; set; }
        public string Denominacion { get; set; }
        public double Importe { get; set; }

        public List<Periodo> Periodos { get; set; }
    }
}
