using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class AddCounter : Rule
    {
        override public String GetName() => "AddCounter";
        public int startValue { get; set; }
        public int steps { get; set; }
        public int numberOfDigit { get; set; }
        public AddCounter() { }
        override public string ArgumentString()
        {
            return startValue.ToString() + "|" + steps.ToString()+"|"+numberOfDigit;
        }
        override public String Rename(String oldName)
        {
            string str = Path.GetFileNameWithoutExtension(oldName);
            string counter = startValue.ToString();
            while (counter.Length < numberOfDigit)
            {
                counter = "0" + counter;
            }
            this.startValue += this.steps;
            string result = str + "(" +counter + ")" + Path.GetExtension(oldName);
            if (result.Length <= 255)
                return result;
            return "|";
        }
        override public Rule Create(Arguments args)
        {
            Argument_3 arg = (Argument_3)args;
            return new AddCounter() { startValue = arg.arg1, steps = arg.arg2, numberOfDigit = arg.arg3 };
        }
        override public Rule Clone()
        {
            return new AddCounter();
        }
    }
}