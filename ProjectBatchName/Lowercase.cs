using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class Lowercase : Rule
    {
        public string name { get => "Lowercase"; }
        public Lowercase() { }
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