using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Gaston.Models
{
    public class MultipleChoiceLevel : Level
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
        public MultipleChoiceLevel(Verb multipleChoiceVerb, string sentence)
        {
            MultipleChoiceVerb = multipleChoiceVerb;
            Sentence = sentence;
        }
    }
}