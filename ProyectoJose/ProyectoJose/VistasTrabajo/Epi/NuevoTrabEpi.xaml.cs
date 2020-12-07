using ProyectoJose.Modelo;
using ProyectoJose.Services;
using ProyectoJose.VistaModelo;
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
    public partial class NuevoTrabEpi : ContentPage
    {
        ModuloPlantilla moduloPlantilla = new ModuloPlantilla();
        ModuloGeneral moduloGeneral = new ModuloGeneral();
     
        int IdTrabajador;
        public NuevoTrabEpi(int trabajadorId)
        {
            InitializeComponent();
            
            IdTrabajador = trabajadorId;
            Fentrega.BackgroundColor=Color.White;
           

            this.BindingContext = new RootModel
            {
                EpiList =moduloGeneral.GetEpis()

            };

        }



       
        

        protected async void Save_Clicked(object sender, EventArgs e)
        {
            int IdEpi=0;
            int selectedIndex = picker.SelectedIndex;
            string fechaMostrar = moduloPlantilla.ObtenerFecha(Fentrega.Date); // convertimos la fecha del datepicker a string 

            DateTime primera = Fentrega.Date; // fecha tomada directamente del datepicker


           
          
            try
            {
                using (var blogContext = new PruebaContext())
               {  
                    if (selectedIndex != -1)
                    {
                      var itemsede = (Epi)picker.ItemsSource[selectedIndex];
                       IdEpi = itemsede.IdEpi;
                   
                        if (moduloPlantilla.ComprobarAlta(primera, blogContext, IdTrabajador))
                        {

                            var eT = blogContext.TrabajadorEpis.Where(x => x.IdEpi == IdEpi 
                            && x.IdTrabajador == IdTrabajador).ToList();
                            if (eT.Count == 0)
                            {
                                var nuevoTrabEpi = new TrabajadorEpi
                                {
                                    IdTrabajador = IdTrabajador,
                                    IdEpi = IdEpi,
                                    FechaEpi = fechaMostrar

                                };

                                await blogContext.TrabajadorEpis.AddAsync(nuevoTrabEpi);

                                await blogContext.SaveChangesAsync();
                            }
                            else
                            {
                                await DisplayAlert("Asignación Epi", "Epi ya existe", "Ok");
                            }

                        }else { await DisplayAlert("Asignación Epi", "la fecha debe ser superior o igual al alta", "Ok"); }
                    }
                    else { await DisplayAlert("Asignación Epi", "Epi no seleccionado", "Ok"); }

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            await Navigation.PopModalAsync();
        }

       


        protected async void Cancel_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new MenuPage());
            await Navigation.PopModalAsync();
        }


    }
}