using ProyectoJose.Modelo;
using ProyectoJose.Services;
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
    public partial class CambioCurso : ContentPage
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        int IdCurso;
        public CambioCurso(int idCurso)
        {
            InitializeComponent();
            IdCurso = idCurso;
        }


        protected override void OnAppearing()
        {
           Curso curso;

            using (var blogContext = new PruebaContext())
            {

                curso = blogContext.Cursos.Where(d => d.IdCurso == IdCurso).First();
                nombre.Text = curso.Nombre;
                duracion.Text = curso.Duracion.ToString();

            }
        }


        async void btn_actualizar(object sender, EventArgs e)
        {
            Curso curso;
            using (var Context = new PruebaContext())
            {
                if (!string.IsNullOrWhiteSpace(nombre.Text) && !moduloGeneral.isnumeric(nombre.Text)) 
                {

                    if (!string.IsNullOrWhiteSpace(duracion.Text) && moduloGeneral.isnumeric(duracion.Text)) 
                    { 

                        var checkCurso = Context.Cursos.Where(x => x.Nombre == nombre.Text.ToUpper() &&
                        x.Duracion== int.Parse(duracion.Text)).FirstOrDefault();

                    if (checkCurso == null)
                    {
                        curso = Context.Cursos.Where(x => x.IdCurso == IdCurso).First();
                        curso.Nombre = nombre.Text.ToUpper();
                        curso.Duracion = int.Parse( duracion.Text);
                        await Context.SaveChangesAsync();
                            nombre.Text = "";
                            duracion.Text = "";
                                
                    }
                    else { await DisplayAlert(" Sin Modificar Curso", "Curso ya existe", "Ok"); }



                     }
                      else { await DisplayAlert("Alerta", "Introduce nombre del curso", "Ok"); }
                }
                else { await DisplayAlert("Alerta", "introduce su duración", "Ok"); }
            }

        }



        async void btn_eliminar(object sender, EventArgs e)
        {
            Curso curso;

            using (var Context = new PruebaContext())
            {              

                    var ce = Context.TrabajadorCursos.Where(ct => ct.IdCurso == IdCurso).FirstOrDefault();

                if (ce == null)
                {
                    curso = Context.Cursos.Where(c => c.IdCurso == IdCurso).First();
                    var booleanAnswer = await DisplayAlert("Eliminar", "¿Estas seguro?", "Si", "No");
                    if (booleanAnswer)
                    {
                        Context.Cursos.Remove(curso);
                        await Context.SaveChangesAsync();
                        await Navigation.PopAsync();
                    }
                }
                else { await DisplayAlert("Eliminar Curso", "Existen trabajadores con el curso", "Ok"); }

            }


        }





    }
}