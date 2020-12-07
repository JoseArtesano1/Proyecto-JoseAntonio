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

namespace ProyectoJose.VistasTrabajo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrabajadoresPag : ContentPage, INotifyPropertyChanged
    {
        

        public TrabajadoresPag()
        {
            InitializeComponent();
        }

        protected  override void OnAppearing()
        {
            trabCollectionView.SelectedItem = null;

            using (var Context = new PruebaContext())
            {
            
                var mistrabajadores = Context.Trabajadors.ToList();
         
                trabCollectionView.ItemsSource = mistrabajadores;
            }
        }

       

        async void trabCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.CurrentSelection.FirstOrDefault() is Trabajador trabajador))
                return;

            var datosTrabaj = new DatosTrabPag(trabajador.IdTrabajador);
            await Navigation.PushAsync(datosTrabaj);  
        }

        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NuevoTrabajador()));
        }

       

    }
}