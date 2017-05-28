using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancer.TrainingDataGenerator
{
    class DescriptionData
    {
        public string ImageName { get; set; }
        public int Class { get; set; }
        public int NoduleSize { get; set; }
        public string Sex { get; set; }
        public string Years { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public static IEnumerable<DescriptionData> Parse(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        yield return ParseLine(line);
                    }
                }
            }
        }

        private static DescriptionData ParseLine(string line)
        {
            var elems = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

            return new DescriptionData()
            {
                ImageName = elems[0],
                Class = Int32.Parse(elems[1]),
                NoduleSize = Int32.Parse(elems[2]),
                Years = elems[3],
                Sex = elems[4],
                X = Int32.Parse(elems[5]),
                Y = Int32.Parse(elems[6])
            };
        }
    }
}
