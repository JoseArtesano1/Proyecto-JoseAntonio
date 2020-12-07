using ProyectoJose.Modelo;
using ProyectoJose.Services;
using ProyectoJose.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoJose.VistasTrabajo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoJose.VistasTrabajo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoTrabajador : ContentPage
    {
        ModuloPlantilla moduloPlantilla = new ModuloPlantilla();
        ModuloGeneral moduloGeneral = new ModuloGeneral();
            
        public NuevoTrabajador()
        {
            InitializeComponent();
        }


        
       

      protected  async void Save_Clicked(System.Object sender, System.EventArgs e)
        {    
            try
            {
                DateTime F_medico= FMed.Date;
                DateTime F_dni = FDni.Date;
                DateTime F_alta= FAltas.Date;

                using (var Context = new PruebaContext())
                {
                    if (!string.IsNullOrWhiteSpace(dni.Text) && !moduloGeneral.isnumeric(dni.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(nombre.Text) && !moduloGeneral.isnumeric(nombre.Text))
                        {
                            if (!string.IsNullOrWhiteSpace(direccion.Text) && !moduloGeneral.isnumeric(direccion.Text))
                            {
                                if (!string.IsNullOrWhiteSpace(telefono.Text) && moduloGeneral.isnumeric(telefono.Text))
                                {
                                    if (!string.IsNullOrWhiteSpace(numeross.Text) && moduloGeneral.isnumericLong(numeross.Text))
                                    {                                       
                                            var currante = new Trabajador { Dni = dni.Text.ToUpper(), Nombre = nombre.Text, Direccion =                          direccion.Text, 
                                                Telefono = int.Parse(telefono.Text), Nseguridads = long.Parse(numeross.Text), 
                                                FechaAlta= moduloPlantilla.ObtenerFecha(F_alta), FechaMedico =                                         moduloPlantilla.ObtenerFecha(F_medico),
                                                FechaDni = moduloPlantilla.ObtenerFecha(F_dni)
                                            };

                                            var t1 = Context.Trabajadors.Where(x => x.Dni == dni.Text.ToUpper()).FirstOrDefault();

                                            if (t1 == null)
                                            {
                                                await Context.Trabajadors.AddAsync(currante);
                                                await Context.SaveChangesAsync();
                                            dni.Text = "";
                                            nombre.Text = "";
                                            direccion.Text = "";
                                            telefono.Text = "";
                                            numeross.Text = "";
                                            FMed.Date = DateTime.Today;
                                            FDni.Date = DateTime.Today;
                                            FAltas.Date = DateTime.Today;
                                            
                                        }
                                            else { await DisplayAlert("Nuevo Trabajador", "Repetido Trabajador", "Ok"); }
                                        
                                    }
                                    else { await DisplayAlert("Alerta", " Introduce Número Seguridad Social", "OK"); }
                                }
                                else { await DisplayAlert("Alerta", " Introduce Teléfono", "OK"); }

                            }
                            else { await DisplayAlert("Alerta", " Introduce Dirección", "OK"); }

                        }
                        else { await DisplayAlert("Alerta", " Introduce Nombre", "OK"); }
                    }
                    else { await DisplayAlert("Alerta", " Introduce Dni", "OK"); }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
                    
            
        }

        async void Cancel_Clicked(System.Object sender, System.EventArgs e)
        {           
            await Navigation.PushModalAsync(new Vistas.DatosTrabPag());
        }

         void btn_limpiar(System.Object sender, System.EventArgs e)
        {
            //moduloGeneral.LimpiarTrabajador(dni.Text, nombre.Text, direccion.Text, telefono.Text, numeross.Text);
            dni.Text = "";
            nombre.Text = "";
            direccion.Text = "";
            telefono.Text = "";
            numeross.Text = "";
            FMed.Date = DateTime.Today;
            FDni.Date = DateTime.Today;
            FAltas.Date = DateTime.Today;
        }


    }
}