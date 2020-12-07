using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoJose.Modelo
{
   public class T_Registro
    {
                [Key]
                public int IdUsuario { get; set; }
                public string Nombre { get; set; }
                public string Usuario { get; set; }
                public string Contrasenia { get; set; }
                public string Autorizacion { get; set; }


    }
}
