using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class AddSuffix : Rule
    {
        public String suffix { get; set; }
        public AddSuffix() { }
        override public String Rename(String oldName)
        {
            string str = Path.GetFileNameWithoutExtension(oldName);
            str += suffix;
            return str + Path.GetExtension(oldName);
        }
        override public Rule Create(Arguments args)
        {
            Argument_1 arg = (Argument_1)args;
            return new AddSuffix() { suffix = arg.arg1 };
        }
    }
}