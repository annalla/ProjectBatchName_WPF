using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProjectBatchName
{
    abstract public class Rule
    {
        abstract public String Rename(String oldName);
        abstract public Rule Create(Arguments args);

    }
}
