using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gaston
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            MusicVolumeSlider.Value = Application.Current.Properties ["MusicVolume"] is double ? (double) (double) Application.Current.Properties ["MusicVolume"] : 0;
            SfxVolumeSlider.Value = Application.Current.Properties ["SfxVolume"] is double ? (double) (double) Application.Current.Properties ["SfxVolume"] : 0;
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }


        private void MusicVolumeSlider_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            Application.Current.Properties["MusicVolume"] = MusicVolumeSlider.Value;
        }

        private void SfxVolumeSlider_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            Application.Current.Properties["SfxVolume"] = SfxVolumeSlider.Value;
        }
    }
}