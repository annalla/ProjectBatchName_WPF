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
            sampleRules.Add("counter", new AddCounter());
            sampleRules.Add("suffix", new AddSuffix());
            sampleRules.Add("prefix", new AddPrefix());
            sampleRules.Add("ext", new ChangeExtension());
            sampleRules.Add("remove", new RemoveAllSpace());
            sampleRules.Add("pascalcase", new PascalCase());
            sampleRules.Add("lowercase", new Lowercase());
            sampleRules.Add("replace",new Replace());
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
