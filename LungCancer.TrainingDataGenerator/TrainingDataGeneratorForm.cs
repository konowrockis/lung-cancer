using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV.Structure;
using LungCancer.Common;
using Newtonsoft.Json;

namespace LungCancer.TrainingDataGenerator
{
    public partial class TrainingDataGenerator : Form
    {
        private BindingList<DataSample> dataSamples;
        private List<FileInfo> images;
        private int currentImage = 0;
        private List<DescriptionData> descriptionsData;
        private DescriptionData currentDescriptionData;
        private const float pixelSize = 0.175f; // mm

        public TrainingDataGenerator()
        {
            InitializeComponent();

            dataSamples = new BindingList<DataSample>();

            entriestListBox.DataSource = dataSamples;
            descriptionsData = DescriptionData.Parse("CLNDAT_EN.txt").ToList();

            //OpenFolder(@"D:\System\Seba\Pulpit\ppo\All247images");
        }

        private void OpenFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            folderDialog.RootFolder = Environment.SpecialFolder.Desktop;

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFolder(folderDialog.SelectedPath);
            }
        }

        private void OpenFolder(string path)
        {
            var directory = new DirectoryInfo(path);

            images = directory
                .EnumerateFiles()
                .Where(f => f.Extension == ".IMG")
                .ToList();

            if (images.Count > 0)
            {
                SetCurrentImage(0);
            }
        }

        private void SetCurrentImage(int imageIndex)
        {
            currentImage = imageIndex;
            nextImageButton.Enabled = currentImage < images.Count - 1;

            var image = RawParser.Open(images[currentImage].FullName).Convert<Bgr, byte>();

            currentDescriptionData = descriptionsData.FirstOrDefault(i => i.ImageName == images[currentImage].Name);

            if (currentDescriptionData != null)
            {
                image.Draw(
                    new CircleF(new PointF(currentDescriptionData.X, currentDescriptionData.Y), currentDescriptionData.NoduleSize / pixelSize),
                    new Bgr(0, 0, 255), 2);

                currentFileNameLabel.Text = $"{images[currentImage].Name} {currentDescriptionData.Class} {currentDescriptionData.Sex} {currentDescriptionData.Years}";
            }
            else
            {
                currentFileNameLabel.Text = images[currentImage].Name;
            }

            currentImagePictureBox.Image = image.Bitmap;
        }

        private void NextImageButton_Click(object sender, EventArgs e)
        {
            SetCurrentImage(currentImage + 1);
        }

        private Point PictureboxToImage(int x, int y)
        {
            const double originalImageSize = 2048;

            var wfactor = originalImageSize / currentImagePictureBox.ClientSize.Width;
            var hfactor = originalImageSize / currentImagePictureBox.ClientSize.Height;

            var resizeFactor = Math.Max(wfactor, hfactor);
            var wborder = (currentImagePictureBox.ClientSize.Width - currentImagePictureBox.ClientSize.Height) / 2;
            var hborder = (currentImagePictureBox.ClientSize.Height - currentImagePictureBox.ClientSize.Width) / 2;

            wborder = Math.Max(0, wborder);
            hborder = Math.Max(0, hborder);

            return new Point((int)((x - wborder) * resizeFactor), (int)((y - hborder) * resizeFactor));
        }

        private void CurrentImagePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (images?.Count > currentImage)
            {
                var point = PictureboxToImage(e.X, e.Y);

                dataSamples.Add(new DataSample
                {
                    X = point.X,
                    Y = point.Y,
                    IsMalicious = e.Button == MouseButtons.Left,
                    FileName = images[currentImage].FullName
                });
            }
        }

        private void SaveDataButton_Click(object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog()
            {
                Filter = "(*.json)|*.json"
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var serialier = new JsonSerializer();

                using (var writer = new StreamWriter(fileDialog.FileName, false))
                {
                    serialier.Serialize(writer, dataSamples.ToList());
                }
            }
        }

        private void OpenDataFileButton_Click(object sender, EventArgs e)
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

                    samplesList
                        .ForEach(dataSample => dataSamples.Add(dataSample));
                }
            }
        }

        private void DeleteEntryButton_Click(object sender, EventArgs e)
        {
            if (entriestListBox.SelectedItem != null)
            {
                var sample = entriestListBox.SelectedItem as DataSample;

                dataSamples.Remove(sample);
            }
        }

        private void CurrentImagePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var point = PictureboxToImage(e.X, e.Y);

            cursorPosLabel.Text = $"X: {point.X} Y: {point.Y}";
        }
    }
}
