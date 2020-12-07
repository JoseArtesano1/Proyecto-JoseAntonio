using Microsoft.EntityFrameworkCore;
using ProyectoJose.Services;
using ProyectoJose.Vistas;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoJose
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           
            MainPage = new NavigationPage(new LoginPag());
        }

        protected override void OnStart()
        {
           
            }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
