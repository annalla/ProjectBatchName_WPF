using BatchNameRule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class Lowercase : Rule
    {
        override public String GetName() => "Lowercase";
        public Lowercase() { }
        override public string ArgumentString()
        {
            return "";
        }
        override public String Rename(String oldName)
        {
            string str = Path.GetFileNameWithoutExtension(oldName);
            str = str.Replace(" ", String.Empty);
            string result = str.ToLower() + Path.GetExtension(oldName);
            if (result.Length <= 255)
                return result;
            return "|";
        }
        override public Rule Create(Arguments args)
        {
            return new Lowercase();
        }
        override public Rule Clone()
        {
            return new Lowercase();
        }
    }
}