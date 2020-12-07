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
    public partial class TipoDiasPag : ContentPage, INotifyPropertyChanged
    {
        public TipoDiasPag()
        {
            InitializeComponent();
        }

        protected  override void OnAppearing()
        {
            ListaTipoDias.SelectedItem = null;

            using (var blogContext = new PruebaContext())
            {
               //await InsertStartData(blogContext);

                var tipos = blogContext.TipoDias.ToList();

                ListaTipoDias.ItemsSource = tipos;

                //await DisplayAlert("Alerta", " correcto", "OK");
            }
        }


        async void TipoCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.CurrentSelection.FirstOrDefault() is TipoDia tipoDia))
                return;

            var tipoPag = new CambioTipoDia(tipoDia.IdTipoDia);
            await Navigation.PushAsync(tipoPag);
        }



    }
}