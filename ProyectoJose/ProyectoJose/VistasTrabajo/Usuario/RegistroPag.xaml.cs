using ProyectoJose.Modelo;
using ProyectoJose.Services;
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
    public partial class RegistroPag : ContentPage
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        public RegistroPag()
        {
            InitializeComponent();
        }

        async void Btn_agregar(System.Object sender, System.EventArgs e)
        {
            var usuario = new T_Registro { Nombre = Nombre.Text, Usuario = Usuario.Text,
                Contrasenia = Contrasenia.Text, Autorizacion=Autoriza.Text };

            try
            {
                using (var Context = new PruebaContext())
                {
                    if (!string.IsNullOrWhiteSpace(Nombre.Text)&& !moduloGeneral.isnumeric(Nombre.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(Usuario.Text) && !moduloGeneral.isnumeric(Nombre.Text))
                        {
                            if (!string.IsNullOrWhiteSpace(Contrasenia.Text) && !moduloGeneral.isnumeric(Contrasenia.Text))
                            {
                                // var d1 = blogContext.T_Registros.Where(x => x.Usuario == Usuario.Text && x.Contrasenia == Contrasenia.Text).FirstOrDefault();
                                if (!string.IsNullOrWhiteSpace(Autoriza.Text) && !moduloGeneral.isnumeric(Autoriza.Text))
                                {

                                    if (moduloGeneral.ObtenerUsuario(Usuario.Text, Contrasenia.Text) == null)

                                    {
                                        await Context.T_Registros.AddAsync(usuario);

                                        await Context.SaveChangesAsync();
                                        Nombre.Text = "";
                                        Usuario.Text = "";
                                        Contrasenia.Text = "";
                                        Autoriza.Text = "";
                                        
                                    }
                                    else { await DisplayAlert("Gestión Trabajadores", "Repetido su usuario/contraseña", "Ok"); }

                                }
                                else { await DisplayAlert("Alert", "introduce Autorización", "OK"); }

                            }
                            else { await DisplayAlert("Alert", "no deben ser solo números", "OK"); }
                        }
                        else { await DisplayAlert("Alert", "Introduce un usuario", "OK"); }

                    }
                    else { await DisplayAlert("Alert", "Introduce un Nombre", "OK"); }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            //  await Navigation.PopModalAsync();

        }

    

    }
}