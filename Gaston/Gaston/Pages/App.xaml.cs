using System;
using System.Linq;
using System.Reflection;
using Gaston.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Gaston
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
            if (!Current.Properties.ContainsKey("SfxVolume") || !Current.Properties.ContainsKey("MusicVolume"))
            {
                Current.Properties["SfxVolume"] = 100;
                Current.Properties["MusicVolume"] = 100;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
