using System;
using Xamarin.Forms;

namespace FancyFrameApp.Tizen
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            // Call 'LoadApplication(Application application)' here to load your application.
            LoadApplication(new FancyFrameApp.App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            app.Run(args);
        }
    }
}
