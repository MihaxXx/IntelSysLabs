using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaternRecognition
{
    class NeuralNetwork : BaseNetwork
    {
        public double LearningSpeed = 0.25;
        private Neuron[][] Layers;
        private Neuron[] Sensors;
        private Neuron[] Outputs;

        /// <summary>
        /// Значение ошибки при обучении единичному образцу. При обучении на наборе образов не используется
        /// </summary>
        public double desiredErrorValue = 0.0005;

        public NeuralNetwork(int[] structure)
        {
            ReInit(structure);
        }

        public override void ReInit(int[] structure, double LearningSpeed = 0.25)
        {
            if (structure.Length < 2)
                throw new Exception("Invalid net structure!The network must contain at least two layers:sensors and outputs!");
            Layers = new Neuron[structure.Length][];
            Layers[0] = new Neuron[structure[0]];
            for (int neuron = 0; neuron < structure[0]; ++neuron)
                Layers[0][neuron] = new Neuron(null);
            Sensors = Layers[0];
            for (int layer = 1; layer < structure.Length; ++layer)
            {
                Layers[layer] = new Neuron[structure[layer]];
                for (int neuron = 0; neuron < structure[layer]; ++neuron)
                    Layers[layer][neuron] = new Neuron(Layers[layer - 1]);  //Инициализируем нейроны данного уровня,указывая на предыдущий слой
            }
            Outputs = Layers[Layers.Length - 1];
        }

        /// <summary>
        /// Обучение сети одному образу
        /// </summary>
        /// <param name="sample"></param>
        /// <returns>Количество итераций для достижения заданного уровня ошибки</returns>
        public override int Train(Sample sample,bool parallel = true)
        {
            int iters = 0;
            Run(sample);
            ++iters;
            BackProp(sample, LearningSpeed);
            while (sample.EstimatedError() > desiredErrorValue)
            {
                Run(sample);
                if (sample.Correct())//правильно распознали образ
                    return iters;
                ++iters;
                BackProp(sample, LearningSpeed);
            }
            return iters;
        }

        public override double TrainOnDataSet(SamplesSet samplesSet, int epochs_count, double acceptable_erorr, bool parallel = true)
        {
            int epoch_to_run = 0;
            double recognized = 0;
            while (epoch_to_run < epochs_count)
            {
                recognized = 0;
                for (int i = 0; i < samplesSet.samples.Count; ++i)
                    if (Train(samplesSet.samples.ElementAt(i)) == 0)//если мы сразу распознали образ
                        recognized += 1;
                recognized /= samplesSet.samples.Count;
                if (recognized > acceptable_erorr) return recognized;
                epoch_to_run++;
            }
            return recognized; //процент распознанных
        }

        //Распознаем образ
        public override FigureType Predict(Sample sample)
        {
            Run(sample);
            return sample.recognizedClass;
        }

        public override double[] getOutput()
        {
            return Outputs.Select(n => n.output).ToArray();
        }

        public override double TestOnDataSet(SamplesSet testSet)
        {
            if (testSet.Count == 0) return double.NaN;
            double recognized = 0;
            for (int i = 0; i < testSet.Count; ++i)
            {
                Sample s = testSet.samples.ElementAt(i);
                Predict(s);
                if (s.Correct())
                    recognized += 1;
            }
            return recognized / testSet.Count;
        }

        //Прямой однократный прогон сети
        public void Run(Sample image)
        {
            if (image.input.Length != Sensors.Length)
                throw new Exception("Incorrect input!");
            for (int i = 0; i < image.input.Length; i++)
                Sensors[i].output = image.input[i];
            for (int i = 1; i < Layers.Length; i++)
                for (int j = 0; j < Layers[i].Length; j++)
                    Layers[i][j].Activate();
            if (image.output == null)
                image.output = new double[Layers[Layers.Length - 1].Length];
            for (int i = 0; i < Layers[Layers.Length - 1].Length; i++)
                image.output[i] = Layers[Layers.Length - 1][i].output;
            image.processOutput();
        }

        //Обратный прогон ошибки и пересчет весов
        private void BackProp(Sample image, double LearningSpeed)
        {
            //Считываем ошибку из образа на выходной слой
            for (int i = 0; i < Layers[Layers.Length - 1].Length; i++)
                Layers[Layers.Length - 1][i].error = image.error[i];
            //Пересчитываем ошибку для каждого нейрона на всех уровнях от выхода к сенсорам
            for (int i = Layers.Length - 1; i >= 0; --i)
                for (int j = 0; j < Layers[i].Length; ++j)
                    Layers[i][j].BackpropError(LearningSpeed);
        }
    }
}
