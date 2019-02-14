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
        
        public static explicit operator Letter(string s)
        {
            Letter letter = new Letter(s);
            return letter;
        }
        
        public string getCharacter()
        {
            return character;
        }
        
    }
}
