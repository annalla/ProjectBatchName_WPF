using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBatchName
{
    public class AddCounter : Rule
    {

        public int startValue { get; set; }
        public int steps { get; set; }
        public int numberOfDigit { get; set; }
        public AddCounter() { }
        override public String Rename(String oldName)
        {
            throw new NotImplementedException();
        }
        override public Rule Create(Arguments args)
        {
            Argument_3 arg = (Argument_3)args;
            return new AddCounter() { startValue = arg.arg1, steps = arg.arg2, numberOfDigit = arg.arg3 };
        }
    }
}