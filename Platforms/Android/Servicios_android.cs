using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Net=Android.Net;
using Android.Graphics;
using AndroidX.Core.App;
using llamada.Servicios;
using PM=Android.Content.PM;
using Uris = Android.Net.Uri;
using llamada.Platforms.Android;
using Android.Content.PM;
using Android.Runtime;
[assembly: Dependency(typeof(Servicios_android))]
namespace llamada.Platforms.Android
{
    [Service(ForegroundServiceType = PM.ForegroundService.TypePhoneCall)]
   public  class Servicios_android :Service, PhoneCaller
    {


        public void llamar(string telefono)
        {
            var uri = Net.Uri.Parse("tel:"+ telefono);
            var intent = new Intent(Intent.);
            intent.SetData(uri);
            var activity = Platform.CurrentActivity;
            activity.StartActivity(intent);
            
        }

        public override IBinder? OnBind(Intent? intent)
        {
            throw new NotImplementedException();
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent? intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            
               

            
            return StartCommandResult.NotSticky;
        }


    }
}
