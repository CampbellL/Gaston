using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaston.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gaston.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class map2 : ContentPage
	{
		public map2 ()
		{
			InitializeComponent ();
		}

        private void Level1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LevelLoader(Level.GetLevelFromJson()));
        }

        private void Level2_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LevelLoader(Level.GetLevelFromJson()));
        }

        private void Level3_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LevelLoader(Level.GetLevelFromJson()));
        }

        private void Level4_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LevelLoader(Level.GetLevelFromJson()));
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Navigation.PopModalAsync();
            Navigation.PushModalAsync(new map1());
        }
    }
}