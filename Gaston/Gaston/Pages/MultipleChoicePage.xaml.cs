using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Gaston.Models;
using Xamarin.Forms;

namespace Gaston.Pages
{
    public partial class MultipleChoicePage : ContentPage
    {

        private readonly MultipleChoiceExample _example;
        public MultipleChoicePage(MultipleChoiceExample example)
        {
            InitializeComponent();
            _example = example;
            foreach (var choice in _example.Verb.Endings)
            {
                var button = new Button()
                {
                    Text = choice.Key,
                };
                button.Clicked += OnButtonClicked;
                StackLayout.Children.Add(button);                         
            }    
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button) sender;
            var test = _example.Verb.Endings.First(d => d.Value.Equals(true));
            _example.Sentence = _example.Sentence.Insert(_example.Sentence.IndexOf("_", StringComparison.Ordinal), button.Text);
            _example.Sentence = _example.Sentence.Replace("_","");
            if (button.Text == test.Key)
            {
                Console.WriteLine("guessed");
            }
            else
            {
                button.BackgroundColor = Color.Red;
            }
        } 
        
    }
}
