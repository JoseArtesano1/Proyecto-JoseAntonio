using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoJose.Modelo
{
    
    public  class Festivo
    {  
        [Key]
        public int IdFestivo { get; set; }
        public string FechaFestivo { get; set; }
    }
}
