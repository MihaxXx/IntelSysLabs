﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Video;
using AForge.Video.DirectShow;

namespace NeuralNetwork1
{

	public delegate void FormUpdater(double progress, double error, TimeSpan time);

    public delegate void UpdateTLGMessages(string msg);

    public partial class Form1 : Form
    {
        /// <summary>
        /// Чат-бот AIML
        /// </summary>
        AIMLBotik botik = new AIMLBotik();

        TLGBotik tlgBot;

        /// <summary>
        /// Генератор изображений (образов)
        /// </summary>
        GenerateImage generator = new GenerateImage();
        
        /// <summary>
        /// Обёртка для ActivationNetwork из Accord.Net
        /// </summary>
        AccordNet AccordNet = null;

        /// <summary>
        /// Абстрактный базовый класс, псевдоним либо для CustomNet, либо для AccordNet
        /// </summary>
        BaseNetwork net = null;

        string imgFile = "";

        private FilterInfoCollection videoDevicesList;
        private Camera processor = new Camera();

        public Form1()
        {
            InitializeComponent();
            tlgBot = new TLGBotik(net, new UpdateTLGMessages(UpdateTLGInfo));
            generator.figure_count = (int)classCounter.Value;
            button3_Click(this, null);
            pictureBox1.Image = Properties.Resources.Title;

            imgFile = System.IO.Directory.GetCurrentDirectory() + "\\";
        }

		public void UpdateLearningInfo(double progress, double error, TimeSpan elapsedTime)
		{
			if (progressBar1.InvokeRequired)
			{
				progressBar1.Invoke(new FormUpdater(UpdateLearningInfo),new Object[] {progress, error, elapsedTime});
				return;
			}
            StatusLabel.Text = "Accuracy: " + error.ToString();
            int prgs = (int)Math.Round(progress*100);
			prgs = Math.Min(100, Math.Max(0,prgs));
            elapsedTimeLabel.Text = "Затраченное время : " + elapsedTime.Duration().ToString(@"hh\:mm\:ss\:ff");
            progressBar1.Value = prgs;
		}

        public void UpdateTLGInfo(string message)
        {
            if (TLGUsersMessages.InvokeRequired)
            {
                TLGUsersMessages.Invoke(new UpdateTLGMessages(UpdateTLGInfo), new Object[] { message });
                return;
            }
            TLGUsersMessages.Text += message + Environment.NewLine;
        }

        private void set_result(Sample figure)
        {
            label1.Text = figure.ToString();

            if (figure.Correct())
                label1.ForeColor = Color.Green;
            else
                label1.ForeColor = Color.Red;

            label1.Text = "Распознано : " + figure.recognizedClass.ToString();

            label8.Text = String.Join("\n", net.getOutput().Select(d => d.ToString()));
            pictureBox1.Image = generator.genBitmap();
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //Sample fig = generator.GenerateFigure();

            //net.Predict(fig);

            //set_result(fig);

            /*var rnd = new Random();
            var fname = "pic" + (rnd.Next() % 100).ToString() + ".jpg";
            pictureBox1.Image.Save(fname);*/

            Image img = (Image)processor.number.Clone();
            Sample fig = generator.Generate((Bitmap)img);

            net.Predict(fig);

            set_result(fig);

        }

