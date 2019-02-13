using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gaston.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LevelLoader : ContentPage
    {

        
        public LevelLoader()
        {
            InitializeComponent();
        }
        
        protected void OnLevelCompleted(object source, EventArgs args)
        {
            
        }
        
        
        

    }
}
