using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gaston.Models;
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
            Verb verb = new Verb("aim",
                new Dictionary<string, bool>
                {
                    {
                        "e",true
                    },
                    {
                        "es",true
                    },
                    {
                        "ons",true
                    },
                }
                );
            Navigation.PushModalAsync(new FingerPaintPage());
        }

        private void Settings_Clicked(object sender, EventArgs e)
        {
            NavigationPage page = new NavigationPage(new SettingsPage());
            NavigationPage.SetHasBackButton(page,true);
            Navigation.PushModalAsync(page);
        }
    }
}
