using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBatchName
{
    public class RuleFactory
    {
        public RuleFactory()
        {
            initRule();
        }
        private static Dictionary<string, Rule> sampleRules = new Dictionary<string, Rule>();
        static void initRule()
        {
            sampleRules.Clear();
            sampleRules.Add("AddCounter", new AddCounter());
            sampleRules.Add("AddSuffix", new AddSuffix());
            sampleRules.Add("AddPrefix", new AddPrefix());
            sampleRules.Add("ChangeExtension", new ChangeExtension());
            sampleRules.Add("Remove", new RemoveAllSpace());
            sampleRules.Add("PascalCase", new PascalCase());
            sampleRules.Add("Lowercase", new Lowercase());
            sampleRules.Add("Replace",new Replace());
            sampleRules.Add("Duplicate", new DuplicateCase());

        }
        public Rule createRule(string strType, Arguments args)
        {
            if (constainRule(strType))
            {
                return sampleRules[strType].Create(args);
            }
            return null;
        }

        private bool constainRule(string strType)
        {
            return sampleRules.ContainsKey(strType);
        }
    }
}
