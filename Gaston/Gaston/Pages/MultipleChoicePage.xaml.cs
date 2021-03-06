﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gaston.Models;
using Xamarin.Forms;

namespace Gaston.Pages
{
    public partial class MultipleChoicePage : ContentPage
    {

        private List<Button> _buttons = new List<Button>();
        private readonly MultipleChoiceExample _example;

        public MultipleChoicePage(MultipleChoiceExample example)
        {
            
            InitializeComponent();
            
            _example = example;
            var test = ButtonGrid.GetChildElements(new Point());
            BindingContext = _example;

            for (int i = 0; i < _example.Verb.Endings.Count; i++)
            {
                var button = new Button()
                {
                    Text = _example.Verb.Endings.ElementAt(i).Key,
                    BorderColor = Color.Black,
                    BorderWidth = 2,
                    CornerRadius = 5,
                    Padding = 5
                };
                Grid.SetColumn(button, i);
                button.Clicked += OnButtonClicked;
                ButtonGrid.Children.Add(button);
                _buttons.Add(button);
            }
        }

        void DisableAllButtons()
        {
            foreach (var button in _buttons)
            {
                button.IsEnabled = false;
            }
        }
        void ExampleLost()
        {
            this.DisableAllButtons();
            this.BackgroundColor = Color.Red;
        }
        
        void ExampleWon()
        {
            this.DisableAllButtons();
            this.BackgroundColor = Color.Lime;
        }
        
        async void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button) sender;
            var test = _example.Verb.Endings.First(d => d.Value.Equals(true));
            if (_example.Sentence.Contains("_"))
            {
                _example.Sentence = _example.Sentence.Insert(_example.Sentence.IndexOf("_", StringComparison.Ordinal),
                    button.Text);
                _example.Sentence = _example.Sentence.Replace("_", "");
            }

            if (button.Text == test.Key)
            {
                this.ExampleWon();
            }
            else
            {
                this.ExampleLost();
            }
        }
    }
}