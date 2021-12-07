using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    class DuplicateCase:Rule
    {
        public string name { get => "Duplicate"; }
        public int start { get; set; }
        public DuplicateCase() { }
        override public String Rename(String oldName)
        {
            string str = Path.GetFileNameWithoutExtension(oldName);
            string counter = start.ToString();            
            string result = str + "_duplicate_" + counter + "" + Path.GetExtension(oldName);
            if (result.Length <= 255)
                return result;
            return "|";
        }
        override public Rule Create(Arguments args)
        {
            Argument_1 arg = (Argument_1)args;
            return new DuplicateCase() { start = Int32.Parse(arg.arg1) };
        }
        override public Rule Clone()
        {
            return new DuplicateCase();
        }
    }
}
