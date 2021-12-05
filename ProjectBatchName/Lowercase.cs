using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class Lowercase : Rule
    {
        public Lowercase() { }
        override public String Rename(String oldName)
        {
            string str = Path.GetFileNameWithoutExtension(oldName);

            // Remove all space 
            /*for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsLetter(str[i]) && !Char.IsLetter(str[i]))
                    str = str.Remove(i, 1);
            }*/
            str = str.Replace(" ", String.Empty);

            return str.ToLower() + Path.GetExtension(oldName);
        }
        override public Rule Create(Arguments args)
        {
            return new Lowercase();
        }
    }
}