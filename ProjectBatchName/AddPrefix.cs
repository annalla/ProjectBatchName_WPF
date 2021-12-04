using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class AddPrefix : Rule
    {
        public String prefix { get; set; }
        public AddPrefix() { }
        override public String Rename(String oldName)
        {
            string str = Path.GetFileNameWithoutExtension(oldName);
            str = prefix + str;
            return str + Path.GetExtension(oldName);
        }
        override public Rule Create(Arguments args)
        {
            Argument_1 arg = (Argument_1)args;
            return new AddPrefix() { prefix = arg.arg1 };
        }
    }
}