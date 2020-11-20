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

using CLIPSNET;


namespace ClipsFormsExample
{
    public partial class ClipsFormsExample : Form
    {
        //num, type, fact
        List<(int, char, string)> facts;
        List<(List<int>, int)> rules;

        private CLIPSNET.Environment clips = new CLIPSNET.Environment();

        public ClipsFormsExample()
        {
            InitializeComponent();
            facts = OldRulesReader.ReadFacts();
            rules = OldRulesReader.ReadRules();
            foreach (var f in facts.Where(f => f.Item2 == '*'))
            {
                FactsList.Items.Add(f);
            }
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            File.WriteAllText(path + "/../../rules.clp", OldRulesReader.ConvertRulesToClips(facts, rules));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void HandleResponse()
        {
            //  Вытаскиаваем факт из ЭС
            String evalStr = "(find-fact ((?f ioproxy)) TRUE)";
            FactAddressValue fv = (FactAddressValue)((MultifieldValue)clips.Eval(evalStr))[0];

            MultifieldValue damf = (MultifieldValue)fv["messages"];
            MultifieldValue vamf = (MultifieldValue)fv["answers"];

            outputBox.Text += "Новая итерация : " + System.Environment.NewLine;
            for (int i = 0; i < damf.Count; i++)
            {
                LexemeValue da = (LexemeValue)damf[i];
                byte[] bytes = Encoding.Default.GetBytes(da.Value);
                string message = Encoding.UTF8.GetString(bytes);
                if (message.StartsWith("[4:20]"))
                {
                    MessageBox.Show(message, "[4:20]", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    this.Close();
                }
                if (message.StartsWith("[S]"))
                {
                    var result = MessageBox.Show("Вы довольны предложенным вариантом?", "[S]", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes || result == DialogResult.No)
                    {
                        string answr = result == DialogResult.Yes ? "Удачный_выбор" : "Неудачный_выбор";
                        clips.Eval($"(assert(element(param { answr })))");
                        outputBox.Text += "Добавлен факт: " + answr + System.Environment.NewLine;
                    }
                }
                else
                    outputBox.Text += message + System.Environment.NewLine;

            }

            var phrases = new List<string>();
            if (vamf.Count > 0)
            {
                outputBox.Text += "----------------------------------------------------" + System.Environment.NewLine;
                for (int i = 0; i < vamf.Count; i++)
                {
                    //  Варианты !!!!!
                    LexemeValue va = (LexemeValue)vamf[i];
                    byte[] bytes = Encoding.Default.GetBytes(va.Value);
                    string message = Encoding.UTF8.GetString(bytes);
                    phrases.Add(message);
                    outputBox.Text += "Добавлен вариант для распознавания " + message + System.Environment.NewLine;
                }
            }
            
            if(vamf.Count == 0)
                clips.Eval("(assert (clearmessage))");
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            clips.Run();
            HandleResponse();
            outputBox.SelectionStart = outputBox.Text.Length;
            outputBox.ScrollToCaret();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            outputBox.Text = "Выполнены команды Clear и Reset." + System.Environment.NewLine;
            //  Здесь сохранение в файл, и потом инициализация через него
            clips.Clear();

            /*string stroka = codeBox.Text;
            System.IO.File.WriteAllText("tmp.clp", codeBox.Text);
            clips.Load("tmp.clp");*/

            //  Так тоже можно - без промежуточного вывода в файл
            clips.LoadFromString(codeBox.Text);

            clips.Reset();
            factButton.Enabled = true;
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            if (clipsOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                codeBox.Text += File.ReadAllText(clipsOpenFileDialog.FileName)+"\r\n";
                Text = "Экспертная система \"Тиндер\" – " + clipsOpenFileDialog.FileName;
            }
        }

        private void fontSelect_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                codeBox.Font = fontDialog1.Font;
                outputBox.Font = fontDialog1.Font;
            }
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            clipsSaveFileDialog.FileName = clipsOpenFileDialog.FileName;
            if (clipsSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(clipsSaveFileDialog.FileName, codeBox.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var selFact in FactsList.CheckedItems.Cast<(int, char, string)>().Select(f => f.Item3.Replace(' ', '_').Replace("\"", "")))
            {
                clips.Eval($"(assert (element (param {selFact})))");
                outputBox.Text += $"Добавлен факт: {selFact}\r\n";
            }
        }

    }
}
