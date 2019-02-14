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
        private readonly Level _level;
        private int _exampleCount = 0;
        private const int LevelCap = 1000;
        private int _playerScore = 0;
        private int _tracker = 0;
        private Example _currentExample;

        public LevelLoader(Level level)
        {
            _level = level;
            InitializeComponent();
            CreatePageInstance();
        }

        private void CreatePageInstance()
        {
            _currentExample = _level.Examples.ElementAt(_exampleCount);
            if (_currentExample.Score >= (LevelCap - _tracker))
            {
                _currentExample = _level.Examples.First(l => l.Score <= (LevelCap - _tracker));
            }

            _tracker += _currentExample.Score;

            if (_currentExample.GetType() == typeof(MultipleChoiceExample))
            {
                _currentPage = new MultipleChoicePage((MultipleChoiceExample) _currentExample);
            }
            else if (_currentExample.GetType() == typeof(FillBlankExample))
            {
                _currentPage = new FillBlankPage((FillBlankExample) _currentExample);
            }
            
            _currentPage.ExampleState.ExampleCompleted += OnLevelCompleted;
            
            Navigation.PushModalAsync(_currentPage);
            _level.Examples.Remove(_currentExample);
            _exampleCount++;
        }

        private void OnLevelCompleted(object source, EventArgs args)
        {
            _playerScore += _currentPage.ExampleState.Score;
            if (_tracker < 1000)
                CreatePageInstance();
            else
            {
                DisplayAlert("You won", "Congratulation with a score of : " + _playerScore,"yay");
            }
        }
    }
}