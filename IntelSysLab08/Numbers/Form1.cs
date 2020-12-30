using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaternRecognition
{
    public partial class Form1 : Form
    {
        private IVideoSource videoSource;
        private FilterInfoCollection videoDevices;

        private MagicEye processor = new MagicEye();

        BaseNetwork net = null;
        NeuralNetwork NeuralNet = null;
        AccordNet AccordNet = null;

        FigureType recognizedClass;

        string pathToImg = @"C:\Users\Mikhail\source\repos\NeuralNetwork2\images";
        static int symbolsCount = 9;
        private double max_error = 0.05;
        private int epoches = 100;

        public Form1()
        {
            InitializeComponent();

            //источники видео
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in videoDevices)
            {
                cmbVideoSource.Items.Add(device.Name);
            }
            if (cmbVideoSource.Items.Count > 0)
            {
                cmbVideoSource.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Камера не найдена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            netTypeBox.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            button1_Click(this, null);
        }


        //получаем картинку
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            processor.ProcessImage((Bitmap)eventArgs.Frame.Clone());
            processor.GetPicture(out Bitmap or, out Bitmap num);
            pictureBox1.Image = or;
            pictureBox2.Image = num;
        }

        //открываем и закрываем источник с видио
        private void buttonStart_Click(object sender, EventArgs e)
        {
            CloseOpenVideoSource();
        }

        //открываем и закрываем источник с видио
        void CloseOpenVideoSource()
        {
            if (videoSource == null)
            {
                videoSource = new VideoCaptureDevice(videoDevices[cmbVideoSource.SelectedIndex].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                videoSource.Start();
                btnStart.Text = "Stop";
            }
            else
            {
                videoSource.SignalToStop();
                videoSource = null;
                btnStart.Text = "Start";
            }

        }

        // Чтобы вебка не падала в обморок при неожиданном закрытии окна
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnStart.Text == "Stop")
                videoSource.SignalToStop();
            if (videoSource != null && videoSource.IsRunning && pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
            videoSource = null;
        }
    
        private void Form1_Load(object sender, EventArgs e)
        {
            CloseOpenVideoSource();
        }

        //порог распознавания
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            processor.ThresholdValue = (float)trackBar1.Value / 1000;  
        }

        //распознаем текущее  изображение
        private void button3_Click(object sender, EventArgs e)
        {
            recognizeFromScreen();
            ShowResult();   
        }


        private void recognizeFromScreen()
        {
            Sample currentImage = new Sample(imgToData(processor.GetImage()), symbolsCount);
            recognizedClass = net.Predict(currentImage);
        }

        //преобразуем изображение в массив значений
        private double[] imgToData(AForge.Imaging.UnmanagedImage img)
        {
            double[] res = new double[img.Width * img.Height];
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    //GetBrightness Возвращает значение освещенности (оттенок-насыщенность-освещенность (HSL)) для данной структуры
                    res[i * img.Width + j] = img.GetPixel(i, j).GetBrightness(); // maybe threshold
                }
            }
            return res;
        }
      
        //меняем количество эпох
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            epoches = (int)EpochesCounter.Value;
        }

        //сбросить сеть
        private void button1_Click(object sender, EventArgs e)
        {
            //  Проверяем корректность задания структуры сети
            int[] structure = netStructureBox.Text.Split(';').Select((c) => int.Parse(c)).ToArray();
            if (structure.Length < 2 || structure[0] != 784 || structure[structure.Length - 1] != symbolsCount)
            {
                MessageBox.Show("А давайте вы структуру сети нормально запишите, ОК?", "Ошибка", MessageBoxButtons.OK);
                return;
            };

            if (netTypeBox.SelectedIndex != 10)
            {
				AccordNet = new AccordNet(structure);
				net = AccordNet;
            }
            else
            {
				NeuralNet = new NeuralNetwork(structure);
				net = NeuralNet;
			}
            epoches = (int)EpochesCounter.Value;
        }

        //обучиться
        private void button4_Click(object sender, EventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            train_networkAsync(epoches, max_error, parallelCheckBox.Checked);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private async Task<double> train_networkAsync(int epoches, double acceptable_error, bool parallel = true)
        {
            //  Выключаем всё ненужное
            NetSettingBox.Enabled = false;
            pictureBox1.Enabled = false;

            //  Создаём новую обучающую выборку
            SamplesSet samples = new SamplesSet();
            Sample newSample;

            foreach (var directory in Directory.GetDirectories(pathToImg))
            {
                int type = -1;
                switch (directory.ToString())
                {
                    case @"C:\Users\Mikhail\source\repos\NeuralNetwork2\images\close":
                        type = 0;
                        break;
                    case @"C:\Users\Mikhail\source\repos\NeuralNetwork2\images\like":
                        type = 1;
                        break;
                    case @"C:\Users\Mikhail\source\repos\NeuralNetwork2\images\minus":
                        type = 2;
                        break;
                    case @"C:\Users\Mikhail\source\repos\NeuralNetwork2\images\pause":
                        type = 3;
                        break;
                    case @"C:\Users\Mikhail\source\repos\NeuralNetwork2\images\play":
                        type = 4;
                        break;
                    case @"C:\Users\Mikhail\source\repos\NeuralNetwork2\images\plus":
                        type = 5;
                        break;
                    case @"C:\Users\Mikhail\source\repos\NeuralNetwork2\images\repeat":
                        type = 6;
                        break;
                    case @"C:\Users\Mikhail\source\repos\NeuralNetwork2\images\rewindbackward":
                        type = 7;
                        break;
                    case @"C:\Users\Mikhail\source\repos\NeuralNetwork2\images\rewindforward":
                        type = 8;
                        break;
                    default:
                        MessageBox.Show("Нет такой папки!" + directory.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
                foreach (var file in Directory.GetFiles(directory))
                {
                    var img = AForge.Imaging.UnmanagedImage.FromManagedImage(new Bitmap(file));
                    newSample = new Sample(imgToData(img), symbolsCount, (FigureType)type);
                    samples.AddSample(newSample);
                    //net.Train(newSample,false);
                }
            }

            //  Обучение запускаем асинхронно, чтобы не блокировать форму
            double f = 0;
            f = await Task.Run(() => net.TrainOnDataSet(samples, epoches, acceptable_error, parallel));

            NetSettingBox.Enabled = true;
            pictureBox1.Enabled = true;
            return f;
        }

        //сохраняем изображение
        private void button2_Click_1(object sender, EventArgs e)
        {
            var r = new Random();
            processor.presave();
            switch (comboBox1.SelectedIndex) {
                case 0: processor.Save(pathToImg + "close" + "\\" + r.Next().ToString() + r.Next().ToString() + ".png");
                break;
                case 1: processor.Save(pathToImg + "like" + "\\" + r.Next().ToString() + r.Next().ToString() + ".png");
                break;
                case 2: processor.Save(pathToImg + "minus" + "\\" + r.Next().ToString() + r.Next().ToString() + ".png");
                break;
                case 3: processor.Save(pathToImg + "pause" + "\\" + r.Next().ToString() + r.Next().ToString() + ".png");
                break;
                case 4: processor.Save(pathToImg + "play" + "\\" + r.Next().ToString() + r.Next().ToString() + ".png");
                break;
                case 5: processor.Save(pathToImg + "plus" + "\\" + r.Next().ToString() + r.Next().ToString() + ".png");
                break;
                case 6: processor.Save(pathToImg + "repeat" + "\\" + r.Next().ToString() + r.Next().ToString() + ".png");
                break;
                case 7: processor.Save(pathToImg + "rewindbackward" + "\\" + r.Next().ToString() + r.Next().ToString() + ".png");
                break;
                case 8: processor.Save(pathToImg + "rewindforward" + "\\" + r.Next().ToString() + r.Next().ToString() + ".png");
                break;
                default: MessageBox.Show("Неизвестно под каким классом сохранять изображение!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
            }
        }

        //если изменили ошибку
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(double.TryParse((sender as TextBox).Text, out double r)){
                max_error = r;
            }
        }

        //кнопка,обучиться изображению
        private void button7_Click(object sender, EventArgs e)
        {
            if (net == null) return;
            learnFromScreen();
        }

        //обучает сеть распознавать текущее изображение
        private void learnFromScreen()
        {
            FigureType currentImg = (FigureType)comboBox1.SelectedIndex;
            Sample currentImage = new Sample(imgToData(processor.GetImage()), symbolsCount, currentImg);
            net.Train(currentImage, false);
        }

        //выводит результаты распознавания
        private void ShowResult()
        {
            var result = net.getOutput();
            ResLabel.Text = "Class: "+ recognizedClass.ToString()+"\n";
            ResLabel.Text += "close" + ": " + result[0].ToString("F4") + "\n";
            ResLabel.Text += "like" + ": " + result[1].ToString("F4") + "\n";
            ResLabel.Text += "minus" + ": " + result[2].ToString("F4") + "\n";
            ResLabel.Text += "pause" + ": " + result[3].ToString("F4") + "\n";
            ResLabel.Text += "play" + ": " + result[4].ToString("F4") + "\n";
            ResLabel.Text += "plus" + ": " + result[5].ToString("F4") + "\n";
            ResLabel.Text += "repeat" + ": " + result[6].ToString("F4") + "\n";
            ResLabel.Text += "rewindbackward" + ": " + result[7].ToString("F4") + "\n";
            ResLabel.Text += "rewindforward" + ": " + result[8].ToString("F4") + "\n";
        }

        //меняем текущую сеть
        private void netTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (netTypeBox.SelectedIndex != 0)
                net = NeuralNet;
            else
                net = AccordNet;
        }
    }
}
