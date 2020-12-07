using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoJose.Modelo
{
  public  class Epi
    {
        [Key]
        public int IdEpi { get; set; }
        public string Nombre { get; set; }

        public List<TrabajadorEpi> TrabajadoresEpi { get; set; }
    }
}
