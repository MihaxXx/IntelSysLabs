namespace IntelSysLab05
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.FactsList = new System.Windows.Forms.CheckedListBox();
            this.FwdSel = new System.Windows.Forms.RadioButton();
            this.BkwdSel = new System.Windows.Forms.RadioButton();
            this.BkwdResFactsList = new System.Windows.Forms.ComboBox();
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.GoBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FactsList
            // 
            this.FactsList.FormattingEnabled = true;
            this.FactsList.Location = new System.Drawing.Point(13, 13);
            this.FactsList.Name = "FactsList";
            this.FactsList.Size = new System.Drawing.Size(344, 424);
            this.FactsList.TabIndex = 0;
            // 
            // FwdSel
            // 
            this.FwdSel.AutoSize = true;
            this.FwdSel.Checked = true;
            this.FwdSel.Location = new System.Drawing.Point(444, 12);
            this.FwdSel.Name = "FwdSel";
            this.FwdSel.Size = new System.Drawing.Size(65, 17);
            this.FwdSel.TabIndex = 1;
            this.FwdSel.TabStop = true;
            this.FwdSel.Text = "Прямой";
            this.FwdSel.UseVisualStyleBackColor = true;
            this.FwdSel.Click += new System.EventHandler(this.FwdSel_Click);
            // 
            // BkwdSel
            // 
            this.BkwdSel.AutoSize = true;
            this.BkwdSel.Location = new System.Drawing.Point(444, 36);
            this.BkwdSel.Name = "BkwdSel";
            this.BkwdSel.Size = new System.Drawing.Size(76, 17);
            this.BkwdSel.TabIndex = 2;
            this.BkwdSel.Text = "Обратный";
            this.BkwdSel.UseVisualStyleBackColor = true;
            this.BkwdSel.Click += new System.EventHandler(this.BkwdSel_Click);
            // 
            // BkwdResFactsList
            // 
            this.BkwdResFactsList.Enabled = false;
            this.BkwdResFactsList.FormattingEnabled = true;
            this.BkwdResFactsList.Location = new System.Drawing.Point(444, 59);
            this.BkwdResFactsList.Name = "BkwdResFactsList";
            this.BkwdResFactsList.Size = new System.Drawing.Size(343, 21);
            this.BkwdResFactsList.TabIndex = 3;
            // 
            // LogBox
            // 
            this.LogBox.Location = new System.Drawing.Point(444, 88);
            this.LogBox.Name = "LogBox";
            this.LogBox.Size = new System.Drawing.Size(344, 350);
            this.LogBox.TabIndex = 4;
            this.LogBox.Text = "";
            // 
            // GoBtn
            // 
            this.GoBtn.Location = new System.Drawing.Point(364, 211);
            this.GoBtn.Name = "GoBtn";
            this.GoBtn.Size = new System.Drawing.Size(75, 23);
            this.GoBtn.TabIndex = 5;
            this.GoBtn.Text = "->";
            this.GoBtn.UseVisualStyleBackColor = true;
            this.GoBtn.Click += new System.EventHandler(this.GoBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GoBtn);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.BkwdResFactsList);
            this.Controls.Add(this.BkwdSel);
            this.Controls.Add(this.FwdSel);
            this.Controls.Add(this.FactsList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox FactsList;
        private System.Windows.Forms.RadioButton FwdSel;
        private System.Windows.Forms.RadioButton BkwdSel;
        private System.Windows.Forms.ComboBox BkwdResFactsList;
        private System.Windows.Forms.RichTextBox LogBox;
        private System.Windows.Forms.Button GoBtn;
    }
}

