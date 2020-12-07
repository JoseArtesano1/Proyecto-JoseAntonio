using ProyectoJose.Modelo;
using ProyectoJose.Services;
using ProyectoJose.Vistas;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoJose.VistasTrabajo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModInfoTrabajador : ContentPage
    {
        ModuloPlantilla moduloPlantilla = new ModuloPlantilla();
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        int IdTrabajador;
        public ModInfoTrabajador(int idTrabajador)
        {
            InitializeComponent();
            IdTrabajador = idTrabajador;
          
        }

        protected override void OnAppearing()
        {
            Trabajador trabajador;

            using (var Context = new PruebaContext())
            {
                trabajador = moduloGeneral.ObtenerTrabajador(IdTrabajador, Context);
                nombre.Text = trabajador.Nombre;
                dni.Text = trabajador.Dni;
                seguridad.Text = Convert.ToString( trabajador.Nseguridads);
                direccion.Text = trabajador.Direccion;
                telefono.Text = Convert.ToString(trabajador.Telefono);
                FAlta.Date = DateTime.Parse(trabajador.FechaAlta);
                Fmedico.Date = DateTime.Parse (trabajador.FechaMedico);
                
                Fdni.Date = DateTime.Parse(trabajador.FechaDni);
            
            }
        }

        
        //DateTime ultima;

        //async void Fecha_Selected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        //{
        //    ultima = e.NewDate;
            
        //    await DisplayAlert("DatePicker", "fecha seleccionada", "Aceptar");
        //}

        
      

        async void btn_actualizar(System.Object sender, System.EventArgs e)
        {
            Trabajador trabajador;
            DateTime F_med=Fmedico.Date;
            DateTime F_dni= Fdni.Date;
            DateTime F_alta = FAlta.Date;

            using (var Context = new PruebaContext())
            {               
                if(!string.IsNullOrWhiteSpace(nombre.Text) && !moduloGeneral.isnumeric(nombre.Text))
                {
                    if (!string.IsNullOrWhiteSpace(dni.Text) && !moduloGeneral.isnumeric(dni.Text))
                    {

                        if (!string.IsNullOrWhiteSpace(seguridad.Text) && moduloGeneral.isnumericLong(seguridad.Text))
                        {

                            if (!string.IsNullOrWhiteSpace(direccion.Text) && !moduloGeneral.isnumeric(direccion.Text))
                            {

                                if (!string.IsNullOrWhiteSpace(telefono.Text) && moduloGeneral.isnumeric(telefono.Text))
                                {
                                    
                                    trabajador = moduloGeneral.ObtenerTrabajador(IdTrabajador, Context);
                                    trabajador.Nombre = nombre.Text;
                                    trabajador.Dni = moduloGeneral.ControlarDni(IdTrabajador,Context,dni.Text.ToUpper());
                                    trabajador.Nseguridads = long.Parse(seguridad.Text);
                                    trabajador.Direccion = direccion.Text;
                                    trabajador.Telefono = int.Parse(telefono.Text);
                                    trabajador.FechaAlta = moduloPlantilla.ObtenerFecha(moduloPlantilla.CheckfechaModifAlta(Context, IdTrabajador, F_alta));
                                  
                                    trabajador.FechaMedico = moduloPlantilla.ObtenerFecha(F_med);
                                    trabajador.FechaDni = moduloPlantilla.ObtenerFecha(F_dni);

                                    await Context.SaveChangesAsync();
                                  //  moduloGeneral.LimpiarTrabajador(dni.Text, nombre.Text, direccion.Text, telefono.Text, seguridad.Text);
                                }
                                else { await DisplayAlert("Alerta", "Introduce un teléfono", "Ok"); }

                            }
                            else { await DisplayAlert("Alerta", "Introduce una dirección", "Ok"); }

                        }
                        else { await DisplayAlert("Alerta", "Introduce número de Seguridad Social", "Ok"); }

                    } else { await DisplayAlert("Alerta", "Introduce Dni/Nie", "Ok"); }

                }
                else { await DisplayAlert("Alerta", "Introduce nombre", "Ok"); }

              
            }
          //  await Navigation.PopModalAsync();
        }

        async void btn_eliminar(object sender, EventArgs e)
        {
            Trabajador trabajador;
           // TrabajadorCurso trabajadorCurso;
            using (var Context = new PruebaContext())
            {
                var te = Context.TrabajadorEpis.Where(et => et.IdTrabajador == IdTrabajador).FirstOrDefault();
                if(te==null)
                { 
                var tc = Context.TrabajadorCursos.Where(x => x.IdTrabajador == IdTrabajador).FirstOrDefault();
                if (tc==null) {
                    var td = Context.Periodos.Where(y => y.IdTrabajador == IdTrabajador).FirstOrDefault();

                    if (td == null) 
                    { 
                    trabajador = moduloGeneral.ObtenerTrabajador(IdTrabajador, Context);
                            var booleanAnswer = await DisplayAlert("Eliminar", "¿Estas seguro?", "Si", "No");

                    if (booleanAnswer)
                    {
                        Context.Trabajadors.Remove(trabajador);
                               
                        await Context.SaveChangesAsync();
                                await Navigation.PushModalAsync(new Vistas.DatosTrabPag());
                    }

                    }
                    else { await DisplayAlert("No Eliminar Trabajador", "Trabajador con días", "Ok"); }
                }
                else { await DisplayAlert("No Eliminar Trabajador", "Trabajador con curso", "Ok"); }

                }
                else { await DisplayAlert("No Eliminar Trabajador", "Trabajador con epi", "Ok"); }
            }

         //   await Navigation.PopModalAsync();
        }

        async void Salir_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
            
        }

    }
}