using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBatchName
{
    public class ChangeExtension : Rule
    {
        public String ext { get; set; }
        public ChangeExtension() { }
        override public String Rename(String oldName)
        {
            //...
            //Kiểm tra extension có phải đuôi có thể chuyển, ngược lại return null;
            return ext;
        }
        override public Rule Create(Arguments args)
        {
            Argument_1 arg = (Argument_1)args;
            return new ChangeExtension() { ext = arg.arg1 };
        }
    }
}
