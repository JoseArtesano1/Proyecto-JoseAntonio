using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Foundation;
using ProyectoJose.iOS;
using ProyectoJose.Services;
using UIKit;
using Xamarin.Forms;


[assembly: Dependency(typeof(CloseApplication))]

namespace ProyectoJose.iOS
{
   public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}