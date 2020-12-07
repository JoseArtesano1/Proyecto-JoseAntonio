using DocumentFormat.OpenXml.Wordprocessing;
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
    public partial class DatosTrabPag : ContentPage
    {
        int IdTrabajador;
        public DatosTrabPag(int idTrabajador)
        {
            InitializeComponent();
            IdTrabajador = idTrabajador;
        }



        protected  override void OnAppearing()
        {
            Trabajador trabajador;
           
                using (var Context = new PruebaContext())
            {
                    if (Context.Trabajadors.Where(d => d.IdTrabajador == IdTrabajador).ToList().Count != 0)
                    {

                       trabajador = Context.Trabajadors.Where(d => d.IdTrabajador == IdTrabajador).First();

                        nombre.Text = trabajador.Nombre;
                        direccion.Text = trabajador.Direccion;
                        telefono.Text = Convert.ToString(trabajador.Telefono);
                        fechaAlta.Text = DateTime.Parse(trabajador.FechaAlta).ToString("dd/MM/yyyy");
                        fechadni.Text = DateTime.Parse(trabajador.FechaDni).ToString("dd/MM/yyyy");
                        dni.Text = trabajador.Dni;
                        seguridad.Text = Convert.ToString(trabajador.Nseguridads);
                        fechamedico.Text = DateTime.Parse(trabajador.FechaMedico).ToString("dd/MM/yyyy");
                    }
                    else
                {  //TODO
                    nombre.Text = "";
                    direccion.Text = "";
                    telefono.Text = null;
                    fechaAlta.Text = "";
                    fechadni.Text = "";
                    dni.Text = "";
                    seguridad.Text = "";
                    fechamedico.Text = "";
                   
                }
            }

           
        }


        async void ToolbarItem_Clicked1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ModInfoTrabajador(IdTrabajador)));
        }


        private void btn_cursos(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new TrabajadorCursoPag(IdTrabajador));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_epis(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new TrabajadorEpiPag(IdTrabajador));
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void btn_dias(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new TrabajadorDiaPag(IdTrabajador));
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}