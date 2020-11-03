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
        IEnumerable<(int, char, string)> facts;
        IEnumerable<(IEnumerable<int>, int)> rules;

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

        private (IEnumerable<int>, int) ParseRule(string line)
        {
            var t = line.Trim().Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
            return (t.First().Split(',').Select(s => int.Parse(s.Trim())), int.Parse(t.Last().Trim()));

        }

        public void ReadDB()
        {
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            facts = File.ReadLines(path + "/../../facts.txt").Select(line => ParseFact(line));

            rules = File.ReadLines(path + "/../../rules.txt").Select(line => ParseRule(line));
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
            if(FwdSel.Checked)
                Go(FactsList.CheckedItems.Cast<(int, char, string)>());
            else
                Go(FactsList.CheckedItems.Cast<(int, char, string)>(), 1, ((int, char, string))BkwdResFactsList.SelectedItem);
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
    }
}
