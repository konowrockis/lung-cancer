﻿namespace LungCancer.NetTrainer
{
    partial class TrainVisualisation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.startPauseButton = new System.Windows.Forms.Button();
            this.lossGraph = new LungCancer.NetTrainer.LossGraph();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.startPauseButton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(465, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 496);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sieć neuronowa";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(85, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "0,01";
            // 
            // lossGraph
            // 
            this.lossGraph.Location = new System.Drawing.Point(85, 64);
            this.lossGraph.Name = "lossGraph1";
            this.lossGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Learning rate:";
            // 
            // startPauseButton
            // 
            this.startPauseButton.Location = new System.Drawing.Point(6, 19);
            this.startPauseButton.Name = "startPauseButton";
            this.startPauseButton.Size = new System.Drawing.Size(182, 23);
            this.startPauseButton.TabIndex = 0;
            this.startPauseButton.Text = "Zacznij uczyć";
            this.startPauseButton.UseVisualStyleBackColor = true;
            this.startPauseButton.Click += new System.EventHandler(this.StartPauseButton_Click);
            // 
            // TrainVisualisation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 496);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lossGraph);
            this.Name = "TrainVisualisation";
            this.Text = "LungCancer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startPauseButton;
        private LungCancer.NetTrainer.LossGraph lossGraph;
    }
}