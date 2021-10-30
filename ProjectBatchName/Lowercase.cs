using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBatchName
{
    public class Lowercase : Rule
    {
        public Lowercase() { }
        override public String Rename(String oldName)
        {
            throw new NotImplementedException();
        }
        override public Rule Create(Arguments args)
        {
            return new Lowercase();
        }
    }
}