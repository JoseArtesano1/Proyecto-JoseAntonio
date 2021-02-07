using ProyectoJose.Modelo;
using ProyectoJose.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Collections;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using ProyectoJose.VistaModelo;

namespace ProyectoJose.VistasTrabajo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoTipoDia : ContentPage
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        public NuevoTipoDia()
        {
            InitializeComponent();
            this.BindingContext = new RootModel
            {
                IsStopVisible = moduloGeneral.Autorizacion()

            };
        }

      

        async void Btn_agregar(System.Object sender, System.EventArgs e)
        {
          
            try
            {
                using (var Context = new PruebaContext())
                {
                    if (!string.IsNullOrWhiteSpace(Nombre.Text) && !moduloGeneral.isnumeric(Nombre.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(Dinero.Text))
                        {
                            if(moduloGeneral.isnumeric(Dinero.Text) || moduloGeneral.isnumericDouble(Dinero.Text))
                            {
                                var tipod = new TipoDia { Denominacion = Nombre.Text.ToUpper(), Importe = double.Parse(Dinero.Text) };

                                var d1 = Context.TipoDias.Where(x => x.Denominacion == Nombre.Text.ToUpper()).FirstOrDefault();

                                if (d1 == null)

                                {
                                    await Context.TipoDias.AddAsync(tipod);
                                    await Context.SaveChangesAsync();
                                    Nombre.Text = "";
                                    Dinero.Text = "";
                                  
                                }
                                else
                                {
                                    await DisplayAlert("Alerta", "Repetido tipo", "Ok");

                                }

                            }
                            else { await DisplayAlert("Alerta", " Introduce importe", "OK"); }
                           
                        }
                        else { await DisplayAlert("Alerta", " Introduce importe", "OK"); }

                    }
                    else { await DisplayAlert("Alerta", " Introduce Denominación", "OK"); }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

           

        }


    
        private async void Btn_cargar (System.Object sender, System.EventArgs e)
        {
            try
            {
                using (var Context = new PruebaContext())
            { 
                var file = await CrossFilePicker.Current.PickFile();

            if (file != null)
            {                                
                                       
                string contents = System.Text.Encoding.UTF8.GetString(file.DataArray); // lectura archivo
                        if (contents != "" && !moduloGeneral.isnumeric(contents))
                        {
                           
                            List<string> dias = contents.Split('\n').Select(p => p.Trim()).ToList(); // guardado clasificado

                            foreach (var item in dias)
                            {
                                if (item.Length == 10) 
                                { 
                                var f1 = Context.Festivos.Where(x => x.FechaFestivo == item).FirstOrDefault();
                                // comprobación de otra carga no repita los ya guardados
                                var newfestivo = new Festivo
                                {
                                    FechaFestivo = item

                                };

                                if (f1 == null)
                                {
                                    await Context.Festivos.AddAsync(newfestivo);
                                    await Context.SaveChangesAsync();
                                        //  await Navigation.PopModalAsync();
                                        lbl.Text = file.FileName;
                                      //  await DisplayAlert("Nuevo Festivo", "Festivos guardados", "Ok");
                                    }
                                else { await DisplayAlert("Nuevo Festivo", "Festivo repetido", "Ok"); }

                                }
                                else { await DisplayAlert("Fichero", "datos erroneos", "Ok"); }
                            }

                        }
                        else { await DisplayAlert("Fichero", "fichero vacio", "Ok"); }

             }
            else { await DisplayAlert("Alerta", " debe seleccionar archivo", "OK"); }

            }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

        }

        private async void Btn_Borrar(System.Object sender, System.EventArgs e)
        {
            List<Festivo> listafestivos;
            using (var Context = new PruebaContext())
            {
                listafestivos = Context.Festivos.ToList();
                if (listafestivos != null)
                {
                    foreach (var item in listafestivos)
                    {
                        Context.Festivos.Remove(item);
                        await Context.SaveChangesAsync();
                        
                    }
                    await DisplayAlert("Mensaje", " datos borrados correctamente", "OK");
                }
                else { await DisplayAlert("Alerta", " no existe festivos", "OK"); }

            }

        }


    }
}