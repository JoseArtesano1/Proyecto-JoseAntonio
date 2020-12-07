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
    public partial class LoginPag : ContentPage
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        public LoginPag()
        {
            InitializeComponent();

             moduloGeneral.InsertStartData();
            
        }


        async void btn_login(object sender, EventArgs e)
        { 
            try
            {
                T_Registro registro;

               
               if( moduloGeneral.ObtenerUsuario(usuario.Text, contra.Text)!=null)
                {
                    registro =  moduloGeneral.ObtenerUsuario(usuario.Text, contra.Text);
                    
                    Constants.Id_usuario = registro.IdUsuario;
                    NavigationPage.SetHasBackButton(this, false);   // quita la flecha back
                    await Navigation.PushAsync(new DatosTrabPag());
                }
                else
                {
                    await DisplayAlert("Alerta", "Verifique su usuario/contraseña", "Ok");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    

    

    }
}