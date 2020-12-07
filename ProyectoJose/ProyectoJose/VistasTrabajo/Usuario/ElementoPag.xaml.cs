using ProyectoJose.Modelo;
using ProyectoJose.Services;
using ProyectoJose.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoJose.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ElementoPag : ContentPage
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        int IdUsuario;
        public ElementoPag(int idUsuario)
        {
            InitializeComponent();
            IdUsuario = idUsuario;

            this.BindingContext = new RootModel
            {
                IsStopVisible = moduloGeneral.Autorizacion()

            };
        }

        async void btn_actualizar(System.Object sender, System.EventArgs e)
        {
            
            T_Registro t_Registro;

            using (var Context = new PruebaContext())
                {
               

                t_Registro = Context.T_Registros.Where(d => d.IdUsuario == IdUsuario).First();

                if ((!string.IsNullOrWhiteSpace(Nombre.Text)) && !moduloGeneral.isnumeric(Nombre.Text))
                {

                    if ((!string.IsNullOrWhiteSpace(Usuario.Text)) && !moduloGeneral.isnumeric(Usuario.Text))
                    {

                        if ((!string.IsNullOrWhiteSpace(Contrasenia.Text)) && !moduloGeneral.isnumeric(Contrasenia.Text))
                        {
                            if ((!string.IsNullOrWhiteSpace(Autoriza.Text)) && !moduloGeneral.isnumeric(Autoriza.Text))
                            {

                                if (moduloGeneral.ObtenerUsuario(Usuario.Text, Contrasenia.Text) == null)
                                {
                                    t_Registro.Nombre = Nombre.Text;
                                    t_Registro.Usuario = Usuario.Text;
                                    t_Registro.Contrasenia = Contrasenia.Text;
                                    t_Registro.Autorizacion = Autoriza.Text;


                                    // blogContext.SaveChanges();

                                    await Context.SaveChangesAsync();

                                }
                                else { await DisplayAlert("Gestión Trabajadores", "Repetido su usuario/contraseña", "Ok"); }


                            } else { await DisplayAlert("Alert", "introduce Autorización", "OK"); }

                            }
                            else { await DisplayAlert("Alert", "no deben ser solo números", "OK"); }

                    } else { await DisplayAlert("Alert", "Introduce un usuario", "OK"); }

                }
                else { await DisplayAlert("Alert", "Introduce un Nombre", "OK"); }
                   
            }

         //   await Navigation.PopModalAsync();

        }

        async void btn_eliminar(object sender, EventArgs e)
        {
            T_Registro t_Registro;
            using (var Context = new PruebaContext())
            {
                t_Registro = Context.T_Registros.Where(d => d.IdUsuario == IdUsuario).First();
                var booleanAnswer = await DisplayAlert("Eliminar", "¿Estas seguro?", "Si", "No");
                if (booleanAnswer) {

                    Context.T_Registros.Remove(t_Registro);
                await Context.SaveChangesAsync();
                }
               
            }
        }

        protected  override void OnAppearing()
        {
            T_Registro t_Registro;

            using (var Context = new PruebaContext())
            {
                t_Registro = Context.T_Registros.Where(d => d.IdUsuario == IdUsuario).First();
                Nombre.Text= t_Registro.Nombre;
                Usuario.Text = t_Registro.Usuario; 
                Contrasenia.Text = t_Registro.Contrasenia;
                Autoriza.Text = t_Registro.Autorizacion;
            }
        }




    }
}