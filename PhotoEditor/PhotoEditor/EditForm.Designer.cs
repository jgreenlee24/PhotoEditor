namespace PhotoEditor
{
    partial class EditForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.toolPanel = new System.Windows.Forms.Panel();
            this.cropButton = new System.Windows.Forms.Button();
            this.invertButton = new System.Windows.Forms.Button();
            this.colorButton = new System.Windows.Forms.Button();
            this.lightLabel = new System.Windows.Forms.Label();
            this.darkLabel = new System.Windows.Forms.Label();
            this.brightnessLabel = new System.Windows.Forms.Label();
            this.brightnessBar = new System.Windows.Forms.TrackBar();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.imageTransformer = new System.ComponentModel.BackgroundWorker();
            this.toolPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(497, 526);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(416, 526);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // toolPanel
            // 
            this.toolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolPanel.Controls.Add(this.cropButton);
            this.toolPanel.Controls.Add(this.invertButton);
            this.toolPanel.Controls.Add(this.colorButton);
            this.toolPanel.Controls.Add(this.lightLabel);
            this.toolPanel.Controls.Add(this.darkLabel);
            this.toolPanel.Controls.Add(this.brightnessLabel);
            this.toolPanel.Controls.Add(this.brightnessBar);
            this.toolPanel.Location = new System.Drawing.Point(12, 441);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(560, 79);
            this.toolPanel.TabIndex = 2;
            // 
            // cropButton
            // 
            this.cropButton.Location = new System.Drawing.Point(353, 29);
            this.cropButton.Name = "cropButton";
            this.cropButton.Size = new System.Drawing.Size(75, 23);
            this.cropButton.TabIndex = 6;
            this.cropButton.Text = "Crop";
            this.cropButton.UseVisualStyleBackColor = true;
            this.cropButton.Visible = false;
            // 
            // invertButton
            // 
            this.invertButton.Location = new System.Drawing.Point(272, 29);
            this.invertButton.Name = "invertButton";
            this.invertButton.Size = new System.Drawing.Size(75, 23);
            this.invertButton.TabIndex = 5;
            this.invertButton.Text = "Invert";
            this.invertButton.UseVisualStyleBackColor = true;
            this.invertButton.Click += new System.EventHandler(this.invertButton_Click);
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(191, 29);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(75, 23);
            this.colorButton.TabIndex = 4;
            this.colorButton.Text = "Color...";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // lightLabel
            // 
            this.lightLabel.AutoSize = true;
            this.lightLabel.Location = new System.Drawing.Point(136, 51);
            this.lightLabel.Name = "lightLabel";
            this.lightLabel.Size = new System.Drawing.Size(30, 13);
            this.lightLabel.TabIndex = 3;
            this.lightLabel.Text = "Light";
            // 
            // darkLabel
            // 
            this.darkLabel.AutoSize = true;
            this.darkLabel.Location = new System.Drawing.Point(14, 51);
            this.darkLabel.Name = "darkLabel";
            this.darkLabel.Size = new System.Drawing.Size(30, 13);
            this.darkLabel.TabIndex = 2;
            this.darkLabel.Text = "Dark";
            // 
            // brightnessLabel
            // 
            this.brightnessLabel.AutoSize = true;
            this.brightnessLabel.Location = new System.Drawing.Point(64, 13);
            this.brightnessLabel.Name = "brightnessLabel";
            this.brightnessLabel.Size = new System.Drawing.Size(56, 13);
            this.brightnessLabel.TabIndex = 1;
            this.brightnessLabel.Text = "Brightness";
            // 
            // brightnessBar
            // 
            this.brightnessBar.LargeChange = 4;
            this.brightnessBar.Location = new System.Drawing.Point(17, 29);
            this.brightnessBar.Maximum = 32;
            this.brightnessBar.Minimum = -32;
            this.brightnessBar.Name = "brightnessBar";
            this.brightnessBar.Size = new System.Drawing.Size(149, 45);
            this.brightnessBar.TabIndex = 0;
            this.brightnessBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.brightnessBar.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            this.brightnessBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.brightnessBar_MouseDown);
            this.brightnessBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.brightnessBar_MouseUp);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(560, 423);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            // 
            // imageTransformer
            // 
            this.imageTransformer.WorkerReportsProgress = true;
            this.imageTransformer.WorkerSupportsCancellation = true;
            this.imageTransformer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.imageTransformer_DoWork);
            this.imageTransformer.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.imageTransformer_ProgressChanged);
            this.imageTransformer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.imageTransformer_RunWorkerCompleted);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.toolPanel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Name = "EditForm";
            this.Text = "Edit Photo";
            this.toolPanel.ResumeLayout(false);
            this.toolPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Panel toolPanel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button cropButton;
        private System.Windows.Forms.Button invertButton;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Label lightLabel;
        private System.Windows.Forms.Label darkLabel;
        private System.Windows.Forms.Label brightnessLabel;
        private System.Windows.Forms.TrackBar brightnessBar;
        private System.ComponentModel.BackgroundWorker imageTransformer;
    }
}
