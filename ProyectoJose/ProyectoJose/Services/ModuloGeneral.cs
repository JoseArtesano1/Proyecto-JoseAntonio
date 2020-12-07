using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProyectoJose.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoJose.Services
{
   public class ModuloGeneral
    {
        #region métodos operación días
        public int TotaldiasNaturales(string fecha1, string fecha2)
        {
            TimeSpan v = TimeSpan.Zero;
            var total = v;

            if (fecha1 != null && fecha2 != null)
            {
                var date = DateTime.Parse(fecha1);
                var date1 = DateTime.Parse(fecha2);
                var total1 = date - date1;
                total = total1;
            }
            else
            {
                var total1 = TimeSpan.Zero;
                total = total1;
            }

            return total.Days + 1;
        }


        public  int TotaldiasLaboral(string fecha1, string fecha2, string tipo)

        {           
            TimeSpan v = TimeSpan.Zero;  // damos un valor 0
            var total = v;
            int valorFinal = 0;

            // comprobar que lo que contamos es del tipo vacaciones

            if (tipo.StartsWith("Va"))
            {
                if (fecha1 != null && fecha2 != null)
                {
                    // listado de dias festivos, sabados y domingos  un for para recorrerlo y un if comparar fechas
                    var date = DateTime.Parse(fecha1);
                    var date1 = DateTime.Parse(fecha2);

                    int i = 0; // numero de dias festivos
                    int b = 0; // numero de dias fin de semana

                    // recorremos la fechas dentro del perido

                    for (var fecha = DateTime.Parse(fecha2); fecha <= date; fecha = fecha.AddDays(1))
                    {                                           

                        // comprobamos fines de semana
                        if ((fecha.DayOfWeek == DayOfWeek.Saturday) || (fecha.DayOfWeek == DayOfWeek.Sunday))
                        {
                            b++;
                        }

                        // comprobamos los festivos
                        if (Festivos().Contains(fecha))
                        {
                            i++;

                        }
                    }

                    int FestivosMasSD = i + b;
                    var total1 = date - date1;
                    total = total1;
                    valorFinal = (total.Days + 1) - FestivosMasSD;
                }
                else
                {
                    var total1 = TimeSpan.Zero;
                    total = total1;
                    valorFinal = total.Days;
                }
            }

            return valorFinal;
        }
         

        public   double TotaldImporte(int numDias, double precio)
        {
            double importe = 0;

            importe = numDias * precio;

            return importe;
        }
          

        public static List<DateTime> Festivos()
        {
            List<DateTime> diasSeleccionados = new List<DateTime>();

            using (var Context = new PruebaContext())
            {
                var listaFestivos = Context.Festivos.ToList();

                foreach (var item in listaFestivos)
                {
                    // convertimos la lista de festivos en string en date
                    var fechaconvertida = DateTime.Parse(item.FechaFestivo);
                    diasSeleccionados.Add(fechaconvertida);
                }
            }

            return diasSeleccionados;
        }

        #endregion

        #region Carga combox  

        public List<object> GetListadoEpi(int IdTrabajador)
        {
            List<object> Listado = new List<object>();
            using (var Context = new PruebaContext())
            {
                // consulta para los epis del trabajador

                var totalEpis = Context.TrabajadorEpis
                    .Join(Context.Epis,
                     f => f.IdEpi,
                     s => s.IdEpi,
                     (f, s) =>
                     new
                     {
                         nombreEpi = s.Nombre,
                         Idepi = s.IdEpi,
                         IdTrdor = f.IdTrabajador,
                         FechaE= DateTime.Parse(f.FechaEpi).ToString("dd/MM/yyyy")
                     }

                ).Where(r => r.IdTrdor == IdTrabajador).ToList();

                foreach (var item in totalEpis)
                {
                    Listado.Add(item);
                }

            }
            return Listado;

        }


        public List<Epi> GetEpis()
        {
            List<Epi> listado;
            using (var Context = new PruebaContext())
            {
                var ListadoEpis = Context.Epis.ToList();

                listado = new List<Epi>();

                foreach (var item in ListadoEpis)
                {
                    listado.Add(item);
                }

            }

            return listado;

        }


        public List<Curso> GetCursos()
        {
            List<Curso> ListaCursos;
            using (var Context = new PruebaContext())
            {
                var miscursos = Context.Cursos.ToList();

                ListaCursos = new List<Curso>();

                foreach (var item in miscursos)
                {
                    ListaCursos.Add(item);
                }
            }

            return ListaCursos;

        }

        #endregion 

        public void LimpiarTrabajador(string txt1, string txt2, string txt3, string txt4, string txt5)
        {
            txt1 = "";
            txt2 = "";
            txt3 = "";
            txt4 = "";
            txt5 = "";
        }

        #region Control entrada datos 

        // recibe valor de un textbox e indica si es o no número
        public bool isnumeric (string txtbox)
        {
            return int.TryParse(txtbox, out int valor);

        }

        public bool isnumericDouble(string txtbox)
        {
            return double.TryParse(txtbox, out double valor);

        }

        public bool isnumericLong(string txtbox)
        {
            return long.TryParse(txtbox, out long valor);
        }

       

        public string ControlarDni(int idtrabajador, PruebaContext context,string dni )
        {
            var activos= context.Trabajadors.ToList();
            var trabajad = ObtenerTrabajador(idtrabajador, context);
            int i = 0;
            bool existe=false;

            while(!existe && i < activos.Count)
            {
                if (activos[i].Dni == dni)
                {
                    if (trabajad.Dni == dni)
                    {
                        existe = true;
                    }
                    else
                    {
                        return trabajad.Dni;
                    }

                }
              
                i++;
            }
            return dni;

        }


        public string ControlarTipoDia(int idtipo, PruebaContext context, string nombre)
        {
            var activos = context.TipoDias.ToList();
            var tipo = context.TipoDias.Where(d => d.IdTipoDia == idtipo).First();
            int i = 0;
            bool existe = false;

            while (!existe && i < activos.Count)
            {
                if (activos[i].Denominacion.Equals(nombre))
                {
                    if (tipo.Denominacion.Equals(nombre))
                    {
                        existe = true;

                    }
                    else
                    {
                        return tipo.Denominacion;
                    }

                }

                i++;
            }
            return nombre;

        }

        #endregion 

        public Trabajador ObtenerTrabajador(int identidad, PruebaContext context)
        {
            return context.Trabajadors.Where(d => d.IdTrabajador == identidad).First();
        }


        public T_Registro ObtenerUsuario(string userName1, string pwd1)
        {
            T_Registro registro;
            using (var ConexionContext = new PruebaContext())
            {
                registro = ConexionContext.T_Registros.Where(x => x.Usuario == userName1 && x.Contrasenia == pwd1)
                    .FirstOrDefault();
            }
            return registro;
        }

        public Trabajador ObtenerTrabajadorSinCtxt(int identidad)
        {            
            using (var Context = new PruebaContext()) 
            { 
                return Context.Trabajadors.Where(d => d.IdTrabajador == identidad).First();
            }
                       
        }


        public string ObtenerCategoria()
        {
            string AutorizacionLog;
            using (var ConexionContext = new PruebaContext())
            {
                AutorizacionLog = ConexionContext.T_Registros.Where(x => x.IdUsuario == Constants.Id_usuario).FirstOrDefault().Autorizacion;
            }
            return AutorizacionLog;
        }

        public bool Autorizacion()
        {
            if (ObtenerCategoria() == "A")
            {
                return true;
            }
            else { return false; }
        }

        public  void InsertStartData()
        {
            using (var ConexionContext = new PruebaContext()) { 
                var registroCount = ConexionContext.T_Registros.Count();

            if (registroCount == 0)
            {
                ConexionContext.AddAsync(new T_Registro { Nombre = "Jose", Usuario = "Jose", Contrasenia = "123j", Autorizacion = "A" });

                ConexionContext.SaveChangesAsync();
            }

            }
        }

    }
}
