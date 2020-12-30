namespace PaternRecognition
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
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label Thershold;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label2;
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.cmbVideoSource = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button3 = new System.Windows.Forms.Button();
            this.ResLabel = new System.Windows.Forms.Label();
            this.NetSettingBox = new System.Windows.Forms.GroupBox();
            this.parallelCheckBox = new System.Windows.Forms.CheckBox();
            this.netStructureBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.netTypeBox = new System.Windows.Forms.ComboBox();
            this.button7 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.EpochesCounter = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            Thershold = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.NetSettingBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EpochesCounter)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(17, 118);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(69, 17);
            label6.TabIndex = 21;
            label6.Text = "Max Error";
            // 
            // Thershold
            // 
            Thershold.AutoSize = true;
            Thershold.Location = new System.Drawing.Point(577, 436);
            Thershold.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            Thershold.Name = "Thershold";
            Thershold.Size = new System.Drawing.Size(47, 17);
            Thershold.TabIndex = 16;
            Thershold.Text = "Порог";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(17, 87);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(119, 17);
            label5.TabIndex = 1006;
            label5.Text = "Количество эпох";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(17, 60);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(110, 17);
            label2.TabIndex = 1003;
            label2.Text = "Структура сети";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(15, 14);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(515, 395);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnStart.Location = new System.Drawing.Point(419, 432);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(111, 54);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Начать";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // cmbVideoSource
            // 
            this.cmbVideoSource.FormattingEnabled = true;
            this.cmbVideoSource.Location = new System.Drawing.Point(15, 466);
            this.cmbVideoSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbVideoSource.Name = "cmbVideoSource";
            this.cmbVideoSource.Size = new System.Drawing.Size(265, 24);
            this.cmbVideoSource.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(580, 14);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(491, 395);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(644, 430);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar1.Maximum = 1000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(427, 56);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.TickFrequency = 100;
            this.trackBar1.Value = 150;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(20, 306);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 50);
            this.button3.TabIndex = 9;
            this.button3.Text = "Распознать";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ResLabel
            // 
            this.ResLabel.AutoSize = true;
            this.ResLabel.Location = new System.Drawing.Point(162, 233);
            this.ResLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ResLabel.Name = "ResLabel";
            this.ResLabel.Size = new System.Drawing.Size(0, 17);
            this.ResLabel.TabIndex = 10;
            // 
            // NetSettingBox
            // 
            this.NetSettingBox.Controls.Add(this.parallelCheckBox);
            this.NetSettingBox.Controls.Add(this.netStructureBox);
            this.NetSettingBox.Controls.Add(label5);
            this.NetSettingBox.Controls.Add(label2);
            this.NetSettingBox.Controls.Add(this.label11);
            this.NetSettingBox.Controls.Add(this.netTypeBox);
            this.NetSettingBox.Controls.Add(this.button7);
            this.NetSettingBox.Controls.Add(this.ResLabel);
            this.NetSettingBox.Controls.Add(this.textBox1);
            this.NetSettingBox.Controls.Add(this.button2);
            this.NetSettingBox.Controls.Add(this.comboBox1);
            this.NetSettingBox.Controls.Add(this.button4);
            this.NetSettingBox.Controls.Add(this.button1);
            this.NetSettingBox.Controls.Add(this.button3);
            this.NetSettingBox.Controls.Add(this.EpochesCounter);
            this.NetSettingBox.Controls.Add(label6);
            this.NetSettingBox.Location = new System.Drawing.Point(1100, 14);
            this.NetSettingBox.Margin = new System.Windows.Forms.Padding(4);
            this.NetSettingBox.Name = "NetSettingBox";
            this.NetSettingBox.Padding = new System.Windows.Forms.Padding(4);
            this.NetSettingBox.Size = new System.Drawing.Size(321, 488);
            this.NetSettingBox.TabIndex = 15;
            this.NetSettingBox.TabStop = false;
            this.NetSettingBox.Text = "Параметры сети";
            // 
            // parallelCheckBox
            // 
            this.parallelCheckBox.AutoSize = true;
            this.parallelCheckBox.Checked = true;
            this.parallelCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.parallelCheckBox.Location = new System.Drawing.Point(20, 204);
            this.parallelCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.parallelCheckBox.Name = "parallelCheckBox";
            this.parallelCheckBox.Size = new System.Drawing.Size(179, 21);
            this.parallelCheckBox.TabIndex = 1011;
            this.parallelCheckBox.Text = "Параллельный расчёт";
            this.parallelCheckBox.UseVisualStyleBackColor = true;
            // 
            // netStructureBox
            // 
            this.netStructureBox.Location = new System.Drawing.Point(150, 55);
            this.netStructureBox.Margin = new System.Windows.Forms.Padding(4);
            this.netStructureBox.Name = "netStructureBox";
            this.netStructureBox.Size = new System.Drawing.Size(159, 22);
            this.netStructureBox.TabIndex = 1010;
            this.netStructureBox.Text = "784;1568;100;9";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 30);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 17);
            this.label11.TabIndex = 1002;
            this.label11.Text = "Сеть";
            // 
            // netTypeBox
            // 
            this.netTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.netTypeBox.FormattingEnabled = true;
            this.netTypeBox.Items.AddRange(new object[] {
            "Accord.Net",
            "Hand Crafted"});
            this.netTypeBox.Location = new System.Drawing.Point(150, 23);
            this.netTypeBox.Margin = new System.Windows.Forms.Padding(4);
            this.netTypeBox.Name = "netTypeBox";
            this.netTypeBox.Size = new System.Drawing.Size(160, 24);
            this.netTypeBox.TabIndex = 1001;
            this.netTypeBox.SelectedIndexChanged += new System.EventHandler(this.netTypeBox_SelectedIndexChanged);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(8, 400);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(160, 44);
            this.button7.TabIndex = 18;
            this.button7.Text = "Обучить образцу";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(150, 115);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 22);
            this.textBox1.TabIndex = 29;
            this.textBox1.Text = "0.0005";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(194, 400);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 76);
            this.button2.TabIndex = 16;
            this.button2.Text = "Сохранить изображение";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "close",
            "like",
            "minus",
            "pause",
            "play",
            "plus",
            "repeat",
            "rewindbackward",
            "rewindforward"});
            this.comboBox1.Location = new System.Drawing.Point(8, 452);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 24);
            this.comboBox1.TabIndex = 17;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(20, 233);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(119, 50);
            this.button4.TabIndex = 25;
            this.button4.Text = "Обучить сеть";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(82, 154);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 42);
            this.button1.TabIndex = 24;
            this.button1.Text = "Пересоздать сеть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EpochesCounter
            // 
            this.EpochesCounter.Location = new System.Drawing.Point(150, 85);
            this.EpochesCounter.Margin = new System.Windows.Forms.Padding(4);
            this.EpochesCounter.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.EpochesCounter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.EpochesCounter.Name = "EpochesCounter";
            this.EpochesCounter.Size = new System.Drawing.Size(99, 22);
            this.EpochesCounter.TabIndex = 1000;
            this.EpochesCounter.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.EpochesCounter.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 441);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "Выберете камеру";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1439, 510);
            this.Controls.Add(this.label1);
            this.Controls.Add(Thershold);
            this.Controls.Add(this.NetSettingBox);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.cmbVideoSource);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.NetSettingBox.ResumeLayout(false);
            this.NetSettingBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EpochesCounter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cmbVideoSource;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label ResLabel;
        private System.Windows.Forms.GroupBox NetSettingBox;
        private System.Windows.Forms.NumericUpDown EpochesCounter;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox netTypeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox netStructureBox;
        private System.Windows.Forms.CheckBox parallelCheckBox;
    }
}

