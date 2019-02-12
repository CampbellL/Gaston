using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Gaston.Models
{
    public class MultipleChoiceExample : Example
    {
        public Verb MultipleChoiceVerb;
        public string Sentence;
        
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
            Sentence = sentence;
        }
    }
}