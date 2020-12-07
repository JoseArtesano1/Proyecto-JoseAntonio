using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoJose.Modelo
{
  public  class Curso
    {
        [Key]
        public int IdCurso { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public List<TrabajadorCurso> TrabajadoresCurso { get; set; }

    }
}
