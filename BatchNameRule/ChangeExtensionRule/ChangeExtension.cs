using BatchNameRule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class ChangeExtension : Rule
    {
        override public String GetName() => "ChangeExtension";

        public string ext { get; set; }
        override public string ArgumentString()
        {
            return ext;
        }
        public ChangeExtension() { }
        override public String Rename(String oldName)
        {
            //Kiểm tra extension có phải đuôi có thể chuyển, ngược lại return null;
            string str = Path.GetFileNameWithoutExtension(oldName);
            string extension = Path.ChangeExtension(Path.GetExtension(oldName), ext);
            str += extension;
            if (str.Length <= 255)
                return str;
            return "|";
        }
        override public Rule Create(Arguments args)
        {
            Argument_1 arg = (Argument_1)args;
            return new ChangeExtension() { ext = arg.arg1 };
        }
        override public Rule Clone()
        {
            return new ChangeExtension();
        }
    }
}
