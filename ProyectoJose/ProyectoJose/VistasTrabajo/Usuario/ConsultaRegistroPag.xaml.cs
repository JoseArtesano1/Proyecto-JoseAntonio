using ProyectoJose.Modelo;
using ProyectoJose.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoJose.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistroPag : ContentPage, INotifyPropertyChanged
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        public ConsultaRegistroPag()
        {
            InitializeComponent();
        }

        protected  override void OnAppearing()
        {
            ListaUsuarios.SelectedItem = null;

            using (var Context = new PruebaContext())
            {
                if (moduloGeneral.ObtenerCategoria() == "A")
                { 
                var registros = Context.T_Registros.ToList();

                ListaUsuarios.ItemsSource = registros;
                }
                else 
                { var registro = Context.T_Registros.Where(x => x.IdUsuario == Constants.Id_usuario).ToList();
                    ListaUsuarios.ItemsSource = registro;
                }
            }
        }


     

        async void registroCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.CurrentSelection.FirstOrDefault() is T_Registro t_Registro))
                return;

            var idusuarioPage = new ElementoPag(t_Registro.IdUsuario);
            await Navigation.PushAsync(idusuarioPage);
        }


    }
}