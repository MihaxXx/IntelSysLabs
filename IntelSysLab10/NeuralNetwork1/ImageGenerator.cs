using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork1
{
    public enum FigureType : byte { Close = 65, Like, Minus, Pause, Play, Plus, Repeat, Rewindbackward, RewindForward, Undef };

    public class GenerateImage
    {
        /// <summary>
        /// Бинарное представление образа
        /// </summary>
        public bool[,] img = new bool[200, 200];

        /// <summary>
        /// Текущая сгенерированная фигура
        /// </summary>
        public FigureType current_Letter = FigureType.Undef;

        /// <summary>
        /// Количество классов генерируемых фигур (4 - максимум)
        /// </summary>
        public int figure_count { get; set; } = 5;



        /// <summary>
        /// Очистка образа
        /// </summary>
        public void ClearImage()
        {
            for (int i = 0; i < 200; ++i)
                for (int j = 0; j < 200; ++j)
                    img[i, j] = false;
        }

        public Sample Generate(Bitmap image, char Letter = (char)90)
        {
            current_Letter = (FigureType)Letter;
            double[] input = new double[21];


            Bitmap a = AForge.Imaging.Image.Clone( new Bitmap(image), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            var uProcessed = AForge.Imaging.UnmanagedImage.FromManagedImage(a);

            AForge.Imaging.Filters.Invert InvertFilter = new AForge.Imaging.Filters.Invert();
            InvertFilter.ApplyInPlace(uProcessed);

            AForge.Imaging.BlobCounterBase bc = new AForge.Imaging.BlobCounter();
            bc.FilterBlobs = true;
            bc.MinWidth = 2;
            bc.MinHeight = 2;

            bc.ProcessImage(uProcessed);
            Rectangle[] rects = bc.GetObjectsRectangles();

            for(int i = 0; i < rects.Length; i++)
            {
                input[i] = (double)rects[i].Width / (double)rects[i].Height;
            }

            input[20] = rects.Length;

            return new Sample(input, figure_count, current_Letter);
        }


        /// <summary>
        /// Возвращает битовое изображение для вывода образа
        /// </summary>
        /// <returns></returns>
        public Bitmap genBitmap()
        {
            Bitmap DrawArea = new Bitmap(200, 200);
            for (int i = 0; i < 200; ++i)
                for (int j = 0; j < 200; ++j)
                    if (img[i, j])
                        DrawArea.SetPixel(i, j, Color.Black);
            return DrawArea;
        }
    }

}
