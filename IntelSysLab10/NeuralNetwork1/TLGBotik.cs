using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Drawing;

namespace NeuralNetwork1
{
    class TLGBotik
    {
        public Telegram.Bot.TelegramBotClient botik = null;

        AIMLBotik bott;

        private UpdateTLGMessages formUpdater;

        private BaseNetwork perseptron = null;

        public TLGBotik(BaseNetwork net,  UpdateTLGMessages updater)
        { 
            var botKey = System.IO.File.ReadAllText("botkey.txt");
            botik = new Telegram.Bot.TelegramBotClient(botKey);
            botik.OnMessage += Botik_OnMessageAsync;
            formUpdater = updater;
            bott = new AIMLBotik();
            perseptron = net;
        }

        public void SetNet(BaseNetwork net)
        {
            perseptron = net;
            formUpdater("Net updated!");
        }

        private void Botik_OnMessageAsync(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            //  Тут очень простое дело - банально отправляем назад сообщения
            var message = e.Message;
            formUpdater("Тип сообщения : " + message.Type.ToString());

            //  Получение файла (картинки)
            if (message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                formUpdater("Picture loadining started");
                var photoId = message.Photo.Last().FileId;
                File fl = botik.GetFileAsync(photoId).Result;

                var img = System.Drawing.Image.FromStream(botik.DownloadFileAsync(fl.FilePath).Result);
                
                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(img);

                //  Масштабируем aforge
                AForge.Imaging.Filters.ResizeBilinear scaleFilter = new AForge.Imaging.Filters.ResizeBilinear(200,200);
                var uProcessed = scaleFilter.Apply(AForge.Imaging.UnmanagedImage.FromManagedImage(bm));

                var bb = uProcessed.ToManagedImage();

                Camera processor = new Camera();
                GenerateImage generator = new GenerateImage();

                processor.ProcessImage(bb);

                ////Сохрание обработанного образа
                //int cnt = new System.IO.DirectoryInfo("D:\\МЕХМАТ\\course4_1\\ИС\\БОТ\\Debug").GetFiles().Length;

                //Bitmap b = new Bitmap(processor.number.Width, processor.number.Height);
                //b = processor.number;
                //cnt++;
                //string s = "D:\\МЕХМАТ\\course4_1\\ИС\\БОТ\\Debug\\" + cnt.ToString()+ ".bmp";
                //b.Save(s);
                ////
                int symbolsCount = 9;

                Sample sample = generator.Generate(processor.number);

                switch (perseptron.Predict(sample))
                {
                    case FigureType.Close: botik.SendTextMessageAsync(message.Chat.Id, "Close");break;
                    case FigureType.Like: botik.SendTextMessageAsync(message.Chat.Id, "Like"); break;
                    case FigureType.Minus: botik.SendTextMessageAsync(message.Chat.Id, "Minus"); break;
                    case FigureType.Pause: botik.SendTextMessageAsync(message.Chat.Id, "Pause"); break;
                    case FigureType.Play: botik.SendTextMessageAsync(message.Chat.Id, "Play"); break;
                    case FigureType.Plus: botik.SendTextMessageAsync(message.Chat.Id, "Plus"); break;
                    case FigureType.Repeat: botik.SendTextMessageAsync(message.Chat.Id, "Repeat"); break;
                    case FigureType.Rewindbackward: botik.SendTextMessageAsync(message.Chat.Id, "Rewindbackward"); break;
                    case FigureType.RewindForward: botik.SendTextMessageAsync(message.Chat.Id, "RewindForward"); break;
                    case FigureType.Undef: botik.SendTextMessageAsync(message.Chat.Id, "Undef"); break;
                }

                formUpdater("Picture recognized!");
                return;
            }

            if (message == null || message.Type != Telegram.Bot.Types.Enums.MessageType.Text) return;

            botik.SendTextMessageAsync(message.Chat.Id, bott.Talk(message.Text));
            formUpdater(message.Text);
            return;
        }

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

        public bool Act()
        {
            try
            {
                botik.StartReceiving();
            }
            catch(Exception e) { 
                return false;
            }
            return true;
        }

        public void Stop()
        {
            botik.StopReceiving();
        }

    }
}
