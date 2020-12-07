using ProyectoJose.Modelo;
using ProyectoJose.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoJose.VistasTrabajo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EpisPag : ContentPage, INotifyPropertyChanged
    {
        public EpisPag()
        {
            InitializeComponent();

        }

        protected  override void OnAppearing()
        {
            episCollection.SelectedItem = null;
            using (var pruebContext = new PruebaContext())
            {                
                var episList = pruebContext.Epis.ToList();

                episCollection.ItemsSource = episList;
            }
        }


        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NuevoEpi()));
        }

        async void EpiCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.CurrentSelection.FirstOrDefault() is Epi epi))
                return;

            var epis = new CambioEpi(epi.IdEpi);
            await Navigation.PushAsync(epis);
        }

    }
}