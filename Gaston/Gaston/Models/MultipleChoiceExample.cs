using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Gaston.Annotations;
using Gaston.Models.States;

namespace Gaston.Models
{
    public class MultipleChoiceExample : Example
    {
        public readonly Verb MultipleChoiceVerb;
        private string _sentence;
        public string Sentence
        {
            set
            {
                if (_sentence != value) {
                    _sentence = value;
                    OnPropertyChanged ();
                }
            }
            get => _sentence;
        }
        public MultipleChoiceExample(Verb multipleChoiceVerb, string sentence)
        {
            Sentence = sentence;             
            MultipleChoiceVerb = multipleChoiceVerb;
        }


    }
}