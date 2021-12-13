using System;

namespace BatchNameRule
{
    abstract public class Rule
    {
        abstract public String Rename(String oldName);
        abstract public Rule Create(Arguments args);
        abstract public Rule Clone();
        abstract public string ArgumentString();
        abstract public String GetName();

    }
}
