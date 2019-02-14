using Gaston.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaston.Models.States;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gaston.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FillBlankPage : GamePage
	{
        private readonly FillBlankExample _example;
        private readonly Stack<Letter> _letterStack = new Stack<Letter>();
        private readonly List<Button> _buttonList = new List<Button>();
        private Letter _last;
        private int _score = 100;
        

        public FillBlankPage (FillBlankExample example)
		{
            ExampleState = new ExampleState();
			InitializeComponent ();            
            _example = example;
            BindingContext = _example;
            int i = 0;
            int j = 0;
            foreach(Letter letter in _example.LetterList)
            {               
                Button button = new Button
                {
                    Text = letter.getCharacter(),
                    BackgroundColor = Color.White
                };
                ButtonGrid.Children.Add(button);
                Grid.SetColumn(button, i);
                Grid.SetRow(button, j);
                button.Clicked += OnClicked;
                i++;
                if (i == 5)
                {
                    j++;
                    i = 0;
                }
                _buttonList.Add(button);
            }
            
		}

        async void OnClicked(object sender,EventArgs args)
        {
            Button button = (Button) sender;
            if(button.BackgroundColor == Color.White)
            {
                if (_example.Sentence.Contains("_"))
                {
                    _example.Sentence = _example.Sentence.Insert(_example.Sentence.IndexOf("_", StringComparison.Ordinal), button.Text);
                }
                _last = new Letter(button.Text);
                _letterStack.Push(_last);
                button.BackgroundColor = Color.Blue;
            }
            else if(button.BackgroundColor == Color.Blue)
            {
                if(button.Text.ToUpper().Equals(_letterStack.Peek().getCharacter().ToUpper()))
                {
                    _example.Sentence = _example.Sentence.Remove(_example.Sentence.IndexOf("_", StringComparison.Ordinal)-1,1);
                    _letterStack.Pop();
                    button.BackgroundColor = Color.White;
                }
                else
                {
                    Letter shaker = _letterStack.Peek();
                    uint timeout = 50;
                    foreach(Button bShake in _buttonList)
                    {
                        if (bShake.Text.Equals(shaker.getCharacter()) && bShake.BackgroundColor == Color.Blue)
                        {
                            await bShake.TranslateTo(-15, 0, timeout);
                            await bShake.TranslateTo(15, 0, timeout);
                            await bShake.TranslateTo(-10, 0, timeout);
                            await bShake.TranslateTo(10, 0, timeout);
                            await bShake.TranslateTo(-5, 0, timeout);
                            await bShake.TranslateTo(5, 0, timeout);
                            bShake.TranslationX = 0;
                        }
                    }
                }
                
            }
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string res = "";            
            foreach(Letter letter in _letterStack)
            {
                res += letter.getCharacter();
            }
            res = Reverse(res);

            if (res.Equals(_example.GetAnswer()))
            {
                this.BackgroundColor = Color.LightSeaGreen;
                _example.Sentence = _example.Sentence.Replace("_", "");
                foreach (Button button in _buttonList)
                {
                    button.IsEnabled = false;
                }
                Label label = new Label {
                    Text = _score.ToString()
                };

                parentStack.Children.Add(label);
                Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
                {
                    label.IsVisible = false;
                    return false;
                });
                
                Navigation.PopModalAsync(true);
                ExampleState.Score = _example.GetExampleScore();      
                ExampleState.Completed = true;

                


            }
            else
            {
                this.BackgroundColor = Color.MediumVioletRed;
                _score -= 20;
                Device.StartTimer(TimeSpan.FromSeconds(1.5), () => {
                    this.BackgroundColor = Color.White;
                    return false;
                });
                
            }
        }
        
        private void Skip_Clicked(object sender, EventArgs e)
        {
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Volume = (Application.Current.Properties["SfxVolume"] is double ? (double)(double)Application.Current.Properties["SfxVolume"] : 0) / 100;
            player.Load("wrongAnswer.mp3");
            player.Play();
            ExampleState.Score = 0;
            Navigation.PopModalAsync(true);
            ExampleState.Completed = true;
        }
        
        public string Reverse(string str)
        {
            char[] chars = str.ToCharArray();
            for (int i = 0, j = str.Length - 1; i < j; i++, j--)
            {
                char c = chars[i];
                chars[i] = chars[j];
                chars[j] = c;
            }
            return new string(chars);
        }
    }
}