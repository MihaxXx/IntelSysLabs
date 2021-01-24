using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
using System.Drawing;

namespace NeuralNetwork1
{
    class Camera
    {
        private readonly object balanceLock = new object();
        // обработанное изображение 
        public AForge.Imaging.UnmanagedImage processed;
        // оригинал и финальное
        public Bitmap original, number;
        public int BlobCount { get; private set; }
        public bool Recongnised { get; private set; }
        public float Angle { get; private set; }
        public float AngleRad { get; private set; }
        public float ThresholdValue = 0.3f;


        // filters objects
        Grayscale grayFilter;
        ResizeBilinear scaleFilter;
        BradleyLocalThresholding threshldFilter;
        Invert InvertFilter;
        AForge.Imaging.BlobCounter Blober;

        Graphics g;

        public Camera()
        {
            grayFilter = new AForge.Imaging.Filters.Grayscale(0.2125, 0.7154, 0.0721);
            scaleFilter = new AForge.Imaging.Filters.ResizeBilinear(200, 200);
            threshldFilter = new AForge.Imaging.Filters.BradleyLocalThresholding();
            InvertFilter = new AForge.Imaging.Filters.Invert();
            Blober = new AForge.Imaging.BlobCounter();
            original = new Bitmap(200, 200);
            g = Graphics.FromImage(original);
            Blober.FilterBlobs = true;
            Blober.MinWidth = 5;
            Blober.MinHeight = 5;
            Blober.ObjectsOrder = AForge.Imaging.ObjectsOrder.Size;

           // for (int i = 65; i <= 90; i++)

            //    System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\" + (char)i);


        }

        public void GetPicture(out Bitmap n)
        {
            lock(balanceLock)
            {
                n = new Bitmap(number);
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

        public void ProcessImage(Bitmap input_image)
        {
            lock (balanceLock)
            {
                int side = Math.Min(input_image.Height, input_image.Width);
                Rectangle cropRect = new Rectangle(0, 0, side, side); // this is square that represents feed from camera
                g.DrawImage(input_image, new Rectangle(0, 0, input_image.Width, input_image.Height), cropRect, GraphicsUnit.Pixel); // place it on original bitmap         
                                                                                                                                    
                // set new processed
                if (processed != null)
                    processed.Dispose();  
                
                //  Конвертируем изображение в градации серого
                processed = grayFilter.Apply(AForge.Imaging.UnmanagedImage.FromManagedImage(original));
                //  Пороговый фильтр применяем. Величина порога берётся из настроек, и меняется на форме
                threshldFilter.PixelBrightnessDifferenceLimit = ThresholdValue;
                threshldFilter.ApplyInPlace(processed);
                //InvertFilter.ApplyInPlace(processed);
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
                    Angle = (float)(Math.Atan(AngleRad) * 180 / Math.PI);


                }
                else
                {
                    // TODO make arrengaments for No blobs case
                    Recongnised = false;
                    Angle = 0;
                    AngleRad = -1;

                }

                if (number != null)
                    number.Dispose();
                number = processed.ToManagedImage();

            }

        }

        public AForge.Imaging.UnmanagedImage GetInput(out double[] input, int countBlocks = 100)
        {

            double toDbl(Color c)
            {
                var s = c.A + c.B + c.G;
                return s > 50 ? 1 : 0;

            }

            input = new double[countBlocks * countBlocks + 1];
            AForge.Imaging.UnmanagedImage img;
            var filter = new ResizeNearestNeighbor(countBlocks, countBlocks);
            double angle;
            lock (balanceLock)
            {
                angle = AngleRad;
                img = filter.Apply(processed);
            }

            double max_input = 0;
            for (int i = 0; i < countBlocks; i++)
            {
                for (int j = 0; j < countBlocks; j++)
                {
                    var d = toDbl(img.GetPixel(i, j));
                    input[i * countBlocks + j] = d;
                    if (d > max_input)
                        max_input = d;
                }

            }
            if (max_input != 0)
                for (int i = 0; i < input.Length - 1; i++)
                {
                    input[i] /= max_input;
                }
            input[countBlocks * countBlocks] = AngleRad / Math.PI / 2.0;
            return img;

        }

    }
}
