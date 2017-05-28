using System.Windows.Forms;
using Emgu.CV.Structure;
using LungCancer.Common;

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

            pictureBox1.Image = image2.Bitmap;
        }
    }
}
