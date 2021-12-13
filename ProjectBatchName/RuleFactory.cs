using BatchNameRule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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

        private static List<string> rules = new List<string>();
        static void initRule()
        {
            sampleRules.Clear();

            // Nạp danh sách các tập tin dll
            string exePath = Assembly.GetExecutingAssembly().Location;
            string folder = Path.GetDirectoryName(exePath);
            var fis = new DirectoryInfo(folder).GetFiles("*.dll");

            foreach (var f in fis) // Lần lượt duyệt qua các file dll
            {
                var assembly = Assembly.LoadFile(f.FullName);
                var types = assembly.GetTypes();

                foreach (var t in types)
                {
                    if (t.IsClass && typeof(Rule).IsAssignableFrom(t))
                    {
                        Rule c = (Rule)Activator.CreateInstance(t);
                        sampleRules.Add(c.GetName(),c);
                        rules.Add(c.GetName());
                    }
                }
            }
            rules.Remove("Duplicate");
        }
        public List<string> getRules()
        {
            return rules;
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
