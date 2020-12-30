using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace PaternRecognition
{
    //Тип фигуры
    public enum FigureType : byte { Close = 0,Like,Minus,Pause, Play,Plus,Repeat, Rewindbackward,RewindForward, Undef };

    //Класс для хранения образа – входной массив сигналов на сенсорах, выходные сигналы сети, и прочее
    public class Sample
    {
        //Входной вектор
        public double[] input = null;

        //Выходной вектор, задаётся извне как результат распознавания
        public double[] output = null;

        // Вектор ошибки, вычисляется по какой-нибудь хитрой формуле
        public double[] error = null;

        //Действительный класс образа. Указывается учителем или определяется номером папки
        public FigureType actualClass;

        //Распознанный класс - определяется после обработки
        public FigureType recognizedClass;

        //Конструктор образа - на основе входных данных для сенсоров, при этом можно указать класс образа, или не указывать
        public Sample(double[] inputValues, int classesCount, FigureType sampleClass = FigureType.Undef)
        {
            //  Клонируем массивчик
            input = (double[])inputValues.Clone();
            output = new double[classesCount];
            if (sampleClass != FigureType.Undef) output[(int)sampleClass] = 1;
            recognizedClass = FigureType.Undef;
            actualClass = sampleClass;
        }

        //Обработка реакции сети на данный образ на основе вектора выходов сети
        public void processOutput()
        {
            if (error == null)
                error = new double[output.Length];

            // Нам так-то выход не нужен, нужна ошибка и определённый класс
            recognizedClass = 0;
            for (int i = 0; i < output.Length; ++i)
            {
                error[i] = ((i == (int)actualClass ? 1 : 0) - output[i]);
                if (output[i] > output[(int)recognizedClass]) recognizedClass = (FigureType)i;
            }
        }

        //Вычисленная суммарная квадратичная ошибка сети. Предполагается, что целевые выходы - 1 для верного, и 0 для остальных
        public double EstimatedError()
        {
            double Result = 0;
            for (int i = 0; i < output.Length; ++i)
                Result += Math.Pow(error[i], 2);
            return Result;
        }

        //Добавляет к аргументу ошибку, соответствующую данному образу (не квадратичную!!!)
        public void updateErrorVector(double[] errorVector)
        {
            for (int i = 0; i < errorVector.Length; ++i)
                errorVector[i] += error[i];
        }

        //Правильно ли распознан образ
        public bool Correct() { return actualClass == recognizedClass; }
    }

    /// <summary>
    /// Выборка образов. Могут быть как классифицированные (обучающая, тестовая выборки), так и не классифицированные (обработка)
    /// </summary>
    public class SamplesSet : IEnumerable
    {
        //Накопленные обучающие образы
        public List<Sample> samples = new List<Sample>();

        
        //Добавление образа к коллекции
        public void AddSample(Sample image)
        {
            samples.Add(image);
        }
        public int Count { get { return samples.Count; } }

        public IEnumerator GetEnumerator()
        {
            return samples.GetEnumerator();
        }

        //Реализация доступа по индексу
        public Sample this[int i]
        {
            get { return samples[i]; }
            set { samples[i] = value; }
        }

        public double ErrorsCount()
        {
            double correct = 0;
            double wrong = 0;
            foreach (var sample in samples)
                if (sample.Correct()) ++correct; else ++wrong;
            return correct / (correct + wrong);
        }
    }
}
