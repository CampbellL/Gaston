using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Gaston.Models
{
    public class FillBlankExample : Example
    {
        public readonly List<Letter> LetterList = new List<Letter>();
        private static int _score = 10;
        public static int Score => _score;

        private string _sentence;
        private readonly string _answer;

        public string Sentence
        {
            set
            {
                if (_sentence != value)
                {
                    _sentence = value;
                    OnPropertyChanged();
                }
            }
            get { return _sentence; }
        }

        public FillBlankExample(string sentence, string answer, List<Letter> letters)
        {
            FillLetterList(letters);
            Sentence = sentence;
            _answer = answer;
        }

        private void FillLetterList(List<Letter> tempList)
        {
            Random rand = new Random();
            while (tempList.Count > 0)
            {
                var random = rand.Next(tempList.Count - 1);
                LetterList.Add(tempList[random]);
                tempList.RemoveAt(random);
            }
        }

        public string GetAnswer()
        {
            return _answer;
        }
    }
}