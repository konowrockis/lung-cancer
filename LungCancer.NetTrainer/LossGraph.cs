using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LungCancer.NetTrainer
{
    public class LossGraph : Panel
    {
        private readonly List<double> losses;

        public LossGraph() : base()
        {
            losses = new List<double>();
        }

        public void AddLoss(double loss)
        {
            losses.Add(loss);

            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(Refresh));
            }
            else
            {
                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            if (losses.Count == 0) return;

            int lossesSkip = losses.Count - Width;

            var currentLosses = losses.Skip(lossesSkip < 0 ? 0 : lossesSkip).ToList();

            var min = currentLosses.Min();
            var max = currentLosses.Max();

            if (max == min)
            {
                max = min + 1;
            }

            int i = 0;

            foreach (var loss in currentLosses)
            {
                var val = currentLosses[i];
                var y = Height - (val - min) / (max - min) * (Height) - 1;

                e.Graphics.FillRectangle(Brushes.Black, i++ + 1, (float)y, 2, 2);
            }
        }
    }
}
