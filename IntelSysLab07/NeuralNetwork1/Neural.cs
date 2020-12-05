using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace NeuralNetwork1
{
    /// <summary>
    /// Класс для хранения образа – входной массив сигналов на сенсорах, выходные сигналы сети, и прочее
    /// </summary>
    public class Sample
    {
        /// <summary>
        /// Входной вектор
        /// </summary>
        public double[] input = null;

        /// <summary>
        /// Выходной вектор, задаётся извне как результат распознавания
        /// </summary>
        public double[] output = null;

        /// <summary>
        /// Вектор ошибки, вычисляется по какой-нибудь хитрой формуле
        /// </summary>
        public double[] error = null;

        /// <summary>
        /// Действительный класс образа. Указывается учителем
        /// </summary>
        public FigureType actualClass;

        /// <summary>
        /// Распознанный класс - определяется после обработки
        /// </summary>
        public FigureType recognizedClass;

        /// <summary>
        /// Конструктор образа - на основе входных данных для сенсоров, при этом можно указать класс образа, или не указывать
        /// </summary>
        /// <param name="inputValues"></param>
        /// <param name="sampleClass"></param>
        public Sample(double[] inputValues, int classesCount, FigureType sampleClass = FigureType.Undef)
        {
            //  Клонируем массивчик
            input = (double[]) inputValues.Clone();
            output = new double[classesCount];
            if (sampleClass != FigureType.Undef) output[(int)sampleClass] = 1;


            recognizedClass = FigureType.Undef;
            actualClass = sampleClass;
        }

        /// <summary>
        /// Обработка реакции сети на данный образ на основе вектора выходов сети
        /// </summary>
        public void processOutput()
        {
            if (error == null)
                error = new double[output.Length];
            
            //  Нам так-то выход не нужен, нужна ошибка и определённый класс
            recognizedClass = 0;
            for(int i = 0; i < output.Length; ++i)
            {
                error[i] = ((i == (int) actualClass ? 1 : 0) - output[i]);
                if (output[i] > output[(int)recognizedClass]) recognizedClass = (FigureType)i;
            }
        }

        /// <summary>
        /// Вычисленная суммарная квадратичная ошибка сети. Предполагается, что целевые выходы - 1 для верного, и 0 для остальных
        /// </summary>
        /// <returns></returns>
        public double EstimatedError()
        {
            double Result = 0;
            for (int i = 0; i < output.Length; ++i)
                Result += Math.Pow(error[i], 2);
            return Result;
        }

        /// <summary>
        /// Добавляет к аргументу ошибку, соответствующую данному образу (не квадратичную!!!)
        /// </summary>
        /// <param name="errorVector"></param>
        /// <returns></returns>
        public void updateErrorVector(double[] errorVector)
        {
            for (int i = 0; i < errorVector.Length; ++i)
                errorVector[i] += error[i];
        }

        /// <summary>
        /// Представление в виде строки
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "Sample decoding : " + actualClass.ToString() + "(" + ((int)actualClass).ToString() + "); " + Environment.NewLine + "Input : ";
            for (int i = 0; i < input.Length; ++i) result += input[i].ToString() + "; ";
            result += Environment.NewLine + "Output : ";
            if (output == null) result += "null;";
            else
                for (int i = 0; i < output.Length; ++i) result += output[i].ToString() + "; ";
            result += Environment.NewLine + "Error : ";

            if (error == null) result += "null;";
            else
                for (int i = 0; i < error.Length; ++i) result += error[i].ToString() + "; ";
            result += Environment.NewLine + "Recognized : " + recognizedClass.ToString() + "(" + ((int)recognizedClass).ToString() + "); " + Environment.NewLine;


            return result;
        }
        
        /// <summary>
        /// Правильно ли распознан образ
        /// </summary>
        /// <returns></returns>
        public bool Correct() { return actualClass == recognizedClass; }
    }
    
    /// <summary>
    /// Выборка образов. Могут быть как классифицированные (обучающая, тестовая выборки), так и не классифицированные (обработка)
    /// </summary>
    public class SamplesSet : IEnumerable
    {
        /// <summary>
        /// Накопленные обучающие образы
        /// </summary>
        public List<Sample> samples = new List<Sample>();
        
        /// <summary>
        /// Добавление образа к коллекции
        /// </summary>
        /// <param name="image"></param>
        public void AddSample(Sample image)
        {
            samples.Add(image);
        }
        public int Count { get { return samples.Count; } }

        public IEnumerator GetEnumerator()
        {
            return samples.GetEnumerator();
        }

        /// <summary>
        /// Реализация доступа по индексу
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
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
        // Тут бы ещё сохранение в файл и чтение сделать, вообще классно было бы
        // И правда удобно было бы!
    }

    public class NeuralNetwork : BaseNetwork
    {
        /// <summary>
        /// Класс нейрона
        /// </summary>
        public class Neuron
        {
            public double output = 0; //выходной сигнал
            public double error = 0; //ошибка
            public List<double> inputWeights = new List<double>(); //входные веса
            public List<Neuron> prevLayer; //ссылка на предыдущий слой

            static Random random = new Random(); //генератор случайных чисел

            /// <summary>
            /// Создание нейрона первого слоя (сенсора)
            /// </summary>
            public Neuron() { }

            /// <summary>
            /// Создание нейронов второго и следующих слоев
            /// </summary>
            /// <param name="neurons">Ссылка на предыдущий слой</param>
            public Neuron(List<Neuron> neurons)
            {
                prevLayer = neurons;
                //заполняем входные веса случайным образом от -1 до 1
                for (int i = 0; i < prevLayer.Count; i++)
                    inputWeights.Add(-1 + random.NextDouble() * 2);
            }

            /// <summary>
            /// Вычисление передаточной функции
            /// </summary>
            public void CalcGearFunction()
            {
                double weight = 0;
                for (int i = 0; i < prevLayer.Count; i++)
                    weight += prevLayer[i].output * inputWeights[i];

                output = 1 / (1 + Math.Exp(-weight)); //сигмоидальная передаточная функция
            }

            public void BackError()
            {
                //обрабатываем ошибку в текущем нейроне
                error *= output * (1 - output);

                //переносим ошибку на предыдущий слой
                for (int i = 0; i < prevLayer.Count; i++)
                    prevLayer[i].error += error * inputWeights[i];

                //корректируем веса
                for (int i = 0; i < prevLayer.Count; i++)
                    inputWeights[i] += error * prevLayer[i].output;

                error = 0;
            }
        }

        public List<List<Neuron>> neuronsLayers = new List<List<Neuron>>(); //список слоев сети

        void Init(int[] structure)
        {
            for (int i = 0; i < structure.Length; i++)
            {
                List<Neuron> neurons = new List<Neuron>(); //создаем слой сети
                for (int j = 0; j < structure[i]; j++)
                    neurons.Add(i == 0 ? new Neuron() : new Neuron(neuronsLayers[i - 1])); //создаем нейроны
                neuronsLayers.Add(neurons);
            }
        }

        public NeuralNetwork(int[] structure)
        {
            Init(structure);
        }

        public override void ReInit(int[] structure, double initialLearningRate = 0.25)
        {
            neuronsLayers.Clear();
            Init(structure);
        }

        /// <summary>
        /// Запустить сеть на заданном примере
        /// </summary>
        void Run(Sample sample)
        {
            for (int i = 0; i < sample.input.Length; i++)
                neuronsLayers[0][i].output = sample.input[i]; //переносим значения на сенсоры

            //Выполняем послойно вычисления передаточных функций
            for (int i = 1; i < neuronsLayers.Count; i++)
                foreach (var neuron in neuronsLayers[i])
                    neuron.CalcGearFunction();

            for (int i = 0; i < neuronsLayers.Last().Count; i++)
                sample.output[i] = neuronsLayers.Last()[i].output; //переносим значения с последнего слоя в вектор значений изображения

            sample.processOutput();
        }

        /// <summary>
        /// Запускаем алгоритм обратного распространения ошибки
        /// </summary>
        void BackError(Sample sample)
        {
            //переносим ошибку из картинки на последний слой нейронов
            for (int i = 0; i < neuronsLayers.Last().Count; i++)
                neuronsLayers.Last()[i].error = sample.error[i];

            //переносим ошибки от слоя к слою
            for (int i = neuronsLayers.Count - 1; i >= 1; i--)
                foreach (var neuron in neuronsLayers[i])
                    neuron.BackError();
        }

        /// <summary>
        /// Тренировать сеть на указанном примере, возвращает число попыток, которое понадобилось для правильного распознавания
        /// </summary>
        public override int Train(Sample sample, bool parallel = true)
        {
            int cnt = 0;
            //запускаем сеть на примере до тех пор, пока правильно не распознается (максимум 100 раз)
            while (cnt < 100)
            {
                Run(sample);

                if (sample.EstimatedError() < 0.2 && sample.Correct()) return cnt;

                //если мы здесь, значит ошибка распознавания (или слишком большая ошибка), запускаем алгоритм обратного распространения ошибки
                cnt++;
                BackError(sample);
            }
            return cnt;
        }

        /// <summary>
        /// Обучить сеть на датасете
        /// </summary>
        /// <param name="samplesSet">Датасет</param>
        /// <param name="epochs_count">Количество эпох</param>
        /// <param name="acceptable_error">Допустимая ошибка</param>
        /// <returns>Точность</returns>
        public override double TrainOnDataSet(SamplesSet samplesSet, int epochs_count, double acceptable_error, bool parallel = false)
        {
            double accuracy = 0;

            int samplesLooked = 0; //сколько всего элементов было рассмотрено
            double allSamplesCount = samplesSet.samples.Count * epochs_count; //сколько всего элементов нужно будет рассмотреть

            while (epochs_count-- > 0)
            {
                double rightCnt = 0;
                //перебираем весь датасет
                foreach (var sample in samplesSet.samples)
                {
                    if (Train(sample) == 0) rightCnt++;
                    samplesLooked++;
                    if (samplesLooked % 50 == 0) //каждые 50 рассмотренных примеров обновляем индикатор прогресса
                        updateDelegate(samplesLooked / allSamplesCount, accuracy, new TimeSpan());
                }


                accuracy = rightCnt / samplesSet.samples.Count; //после конца каждой эпохи перессчитываем точность для передачи ее в форму
                if (accuracy >= 1 - acceptable_error - 1e-10) //если точность соответствует допустимой ошибке, выходим
                {
                    updateDelegate(1, accuracy, new TimeSpan());
                    return accuracy;
                }
                else updateDelegate(samplesLooked / allSamplesCount, accuracy, new TimeSpan());
            }

            updateDelegate(1, accuracy, new TimeSpan());
            return accuracy;
        }

        /// <summary>
        /// Запустить сеть на примере и вернуть распознанный класс
        /// </summary>
        public override FigureType Predict(Sample sample)
        {
            Run(sample);
            return sample.recognizedClass;
        }

        /// <summary>
        /// Тестировать сеть на датасете
        /// </summary>
        /// <param name="testSet">Датасет</param>
        /// <returns>Точность</returns>
        public override double TestOnDataSet(SamplesSet testSet)
        {
            if (testSet.Count == 0) return double.NaN;

            int rightCnt = 0;
            foreach (var sample in testSet.samples)
            {
                Predict(sample);
                if (sample.Correct()) rightCnt++;
            }

            return rightCnt * 1.0 / testSet.Count;
        }

        public override double[] getOutput() => neuronsLayers.Last().Select(neuron => neuron.output).ToArray();
    }
}
