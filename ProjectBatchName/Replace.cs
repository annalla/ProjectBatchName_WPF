using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBatchName
{
    public class Replace : Rule
    {
        public String oldRep { get; set; }
        public String newRep { get; set; }
        override public String Rename(String oldName)
        {
            throw new NotImplementedException();
        }
        override public Rule Create(Arguments args)
        {
            Argument_2 arg = (Argument_2)args;
            return new Replace() { oldRep = arg.arg1, newRep = arg.arg2 };
        }
    }
}