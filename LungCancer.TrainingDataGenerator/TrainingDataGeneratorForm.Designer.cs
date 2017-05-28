namespace LungCancer.TrainingDataGenerator
{
    partial class TrainingDataGenerator
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.cursorPosLabel = new System.Windows.Forms.Label();
            this.openDataFileButton = new System.Windows.Forms.Button();
            this.saveDataButton = new System.Windows.Forms.Button();
            this.deleteEntryButton = new System.Windows.Forms.Button();
            this.currentFileNameLabel = new System.Windows.Forms.Label();
            this.entriestListBox = new System.Windows.Forms.ListBox();
            this.nextImageButton = new System.Windows.Forms.Button();
            this.currentImagePictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentImagePictureBox)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cursorPosLabel);
            this.panel2.Controls.Add(this.openDataFileButton);
            this.panel2.Controls.Add(this.saveDataButton);
            this.panel2.Controls.Add(this.deleteEntryButton);
            this.panel2.Controls.Add(this.currentFileNameLabel);
            this.panel2.Controls.Add(this.entriestListBox);
            this.panel2.Controls.Add(this.nextImageButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(905, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(246, 757);
            this.panel2.TabIndex = 1;
            // 
            // cursorPosLbl
            // 
            this.cursorPosLabel.AutoSize = true;
            this.cursorPosLabel.Location = new System.Drawing.Point(6, 346);
            this.cursorPosLabel.Name = "cursorPosLbl";
            this.cursorPosLabel.Size = new System.Drawing.Size(48, 13);
            this.cursorPosLabel.TabIndex = 6;
            this.cursorPosLabel.Text = "X: 0 Y: 0";
            // 
            // openDataFileButton
            // 
            this.openDataFileButton.Location = new System.Drawing.Point(6, 426);
            this.openDataFileButton.Name = "openDataFileButton";
            this.openDataFileButton.Size = new System.Drawing.Size(229, 23);
            this.openDataFileButton.TabIndex = 5;
            this.openDataFileButton.Text = "Otwórz";
            this.openDataFileButton.UseVisualStyleBackColor = true;
            this.openDataFileButton.Click += new System.EventHandler(this.OpenDataFileButton_Click);
            // 
            // saveDataButton
            // 
            this.saveDataButton.Location = new System.Drawing.Point(6, 397);
            this.saveDataButton.Name = "saveDataButton";
            this.saveDataButton.Size = new System.Drawing.Size(229, 23);
            this.saveDataButton.TabIndex = 5;
            this.saveDataButton.Text = "Zapisz";
            this.saveDataButton.UseVisualStyleBackColor = true;
            this.saveDataButton.Click += new System.EventHandler(this.SaveDataButton_Click);
            // 
            // deleteEntryButton
            // 
            this.deleteEntryButton.Location = new System.Drawing.Point(6, 368);
            this.deleteEntryButton.Name = "deleteEntryButton";
            this.deleteEntryButton.Size = new System.Drawing.Size(229, 23);
            this.deleteEntryButton.TabIndex = 3;
            this.deleteEntryButton.Text = "Usuń";
            this.deleteEntryButton.UseVisualStyleBackColor = true;
            this.deleteEntryButton.Click += new System.EventHandler(this.DeleteEntryButton_Click);
            // 
            // currentFileNameLabel
            // 
            this.currentFileNameLabel.AutoSize = true;
            this.currentFileNameLabel.Location = new System.Drawing.Point(6, 12);
            this.currentFileNameLabel.Name = "currentFileNameLabel";
            this.currentFileNameLabel.Size = new System.Drawing.Size(0, 13);
            this.currentFileNameLabel.TabIndex = 2;
            // 
            // entriestListBox
            // 
            this.entriestListBox.FormattingEnabled = true;
            this.entriestListBox.Location = new System.Drawing.Point(6, 62);
            this.entriestListBox.Name = "entriestListBox";
            this.entriestListBox.Size = new System.Drawing.Size(229, 277);
            this.entriestListBox.TabIndex = 1;
            // 
            // nextImageButton
            // 
            this.nextImageButton.Enabled = false;
            this.nextImageButton.Location = new System.Drawing.Point(6, 33);
            this.nextImageButton.Name = "nextImageButton";
            this.nextImageButton.Size = new System.Drawing.Size(229, 23);
            this.nextImageButton.TabIndex = 0;
            this.nextImageButton.Text = "Następny";
            this.nextImageButton.UseVisualStyleBackColor = true;
            this.nextImageButton.Click += new System.EventHandler(this.NextImageButton_Click);
            // 
            // currentImagePictureBox
            // 
            this.currentImagePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentImagePictureBox.Location = new System.Drawing.Point(0, 24);
            this.currentImagePictureBox.Name = "currentImagePictureBox";
            this.currentImagePictureBox.Size = new System.Drawing.Size(905, 757);
            this.currentImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.currentImagePictureBox.TabIndex = 2;
            this.currentImagePictureBox.TabStop = false;
            this.currentImagePictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CurrentImagePictureBox_MouseDown);
            this.currentImagePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CurrentImagePictureBox_MouseMove);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1151, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.fileToolStripMenuItem.Text = "Plik";
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.openFolderToolStripMenuItem.Text = "Otwórz folder...";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.OpenFolderToolStripMenuItem_Click);
            // 
            // TrainingDataGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 781);
            this.Controls.Add(this.currentImagePictureBox);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "TrainingDataGenerator";
            this.Text = "Data generator";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentImagePictureBox)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox entriestListBox;
        private System.Windows.Forms.Button nextImageButton;
        private System.Windows.Forms.PictureBox currentImagePictureBox;
        private System.Windows.Forms.Button deleteEntryButton;
        private System.Windows.Forms.Label currentFileNameLabel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.Button saveDataButton;
        private System.Windows.Forms.Button openDataFileButton;
        private System.Windows.Forms.Label cursorPosLabel;
    }
}