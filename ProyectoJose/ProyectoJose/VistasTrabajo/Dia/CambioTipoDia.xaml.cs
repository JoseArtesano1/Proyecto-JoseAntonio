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
    public partial class CambioTipoDia : ContentPage
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        int IdTipoDia;
        public CambioTipoDia(int idTipodia)
        {
            InitializeComponent();
            IdTipoDia = idTipodia;
        }

        protected override void OnAppearing()
        {
            TipoDia tipoDia;

            using (var blogContext = new PruebaContext())
            {
                tipoDia = blogContext.TipoDias.Where(d => d.IdTipoDia == IdTipoDia).First();
                nombre.Text = tipoDia.Denominacion;
                dinero.Text = Convert.ToString (tipoDia.Importe);
               
            }
        }

        async void btn_actualizar(System.Object sender, System.EventArgs e)
        {
            TipoDia tipoDia;

            using (var Context = new PruebaContext())
            {
                if (!string.IsNullOrWhiteSpace(nombre.Text) && !moduloGeneral.isnumeric(nombre.Text)) {

                    if (!string.IsNullOrWhiteSpace(dinero.Text)) {

                        if (moduloGeneral.isnumeric(dinero.Text) || moduloGeneral.isnumericDouble(dinero.Text)) { 
                                                      
                            tipoDia = Context.TipoDias.Where(d => d.IdTipoDia == IdTipoDia).First();
                            tipoDia.Denominacion = moduloGeneral.ControlarTipoDia(IdTipoDia, Context,nombre.Text.ToUpper());
                            tipoDia.Importe = double.Parse( dinero.Text);
               
                            await Context.SaveChangesAsync();

                                nombre.Text = "";
                                dinero.Text = "";
                           
                        }
                        else { await DisplayAlert("Alerta", " Introduce importe", "OK"); }

                    }
                    else { await DisplayAlert("Alerta", " Introduce importe", "OK"); }


                }
                else { await DisplayAlert("Alerta", " Introduce Denominación", "OK"); }
            }
           
        }

        async void btn_eliminar(object sender, EventArgs e)
        {
            TipoDia tipoDia;

            using (var Context = new PruebaContext())
            {
                var tc = Context.Periodos.Where(x => x.IdTipoDia == IdTipoDia).FirstOrDefault();
                if (tc == null)
                {
                    tipoDia = Context.TipoDias.Where(d => d.IdTipoDia == IdTipoDia).First();
                    var booleanAnswer = await DisplayAlert("Eliminar", "¿Estas seguro?", "Si", "No");
                    if (booleanAnswer)
                    {
                        Context.TipoDias.Remove(tipoDia);
                        await Context.SaveChangesAsync();
                        await Navigation.PopAsync();
                    }
                }
                else { await DisplayAlert("No Eliminar Tipo", "Periodo con Tipo", "Ok"); }
            }

            
        }

      


    }
}