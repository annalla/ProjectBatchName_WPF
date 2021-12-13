using BatchNameRule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class AddSuffix : Rule
    {
        override public String GetName() => "AddSuffix";
        public string suffix { get; set; }
        public AddSuffix() { }
        override public string ArgumentString()
        {
            return suffix;
        }
        override public String Rename(String oldName)
        {
            string str = Path.GetFileNameWithoutExtension(oldName);
            str += suffix;
            string result = str + Path.GetExtension(oldName);
            if (result.Length <= 255)
                return result;
            return "|";
        }
        override public Rule Create(Arguments args)
        {
            Argument_1 arg = (Argument_1)args;
            return new AddSuffix() { suffix = arg.arg1 };
        }
        override public Rule Clone()
        {
            return new AddSuffix();
        }
    }

}