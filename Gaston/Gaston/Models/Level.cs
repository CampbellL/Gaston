using System.Collections.Generic;
using System.Xml.Schema;

namespace Gaston.Models
{
    public class Level
    {
        public List<Example> Examples;

        public Level(List<Example> examples)
        {
            Examples = examples;
        }
    }
}