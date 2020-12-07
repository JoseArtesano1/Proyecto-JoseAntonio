using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProyectoJose.Droid;
using ProyectoJose.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]
namespace ProyectoJose.Droid
{
   public class CloseApplication: ICloseApplication
    {
       // [Obsolete]
        public void closeApplication()
        {
            // var activity = (Activity)Forms.Context;
            // activity.FinishAffinity();
            System.Environment.Exit(0);


        }
    }
}