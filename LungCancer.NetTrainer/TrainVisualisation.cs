using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LungCancer.NetTrainer
{
    public partial class TrainVisualisation : Form
    {
        public event EventHandler StartTraining;
        public event EventHandler PauseTraining;

        private bool isTrainingRunning = false;

        public TrainVisualisation()
        {
            InitializeComponent();
        }

        public void AddLoss(double loss)
        {
            lossGraph.AddLoss(loss);
        }

        private void StartPauseButton_Click(object sender, EventArgs e)
        {
            if (!isTrainingRunning)
            {
                var learningRate = float.Parse(textBox1.Text);

                StartTraining?.Invoke(learningRate, EventArgs.Empty);
                startPauseButton.Text = "Zatrzymaj uczenie";
            }
            else
            {
                PauseTraining?.Invoke(this, EventArgs.Empty);
                startPauseButton.Text = "Zacznij uczyć";
            }

            isTrainingRunning = !isTrainingRunning;
            textBox1.Enabled = isTrainingRunning;
        }
    }
}
