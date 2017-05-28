using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConvNetSharp.Volume;
using ConvNetSharp.Volume.GPU.Single;
using Emgu.CV;
using Emgu.CV.Structure;

namespace LungCancer.Common
{
    public class DataSample
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsMalicious { get; set; }
        public string FileName { get; set; }

        public override string ToString()
        {
            var isMalicious = IsMalicious ? "malicious" : "harmless ";
            return $"{isMalicious} {X} {Y} {new FileInfo(FileName).Name}";
        }

        public static void GenerateVolumes(List<DataSample> dataSamples, int windowSize, out Volume trainingSamples, out Volume trainingLabels)
        {
            float[] trainingData = new float[windowSize * windowSize * dataSamples.Count];
            float[] labelsData = new float[2 * dataSamples.Count];
            
            var images = dataSamples.GroupBy(sample => sample.FileName);

            int i = 0;

            foreach (var imageSamples in images)
            {
                var image = RawParser.Open(imageSamples.Key)
                    .Convert<Gray, float>()
                    .Mul(1.0 / 4096.0);

                foreach (var sample in imageSamples)
                {
                    GenerateData(image, sample.X, sample.Y, windowSize, trainingData, i * windowSize * windowSize);

                    labelsData[i++ + (sample.IsMalicious ? 1 : 0)] = 1.0f;
                }
            }

            trainingSamples = new Volume(trainingData, new Shape(windowSize, windowSize, 1, dataSamples.Count));
            trainingLabels = new Volume(labelsData, new Shape(1, 1, 2, dataSamples.Count));
        }

        private static void GenerateData(Image<Gray, float> image, int sourceX, int sourceY, int windowSize, float[] data, int startIndex)
        {
            int i = startIndex;

            for (int x = sourceX - windowSize / 2; x <= sourceX + windowSize / 2; x++)
            {
                for (int y = sourceY - windowSize / 2; y <= sourceY + windowSize / 2; y++)
                {
                    if (x < 0 || x > 2048 || y < 0 || y > 2048)
                    {
                        data[i++] = 0;
                    }
                    else
                    {
                        data[i++] = image.Data[y, x, 0];
                    }
                }
            }
        }
    }
}
