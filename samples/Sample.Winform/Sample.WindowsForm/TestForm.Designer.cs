namespace Sample.WindowsForm
{
	partial class TestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSources = new System.Windows.Forms.ToolStripDropDownButton();
            this.sepSourceList = new System.Windows.Forms.ToolStripSeparator();
            this.reloadSourcesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStartCapture = new System.Windows.Forms.ToolStripButton();
            this.btnStopScan = new System.Windows.Forms.ToolStripButton();
            this.btnSaveImage = new System.Windows.Forms.ToolStripButton();
            this.groupDuplex = new System.Windows.Forms.GroupBox();
            this.ckDuplex = new System.Windows.Forms.CheckBox();
            this.groupSize = new System.Windows.Forms.GroupBox();
            this.comboSize = new System.Windows.Forms.ComboBox();
            this.groupDepth = new System.Windows.Forms.GroupBox();
            this.comboDepth = new System.Windows.Forms.ComboBox();
            this.groupDPI = new System.Windows.Forms.GroupBox();
            this.comboDPI = new System.Windows.Forms.ComboBox();
            this.btnAllSettings = new System.Windows.Forms.Button();
            this.groupImages = new System.Windows.Forms.GroupBox();
            this.comboImages = new System.Windows.Forms.ComboBox();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.groupPages = new System.Windows.Forms.GroupBox();
            this.UpDownNumPages = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupDuplex.SuspendLayout();
            this.groupSize.SuspendLayout();
            this.groupDepth.SuspendLayout();
            this.groupDPI.SuspendLayout();
            this.groupImages.SuspendLayout();
            this.panelOptions.SuspendLayout();
            this.groupPages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownNumPages)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1143, 590);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "Test";
            this.saveFileDialog1.Filter = "png files|*.png";
            this.saveFileDialog1.Title = "Save Image";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSources,
            this.btnStartCapture,
            this.btnStopScan,
            this.btnSaveImage});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1143, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSources
            // 
            this.btnSources.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSources.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sepSourceList,
            this.reloadSourcesListToolStripMenuItem});
            this.btnSources.Image = ((System.Drawing.Image)(resources.GetObject("btnSources.Image")));
            this.btnSources.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSources.Name = "btnSources";
            this.btnSources.Size = new System.Drawing.Size(94, 22);
            this.btnSources.Text = "Select &sources";
            this.btnSources.DropDownOpening += new System.EventHandler(this.btnSources_DropDownOpening);
            // 
            // sepSourceList
            // 
            this.sepSourceList.Name = "sepSourceList";
            this.sepSourceList.Size = new System.Drawing.Size(168, 6);
            // 
            // reloadSourcesListToolStripMenuItem
            // 
            this.reloadSourcesListToolStripMenuItem.Name = "reloadSourcesListToolStripMenuItem";
            this.reloadSourcesListToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.reloadSourcesListToolStripMenuItem.Text = "&Reload sources list";
            this.reloadSourcesListToolStripMenuItem.Click += new System.EventHandler(this.reloadSourcesListToolStripMenuItem_Click);
            // 
            // btnStartCapture
            // 
            this.btnStartCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStartCapture.Enabled = false;
            this.btnStartCapture.Image = ((System.Drawing.Image)(resources.GetObject("btnStartCapture.Image")));
            this.btnStartCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartCapture.Name = "btnStartCapture";
            this.btnStartCapture.Size = new System.Drawing.Size(62, 22);
            this.btnStartCapture.Text = "S&tart scan";
            this.btnStartCapture.Click += new System.EventHandler(this.btnStartCapture_Click);
            // 
            // btnStopScan
            // 
            this.btnStopScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStopScan.Enabled = false;
            this.btnStopScan.Image = ((System.Drawing.Image)(resources.GetObject("btnStopScan.Image")));
            this.btnStopScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStopScan.Name = "btnStopScan";
            this.btnStopScan.Size = new System.Drawing.Size(62, 22);
            this.btnStopScan.Text = "Sto&p scan";
            this.btnStopScan.Click += new System.EventHandler(this.btnStopScan_Click);
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSaveImage.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveImage.Image")));
            this.btnSaveImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(71, 22);
            this.btnSaveImage.Text = "S&ave image";
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // groupDuplex
            // 
            this.groupDuplex.Controls.Add(this.ckDuplex);
            this.groupDuplex.Enabled = false;
            this.groupDuplex.Location = new System.Drawing.Point(8, 176);
            this.groupDuplex.Margin = new System.Windows.Forms.Padding(8);
            this.groupDuplex.Name = "groupDuplex";
            this.groupDuplex.Size = new System.Drawing.Size(102, 48);
            this.groupDuplex.TabIndex = 6;
            this.groupDuplex.TabStop = false;
            this.groupDuplex.Text = "Duplex";
            // 
            // ckDuplex
            // 
            this.ckDuplex.AutoSize = true;
            this.ckDuplex.Location = new System.Drawing.Point(18, 24);
            this.ckDuplex.Name = "ckDuplex";
            this.ckDuplex.Size = new System.Drawing.Size(65, 17);
            this.ckDuplex.TabIndex = 0;
            this.ckDuplex.Text = "Enabled";
            this.ckDuplex.UseVisualStyleBackColor = true;
            this.ckDuplex.CheckedChanged += new System.EventHandler(this.ckDuplex_CheckedChanged);
            // 
            // groupSize
            // 
            this.groupSize.Controls.Add(this.comboSize);
            this.groupSize.Enabled = false;
            this.groupSize.Location = new System.Drawing.Point(8, 111);
            this.groupSize.Margin = new System.Windows.Forms.Padding(8, 8, 8, 3);
            this.groupSize.Name = "groupSize";
            this.groupSize.Size = new System.Drawing.Size(237, 54);
            this.groupSize.TabIndex = 5;
            this.groupSize.TabStop = false;
            this.groupSize.Text = "Size";
            // 
            // comboSize
            // 
            this.comboSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSize.FormattingEnabled = true;
            this.comboSize.Location = new System.Drawing.Point(18, 19);
            this.comboSize.Name = "comboSize";
            this.comboSize.Size = new System.Drawing.Size(200, 21);
            this.comboSize.TabIndex = 0;
            this.comboSize.SelectedIndexChanged += new System.EventHandler(this.comboSize_SelectedIndexChanged);
            // 
            // groupDepth
            // 
            this.groupDepth.Controls.Add(this.comboDepth);
            this.groupDepth.Enabled = false;
            this.groupDepth.Location = new System.Drawing.Point(134, 46);
            this.groupDepth.Margin = new System.Windows.Forms.Padding(8, 8, 8, 3);
            this.groupDepth.Name = "groupDepth";
            this.groupDepth.Size = new System.Drawing.Size(111, 54);
            this.groupDepth.TabIndex = 4;
            this.groupDepth.TabStop = false;
            this.groupDepth.Text = "Depth";
            // 
            // comboDepth
            // 
            this.comboDepth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDepth.FormattingEnabled = true;
            this.comboDepth.Location = new System.Drawing.Point(18, 19);
            this.comboDepth.Name = "comboDepth";
            this.comboDepth.Size = new System.Drawing.Size(74, 21);
            this.comboDepth.TabIndex = 0;
            this.comboDepth.SelectedIndexChanged += new System.EventHandler(this.comboDepth_SelectedIndexChanged);
            // 
            // groupDPI
            // 
            this.groupDPI.Controls.Add(this.comboDPI);
            this.groupDPI.Enabled = false;
            this.groupDPI.Location = new System.Drawing.Point(8, 46);
            this.groupDPI.Margin = new System.Windows.Forms.Padding(8, 8, 8, 3);
            this.groupDPI.Name = "groupDPI";
            this.groupDPI.Size = new System.Drawing.Size(118, 54);
            this.groupDPI.TabIndex = 0;
            this.groupDPI.TabStop = false;
            this.groupDPI.Text = "DPI";
            // 
            // comboDPI
            // 
            this.comboDPI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboDPI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDPI.FormattingEnabled = true;
            this.comboDPI.Location = new System.Drawing.Point(18, 19);
            this.comboDPI.Name = "comboDPI";
            this.comboDPI.Size = new System.Drawing.Size(81, 21);
            this.comboDPI.TabIndex = 0;
            this.comboDPI.SelectedIndexChanged += new System.EventHandler(this.comboDPI_SelectedIndexChanged);
            // 
            // btnAllSettings
            // 
            this.btnAllSettings.Enabled = false;
            this.btnAllSettings.Location = new System.Drawing.Point(12, 12);
            this.btnAllSettings.Name = "btnAllSettings";
            this.btnAllSettings.Size = new System.Drawing.Size(148, 23);
            this.btnAllSettings.TabIndex = 7;
            this.btnAllSettings.Text = "Open driver settings";
            this.btnAllSettings.UseVisualStyleBackColor = true;
            this.btnAllSettings.Click += new System.EventHandler(this.btnAllSettings_Click);
            // 
            // groupImages
            // 
            this.groupImages.Controls.Add(this.comboImages);
            this.groupImages.Location = new System.Drawing.Point(114, 231);
            this.groupImages.Name = "groupImages";
            this.groupImages.Size = new System.Drawing.Size(131, 47);
            this.groupImages.TabIndex = 8;
            this.groupImages.TabStop = false;
            this.groupImages.Text = "Images";
            // 
            // comboImages
            // 
            this.comboImages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboImages.FormattingEnabled = true;
            this.comboImages.Location = new System.Drawing.Point(14, 18);
            this.comboImages.Name = "comboImages";
            this.comboImages.Size = new System.Drawing.Size(94, 21);
            this.comboImages.TabIndex = 1;
            this.comboImages.SelectedIndexChanged += new System.EventHandler(this.comboImages_SelectedIndexChanged);
            // 
            // panelOptions
            // 
            this.panelOptions.Controls.Add(this.groupPages);
            this.panelOptions.Controls.Add(this.groupDuplex);
            this.panelOptions.Controls.Add(this.groupSize);
            this.panelOptions.Controls.Add(this.groupDPI);
            this.panelOptions.Controls.Add(this.groupDepth);
            this.panelOptions.Controls.Add(this.groupImages);
            this.panelOptions.Controls.Add(this.btnAllSettings);
            this.panelOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelOptions.Location = new System.Drawing.Point(0, 25);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(261, 590);
            this.panelOptions.TabIndex = 4;
            // 
            // groupPages
            // 
            this.groupPages.Controls.Add(this.UpDownNumPages);
            this.groupPages.Location = new System.Drawing.Point(6, 231);
            this.groupPages.Name = "groupPages";
            this.groupPages.Size = new System.Drawing.Size(102, 47);
            this.groupPages.TabIndex = 9;
            this.groupPages.TabStop = false;
            this.groupPages.Text = "Num Pages";
            // 
            // UpDownNumPages
            // 
            this.UpDownNumPages.Location = new System.Drawing.Point(14, 19);
            this.UpDownNumPages.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.UpDownNumPages.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.UpDownNumPages.Name = "UpDownNumPages";
            this.UpDownNumPages.Size = new System.Drawing.Size(69, 20);
            this.UpDownNumPages.TabIndex = 10;
            this.UpDownNumPages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UpDownNumPages.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.UpDownNumPages.ValueChanged += new System.EventHandler(this.UpDownNumPages_ValueChanged);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 615);
            this.Controls.Add(this.panelOptions);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestForm";
            this.Text = "Test Form";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupDuplex.ResumeLayout(false);
            this.groupDuplex.PerformLayout();
            this.groupSize.ResumeLayout(false);
            this.groupDepth.ResumeLayout(false);
            this.groupDPI.ResumeLayout(false);
            this.groupImages.ResumeLayout(false);
            this.panelOptions.ResumeLayout(false);
            this.groupPages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UpDownNumPages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton btnSources;
        private System.Windows.Forms.ToolStripSeparator sepSourceList;
        private System.Windows.Forms.ToolStripMenuItem reloadSourcesListToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupDPI;
        private System.Windows.Forms.GroupBox groupDepth;
        private System.Windows.Forms.GroupBox groupSize;
        private System.Windows.Forms.GroupBox groupDuplex;
        private System.Windows.Forms.ComboBox comboDPI;
        private System.Windows.Forms.ComboBox comboSize;
        private System.Windows.Forms.ComboBox comboDepth;
        private System.Windows.Forms.ToolStripButton btnStartCapture;
        private System.Windows.Forms.ToolStripButton btnStopScan;
        private System.Windows.Forms.ToolStripButton btnSaveImage;
        private System.Windows.Forms.CheckBox ckDuplex;
        private System.Windows.Forms.Button btnAllSettings;
        private System.Windows.Forms.GroupBox groupImages;
        private System.Windows.Forms.ComboBox comboImages;
        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.GroupBox groupPages;
        private System.Windows.Forms.NumericUpDown UpDownNumPages;
    }
}

