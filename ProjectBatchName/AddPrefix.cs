using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class AddPrefix : Rule
    {
        public string name { get => "AddPrefix"; }
        public String prefix { get; set; }
        public AddPrefix() { }
        override public String Rename(String oldName)
        {
            string str = Path.GetFileNameWithoutExtension(oldName);
            str = prefix + str;
            string result = str + Path.GetExtension(oldName);
            if (result.Length <= 255)
                return result;
            return "|";
        }
        override public Rule Create(Arguments args)
        {
            Argument_1 arg = (Argument_1)args;
            return new AddPrefix() { prefix = arg.arg1 };
        }
        override public Rule Clone()
        {
            return new AddPrefix();
        }
    }
}