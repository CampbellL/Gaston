

using System.Collections.Generic;

namespace Gaston.Models
{
    public class Verb
    {
        public Dictionary<string, bool> Endings;

        public string Root;
        public Verb(string root,Dictionary<string,bool> endings)
        {
            this.Endings = endings;
            this.Root = root;

        }
    }
}