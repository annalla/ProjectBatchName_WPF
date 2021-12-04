using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class PascalCase : Rule
    {
        public PascalCase() { }
        override public String Rename(String oldName)
        {
            string result = "";
            string str = Path.GetFileNameWithoutExtension(oldName);

            
            // PascalCase
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsLetter(str[i]) && !Char.IsNumber(str[i]) && Char.IsLetter(str[i + 1]))
                {
                    result = result + " " + Char.ToUpper(str[i + 1]); i++; continue;
                }
                else if (!Char.IsLetter(str[i]) && !Char.IsNumber(str[i]) && Char.IsNumber(str[i + 1]))
                {
                    result = result + " " + str[i + 1]; i++; continue;
                }
                else if (i == 0)
                {
                    result = result + Char.ToUpper(str[i]);
                }
                else if (Char.IsLetter(str[i]))
                    result = result + Char.ToLower(str[i]);
                else if (Char.IsNumber(str[i]))
                    result = result + str[i];

            }
            // Remove all space 
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsLetter(str[i]) && !Char.IsLetter(str[i]))
                    str = str.Remove(i, 1);
            }
            result += Path.GetExtension(oldName);

            return result;
        }
        override public Rule Create(Arguments args)
        {
            return new PascalCase();
        }
    }
}