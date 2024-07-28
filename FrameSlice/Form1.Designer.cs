namespace FrameSlice
{
    partial class PhotoCutter
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhotoCutter));
            this.vlcControl1 = new Vlc.DotNet.Forms.VlcControl();
            this.StartStopButton = new System.Windows.Forms.Button();
            this.textBoxURLVideo = new System.Windows.Forms.TextBox();
            this.BrowseVideo = new System.Windows.Forms.Button();
            this.muteButton = new System.Windows.Forms.Button();
            this.tblImageDestination = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tblCSVDestination = new System.Windows.Forms.TextBox();
            this.BrowseButtonImage = new System.Windows.Forms.Button();
            this.BrowseButtonCSV = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.comboBoxCamera = new System.Windows.Forms.ComboBox();
            this.syncButton = new System.Windows.Forms.Button();
            this.labelCurrentTime = new System.Windows.Forms.Label();
            this.textBoxStriderCSV = new System.Windows.Forms.TextBox();
            this.loadStriderCSVButton = new System.Windows.Forms.Button();
            this.csvImage = new System.Windows.Forms.PictureBox();
            this.nextImageButton = new System.Windows.Forms.Button();
            this.previousImageButton = new System.Windows.Forms.Button();
            this.previousFrameButton = new System.Windows.Forms.Button();
            this.nextFrameButton = new System.Windows.Forms.Button();
            this.goButton = new System.Windows.Forms.Button();
            this.labelMP4Index = new System.Windows.Forms.Label();
            this.vScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ImageTimeLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.estimatedTimeLabel = new System.Windows.Forms.Label();
            this.ImagesFromImagesLabel = new System.Windows.Forms.Label();
            this.lockButton = new System.Windows.Forms.Button();
            this.ProcessImage = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.csvImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessImage)).BeginInit();
            this.SuspendLayout();
            // 
            // vlcControl1
            // 
            this.vlcControl1.BackColor = System.Drawing.Color.Black;
            this.vlcControl1.Location = new System.Drawing.Point(12, 39);
            this.vlcControl1.Name = "vlcControl1";
            this.vlcControl1.Size = new System.Drawing.Size(648, 396);
            this.vlcControl1.Spu = -1;
            this.vlcControl1.TabIndex = 0;
            this.vlcControl1.Text = "vlcControl1";
            this.vlcControl1.VlcLibDirectory = ((System.IO.DirectoryInfo)(resources.GetObject("vlcControl1.VlcLibDirectory")));
            this.vlcControl1.VlcMediaplayerOptions = null;
            // 
            // StartStopButton
            // 
            this.StartStopButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("StartStopButton.BackgroundImage")));
            this.StartStopButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.StartStopButton.Location = new System.Drawing.Point(15, 441);
            this.StartStopButton.Name = "StartStopButton";
            this.StartStopButton.Size = new System.Drawing.Size(30, 25);
            this.StartStopButton.TabIndex = 2;
            this.StartStopButton.UseVisualStyleBackColor = true;
            this.StartStopButton.Click += new System.EventHandler(this.Playbutton_Click);
            // 
            // textBoxURLVideo
            // 
            this.textBoxURLVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxURLVideo.Location = new System.Drawing.Point(173, 10);
            this.textBoxURLVideo.Name = "textBoxURLVideo";
            this.textBoxURLVideo.ReadOnly = true;
            this.textBoxURLVideo.Size = new System.Drawing.Size(418, 24);
            this.textBoxURLVideo.TabIndex = 6;
            // 
            // BrowseVideo
            // 
            this.BrowseVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.BrowseVideo.Location = new System.Drawing.Point(597, 10);
            this.BrowseVideo.Name = "BrowseVideo";
            this.BrowseVideo.Size = new System.Drawing.Size(18, 23);
            this.BrowseVideo.TabIndex = 7;
            this.BrowseVideo.Text = ":";
            this.BrowseVideo.UseVisualStyleBackColor = true;
            this.BrowseVideo.Click += new System.EventHandler(this.BrowseVideo_Click);
            // 
            // muteButton
            // 
            this.muteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.muteButton.Location = new System.Drawing.Point(530, 441);
            this.muteButton.Name = "muteButton";
            this.muteButton.Size = new System.Drawing.Size(53, 25);
            this.muteButton.TabIndex = 8;
            this.muteButton.Text = "Mute";
            this.muteButton.UseVisualStyleBackColor = true;
            this.muteButton.Click += new System.EventHandler(this.MuteButton_Click);
            // 
            // tblImageDestination
            // 
            this.tblImageDestination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tblImageDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tblImageDestination.Location = new System.Drawing.Point(204, 528);
            this.tblImageDestination.Name = "tblImageDestination";
            this.tblImageDestination.Size = new System.Drawing.Size(393, 24);
            this.tblImageDestination.TabIndex = 9;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBox2.Location = new System.Drawing.Point(15, 528);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(189, 24);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "   Image Destination Folder";
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBox3.Location = new System.Drawing.Point(15, 558);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(189, 24);
            this.textBox3.TabIndex = 12;
            this.textBox3.Text = "     CSV Destination Folder";
            // 
            // tblCSVDestination
            // 
            this.tblCSVDestination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tblCSVDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tblCSVDestination.Location = new System.Drawing.Point(204, 558);
            this.tblCSVDestination.Name = "tblCSVDestination";
            this.tblCSVDestination.Size = new System.Drawing.Size(393, 24);
            this.tblCSVDestination.TabIndex = 11;
            // 
            // BrowseButtonImage
            // 
            this.BrowseButtonImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.BrowseButtonImage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BrowseButtonImage.Location = new System.Drawing.Point(597, 528);
            this.BrowseButtonImage.Name = "BrowseButtonImage";
            this.BrowseButtonImage.Size = new System.Drawing.Size(18, 24);
            this.BrowseButtonImage.TabIndex = 13;
            this.BrowseButtonImage.Text = ":";
            this.BrowseButtonImage.UseVisualStyleBackColor = true;
            this.BrowseButtonImage.Click += new System.EventHandler(this.BrowseButtonImage_Click);
            // 
            // BrowseButtonCSV
            // 
            this.BrowseButtonCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.BrowseButtonCSV.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BrowseButtonCSV.Location = new System.Drawing.Point(597, 558);
            this.BrowseButtonCSV.Name = "BrowseButtonCSV";
            this.BrowseButtonCSV.Size = new System.Drawing.Size(18, 24);
            this.BrowseButtonCSV.TabIndex = 14;
            this.BrowseButtonCSV.Text = ":";
            this.BrowseButtonCSV.UseVisualStyleBackColor = true;
            this.BrowseButtonCSV.Click += new System.EventHandler(this.BrowseButtonCSV_Click);
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBox5.Location = new System.Drawing.Point(15, 498);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(189, 24);
            this.textBox5.TabIndex = 15;
            this.textBox5.Text = "     Choose camera name";
            // 
            // comboBoxCamera
            // 
            this.comboBoxCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.comboBoxCamera.FormattingEnabled = true;
            this.comboBoxCamera.Location = new System.Drawing.Point(204, 498);
            this.comboBoxCamera.Name = "comboBoxCamera";
            this.comboBoxCamera.Size = new System.Drawing.Size(162, 24);
            this.comboBoxCamera.TabIndex = 17;
            this.comboBoxCamera.SelectedIndexChanged += new System.EventHandler(this.comboBoxCamera_SelectedIndexChanged);
            // 
            // syncButton
            // 
            this.syncButton.Location = new System.Drawing.Point(666, 211);
            this.syncButton.Name = "syncButton";
            this.syncButton.Size = new System.Drawing.Size(75, 23);
            this.syncButton.TabIndex = 18;
            this.syncButton.Text = "Sync";
            this.syncButton.UseVisualStyleBackColor = true;
            this.syncButton.Click += new System.EventHandler(this.syncButton_Click);
            // 
            // labelCurrentTime
            // 
            this.labelCurrentTime.AutoSize = true;
            this.labelCurrentTime.Location = new System.Drawing.Point(587, 415);
            this.labelCurrentTime.Name = "labelCurrentTime";
            this.labelCurrentTime.Size = new System.Drawing.Size(70, 13);
            this.labelCurrentTime.TabIndex = 19;
            this.labelCurrentTime.Text = "00:00:00:000";
            // 
            // textBoxStriderCSV
            // 
            this.textBoxStriderCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxStriderCSV.Location = new System.Drawing.Point(751, 8);
            this.textBoxStriderCSV.Name = "textBoxStriderCSV";
            this.textBoxStriderCSV.ReadOnly = true;
            this.textBoxStriderCSV.Size = new System.Drawing.Size(359, 24);
            this.textBoxStriderCSV.TabIndex = 20;
            // 
            // loadStriderCSVButton
            // 
            this.loadStriderCSVButton.Location = new System.Drawing.Point(1116, 8);
            this.loadStriderCSVButton.Name = "loadStriderCSVButton";
            this.loadStriderCSVButton.Size = new System.Drawing.Size(129, 23);
            this.loadStriderCSVButton.TabIndex = 21;
            this.loadStriderCSVButton.Text = "Load Strider CSV";
            this.loadStriderCSVButton.UseVisualStyleBackColor = true;
            this.loadStriderCSVButton.Click += new System.EventHandler(this.loadStriderCSVButton_Click);
            // 
            // csvImage
            // 
            this.csvImage.Location = new System.Drawing.Point(751, 39);
            this.csvImage.Name = "csvImage";
            this.csvImage.Size = new System.Drawing.Size(494, 396);
            this.csvImage.TabIndex = 22;
            this.csvImage.TabStop = false;
            // 
            // nextImageButton
            // 
            this.nextImageButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nextImageButton.BackgroundImage")));
            this.nextImageButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.nextImageButton.FlatAppearance.BorderSize = 0;
            this.nextImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextImageButton.Location = new System.Drawing.Point(1026, 441);
            this.nextImageButton.Name = "nextImageButton";
            this.nextImageButton.Size = new System.Drawing.Size(25, 25);
            this.nextImageButton.TabIndex = 23;
            this.nextImageButton.UseVisualStyleBackColor = true;
            this.nextImageButton.Click += new System.EventHandler(this.nextImageButton_Click);
            // 
            // previousImageButton
            // 
            this.previousImageButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("previousImageButton.BackgroundImage")));
            this.previousImageButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.previousImageButton.FlatAppearance.BorderSize = 0;
            this.previousImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previousImageButton.Location = new System.Drawing.Point(995, 441);
            this.previousImageButton.Name = "previousImageButton";
            this.previousImageButton.Size = new System.Drawing.Size(25, 25);
            this.previousImageButton.TabIndex = 24;
            this.previousImageButton.UseVisualStyleBackColor = true;
            this.previousImageButton.Click += new System.EventHandler(this.previousImageButton_Click);
            // 
            // previousFrameButton
            // 
            this.previousFrameButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("previousFrameButton.BackgroundImage")));
            this.previousFrameButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.previousFrameButton.Location = new System.Drawing.Point(589, 441);
            this.previousFrameButton.Name = "previousFrameButton";
            this.previousFrameButton.Size = new System.Drawing.Size(30, 25);
            this.previousFrameButton.TabIndex = 25;
            this.previousFrameButton.UseVisualStyleBackColor = true;
            // 
            // nextFrameButton
            // 
            this.nextFrameButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nextFrameButton.BackgroundImage")));
            this.nextFrameButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.nextFrameButton.Location = new System.Drawing.Point(625, 441);
            this.nextFrameButton.Name = "nextFrameButton";
            this.nextFrameButton.Size = new System.Drawing.Size(30, 25);
            this.nextFrameButton.TabIndex = 26;
            this.nextFrameButton.UseVisualStyleBackColor = true;
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(751, 528);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(88, 54);
            this.goButton.TabIndex = 27;
            this.goButton.Text = "Go";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // labelMP4Index
            // 
            this.labelMP4Index.AutoSize = true;
            this.labelMP4Index.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelMP4Index.Location = new System.Drawing.Point(621, 12);
            this.labelMP4Index.Name = "labelMP4Index";
            this.labelMP4Index.Size = new System.Drawing.Size(31, 20);
            this.labelMP4Index.TabIndex = 28;
            this.labelMP4Index.Text = "0/0";
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(48, 441);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(479, 25);
            this.vScrollBar1.TabIndex = 30;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBox1.Location = new System.Drawing.Point(12, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(155, 24);
            this.textBox1.TabIndex = 32;
            this.textBox1.Text = "  GoPro Video Folder";
            // 
            // ImageTimeLabel
            // 
            this.ImageTimeLabel.AutoSize = true;
            this.ImageTimeLabel.Location = new System.Drawing.Point(1169, 415);
            this.ImageTimeLabel.Name = "ImageTimeLabel";
            this.ImageTimeLabel.Size = new System.Drawing.Size(70, 13);
            this.ImageTimeLabel.TabIndex = 33;
            this.ImageTimeLabel.Text = "00:00:00:000";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 604);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(582, 23);
            this.progressBar.TabIndex = 34;
            // 
            // estimatedTimeLabel
            // 
            this.estimatedTimeLabel.AutoSize = true;
            this.estimatedTimeLabel.Location = new System.Drawing.Point(21, 588);
            this.estimatedTimeLabel.Name = "estimatedTimeLabel";
            this.estimatedTimeLabel.Size = new System.Drawing.Size(199, 13);
            this.estimatedTimeLabel.TabIndex = 36;
            this.estimatedTimeLabel.Text = "Estimated Time Remaining: Calculating...";
            this.estimatedTimeLabel.Visible = false;
            // 
            // ImagesFromImagesLabel
            // 
            this.ImagesFromImagesLabel.AutoSize = true;
            this.ImagesFromImagesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ImagesFromImagesLabel.Location = new System.Drawing.Point(603, 607);
            this.ImagesFromImagesLabel.Name = "ImagesFromImagesLabel";
            this.ImagesFromImagesLabel.Size = new System.Drawing.Size(28, 17);
            this.ImagesFromImagesLabel.TabIndex = 37;
            this.ImagesFromImagesLabel.Text = "0/0";
            this.ImagesFromImagesLabel.Visible = false;
            // 
            // lockButton
            // 
            this.lockButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lockButton.BackgroundImage")));
            this.lockButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lockButton.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lockButton.Enabled = false;
            this.lockButton.Location = new System.Drawing.Point(694, 240);
            this.lockButton.Name = "lockButton";
            this.lockButton.Size = new System.Drawing.Size(24, 23);
            this.lockButton.TabIndex = 38;
            this.lockButton.UseVisualStyleBackColor = true;
            this.lockButton.Visible = false;
            this.lockButton.Click += new System.EventHandler(this.lockButton_Click);
            // 
            // ProcessImage
            // 
            this.ProcessImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ProcessImage.BackgroundImage")));
            this.ProcessImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ProcessImage.Location = new System.Drawing.Point(12, 39);
            this.ProcessImage.Name = "ProcessImage";
            this.ProcessImage.Size = new System.Drawing.Size(648, 396);
            this.ProcessImage.TabIndex = 39;
            this.ProcessImage.TabStop = false;
            this.ProcessImage.Visible = false;
            // 
            // PhotoCutter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1299, 639);
            this.Controls.Add(this.ProcessImage);
            this.Controls.Add(this.lockButton);
            this.Controls.Add(this.ImagesFromImagesLabel);
            this.Controls.Add(this.estimatedTimeLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.ImageTimeLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.labelMP4Index);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.nextFrameButton);
            this.Controls.Add(this.previousFrameButton);
            this.Controls.Add(this.previousImageButton);
            this.Controls.Add(this.nextImageButton);
            this.Controls.Add(this.csvImage);
            this.Controls.Add(this.loadStriderCSVButton);
            this.Controls.Add(this.textBoxStriderCSV);
            this.Controls.Add(this.labelCurrentTime);
            this.Controls.Add(this.syncButton);
            this.Controls.Add(this.comboBoxCamera);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.BrowseButtonCSV);
            this.Controls.Add(this.BrowseButtonImage);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.tblCSVDestination);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.tblImageDestination);
            this.Controls.Add(this.muteButton);
            this.Controls.Add(this.BrowseVideo);
            this.Controls.Add(this.textBoxURLVideo);
            this.Controls.Add(this.StartStopButton);
            this.Controls.Add(this.vlcControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "PhotoCutter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manam Applications - FrameSlice";
            this.Load += new System.EventHandler(this.PhotoCutter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.csvImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Vlc.DotNet.Forms.VlcControl vlcControl1;
        private System.Windows.Forms.Button StartStopButton;
        private System.Windows.Forms.TextBox textBoxURLVideo;
        private System.Windows.Forms.Button BrowseVideo;
        private System.Windows.Forms.TextBox tblImageDestination;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox tblCSVDestination;
        private System.Windows.Forms.Button BrowseButtonImage;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ComboBox comboBoxCamera;
        private System.Windows.Forms.Button syncButton;
        private System.Windows.Forms.Label labelCurrentTime;
        private System.Windows.Forms.TextBox textBoxStriderCSV;
        private System.Windows.Forms.Button loadStriderCSVButton;
        private System.Windows.Forms.PictureBox csvImage;
        private System.Windows.Forms.Button nextImageButton;
        private System.Windows.Forms.Button previousImageButton;
        private System.Windows.Forms.Button previousFrameButton;
        private System.Windows.Forms.Button nextFrameButton;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Label labelMP4Index;
        private System.Windows.Forms.HScrollBar vScrollBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label ImageTimeLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label estimatedTimeLabel;
        private System.Windows.Forms.Label ImagesFromImagesLabel;
        private System.Windows.Forms.Button muteButton;
        private System.Windows.Forms.Button lockButton;
        private System.Windows.Forms.PictureBox ProcessImage;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button BrowseButtonCSV;
    }
}

