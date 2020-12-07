using ProyectoJose.Modelo;
using ProyectoJose.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoJose.VistasTrabajo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrabajadorDiaPag : ContentPage
    {
        int IdTrabajador;
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        public TrabajadorDiaPag(int idTrabajador)
        {
            InitializeComponent();
            IdTrabajador = idTrabajador;
            listaDia.Text = "LISTADO DE DIAS:  " + moduloGeneral.ObtenerTrabajadorSinCtxt(idTrabajador).Nombre.ToUpper();
        }

        protected override void OnAppearing()
        {

            using (var blogContext = new PruebaContext())
            {
                // consulta para los cursos del trabajador
                  diasCollection.ItemsSource =GetListadoDias(IdTrabajador);

            }
        }

        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NuevoTrabDia(IdTrabajador)));
        }



      

        async void btn_borrar(object sender, EventArgs e)
        {
            Periodo periodo;
            using (var blogContext = new PruebaContext())
            {
                var button = sender as Button;
               
                object dia = new object();
                dia = button?.BindingContext;  // contiene las propiedades de periodo
                
                int id = 0;

                var trabinfoListDia = blogContext.Periodos
                 .Join(blogContext.TipoDias,
                     f => f.IdTipoDia,
                     s => s.IdTipoDia,
                     (f, s) =>
                     new
                     { 
                         FechaPrimera = DateTime.Parse (f.FechaInicio).ToString("dd/MM/yyyy"),
                         FechaSegunda = DateTime.Parse(f.FechaFin).ToString("dd/MM/yyyy"),
                         IdTrdor = f.IdTrabajador,
                         NombreTipo = s.Denominacion,
                         Total = moduloGeneral.TotaldiasNaturales(f.FechaFin, f.FechaInicio),
                         TotalNoLaboral = moduloGeneral.TotaldiasLaboral(f.FechaFin, f.FechaInicio, s.Denominacion),
                         DineroDia = s.Importe,
                         ImporteDias = moduloGeneral.TotaldImporte(moduloGeneral.TotaldiasNaturales(f.FechaFin, f.FechaInicio), s.Importe),
                         Idperiodo=f.IdPeriodo,
                     }

                ).Where(r => r.IdTrdor == IdTrabajador).ToList();


                foreach (var valor in trabinfoListDia)
                {
                    if (valor.Equals(dia))
                    {
                       id = valor.Idperiodo;
                    }
                }

                periodo= blogContext.Periodos
                    .Where(p=> p.IdPeriodo==id).First();

                blogContext.Remove(periodo);  

                await blogContext.SaveChangesAsync();

                diasCollection.ItemsSource = GetListadoDias(IdTrabajador);

            }


        }

        private  ArrayList GetListadoDias(int IdTrabajador)
        {
            ArrayList ListaDias = new ArrayList();
            using (var blogContext = new PruebaContext())
            {
                var trabinfoListDia = blogContext.Periodos
                 .Join(blogContext.TipoDias,
                     f => f.IdTipoDia,
                     s => s.IdTipoDia,
                     (f, s) =>
                     new
                     {
                         FechaPrimera = DateTime.Parse(f.FechaInicio).ToString("dd/MM/yyyy"),
                         FechaSegunda = DateTime.Parse(f.FechaFin).ToString("dd/MM/yyyy"),
                         IdTrdor = f.IdTrabajador,
                         NombreTipo = s.Denominacion,
                         Total = moduloGeneral.TotaldiasNaturales(f.FechaFin, f.FechaInicio),
                         TotalNoLaboral = moduloGeneral.TotaldiasLaboral(f.FechaFin, f.FechaInicio, s.Denominacion),
                         DineroDia = s.Importe,
                         ImporteDias = moduloGeneral. TotaldImporte(moduloGeneral.TotaldiasNaturales(f.FechaFin, f.FechaInicio), s.Importe),
                         Idperiodo = f.IdPeriodo,
                     }

                ).Where(r => r.IdTrdor == IdTrabajador).ToArray();

                foreach (var item in trabinfoListDia)
                {
                    ListaDias.Add(item);
                }

            }

            return ListaDias;

        }



    }
}