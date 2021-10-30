using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBatchName
{
    public class AddPrefix : Rule
    {
        public String prefix { get; set; }
        public AddPrefix() { }
        override public String Rename(String oldName)
        {
            throw new NotImplementedException();
        }
        override public Rule Create(Arguments args)
        {
            Argument_1 arg = (Argument_1)args;
            return new AddPrefix() { prefix = arg.arg1 };
        }
    }
}