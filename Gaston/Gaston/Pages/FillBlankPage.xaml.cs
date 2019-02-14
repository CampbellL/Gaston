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
        private Stack<Letter> letterStack = new Stack<Letter>();
        private List<Button> buttonList = new List<Button>();
        private Letter last;
        private int score = 100;

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
                buttonList.Add(button);
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
                last = new Letter(button.Text);
                letterStack.Push(last);
                button.BackgroundColor = Color.Blue;
            }
            else if(button.BackgroundColor == Color.Blue)
            {
                if(button.Text.ToUpper().Equals(letterStack.Peek().getCharacter().ToUpper()))
                {
                    _example.Sentence = _example.Sentence.Remove(_example.Sentence.IndexOf("_", StringComparison.Ordinal)-1,1);
                    Console.WriteLine(letterStack.Pop().getCharacter());
                    button.BackgroundColor = Color.White;
                }
                else
                {
                    Letter shaker = letterStack.Peek();
                    uint timeout = 50;
                    foreach(Button bShake in buttonList)
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
            foreach(Letter letter in letterStack)
            {
                res += letter.getCharacter();
            }
            res = Reverse(res);

            if (res.Equals(_example.GetAnswer()))
            {
                this.BackgroundColor = Color.LightSeaGreen;
                _example.Sentence = _example.Sentence.Replace("_", "");
                foreach (Button button in buttonList)
                {
                    button.IsEnabled = false;
                }
                Label label = new Label {
                    Text = score.ToString()
                };

                parentStack.Children.Add(label);
                Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
                {
                    label.IsVisible = false;
                    return false;
                });
                
                Navigation.PopModalAsync(true);
                ExampleState.Score = score;      
                ExampleState.Completed = true;

                


            }
            else
            {
                this.BackgroundColor = Color.MediumVioletRed;
                score -= 20;
                Device.StartTimer(TimeSpan.FromSeconds(1.5), () => {
                    this.BackgroundColor = Color.White;
                    return false;
                });
                
            }
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