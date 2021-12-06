using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class RemoveAllSpace : Rule
    {
        override public String Rename(String oldName)
        {
            string str = Path.GetFileNameWithoutExtension(oldName);

            // Remove all space from the beginning
            while (!Char.IsLetter(str[0]) && !Char.IsNumber(str[0]))
                str = str.Remove(0, 1);

            // Remove all space from the end
            while (!Char.IsLetter(str[str.Length - 1]) && !Char.IsNumber(str[str.Length - 1]))
                str = str.Remove(str.Length - 1);
            return str + Path.GetExtension(oldName); ;
        }
        override public Rule Create(Arguments args)
        {
            return new RemoveAllSpace() ;
        }
        override public Rule Clone()
        {
            return new RemoveAllSpace();
        }
    }
}