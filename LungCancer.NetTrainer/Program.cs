using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ConvNetSharp.Core;
using ConvNetSharp.Core.Layers.Single;
using ConvNetSharp.Core.Serialization;
using ConvNetSharp.Core.Training.Single;
using ConvNetSharp.Volume;
using ConvNetSharp.Volume.Single;
using LungCancer.Common;
using Newtonsoft.Json;

namespace LungCancer.NetTrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var trainer = new Program();

            trainer.Start();
        }

        private const int windowSize = 25;
        private int numOfSamples = 1537;
        private Net<float> net;
        private float[] trainingData;
        private float[] trainingLabels;
        private SgdTrainer trainer;

        private TrainVisualisation visualisation;

        private bool enableTraining = false;
        private AutoResetEvent waitForTraining = new AutoResetEvent(false);

        private void Start()
        {
            // LoadTrainingData("data.json");
            // SaveTrainingData("data.dat", "labels.dat");

            visualisation = new TrainVisualisation();

            new Thread(new ThreadStart(() =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(visualisation);
            }))
            .Start();

            LoadRawTrainingData("data.dat", "labels.dat");
            BuilderInstance.Volume = new VolumeBuilder();

            CreateNetwork();

            //using (var reader = new StreamReader("net.json"))
            //{
            //     net = SerializationExtensions.FromJson<float>(reader.ReadToEnd());
            //}

            visualisation.StartTraining += Visualisation_StartTraining;
            visualisation.PauseTraining += Visualisation_PauseTraining;

            int i = 0;

            while (true)
            {
                if (!enableTraining)
                {
                    waitForTraining.WaitOne();
                }
                Train(5);

                if (i++ % 25 == 0)
                {
                    var netString = net.ToJson();

                    using (var writer = new StreamWriter("net.json"))
                    {
                        writer.Write(netString);
                    }
                }
            }
        }

        private void Visualisation_PauseTraining(object sender, EventArgs e)
        {
            enableTraining = false;
        }

        private void Visualisation_StartTraining(object sender, EventArgs e)
        {
            float learningRate = (float)sender;

            trainer.LearningRate = learningRate;

            enableTraining = true;
            waitForTraining.Set();
        }

        private void CreateNetwork()
        {
            net = new Net<float>();

            net.AddLayer(new InputLayer(windowSize, windowSize, 1));
            net.AddLayer(new ConvLayer(5, 5, 3) { Stride = 1, Pad = 0}); // 21 x 21
            net.AddLayer(new ReluLayer()); // 21 x 21
            net.AddLayer(new PoolLayer(2, 2)); // 10 x 10
            net.AddLayer(new ConvLayer(5, 5, 3) { Stride = 1, Pad = 0}); // 6 x 6
            net.AddLayer(new ReluLayer());
            net.AddLayer(new PoolLayer(2, 2));
            net.AddLayer(new FullyConnLayer(16));
            net.AddLayer(new TanhLayer());
            net.AddLayer(new FullyConnLayer(2));
            net.AddLayer(new SoftmaxLayer(2));

            trainer = new SgdTrainer(net) { LearningRate = 0.01f, L2Decay = 0.001f };
        }

        private void LoadTrainingData(string dataFile)
        {
            var serialier = new JsonSerializer();

            using (var reader = new StreamReader(dataFile))
            {
                var samplesList = serialier.Deserialize(reader, typeof(List<DataSample>)) as List<DataSample>;

                numOfSamples = samplesList.Count;

                DataSample.GenerateTrainingData(samplesList, windowSize, out trainingData, out trainingLabels);
            }
        }

        private void SaveTrainingData(string dataFile, string labelsFile)
        {
            using (var writer = new FileStream(dataFile, FileMode.OpenOrCreate))
            {
                for (int i = 0; i < trainingData.Length; i++)
                {
                    writer.Write(BitConverter.GetBytes(trainingData[i]), 0, 4);
                }
            }

            using (var writer = new FileStream(labelsFile, FileMode.OpenOrCreate))
            {
                for (int i = 0; i < trainingLabels.Length; i++)
                {
                    writer.Write(BitConverter.GetBytes(trainingLabels[i]), 0, 4);
                }
            }
        }

        private void LoadRawTrainingData(string trainingDataFile, string trainingLabelsFile)
        {
            List<float> trainingData = new List<float>();
            List<float> trainingLabels = new List<float>();

            using (var reader = new FileStream(trainingDataFile, FileMode.Open, FileAccess.Read))
            {
                byte[] buf = new byte[4];

                while (reader.Read(buf, 0, 4) == 4)
                {
                    trainingData.Add(BitConverter.ToSingle(buf, 0));
                }
            }

            using (var reader = new FileStream(trainingLabelsFile, FileMode.Open, FileAccess.Read))
            {
                byte[] buf = new byte[4];

                while (reader.Read(buf, 0, 4) == 4)
                {
                    trainingLabels.Add(BitConverter.ToSingle(buf, 0));
                }
            }

            this.trainingData = trainingData.ToArray();
            this.trainingLabels = trainingLabels.ToArray();
        }

        private void Train(int iterations = 50)
        {
            var samples = new Volume(trainingData, new Shape(windowSize, windowSize, 1, numOfSamples));
            var labels = new Volume(trainingLabels, new Shape(1, 1, 2, numOfSamples));

            var start = DateTime.Now;

            double avloss = 0.0;

            for (int i = 0; i < iterations; i++)
            {
                trainer.Train(samples, labels);
                avloss += trainer.Loss;
            }

            avloss /= iterations;

            visualisation.AddLoss(avloss);

            Console.WriteLine($"Loss: {avloss} Step took: {DateTime.Now - start}.");
        }

        private void Test()
        {
            //var prob = net.Forward(trainingVolume);
            //var pred = net.GetPrediction();

            //for (int i = 0; i < numOfSamples; i++)
            //{
            //    Console.Write($"{prob.Get(i * 2 + 1)}, ");
            //}

            //Console.WriteLine();
        }
    }
}
