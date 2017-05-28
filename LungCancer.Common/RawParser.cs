using System;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

namespace LungCancer.Common
{
    public static class RawParser
    {
        private const int DefaultImageSize = 2048;
        private const int PixelDepth = 2;

        public static Image<Gray, UInt16> Open(string fileName)
        {
            byte[] data = new byte[DefaultImageSize * DefaultImageSize * PixelDepth];

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                fs.Read(data, 0, DefaultImageSize * DefaultImageSize * PixelDepth);
            }

            ushort[,,] imageData = new ushort[DefaultImageSize, DefaultImageSize, 1];
            
            for (int x = 0; x < DefaultImageSize; x++)
            {
                for (int y = 0; y < DefaultImageSize; y++)
                {
                    int index = (y * DefaultImageSize + x) * PixelDepth;
                    ushort pixel = (ushort)(data[index] * 256 + data[index + 1]);

                    imageData[y, x, 0] = pixel;
                }
            }

            return new Image<Gray, UInt16>(imageData);
        }
    }
}
