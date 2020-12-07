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
    public partial class CursosPag : ContentPage, INotifyPropertyChanged
    {
        public CursosPag()
        {
            InitializeComponent();
        }

        protected  override void OnAppearing()
        {
            cursoCollection.SelectedItem = null;
            using (var pruebContext = new PruebaContext())
            {
               
                var cursoList = pruebContext.Cursos.ToList();

                cursoCollection.ItemsSource = cursoList;
            }
        }

        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NuevoCurso()));
        }

     

        async void CursoCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)

        {
            if(!(e.CurrentSelection.FirstOrDefault() is Curso curso))
            
                return;
                var cursos = new CambioCurso(curso.IdCurso);
                await Navigation.PushAsync(cursos);
            
        }



    }
}