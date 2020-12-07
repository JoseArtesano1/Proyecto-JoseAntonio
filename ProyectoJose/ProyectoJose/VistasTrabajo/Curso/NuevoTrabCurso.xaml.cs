using ProyectoJose.Modelo;
using ProyectoJose.Services;
using ProyectoJose.VistaModelo;
using ProyectoJose.Vistas;
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
    public partial class NuevoTrabCurso : ContentPage
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        int IdTrabajador;
        int IdCurso;
        public NuevoTrabCurso(int trabajadorId)
        {
            InitializeComponent();
            IdTrabajador = trabajadorId;

            this.BindingContext = new RootModel
            {
                CursoList = moduloGeneral.GetCursos()

            };


        }
                     

     
        protected async void Save_Clicked(object sender, EventArgs e)
        {
            
            try
            {
                using (var Context = new PruebaContext())
                {
                    // duracion.Text = blogContext.Cursos.Where(x => x.IdCurso == IdCurso).First().Duracion.ToString();
                    if (picker.SelectedIndex!=-1) 
                    { 
                        var cT = Context.TrabajadorCursos.Where(x => x.IdCurso == IdCurso && x.IdTrabajador==IdTrabajador)
                        .FirstOrDefault();
                            if (cT == null) 
                            {
                            var nuevoTrabCurso = new TrabajadorCurso
                            {
                                IdTrabajador = IdTrabajador,
                                IdCurso = IdCurso,

                            };

                            await Context.TrabajadorCursos.AddAsync(nuevoTrabCurso);
                            await Context.SaveChangesAsync();

                            }else
                            {
                                await DisplayAlert("Asignación Curso", "Curso ya existe", "Ok");
                            }

                    }
                    else { await DisplayAlert("Asignación Curso", "Curso no seleccionado", "Ok"); }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            await Navigation.PopModalAsync();
        }


        protected async void Cancel_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new MenuPage());
            await Navigation.PopModalAsync();
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var itemsedc = (Curso)picker.ItemsSource[selectedIndex];
                IdCurso = itemsedc.IdCurso;
                duracion.Text = "CURSO SELECCIONADO:  " + itemsedc.Nombre.ToUpper() + "  DURACIÓN:   "
                    + itemsedc.Duracion.ToString();
            }
        }

    }
}