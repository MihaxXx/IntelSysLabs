using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoFormsExample
{
    public partial class ConfidenceDialog : Form
    {
        public string fact;
        public int confidence = 0;
        public ConfidenceDialog(string _fact)
        {
            InitializeComponent();
            fact = _fact;
            msg.Text += _fact;
            this.Text = _fact;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            confidence = trackBar1.Value;
            DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            curVal.Text = trackBar1.Value.ToString();
        }
    }
}
