using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Gaston.Models
{
    public class FillBlankExample : Example
    {

        public List<Letter> letterList = new List<Letter>();

        private string _sentence;
        private string _answer;
        public string Sentence
        {
            set
            {
                if (_sentence != value)
                {
                    _sentence = value;
                    OnPropertyChanged ("Sentence");
                }
            }
            get { return _sentence; }
        }

        public FillBlankExample(string sentence,string answer)
        {
            fillLetterList();
            Sentence = sentence;
            _answer = answer;
        }

        public void fillLetterList()
        {
            List<Letter> tempList = new List<Letter>();
            Random rand = new Random();
            int random;
            tempList.Add(new Letter("v"));
            tempList.Add(new Letter("a"));
            tempList.Add(new Letter("i"));
            tempList.Add(new Letter("s"));
            tempList.Add(new Letter("n"));
            tempList.Add(new Letter("o"));
            tempList.Add(new Letter("k"));
            tempList.Add(new Letter("q"));
            tempList.Add(new Letter("e"));
            tempList.Add(new Letter("e"));

            while(tempList.Count > 0)
            {
                random = rand.Next(tempList.Count-1);
                letterList.Add(tempList[random]);
                tempList.RemoveAt(random);
            }
        }

        public string getAnswer()
        {
            return _answer;
        }
    }
}
