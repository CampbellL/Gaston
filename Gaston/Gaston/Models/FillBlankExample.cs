using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Gaston.Models
{
    public class FillBlankExample : Example
    {
        public readonly List<Letter> LetterList = new List<Letter>();
        private string _sentence;
        private readonly string _answer;
        private readonly ScoreTracker _scoreTracker;

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
            _scoreTracker = new ScoreTracker(15,300,75,1);
            Score = 300;
            FillLetterList(letters);
            Sentence = sentence;
            _answer = answer;
        }


        public int GetExampleScore()
        {
            return _scoreTracker.GetScore();
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