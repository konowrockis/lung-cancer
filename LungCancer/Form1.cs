using System.Windows.Forms;

namespace LungCancer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            var image = RawParser.Open(@"Images\JPCLN001.IMG");

            pictureBox1.Image = image.Bitmap;
        }
    }
}
