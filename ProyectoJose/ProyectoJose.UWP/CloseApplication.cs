using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoJose.Services;
using ProyectoJose.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]
namespace ProyectoJose.UWP
{
   public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            Windows.UI.Xaml.Application.Current.Exit();
        }
    }
}
