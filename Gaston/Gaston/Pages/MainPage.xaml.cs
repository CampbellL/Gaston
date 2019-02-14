using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Gaston.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Gaston.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Play_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LevelLoader(Level.GetLevelFromJson()));
        }

        private void Settings_Clicked(object sender, EventArgs e)
        {
            NavigationPage page = new NavigationPage(new SettingsPage());
            NavigationPage.SetHasBackButton(page,true);
            Navigation.PushModalAsync(page);
        }
    }
}
