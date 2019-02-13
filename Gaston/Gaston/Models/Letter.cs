using System;
using System.Collections.Generic;
using System.Text;

namespace Gaston.Models
{
    public class Letter
    {
        string character;

        public Letter(string character)
        {
            this.character = character;
        }

        public string getCharacter()
        {
            return character;
        }
        
    }
}
