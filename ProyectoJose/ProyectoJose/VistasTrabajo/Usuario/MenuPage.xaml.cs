using ProyectoJose.Services;
using ProyectoJose.VistasTrabajo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoJose.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatosTrabPag: MasterDetailPage
    {
        ModuloGeneral moduloGeneral = new ModuloGeneral();
        public List<MasterPageItem> MenuList { get; set; }
        public DatosTrabPag()
        {
            InitializeComponent();
            MenuList = new List<MasterPageItem>();

            if (moduloGeneral.ObtenerCategoria() == "A") { 
            MenuList.Add(new MasterPageItem() { Title = "Nuevo Usuario", Icon = @"Recursos/nuevousuario.png", TargetType = typeof(RegistroPag) });
            MenuList.Add(new MasterPageItem() { Title = "Usuarios", Icon = @"Recursos/usuario.png", TargetType = typeof(ConsultaRegistroPag) });
            MenuList.Add(new MasterPageItem() { Title = "Trabajadores", Icon = @"Recursos/trabajador.png", TargetType = typeof(TrabajadoresPag) });
            MenuList.Add(new MasterPageItem() { Title = "Cursos", Icon = @"Recursos/formacion.png", TargetType = typeof(CursosPag) });
            MenuList.Add(new MasterPageItem() { Title = "Epis", Icon = @"Recursos/casco.png", TargetType = typeof(EpisPag) });
            MenuList.Add(new MasterPageItem() { Title = "Nuevo Tipo Dia", Icon = @"Recursos/nuevoperiodo.png", TargetType = typeof(NuevoTipoDia) });
            MenuList.Add(new MasterPageItem() { Title = "Tipos Dias", Icon = @"Recursos/tipoperiodo.png", TargetType = typeof(TipoDiasPag) });
            }
            else
            {
                MenuList.Add(new MasterPageItem() { Title = "Usuarios", Icon = @"Recursos/usuario.png", TargetType = typeof(ConsultaRegistroPag) });
                MenuList.Add(new MasterPageItem() { Title = "Trabajadores", Icon = @"Recursos/trabajador.png", TargetType = typeof(TrabajadoresPag) });
                MenuList.Add(new MasterPageItem() { Title = "Cursos", Icon = @"Recursos/formacion.png", TargetType = typeof(CursosPag) });
                MenuList.Add(new MasterPageItem() { Title = "Epis", Icon = @"Recursos/casco.png", TargetType = typeof(EpisPag) });
                MenuList.Add(new MasterPageItem() { Title = "Nuevo Tipo Dia", Icon = @"Recursos/nuevoperiodo.png", TargetType = typeof(NuevoTipoDia) });
                MenuList.Add(new MasterPageItem() { Title = "Tipos Dias", Icon = @"Recursos/tipoperiodo.png", TargetType = typeof(TipoDiasPag) });
            }

            navigationDrawerList.ItemsSource = MenuList;
          
            NavigationPage.SetHasNavigationBar(this, false);  // quita doble menú en android
           
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));

        }

      

            private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }


        private void btn_Cerrar(object sender, EventArgs e)
        {

            var closer = DependencyService.Get<ICloseApplication>();
            if (closer != null)
                closer.closeApplication();


        }

    }
}