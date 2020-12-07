using ProyectoJose.Modelo;
using ProyectoJose.Services;
using ProyectoJose.VistaModelo;
using System;
using System.Collections;
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
    public partial class TrabajadorCursoPag : ContentPage, INotifyPropertyChanged
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        int IdTrabajador;
        public TrabajadorCursoPag(int idTrabajador)
        {
            InitializeComponent();
            IdTrabajador = idTrabajador;
            listaCurso.Text = "LISTADO DE CURSOS:  " + moduloGeneral.ObtenerTrabajadorSinCtxt(idTrabajador).Nombre.ToUpper();
           // BindingContext = new TrabajadorCurso();
        }


        protected override void OnAppearing()
        {

            cursoCollection.ItemsSource = GetListadoCursos();
        }

        private ArrayList GetListadoCursos()
        {
           ArrayList ListaCursos= new ArrayList();
            using (var blogContext = new PruebaContext())
            {
               var Listado = blogContext.TrabajadorCursos
                    .Join(blogContext.Cursos,
                     f => f.IdCurso,
                     s => s.IdCurso,
                     (f, s) =>
                     new
                     {
                         nombrecurso = s.Nombre,
                         tiempo = s.Duracion,
                         IdTrdor = f.IdTrabajador,
                         IdCurso = f.IdCurso
                     }

                ).Where(r => r.IdTrdor == IdTrabajador).ToArray();

                foreach (var item in Listado)
                {
                    ListaCursos.Add(item);
                }

            }

            return ListaCursos;

        }


        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
                {
                    await Navigation.PushModalAsync(new NavigationPage(new NuevoTrabCurso(IdTrabajador)));
                }

        async void btn_borrar(object sender, EventArgs e)
        {
            TrabajadorCurso trabajadorCurso;
           
            using (var blogContext = new PruebaContext())
            {
                var button = sender as Button;

                
                object fracaso = new object();
                fracaso = button?.BindingContext;
                int identificador=0;
               // List<string> results = GetListadoCursos().Cast<object>().Select(x => x.ToString())
                  //                                  .ToList();
                               
                var totalCursos = blogContext.TrabajadorCursos
                     .Join(blogContext.Cursos,
                      f => f.IdCurso,
                      s => s.IdCurso,
                      (f, s) =>
                      new
                      {
                          nombrecurso = s.Nombre,
                          tiempo = s.Duracion,
                          IdTrdor = f.IdTrabajador,
                          IdCurso = f.IdCurso
                      }

                 ).Where(r => r.IdTrdor == IdTrabajador).ToList();

                foreach (var valor in totalCursos)
                {
                    if (valor.Equals(fracaso))
                    {
                        identificador = valor.IdCurso;
                    }
                }

                trabajadorCurso = blogContext.TrabajadorCursos
                    .Where(ct => ct.IdTrabajador == IdTrabajador && ct.IdCurso == identificador).First();
                
               blogContext.Remove(trabajadorCurso);

                await blogContext.SaveChangesAsync();
             
                cursoCollection.ItemsSource = GetListadoCursos();
            }
         
            
        }


    }
}