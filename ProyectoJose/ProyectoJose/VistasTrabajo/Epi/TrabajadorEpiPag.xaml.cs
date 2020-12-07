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
    public partial class TrabajadorEpiPag : ContentPage
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        int IdTrabajador;
        public TrabajadorEpiPag(int idTrabajador)
        {
            InitializeComponent();
            IdTrabajador = idTrabajador;
            listaEpi.Text = "EPIS TRABAJADOR:  " + moduloGeneral.ObtenerTrabajadorSinCtxt(idTrabajador).Nombre.ToUpper();

        }



        protected override void OnAppearing()
        {
                         
                episCollection.ItemsSource =moduloGeneral.GetListadoEpi(IdTrabajador);
    
        }


        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NuevoTrabEpi(IdTrabajador)));
        }



        async void btn_borrar(object sender, EventArgs e)
        {
            TrabajadorEpi trabajadorEpi;

            using(var pruebaContext= new PruebaContext())
            {
                var button = sender as Button;

                // obtengo todo el contenido de lo seleccionado como un objeto con propiedades
                object listaepi = new object();
                listaepi = button?.BindingContext;
                int identificador = 0;
                string fecha = "";

                // obtengo las propiedades de toda la pagina
                var totalEpis = 
                    
                    pruebaContext.TrabajadorEpis
                   .Join(pruebaContext.Epis,
                    f => f.IdEpi,
                    s => s.IdEpi,
                    (f, s) =>
                    new
                    {
                        nombreEpi = s.Nombre,
                        Idepi = s.IdEpi,
                        IdTrdor = f.IdTrabajador,
                        FechaE = DateTime.Parse( f.FechaEpi).ToString("dd/MM/yyyy")
                    }

               ).Where(r => r.IdTrdor == IdTrabajador).ToList();

                // comparo el objeto seleccionado con la pagina y saco la propiedad cuando son iguales
                foreach(var item in totalEpis)
                {
                    if(item.Equals(listaepi))
                    {
                        identificador = item.Idepi;
                        fecha = item.FechaE;
                    }

                }

                // con la propiedad busco el objeto de la tabla
                trabajadorEpi= pruebaContext.TrabajadorEpis
                    .Where(ce => ce.IdTrabajador == IdTrabajador && ce.IdEpi == identificador).First();
                 

                pruebaContext.Remove(trabajadorEpi);

                await pruebaContext.SaveChangesAsync();

                episCollection.ItemsSource =moduloGeneral.GetListadoEpi(IdTrabajador);
            }

        }

    }
}