        private double train_networkAsync(int training_size, int epoches, double acceptable_error, bool parallel = true)
        {

            //  Выключаем всё ненужное
            label1.Text = "Выполняется обучение...";
            label1.ForeColor = Color.Red;
            groupBox1.Enabled = false;
            pictureBox1.Enabled = false;
            trainOneButton.Enabled = false;

            //  Создаём новую обучающую выборку
            SamplesSet samples = new SamplesSet();


            int cnt_g = training_size / 5;
           //int cnt_g = training_size / 26;

            int cnt_gg = cnt_g;

            int d = 0;

            int char_c = 65;

            int j = 1;

            for (int i = 0; i < training_size; i++)
            {
                if (char_c > 90) continue;

                int cnt = new System.IO.DirectoryInfo(imgFile + "\\" + (char)(char_c)).GetFiles().Length;

                if (cnt_g < cnt) cnt = cnt_g;

                if (char_c == 14 + 65)
                    d++;

                if (j > cnt_g || cnt < 0 || j > cnt)
                {
                    j++;
                    //continue;
                }
                else
                {
                    string s = imgFile + (char)(char_c) + "\\" + (char)(char_c) + j.ToString() + ".bmp";
                    Bitmap img = new Bitmap(Image.FromFile(s));
                    Sample fig = generator.Generate(img, (char)(char_c));
                    samples.AddSample(fig);
                    j++;
                }


                if (i == cnt_g)
                {
                    char_c++;
                    cnt_g += cnt_gg;
                    j = 1;
                }
            }

            //  Обучение запускаем асинхронно, чтобы не блокировать форму
            double f = net.TrainOnDataSet(samples, epoches, acceptable_error, parallel);

            label1.Text = "Щелкните на картинку для теста нового образа";
            label1.ForeColor = Color.Green;
            groupBox1.Enabled = true;
            pictureBox1.Enabled = true;
            trainOneButton.Enabled = true;
            StatusLabel.Text = "Accuracy: " + f.ToString();
            StatusLabel.ForeColor = Color.Green;
            return f;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            train_networkAsync((int)TrainingSizeCounter.Value, (int)EpochesCounter.Value, (100 - AccuracyCounter.Value) / 100.0, parallelCheckBox.Checked);
            #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            //  Тут просто тестирование новой выборки
            //  Создаём новую обучающую выборку
            SamplesSet samples = new SamplesSet();

            int cnt_g = (int)TrainingSizeCounter.Value / 26;


            for (int i = 0; i < (int)TrainingSizeCounter.Value; i++)
            {
                int cnt = new System.IO.DirectoryInfo(imgFile + "\\" + (char)(i + 65)).GetFiles().Length;

                if (cnt_g < cnt) cnt = cnt_g;

                for (int j = 1; j <= cnt; j++)
                {
                    Bitmap img = new Bitmap(Image.FromFile(imgFile + "\\" + (char)(i + 65) + "\\" + (char)(i + 65) + j.ToString() + ".bmp"));
                    Sample fig = generator.Generate(img, (char)(i + 65));
                    samples.AddSample(fig);
                }
            }

            double accuracy = net.TestOnDataSet(samples);

            StatusLabel.Text = string.Format("Точность на тестовой выборке : {0,5:F2}%", accuracy * 100);
            if (accuracy * 100 >= AccuracyCounter.Value)
                StatusLabel.ForeColor = Color.Green;
            else
                StatusLabel.ForeColor = Color.Red;

            this.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //  Проверяем корректность задания структуры сети
            int[] structure = netStructureBox.Text.Split(';').Select((c) => int.Parse(c)).ToArray();
            if (structure.Length < 2)
            {
                MessageBox.Show("А давайте вы структуру сети нормально запишите, ОК?", "Ошибка", MessageBoxButtons.OK);
                return;
            };

            
            AccordNet = new AccordNet(structure);
            AccordNet.updateDelegate = UpdateLearningInfo;

            net = AccordNet;

            tlgBot.SetNet(net);

        }

        private void classCounter_ValueChanged(object sender, EventArgs e)
        {
            generator.figure_count = (int)classCounter.Value;
            var vals = netStructureBox.Text.Split(';');
            int outputNeurons;
            if (int.TryParse(vals.Last(),out outputNeurons))
            {
                vals[vals.Length - 1] = classCounter.Value.ToString();
                netStructureBox.Text = vals.Aggregate((partialPhrase, word) => $"{partialPhrase};{word}");
            }
        }

        private void btnTrainOne_Click(object sender, EventArgs e)
        {
            //if (net == null) return;
            //int cnt = new System.IO.DirectoryInfo(imgFile + "\\" + comboBox1.SelectedItem).GetFiles().Length;

            ////Bitmap b = new Bitmap(processor.number.Width, processor.number.Height);
            //// b = processor.number;
            ////cnt++;
            ////string s = imgFile + comboBox1.SelectedItem + "\\" + comboBox1.SelectedItem + cnt.ToString() + ".bmp";
            //// b.Save(s);

            //Bitmap img = new Bitmap(Image.FromFile(imgFile + comboBox1.SelectedItem + "\\" + comboBox1.SelectedItem + cnt.ToString() + ".bmp"));
            //Sample fig = generator.Generate(img, (char)comboBox1.SelectedItem);
            //pictureBox1.Image = generator.genBitmap();
            //pictureBox1.Invalidate();
            //net.Train(fig, false);
            //set_result(fig);
        }

        private void netTrainButton_MouseEnter(object sender, EventArgs e)
        {
            infoStatusLabel.Text = "Обучить нейросеть с указанными параметрами";
        }

        private void testNetButton_MouseEnter(object sender, EventArgs e)
        {
            infoStatusLabel.Text = "Тестировать нейросеть на тестовой выборке такого же размера";
        }

        private void netTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            net = AccordNet;
        }

        private void recreateNetButton_MouseEnter(object sender, EventArgs e)
        {
            infoStatusLabel.Text = "Заново пересоздаёт сеть с указанными параметрами";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var phrase = AIMLInput.Text;
            if (phrase.Length > 0)
                AIMLOutput.Text += botik.Talk(phrase) + Environment.NewLine;
        }

        private void TLGBotOnButton_Click(object sender, EventArgs e)
        {
            tlgBot.Act();
            TLGBotOnButton.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void netStructureBox_TextChanged(object sender, EventArgs e)
        {

        }
    }

  }
