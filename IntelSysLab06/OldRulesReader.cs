using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClipsFormsExample
{
    static class OldRulesReader
    {
        static private(int, char, string) ParseFact(string line)
        {
            var t = line.Trim().Split(' ');
            if (!(t[1] == "*" || t[1] == "/"))
                return (int.Parse(t[0]), ' ', string.Join(" ", t.Skip(1)));
            else
                return (int.Parse(t[0]), t[1][0], string.Join(" ", t.Skip(2)));
        }

        static private (List<int>, int) ParseRule(string line)
        {
            var t = line.Trim().Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
            return (t.First().Split(',').Select(s => int.Parse(s.Trim())).ToList(), int.Parse(t.Last().Trim()));

        }

        static public List<(int, char, string)> ReadFacts()
        {
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (!File.Exists(path + "/../../facts.txt"))
                return null;
            return File.ReadLines(path + "/../../facts.txt").Select(line => ParseFact(line)).ToList();
        }
        static public List<(List<int>, int)> ReadRules()
        {
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (!File.Exists(path + "/../../rules.txt"))
                return null;
            return File.ReadLines(path + "/../../rules.txt").Select(line => ParseRule(line)).ToList();
        }
        static public string ConvertRulesToClips(List<(int, char, string)> facts, List<(List<int>, int)> rules)
        {
            string res = string.Empty;
            foreach(var rule in rules.Select(r => (r.Item1.Select(req => facts[req-1].Item3.Replace(' ','_').Replace("\"","")),facts[r.Item2-1].Item3.Replace(' ', '_').Replace("\"", ""))))
            {
                res +=
                    $"(defrule {string.Join("-",rule.Item1)}-{rule.Item2}" + "\r\n" +
                    "(declare(salience 40))" + "\r\n" +
                    string.Join("\r\n",rule.Item1.Select(req => $"(element(param {req}))"))+
                    "\r\n" + "=>" + "\r\n" +
                    $"(assert(element(param {rule.Item2})))" + "\r\n" +
                    $"(assert(appendmessagehalt \"{string.Join(" + ",rule.Item1)} -> {rule.Item2}\"))" + "\r\n" +
                    ")\r\n\r\n";
            }

            return res;
        }
    }
}
