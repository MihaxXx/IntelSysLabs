using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaternRecognition
{
    //Класс представления нейрона сети
    public class Neuron
    {
        public double datasum = 0;
        public double output = 0;
        public double error = 0;

        public static double bias = 1.0;
        public double biasWeight = initMinWeight + rand.NextDouble() * (initMaxWeight - initMinWeight);

        //Минимальное и максимальное значения для начальной инициализации весов
        private static double initMinWeight = -1;
        private static double initMaxWeight = 1;

        private static Random rand = new Random();

        public Neuron[] prevLayer = null;
        public int prevLayerSize = 0;

        //Вектор входных весов нейрона
        public double[] weights = null;

        //Создаем нейрон с вектором входящих весов.Должны знать,количество нейронов на предыдущем слое
        public Neuron(Neuron[] prevLayerNeurons)
        {
            prevLayer = prevLayerNeurons; //Инициализируем предыдущий слой
            if (prevLayerNeurons == null) return;//Для входного слоя не инициализируем
            prevLayerSize = prevLayerNeurons.Length;
            weights = new double[prevLayerSize];
            for (int i = 0; i < weights.Length; ++i)
                weights[i] = initMinWeight + rand.NextDouble() * (initMaxWeight - initMinWeight);
        }

        //Вычисление пороговой функции (активация нейрона)
        public void Activate()
        {
            datasum = biasWeight * bias;
            for (int i = 0; i < prevLayer.Length; ++i)
                datasum += prevLayer[i].output * weights[i];
            output = ActivationFunction(datasum);
            datasum = 0;
        }

        //Распространение ошибки на предыдущий слой и пересчёт весов. Предварительно там ошибка должна быть сброшена (хотя бы в процессе обновления весов)
        public void BackpropError(double LearningSpeed)
        {
            //Применяется дельта-правило
            //https://mattmazur.com/2015/03/17/a-step-by-step-backpropagation-example/
            //Обрабатываем ошибку в текущем нейроне
            error *= DerivativeFunction(output);
            biasWeight += LearningSpeed * error * bias;
            //При движении назад и обновлении весов,используем старые веса,а не обновленные
            for (int i = 0; i < prevLayerSize; i++)
                prevLayer[i].error += error * weights[i];
            //Входные веса изменяются на величину равную произведению скорости обучения на выход предыдущего слоя на коэффициент влияния весов на общую ошибку 
            for (int i = 0; i < prevLayerSize; i++)
                weights[i] += LearningSpeed * error * prevLayer[i].output;
            error = 0;
        }

        /*Одной из наиболее известных функций активации является сигмоидальная функция
         Передаточная (активационная) функция
         inp - взвешенная сумма входных сигналов
             */
        public static double ActivationFunction(double inp)
        {
            return 1 / (1 + Math.Exp(-inp));
        }

        //Производная функции активации
        public static double DerivativeFunction(double output)
        {
            return output * (1 - output);
        }

    }
}
