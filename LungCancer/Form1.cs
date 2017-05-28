using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ConvNetSharp.Core;
using ConvNetSharp.Core.Layers.Single;
using ConvNetSharp.Core.Training.Single;
using ConvNetSharp.Volume.GPU.Single;
using Emgu.CV;
using Emgu.CV.Structure;
using LungCancer.Common;
using Newtonsoft.Json;

namespace LungCancer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var image = RawParser.Open("Images\\JPCLN001.IMG");

            var image2 = image
                .SmoothMedian(3)
                .Convert<Gray, double>()
                .Mul(1.0 / 4096.0);

            //var image3 = image2.Convert<Gray, float>();
            // image3.ROI = new Rectangle(100, 100, 2048 - 200, 2048 - 200);
            // image3 = image3.Copy();

            //image3 = image3.SmoothGaussian(21).Sub(image3);

            //var image4 = image3.Pow(2);//.SmoothGaussian(51).Pow(0.5);
            //Emgu.CV.CvInvoke.Divide(image3, image4, image3, 1);

            //image5 = image5.ThresholdAdaptive(new Gray(255), Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.Binary, 41, new Gray(0));

            //image3.Draw(new Ellipse(new PointF(1634, 692), new SizeF(150, 150), 0), new Gray(1000), 2);

            //pictureBox1.Image = image2.Bitmap;

            CreateTrainingData(image2);

            new Thread(new ThreadStart(() =>
            {
                CreateNetwork();

                for (int i = 0; i < 100; i++)
                {
                    var start = DateTime.Now;

                    Test();
                    Train();

                    Console.WriteLine($"Step took: {DateTime.Now - start}.");
                }
                Test();
            })).Start();
            
            
            //TrainingDataGenerator tdg = new TrainingDataGenerator();
            //tdg.Show();
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

            trainer = new SgdTrainer(net) { LearningRate = 0.01f, L2Decay = 0.001f, Momentum = 0.9f, BatchSize = 25 };
        }

        private void CreateTrainingData(Image<Gray, double> image)
        {
            var fileDialog = new OpenFileDialog()
            {
                Filter = "(*.json)|*.json"
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var serialier = new JsonSerializer();

                using (var reader = new StreamReader(fileDialog.FileName))
                {
                    var samplesList = serialier.Deserialize(reader, typeof(List<DataSample>)) as List<DataSample>;

                    numOfSamples = samplesList.Count;

                    DataSample.GenerateVolumes(samplesList, windowSize, out trainingVolume, out trainingLabels);
                }
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
            //var prob = net.Forward(trainingVolume);
            //var pred = net.GetPrediction();

            //for (int i = 0; i < numOfSamples; i++)
            //{
            //    Console.Write($"{prob.Get(i * 2 + 1)}, ");
            //}

            Console.WriteLine();
        }
    }
}
