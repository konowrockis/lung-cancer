using System;
using System.Collections.Generic;
using System.IO;
using ConvNetSharp.Core;
using ConvNetSharp.Core.Layers.Single;
using ConvNetSharp.Core.Training.Single;
using ConvNetSharp.Volume.GPU.Single;
using LungCancer.Common;
using Newtonsoft.Json;

namespace LungCancer.NetTrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var trainer = new Program();

            trainer.CreateTrainingData("data.json");

            trainer.CreateNetwork();

            for (int i = 0; i < 1000; i++)
            {
                var start = DateTime.Now;

                trainer.Test();
                trainer.Train();

                Console.WriteLine($"Step took: {DateTime.Now - start}.");
            }
            trainer.Test();
        }

        private const int windowSize = 5;
        private int numOfSamples;
        private Net<float> net;
        private Volume trainingVolume;
        private Volume trainingLabels;

        SgdTrainer trainer;

        private void CreateNetwork()
        {
            BuilderInstance.Volume = new VolumeBuilder();

            net = new Net<float>();

            const int filters = 10;

            net.AddLayer(new InputLayer(windowSize, windowSize, 1));
            net.AddLayer(new ConvLayer(5, 5, filters) { Stride = 1, Pad = 2 });
            net.AddLayer(new ReluLayer());
            net.AddLayer(new PoolLayer(2, 2) { Stride = 2 });
            net.AddLayer(new ConvLayer(5, 5, filters) { Stride = 1, Pad = 2 });
            net.AddLayer(new ReluLayer());
            net.AddLayer(new FullyConnLayer(20));
            net.AddLayer(new TanhLayer());
            net.AddLayer(new FullyConnLayer(2));
            net.AddLayer(new SoftmaxLayer(2));

            trainer = new SgdTrainer(net) { LearningRate = 0.01f, L2Decay = 0.001f, Momentum = 0.9f };
        }

        private void CreateTrainingData(string dataFile)
        {
            var serialier = new JsonSerializer();

            using (var reader = new StreamReader(dataFile))
            {
                var samplesList = serialier.Deserialize(reader, typeof(List<DataSample>)) as List<DataSample>;

                numOfSamples = samplesList.Count;

                DataSample.GenerateVolumes(samplesList, windowSize, out trainingVolume, out trainingLabels);
            }
        }

        private void Train(int iterations = 50)
        {
            double avloss = 0.0;

            for (int i = 0; i < iterations; i++)
            {
                trainer.Train(trainingVolume, trainingLabels);
                avloss += trainer.Loss;
            }

            avloss /= iterations;
            Console.WriteLine("Loss:" + avloss);
        }

        private void Test()
        {
            var prob = net.Forward(trainingVolume);
            var pred = net.GetPrediction();

            for (int i = 0; i < numOfSamples; i++)
            {
                Console.Write($"{prob.Get(i * 2 + 1)}, ");
            }

            Console.WriteLine();
        }
    }
}
