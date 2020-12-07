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
    public partial class NuevoCurso : ContentPage
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        public NuevoCurso()
        {
            InitializeComponent();
        }

        protected async void Save_Clicked(object sender, EventArgs e)
        {           
            try
            {               
                using (var Context = new PruebaContext())
                {
                    if (!string.IsNullOrWhiteSpace(nombreCell.Text) && !moduloGeneral.isnumeric(nombreCell.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(horasCell.Text) && moduloGeneral.isnumeric(horasCell.Text))
                        {

                            var newCurso = new Curso
                            {
                                Nombre = nombreCell.Text.ToUpper(),
                                Duracion = int.Parse(horasCell.Text)
                            };

                            var c1 = Context.Cursos.Where(x => x.Nombre == nombreCell.Text.ToUpper()
                            && x.Duracion == int.Parse(horasCell.Text)).FirstOrDefault();

                            if (c1 == null)
                            {
                                await Context.Cursos.AddAsync(newCurso);
                                await Context.SaveChangesAsync();
                                nombreCell.Text = "";
                                horasCell.Text = "";
                                await Navigation.PopModalAsync();

                            }
                            else { await DisplayAlert("Nuevo Curso", "Curso repetido", "Ok"); }

                        }
                        else { await DisplayAlert("Alerta", "Introduce horas", "Ok"); }
                    
                    }
                    else { await DisplayAlert("Nuevo Curso", "Introduce un curso", "Ok"); }

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

          //  await Navigation.PopModalAsync();
        }

      

       protected async void Cancel_Clicked(object sender, EventArgs e)
        {
            // await Navigation.PushModalAsync(new MenuPage( ));
            await Navigation.PopModalAsync();
        }
     


    }
}