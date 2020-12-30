using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using Accord.Neuro;

namespace PaternRecognition
{
    class MagicEye
    {

        private readonly object balanceLock = new object();
        // обработанное изображение 
        public AForge.Imaging.UnmanagedImage processed;

        // оригинал и финальное
        public Bitmap original, final;
        public int BlobCount { get; private set; }
        public bool Recongnised { get; private set; }
        public float Angle { get; private set; }
        public float AngleRad { get; private set; }
        public float ThresholdValue = 0.15f;


        // filters objects
        Grayscale grayFilter;
        //The class implements image resizing filter using bilinear interpolation algorithm.
        ResizeBilinear scaleFilter;
        //The image processing routine implements local thresholding technique
        BradleyLocalThresholding threshldFilter;
        //The filter inverts colored and grayscale images.инверсия
        Invert InvertFilter;
        //The class counts and extracts stand alone objects in images 
        AForge.Imaging.BlobCounter Blober;
        
        Graphics g;
        public MagicEye()
        {
            grayFilter = new AForge.Imaging.Filters.Grayscale(0.2125, 0.7154, 0.0721);
            scaleFilter = new AForge.Imaging.Filters.ResizeBilinear(300, 300);
            threshldFilter = new AForge.Imaging.Filters.BradleyLocalThresholding();
            InvertFilter = new AForge.Imaging.Filters.Invert();
            Blober = new AForge.Imaging.BlobCounter();
            original = new Bitmap(300, 300);
            g = Graphics.FromImage(original);
            Blober.FilterBlobs = true;
            Blober.MinWidth = 5;
            Blober.MinHeight = 5;
            Blober.ObjectsOrder = AForge.Imaging.ObjectsOrder.Size;
        }


        //инициализируем готовое изображение и применяем трансформации
        public void ProcessImage(Bitmap input_image)
        {
            lock (balanceLock)
            {
            int side =Math.Min(input_image.Height,input_image.Width);
            Rectangle cropRect = new Rectangle(0, 0 ,side, side);// this is square that represents feed from camera
            //g.DrawImage(input_image, new Rectangle(0, 0, input_image.Width, input_image.Height), cropRect, GraphicsUnit.Pixel); // place it on original bitmap         
                original = new Bitmap(input_image);                                                                                                                // set new processed
            if (processed != null)
                processed.Dispose();  //Конвертируем изображение в градации серого
            processed = grayFilter.Apply(AForge.Imaging.UnmanagedImage.FromManagedImage(original));
            //  Пороговый фильтр применяем. Величина порога берётся из настроек, и меняется на форме
            threshldFilter.PixelBrightnessDifferenceLimit = ThresholdValue;
            threshldFilter.ApplyInPlace(processed);  
            InvertFilter.ApplyInPlace(processed);
            Blober.ProcessImage(processed);
            AForge.Imaging.Blob[] blobs = Blober.GetObjectsInformation();
            BlobCount = blobs.Length;
            
            if (blobs.Length > 0)
            {
                var BiggestBlob = blobs[0];
                Recongnised = true;
                Blober.ExtractBlobsImage(processed, BiggestBlob, false);
                processed = BiggestBlob.Image;
                AForge.Point mc = BiggestBlob.CenterOfGravity;
                AForge.Point ic = new AForge.Point((float)BiggestBlob.Image.Width / 2, (float)BiggestBlob.Image.Height / 2);
                AngleRad = (ic.Y - mc.Y) / (ic.X - mc.X);
                Angle =  (float)(Math.Atan(AngleRad) * 180 / Math.PI);     
            }
            else
            {
                // TODO make arrengaments for No blobs case
                Recongnised = false;
                Angle = 0;
                AngleRad = -1;
            }
            if (final != null)
                final.Dispose();
            final = processed.ToManagedImage();
            }
        }

        public void GetPicture(out Bitmap or, out Bitmap num)
        {
            lock (balanceLock)
            {
                or = new Bitmap(original);
                num = new Bitmap(final);

            }
        }

        //формируем финальное изображение для сохранения
        private AForge.Imaging.UnmanagedImage sav;
        public void presave()
        {
            lock (balanceLock)
            {
                sav = AForge.Imaging.UnmanagedImage.FromManagedImage(final);
            }
        }

        public AForge.Imaging.UnmanagedImage GetImage()
        {
            ResizeBicubic resize = new ResizeBicubic(28, 28);
            lock (balanceLock)
            {
                return resize.Apply(processed);
            }            
        }

        //сохраняем изображение в указанный путь
        public void Save(string path)
        {
            ResizeBicubic resize = new ResizeBicubic(28, 28);
            resize.Apply(sav).ToManagedImage().Save(path);
        }

    }
}

