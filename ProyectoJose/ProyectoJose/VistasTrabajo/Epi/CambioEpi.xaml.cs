using ProyectoJose.Modelo;
using ProyectoJose.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoJose.VistasTrabajo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CambioEpi : ContentPage
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        int IdEpi;
        public CambioEpi(int idEpi)
        {
            InitializeComponent();
            IdEpi = idEpi;
        }

        protected override void OnAppearing()
        {
            Epi epi;

            using (var blogContext = new PruebaContext())
            {                
                epi = blogContext.Epis.Where(d => d.IdEpi == IdEpi).First();
                nombre.Text = epi.Nombre;
                
            }
        }


        async void btn_actualizar(object sender, EventArgs e)
        {
            Epi epi;
            using (var Context= new PruebaContext())
            {
                if (!string.IsNullOrWhiteSpace(nombre.Text) && !moduloGeneral.isnumeric(nombre.Text)) 
                { 
                    var checkEpi = Context.Epis.Where(x => x.Nombre == nombre.Text.ToUpper()).FirstOrDefault();

                if (checkEpi==null) { 
                epi = Context.Epis.Where(x => x.IdEpi == IdEpi).First();
                epi.Nombre = nombre.Text.ToUpper();

                await Context.SaveChangesAsync();
                }
                else { await DisplayAlert("Sin Modificar Epi", "Epi ya existe", "Ok"); }

                }
                else { await DisplayAlert("Alerta", "Introduce nombre del epi", "Ok"); }
            }

        }



        async void btn_eliminar(object sender, EventArgs e)
        {
            Epi epi;

            using(var Context = new PruebaContext())
            {
                var te = Context.TrabajadorEpis.Where(ep => ep.IdEpi == IdEpi).FirstOrDefault();

                if(te==null)
                {
                    epi = Context.Epis.Where(eps => eps.IdEpi == IdEpi).First();
                    var booleanAnswer = await DisplayAlert("Eliminar", "¿Estas seguro?", "Si", "No");
                    if(booleanAnswer)
                    {
                        Context.Epis.Remove(epi);
                        await Context.SaveChangesAsync();
                        await Navigation.PopAsync();
                    }
                }
                else { await DisplayAlert("Alerta", "Existen trabajadores con el epi", "Ok"); }

            }


        }



    }
}