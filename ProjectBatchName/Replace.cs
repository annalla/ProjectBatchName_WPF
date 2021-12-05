using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class Replace : Rule
    {
        public String oldRep { get; set; }
        public String newRep { get; set; }
        override public String Rename(String oldName)
        {
            string str = "";
            //Replacing oldRep into space newRep
            if (oldRep == Path.GetFileNameWithoutExtension(oldName))
            {
                str = Path.GetFileNameWithoutExtension(oldName);
            }
            str = newRep;
            return str + Path.GetExtension(oldName);
        }
        override public Rule Create(Arguments args)
        {
            Argument_2 arg = (Argument_2)args;
            return new Replace() { oldRep = arg.arg1, newRep = arg.arg2 };
        }
    }
}