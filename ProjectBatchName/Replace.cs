using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class Replace : Rule
    {
        public string name { get => "replace"; }
        public String oldRep { get; set; }
        public String newRep { get; set; }
        override public String Rename(String oldName)
        {
            //Replacing oldRep into space newRep
            
            string str = Path.GetFileNameWithoutExtension(oldName);
            str = str.Replace(oldRep,newRep);
            string result = str + Path.GetExtension(oldName);
            if (result.Length <= 255)
                return result;
            return "|";
        }
        override public Rule Create(Arguments args)
        {
            Argument_2 arg = (Argument_2)args;
            return new Replace() { oldRep = arg.arg1, newRep = arg.arg2 };
        }
        override public Rule Clone()
        {
            return new Replace();
        }
    }
}