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
        public Verb MultipleChoiceVerb;
        private string _sentence;
        public string Sentence
        {
            set
            {
                if (_sentence != value) {
                    _sentence = value;
                    OnPropertyChanged ("Sentence");
                }
            }
            get => _sentence;
        }

        public Verb Verb =
            new Verb("aim", new Dictionary<string, bool>()
            {
                {
                    "ons", true
                },
                {
                    "e", false
                },
                {
                    "es", false
                },
            });
        public MultipleChoiceExample(Verb multipleChoiceVerb, string sentence)
        {
            MultipleChoiceVerb = multipleChoiceVerb;
            Sentence = sentence.Insert(
                sentence.IndexOf("_", StringComparison.Ordinal),
                multipleChoiceVerb.Root
                );
        }


    }
}