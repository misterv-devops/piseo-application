/*
 * Erstellt mit SharpDevelop.
 * Benutzer: Seppel
 * Datum: 08.06.2010
 * Zeit: 17:08
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace CAS4DLLTest
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblDLLInfo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbInterface = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbInterfaceOption = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            this.tbConfig = new System.Windows.Forms.TextBox();
            this.tbCalib = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnInit = new System.Windows.Forms.Button();
            this.btnDarkCurrent = new System.Windows.Forms.Button();
            this.btnMeasure = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnBrowseForConfig = new System.Windows.Forms.Button();
            this.btnBrowseForCalib = new System.Windows.Forms.Button();
            this.pgResults = new System.Windows.Forms.PropertyGrid();
            this.pbSpectrum = new System.Windows.Forms.PictureBox();
            this.udIntTime = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGetTOPInfo = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpectrum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udIntTime)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDLLInfo
            // 
            this.lblDLLInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDLLInfo.AutoEllipsis = true;
            this.lblDLLInfo.Location = new System.Drawing.Point(107, 9);
            this.lblDLLInfo.Name = "lblDLLInfo";
            this.lblDLLInfo.Size = new System.Drawing.Size(464, 13);
            this.lblDLLInfo.TabIndex = 1;
            this.lblDLLInfo.Text = "(DLL Info)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "DLL:";
            // 
            // cbInterface
            // 
            this.cbInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterface.FormattingEnabled = true;
            this.cbInterface.Location = new System.Drawing.Point(107, 30);
            this.cbInterface.Name = "cbInterface";
            this.cbInterface.Size = new System.Drawing.Size(137, 21);
            this.cbInterface.TabIndex = 3;
            this.cbInterface.SelectedIndexChanged += new System.EventHandler(this.CbInterfaceSelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Interface:";
            // 
            // cbInterfaceOption
            // 
            this.cbInterfaceOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterfaceOption.FormattingEnabled = true;
            this.cbInterfaceOption.Location = new System.Drawing.Point(250, 30);
            this.cbInterfaceOption.Name = "cbInterfaceOption";
            this.cbInterfaceOption.Size = new System.Drawing.Size(137, 21);
            this.cbInterfaceOption.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Config:";
            // 
            // OFD
            // 
            this.OFD.FileName = "openFileDialog1";
            this.OFD.Filter = "INI files (*.ini)|*.ini|ISC files (*.isc)|*.isc|All files (*.*)|*.*";
            // 
            // tbConfig
            // 
            this.tbConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConfig.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbConfig.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbConfig.Location = new System.Drawing.Point(107, 57);
            this.tbConfig.Name = "tbConfig";
            this.tbConfig.Size = new System.Drawing.Size(400, 20);
            this.tbConfig.TabIndex = 6;
            this.tbConfig.TextChanged += new System.EventHandler(this.TbConfigTextChanged);
            // 
            // tbCalib
            // 
            this.tbCalib.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCalib.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbCalib.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbCalib.Location = new System.Drawing.Point(107, 83);
            this.tbCalib.Name = "tbCalib";
            this.tbCalib.Size = new System.Drawing.Size(430, 20);
            this.tbCalib.TabIndex = 10;
            this.tbCalib.TextChanged += new System.EventHandler(this.TbConfigTextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Calibration:";
            // 
            // btnInit
            // 
            this.btnInit.Enabled = false;
            this.btnInit.Location = new System.Drawing.Point(12, 135);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 14;
            this.btnInit.Text = "Init";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.BtnInitClick);
            // 
            // btnDarkCurrent
            // 
            this.btnDarkCurrent.Enabled = false;
            this.btnDarkCurrent.Location = new System.Drawing.Point(12, 193);
            this.btnDarkCurrent.Name = "btnDarkCurrent";
            this.btnDarkCurrent.Size = new System.Drawing.Size(75, 23);
            this.btnDarkCurrent.TabIndex = 16;
            this.btnDarkCurrent.Text = "Dark current";
            this.btnDarkCurrent.UseVisualStyleBackColor = true;
            this.btnDarkCurrent.Click += new System.EventHandler(this.BtnDarkCurrentClick);
            // 
            // btnMeasure
            // 
            this.btnMeasure.Enabled = false;
            this.btnMeasure.Location = new System.Drawing.Point(12, 222);
            this.btnMeasure.Name = "btnMeasure";
            this.btnMeasure.Size = new System.Drawing.Size(75, 23);
            this.btnMeasure.TabIndex = 17;
            this.btnMeasure.Text = "Measure";
            this.btnMeasure.UseVisualStyleBackColor = true;
            this.btnMeasure.Click += new System.EventHandler(this.BtnMeasureClick);
            // 
            // btnDone
            // 
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point(12, 251);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 18;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.BtnDoneClick);
            // 
            // btnBrowseForConfig
            // 
            this.btnBrowseForConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseForConfig.Location = new System.Drawing.Point(543, 55);
            this.btnBrowseForConfig.Name = "btnBrowseForConfig";
            this.btnBrowseForConfig.Size = new System.Drawing.Size(28, 23);
            this.btnBrowseForConfig.TabIndex = 8;
            this.btnBrowseForConfig.Text = "...";
            this.btnBrowseForConfig.UseVisualStyleBackColor = true;
            this.btnBrowseForConfig.Click += new System.EventHandler(this.BtnBrowseForConfigClick);
            // 
            // btnBrowseForCalib
            // 
            this.btnBrowseForCalib.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseForCalib.Location = new System.Drawing.Point(543, 81);
            this.btnBrowseForCalib.Name = "btnBrowseForCalib";
            this.btnBrowseForCalib.Size = new System.Drawing.Size(28, 23);
            this.btnBrowseForCalib.TabIndex = 11;
            this.btnBrowseForCalib.Text = "...";
            this.btnBrowseForCalib.UseVisualStyleBackColor = true;
            this.btnBrowseForCalib.Click += new System.EventHandler(this.BtnBrowseForCalibClick);
            // 
            // pgResults
            // 
            this.pgResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pgResults.HelpVisible = false;
            this.pgResults.Location = new System.Drawing.Point(107, 135);
            this.pgResults.Name = "pgResults";
            this.pgResults.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgResults.Size = new System.Drawing.Size(179, 261);
            this.pgResults.TabIndex = 19;
            this.pgResults.ToolbarVisible = false;
            // 
            // pbSpectrum
            // 
            this.pbSpectrum.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSpectrum.Location = new System.Drawing.Point(292, 135);
            this.pbSpectrum.Name = "pbSpectrum";
            this.pbSpectrum.Size = new System.Drawing.Size(279, 261);
            this.pbSpectrum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSpectrum.TabIndex = 16;
            this.pbSpectrum.TabStop = false;
            this.pbSpectrum.Paint += new System.Windows.Forms.PaintEventHandler(this.PbSpectrumPaint);
            // 
            // udIntTime
            // 
            this.udIntTime.Location = new System.Drawing.Point(107, 109);
            this.udIntTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.udIntTime.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.udIntTime.Name = "udIntTime";
            this.udIntTime.Size = new System.Drawing.Size(80, 20);
            this.udIntTime.TabIndex = 13;
            this.udIntTime.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Int. time [ms]:";
            // 
            // btnGetTOPInfo
            // 
            this.btnGetTOPInfo.Enabled = false;
            this.btnGetTOPInfo.Location = new System.Drawing.Point(12, 164);
            this.btnGetTOPInfo.Name = "btnGetTOPInfo";
            this.btnGetTOPInfo.Size = new System.Drawing.Size(75, 23);
            this.btnGetTOPInfo.TabIndex = 15;
            this.btnGetTOPInfo.Text = "TOP info...";
            this.btnGetTOPInfo.UseVisualStyleBackColor = true;
            this.btnGetTOPInfo.Click += new System.EventHandler(this.BtnGetTOPInfoClick);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.Image")));
            this.btnDownload.Location = new System.Drawing.Point(513, 55);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(28, 23);
            this.btnDownload.TabIndex = 7;
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 408);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnGetTOPInfo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.udIntTime);
            this.Controls.Add(this.pbSpectrum);
            this.Controls.Add(this.pgResults);
            this.Controls.Add(this.btnBrowseForCalib);
            this.Controls.Add(this.btnBrowseForConfig);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnMeasure);
            this.Controls.Add(this.btnDarkCurrent);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.tbCalib);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbConfig);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbInterfaceOption);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbInterface);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDLLInfo);
            this.MinimumSize = new System.Drawing.Size(436, 264);
            this.Name = "MainForm";
            this.Text = "CAS4 DLL C# Demo Application";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormFormClosed);
            this.Shown += new System.EventHandler(this.MainFormShown);
            ((System.ComponentModel.ISupportInitialize)(this.pbSpectrum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udIntTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		private System.Windows.Forms.Button btnGetTOPInfo;
		private System.Windows.Forms.NumericUpDown udIntTime;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.PictureBox pbSpectrum;
		private System.Windows.Forms.PropertyGrid pgResults;
		private System.Windows.Forms.TextBox tbConfig;
		private System.Windows.Forms.TextBox tbCalib;
		private System.Windows.Forms.OpenFileDialog OFD;
		private System.Windows.Forms.Button btnBrowseForCalib;
		private System.Windows.Forms.Button btnBrowseForConfig;
		private System.Windows.Forms.Button btnDone;
		private System.Windows.Forms.Button btnMeasure;
		private System.Windows.Forms.Button btnDarkCurrent;
		private System.Windows.Forms.Button btnInit;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbInterfaceOption;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbInterface;
		private System.Windows.Forms.Label lblDLLInfo;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDownload;
	}
}
