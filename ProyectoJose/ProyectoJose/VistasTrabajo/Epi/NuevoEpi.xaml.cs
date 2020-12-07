using ProyectoJose.Modelo;
using ProyectoJose.Services;
using ProyectoJose.Vistas;
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
    public partial class NuevoEpi : ContentPage
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        public NuevoEpi()
        {
            InitializeComponent();
        }

        protected async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (var Context = new PruebaContext())
                {
                    if ((!string.IsNullOrWhiteSpace(nombreCell.Text)) && (!moduloGeneral.isnumeric(nombreCell.Text)))
                    {
                        var newEpi = new Epi
                        {
                            Nombre = nombreCell.Text.ToUpper(),
                           
                        };


                        var e1 = Context.Epis.Where(x => x.Nombre == nombreCell.Text.ToUpper()).FirstOrDefault();

                        if (e1 == null)
                        {
                            await Context.Epis.AddAsync(newEpi);

                            await Context.SaveChangesAsync();
                            await Navigation.PopModalAsync();

                        }
                        else { await DisplayAlert("Nuevo Epi", "Epi repetido", "Ok"); }


                    }
                    else { await DisplayAlert("Alerta", "Introduce Epi", "Ok"); }

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            
        }


        protected async void Cancel_Clicked(object sender, EventArgs e)

        {
            await Navigation.PopModalAsync();
            
        }


    }
}