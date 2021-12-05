using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectBatchName
{
    public class ChangeExtension : Rule
    {
        public String ext { get; set; }
        public ChangeExtension() { }
        override public String Rename(String oldName)
        {
            //Kiểm tra extension có phải đuôi có thể chuyển, ngược lại return null;
            string str = Path.GetFileNameWithoutExtension(oldName);
            string extension = Path.ChangeExtension(Path.GetExtension(oldName), ext);
            str += extension;
            return str;
            //return "";
            //File file = new File(str); 
            //if (FileLoadException(file)) {
            //    return null;
            //} else {
            //    return str;
            //}
        }
        override public Rule Create(Arguments args)
        {
            Argument_1 arg = (Argument_1)args;
            return new ChangeExtension() { ext = arg.arg1 };
        }
    }
}
