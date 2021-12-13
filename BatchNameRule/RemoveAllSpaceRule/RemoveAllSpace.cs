using BatchNameRule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class RemoveAllSpace : Rule
    {
        override public String GetName() => "RemoveAllSpace";
        public RemoveAllSpace() { }
        override public string ArgumentString()
        {
            return "";
        }
        override public String Rename(String oldName)
        {
            string str = Path.GetFileNameWithoutExtension(oldName);
            str = str.Replace(" ", String.Empty);
            string result = str + Path.GetExtension(oldName);
            if (result.Length <= 255)
                return result;
            return "|";
        }
        override public Rule Create(Arguments args)
        {
            return new RemoveAllSpace();
        }
        override public Rule Clone()
        {
            return new RemoveAllSpace();
        }
    }
}