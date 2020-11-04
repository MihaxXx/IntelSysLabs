using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IntelSysLab05
{
    public partial class Form1 : Form
    {
        //num, type, fact
        List<(int, char, string)> facts;
        List<(List<int>, int)> rules;

        public Form1()
        {
            InitializeComponent();
            ReadDB();
            foreach(var f in facts.Where(f => f.Item2 == '*'))
            {
                FactsList.Items.Add(f);
            }
            foreach (var f in facts.Where(f => f.Item2 == '/'))
            {
                BkwdResFactsList.Items.Add(f);
            }
            BkwdResFactsList.SelectedIndex = 0;
        }

        private (int, char, string) ParseFact(string line)
        {
            var t = line.Trim().Split(' ');
            if (!(t[1] == "*" || t[1] == "/"))
                return (int.Parse(t[0]), ' ', string.Join(" ", t.Skip(1)));
            else
                return (int.Parse(t[0]), t[1][0], string.Join(" ", t.Skip(2)));
        }

        private (List<int>, int) ParseRule(string line)
        {
            var t = line.Trim().Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
            return (t.First().Split(',').Select(s => int.Parse(s.Trim())).ToList(), int.Parse(t.Last().Trim()));

        }

        public void ReadDB()
        {
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            facts = File.ReadLines(path + "/../../facts.txt").Select(line => ParseFact(line)).ToList();

            rules = File.ReadLines(path + "/../../rules.txt").Select(line => ParseRule(line)).ToList();
        }

        private void BkwdSel_Click(object sender, EventArgs e)
        {
            FwdSel.Checked = !BkwdSel.Checked;
            BkwdResFactsList.Enabled = true;
        }

        private void FwdSel_Click(object sender, EventArgs e)
        {
            BkwdSel.Checked = !FwdSel.Checked;
            BkwdResFactsList.Enabled = false;
        }

        private void GoBtn_Click(object sender, EventArgs e)
        {
            LogBox.Clear();
            if (FwdSel.Checked)
                Go(FactsList.CheckedItems.Cast<(int, char, string)>());
            else
            {
                var res = Backward(FactsList.CheckedItems.Cast<(int, char, string)>().ToList(), ((int, char, string))BkwdResFactsList.SelectedItem, new HashSet<int>());
                if (res)
                    LogBox.Text += "Result: " + (((int, char, string))BkwdResFactsList.SelectedItem).ToString() + " reached.";
                else
                    LogBox.Text += "Result: " + (((int, char, string))BkwdResFactsList.SelectedItem).ToString()+ " not reached.";
                //Go(FactsList.CheckedItems.Cast<(int, char, string)>(), 1, ((int, char, string))BkwdResFactsList.SelectedItem);
            }
        }

        private void Go(IEnumerable<(int, char, string)> inFacts, int Bkwd=0, (int, char, string) res = default((int, char, string)))
        {
            var cur = new HashSet< (int, char, string)>(inFacts);
            var curNums = new HashSet<int>(cur.Select(t => t.Item1));
            List<(int, char, string)> prev;
            int fin;
            do
            {
                prev = cur.ToList();
                foreach (var r in rules)
                {
                    if (r.Item1.All(curNums.Contains) && !curNums.Contains(r.Item2))//Все требования правила выполнены
                    {
                        var ttt = facts.Where(f => f.Item1 == r.Item2).First();
                        cur.Add(ttt);
                        curNums.Add(ttt.Item1);
                        LogBox.Text += "Added fact:" + ttt.ToString()+ " by ("+string.Join(",", r.Item1)+") -> "+ r.Item2  + "\n";
                    }
                }
                fin = cur.Where(f => f.Item2 == '/').Count();
            }
            while ((Bkwd == 0 ? fin == 0 : !cur.Contains(res)) && cur.Count() > prev.Count());
            if (Bkwd == 0)
            {
                if (fin > 0)
                    LogBox.Text += "Result: " + string.Join(", ", cur.Where(f => f.Item2 == '/'));
                else
                    LogBox.Text += "Result: " + "Подходящих заведений не найдено.";
            }
            else
            {
                if (cur.Contains(res))
                    LogBox.Text += "Result: " + res.ToString() + " reached.";
                else
                    LogBox.Text += "Result: " + res.ToString() + " not reached.";
            }
        }

        public bool Backward(List<(int, char, string)> knowledge, (int, char, string) factToProve, HashSet<int> openFacts, int depth = 1)
        {
            depth++;

            // если факт в базе знаний
            if (knowledge.Contains(factToProve))
            {
                return true;
            }

            // если мы этот факт уже пытались доказать
            if (openFacts.Contains(factToProve.Item1))
            {
                return false;
            }
            else
                openFacts.Add(factToProve.Item1);

            // ищем все продукции, в которых наш факт явлется следствием
            var AcceptRules = rules.Where(r => r.Item2 == factToProve.Item1).ToList();

            // если подходящих продукций нет
            if (AcceptRules.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < AcceptRules.Count; i++)
            {
                bool f = true;

                foreach (var cond in AcceptRules[i].Item1)
                {
                    if (!Backward(knowledge, facts.Where(fct => fct.Item1 == cond).First(), openFacts, depth))
                    {
                        f = false;
                    }
                }

                if (f)
                {
                    knowledge.Add(factToProve);
                    LogBox.Text += "Added fact:" + factToProve.ToString() + " by (" + string.Join(",", AcceptRules[i].Item1) + ") -> " + AcceptRules[i].Item2 + "\n";
                    return true;
                }
            }

            return false;
        }
    }
}
