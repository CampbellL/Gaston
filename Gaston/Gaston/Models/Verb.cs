

using System.Collections.Generic;

namespace Gaston.Models
{
    public class Verb
    {
        public Dictionary<string, bool> Endings;

        public Verb(Dictionary<string,bool> endings)
        {
            this.Endings = endings;
        }
    }
}