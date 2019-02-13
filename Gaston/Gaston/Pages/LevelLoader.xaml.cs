using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaston.Models;
using Gaston.Models.States;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gaston.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LevelLoader : ContentPage
    {
        private GamePage _currentPage;
        private Level _level;
        private int _exampleCount = 0;
        public LevelLoader(Level level)
        {
            _level = level;
            InitializeComponent();
            CreatePageInstance();
        }

        private void CreatePageInstance()
        {
            
            Example currentExample = _level.Examples.ElementAt(_exampleCount);
            if (currentExample.GetType() == typeof(MultipleChoiceExample))
            {
                _currentPage = new MultipleChoicePage((MultipleChoiceExample) currentExample);
            }
            else if (currentExample.GetType() == typeof(FillBlankExample))
            {
                _currentPage = new FillBlankPage((FillBlankExample) currentExample);
            }            
            _currentPage.ExampleState.ExampleCompleted += OnLevelCompleted;
            
            Navigation.PushModalAsync(_currentPage);
            _exampleCount++;
        }

        private void OnLevelCompleted(object source, EventArgs args)
        {
            CreatePageInstance();
        }
    }
}
