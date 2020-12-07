using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ProyectoJose.Modelo;
using ProyectoJose.Services;
using ProyectoJose.VistaModelo;
using ProyectoJose.Vistas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoJose.VistasTrabajo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoTrabDia : ContentPage
    {
        ModuloPlantilla moduloPlantilla = new ModuloPlantilla();
        int IdTrabajador;
        public NuevoTrabDia(int trabajadorId)
        {
            InitializeComponent();

            IdTrabajador = trabajadorId;

            this.BindingContext = new RootModel
            {
                TipoList = GetTipos()

            };

        }


        private List<TipoDia> GetTipos()
        {
            List<TipoDia> ListaTipos;
            using (var blogContext = new PruebaContext())
            {
                var mistipos = blogContext.TipoDias.ToList();

                picker.ItemsSource = mistipos;
                ListaTipos = new List<TipoDia>();

                foreach (var item in mistipos)
                {
                    ListaTipos.Add(item);
                }

            }

            return ListaTipos;

        }



       


        protected async void Save_Clicked(object sender, EventArgs e)
        {
            int idTipoDia = 0;
            int selectedIndex = picker.SelectedIndex;
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Vacaciones.docx");

            string fechaMostrar = moduloPlantilla.ObtenerFecha(Finicio.Date);
            string fechaMostrar2 = moduloPlantilla.ObtenerFecha(Ffin.Date);
            DateTime primera=Finicio.Date;
            DateTime ultima=Ffin.Date;


            try
            {
                using (var Context = new PruebaContext())
                {

                    if (selectedIndex != -1)
                    {
                        var itemsedt = (TipoDia)picker.ItemsSource[selectedIndex];
                        idTipoDia = itemsedt.IdTipoDia;


                        // comprobar que estan seleccionadas

                        if (moduloPlantilla.ComprobarAlta(primera, Context, IdTrabajador))
                        {


                            // comprobar que la fecha inicio es inferior o igual a la final

                            if (DateTime.Compare(primera, ultima) < 0 || DateTime.Compare(primera, ultima) == 0)
                            {
                                // comprobar que el conjunto de fechas del trabajador que la fecha inicio es introducida es mayor que la fecha fin que tengamos almacenada

                                var resultado = Context.Periodos.Where(x => x.IdTrabajador == IdTrabajador).ToList();

                                if (resultado.Count != 0)
                                { // tenemos

                                    if (!moduloPlantilla.ComprobarPeriodo(primera, ultima, Context, IdTrabajador))
                                    { //comparamos lo que tenemos con lo introducido
                                        var nuevoTrabP = new Periodo
                                        {
                                            FechaInicio = primera.ToString("d"),
                                            FechaFin = fechaMostrar2,
                                            IdTrabajador = IdTrabajador,
                                            IdTipoDia = idTipoDia,

                                        };

                                        await Context.Periodos.AddAsync(nuevoTrabP);

                                        await Context.SaveChangesAsync();

                                        OpenControlTextToWordDocument(dbPath, idTipoDia, Context); // comprobar y abrir

                                    }
                                    else { await DisplayAlert("Alerta", "periódo coincide", "OK"); }

                                }
                                else // añadimos uno nuevo 
                                {
                                    var nuevoTrabP = new Periodo
                                    {
                                        FechaInicio = primera.ToString("d"),
                                        FechaFin = fechaMostrar2,
                                        IdTrabajador = IdTrabajador,
                                        IdTipoDia = idTipoDia,

                                    };

                                    await Context.Periodos.AddAsync(nuevoTrabP);

                                    await Context.SaveChangesAsync();
                                    OpenControlTextToWordDocument(dbPath, idTipoDia, Context);
                                }
                            }
                            else { await DisplayAlert("Alerta", "la fecha de inicio debe se menor o igual a fecha fin", "OK"); }

                        }
                        else { await DisplayAlert("Alerta", "la fecha de alta debe se menor o igual a fecha inicio", "OK"); }
                    }
                    else { await DisplayAlert("Alerta", " debe seleccionar un tipo", "OK"); }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

        }



        protected async void Cancel_Clicked(object sender, EventArgs e)
        {
            try {
                 
                await Navigation.PopModalAsync();
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }


        async void OpenControlTextToWordDocument(string filepath, int idTipo, PruebaContext contexto)
        {
           
            DateTime primera = Finicio.Date;
            DateTime ultima = Ffin.Date;
            string TipodeDia = contexto.TipoDias.Where(x => x.IdTipoDia == idTipo).FirstOrDefault().Denominacion.ToUpper();

            if (TipodeDia.StartsWith("VA"))  // comprobar que solo vacaciones
            {

                if (File.Exists(filepath))
                {
                    await Launcher.OpenAsync(new OpenFileRequest()  //abrir
                    {
                        File = new ReadOnlyFile(filepath)
                    });

                    Pregunta(filepath); // comprobar y cerrar

                }
                else
                {
                    moduloPlantilla.AddTextToWordDocument(filepath, IdTrabajador, primera, ultima); // crear

                    await Launcher.OpenAsync(new OpenFileRequest()   // abrir
                    {
                        File = new ReadOnlyFile(filepath)
                    });

                    Pregunta(filepath); // comprobar y cerrar

                }

            }
            else { await Navigation.PopModalAsync(); }

        }



        async void Pregunta(string file)
        {
            var booleanAnswer = await DisplayAlert("Cerrar documento", " Elimina el documento ¿Estas seguro?",
                "Si", "No");
            if (booleanAnswer)
            {
                while (moduloPlantilla.IsFileLocked(file))
                {
                    await DisplayAlert("Cerrar documento", "El documento está abierto", "Ok");
                }
                File.Delete(file);
                await Navigation.PopModalAsync();
            }
            else
            {
                while (moduloPlantilla.IsFileLocked(file))
                {
                    await DisplayAlert("Finalizar", "El documento está abierto", "Ok");
                    NavigationPage.SetHasBackButton(this, false);
                }
                File.Delete(file);
                await Navigation.PopModalAsync();
            }
        }


    }

}