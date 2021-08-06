/*
 * Created with SharpDevelop.
 * User: SMO
 * Date: 08.06.2010
 * Time: 17:08
 * 
 */
//enable this define to use TLCI calculation DLL
//BUT: you will need to add InstrumentSystems.CAS4.TLCIDLL.cs to the project!
//#define TLCI
//same as above: enable this define to use the TM30 calculation DLL, but add the TM30DLL.cs file o the project
//#define TM30

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using InstrumentSystems.CAS4;
using System.IO;

namespace CAS4DLLTest
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			casID = -1;
		}
		
		private StringBuilder sb = new StringBuilder(256);
		private int casID;
		private double[] spectrum = new double[0];
		private double[] lambdas = new double[0];
		private double spectrumMax = 0;
		
		private class MyComboBoxItem{
			public string Name { get; set; }
			public int ID { get; set; }
			
			public MyComboBoxItem (string AName, int AID) {
				Name = AName;
				ID = AID;
			}
		}
		
		private class MySpectrumResults{
			public string CalibrationUnit { get; set; }
			public int MaxADCValue { get; set; }
			public double DarkCurrentAge { get; set; }
			public double RadInt { get; set; }
			public string RadIntUnit { get; set; }
			public double PhotInt { get; set; }
			public string PhotIntUnit { get; set; }
			public double Centroid { get; set; }
			public double SecondMoment { get; set; }
			public double ThirdMoment { get; set; }
#if TLCI
            public double TLCI_Qa { get; set; }
            public double TLCI_dEa { get; set; }
            public double TLCI_dCCT { get; set; }
            public double TLCI_CCT { get; set; }
#endif
#if TM30
            public double TM30_Rf { get; set; }
            public double TM30_Rg { get; set; }
#endif
        }
		
		public int SelectedInterface { get {
				return (cbInterface.Items[cbInterface.SelectedIndex] as MyComboBoxItem).ID;
			}
		}
			
		public int SelectedInterfaceOption { get {
				if (cbInterfaceOption.SelectedIndex >= 0) {
					return (cbInterfaceOption.Items[cbInterfaceOption.SelectedIndex] as MyComboBoxItem).ID;
				} else {
					return 0;
				}
			}
		}

#if TLCI
        public int CheckTLCIError(int AError)
        {
            if (AError < TLCIDLL.tiErrorNoError)
            {
                TLCIDLL.tlciGetString(AError, 0, 0, sb, sb.Capacity);
                throw new Exception(string.Format("CAS TLCI DLL error ({0}): {1}", AError, sb.ToString()));
            }
            return AError;
        }

        public double GetTLCIFloat(int AWhat, int AIndex, int AExtra)
        {
            double TempFloat = 0;
            CheckTLCIError(TLCIDLL.tlciGetFloat(AWhat, AIndex, AExtra, out TempFloat));
            return TempFloat;
        }
#endif
#if TM30
        public int CheckTM30Error(int AError)
        {
            if (AError < TM30DLL.tiErrorNoError)
            {
                TM30DLL.tm30GetString(AError, 0, 0, sb, sb.Capacity);
                throw new Exception(string.Format("CAS TM30 DLL error ({0}): {1}", AError, sb.ToString()));
            }
            return AError;
        }

        public double GetTM30Float(int AWhat, int AIndex, int AExtra)
        {
            double TempFloat = 0;
            CheckTM30Error(TM30DLL.tm30GetFloat(AWhat, AIndex, AExtra, out TempFloat));
            return TempFloat;
        }
#endif
        public int CheckCASError(int AError)
        {
			if (AError < CAS4DLL.ErrorNoError) {
				CAS4DLL.casGetErrorMessage(AError, sb, sb.Capacity);
				throw new Exception(string.Format("CAS DLL error ({0}): {1}", AError, sb.ToString()));
			}
			return AError;
		}

		public void CheckCASError() {
			CheckCASError(CAS4DLL.casGetError(casID));
		}
		
		private void CasDoneWhenNeeded()
		{
			if (casID >= 0) CAS4DLL.casDoneDevice(casID);
			casID = -1;
		}

		private void FillInterfaceCombo ()
		{
			//fill the interface combo box
			int theIdx;
			int selectIdx;
			
			selectIdx = 0;
			cbInterface.BeginUpdate();
			try {
				cbInterface.Items.Clear();
				cbInterface.DisplayMember="Name";
				theIdx = 0;
				for (int i=0; i < CAS4DLL.casGetDeviceTypes(); i++ ) {
					CAS4DLL.casGetDeviceTypeName(i, sb, sb.Capacity);
					//skip interfaces with empty names
					if (sb.Length>0) {
						theIdx = cbInterface.Items.Add(new MyComboBoxItem(sb.ToString(), i));
						//if this is the demo interface, then store the index so it can be selected later
						if (i == CAS4DLL.InterfaceTest) selectIdx = theIdx;
					}
				}
			} finally {
				cbInterface.EndUpdate();
			}
				
			cbInterface.SelectedIndex = selectIdx;
		}
			
		private void MeasureDarkCurrent () {
			
			//this method measures the dark current
			//it does not cater for devices without a shutter
			CAS4DLL.casSetShutter(casID, 1);
			CheckCASError();
			try
			{
				CAS4DLL.casMeasureDarkCurrent(casID);
				CheckCASError();
			}
			finally {
				//if the measure failed, at least make sure the shutter is opened again
				CAS4DLL.casSetShutter(casID, 0);
			}
			CheckCASError();
		}
		
		private void SetParameter () {
			CAS4DLL.casSetMeasurementParameter(casID, CAS4DLL.mpidIntegrationTime, (double)udIntTime.Value);

			//set other parameter like mpidNewDensityFilter or options like AutoRange etc.
		}
		private void Measure () {
			
			//this method measures a spectrum
			CAS4DLL.casMeasure(casID);
			CheckCASError();
		}
			
		private void GetResults () {
			//create new object which holds a few results; shown in property grid
			MySpectrumResults res = new MySpectrumResults();

			CheckCASError(CAS4DLL.casGetDeviceParameterString(casID, CAS4DLL.dpidCalibrationUnit, sb, sb.Capacity));
			res.CalibrationUnit = sb.ToString();
		
			res.MaxADCValue = (int)Math.Round(CAS4DLL.casGetMeasurementParameter(casID, CAS4DLL.mpidMaxADCValue));
			CheckCASError();
			res.DarkCurrentAge = Math.Round(CAS4DLL.casGetMeasurementParameter(casID, CAS4DLL.mpidLastDCAge)/60000, 1);
			CheckCASError();
			
			//now we need actual calculated results, so we need to call casColorMetric
			//there may be other calls necessary for other results (CRI, Width50 etc.)
			CheckCASError(CAS4DLL.casColorMetric(casID));

			double tempFloat;
			tempFloat = 0;
			CAS4DLL.casGetRadInt(casID, out tempFloat, sb, sb.Capacity);
			CheckCASError();
			res.RadInt = tempFloat;
			res.RadIntUnit = sb.ToString();

			CAS4DLL.casGetPhotInt(casID, out tempFloat, sb, sb.Capacity);
			CheckCASError();
			res.PhotInt = tempFloat;
			res.PhotIntUnit = sb.ToString();
			
			res.Centroid = Math.Round(CAS4DLL.casGetCentroid(casID), 2);
			CheckCASError();
			
			res.SecondMoment = Math.Round(GetMoment(2, res.Centroid), 2);
			res.ThirdMoment = Math.Round(GetMoment(3, res.Centroid), 2);

#if TLCI
            CheckTLCIError(TLCIDLL.tlciPerform(TLCIDLL.tiwCalculateTLCI, casID, 0));
            res.TLCI_Qa = GetTLCIFloat(TLCIDLL.tiwTLCIQ, 0, 0);
            res.TLCI_dEa = GetTLCIFloat(TLCIDLL.tiwTLCIdE, 0, 0);
            res.TLCI_dCCT = GetTLCIFloat(TLCIDLL.tiwTLCIdCCT, 0, 0);
            res.TLCI_CCT = GetTLCIFloat(TLCIDLL.tiwTLCICCT, 0, 0);
#endif
#if TM30
            CheckTM30Error(TM30DLL.tm30Perform(TM30DLL.tiwCalculateTM30, casID, 0));
            res.TM30_Rf = GetTM30Float(TM30DLL.tiwTM30FidelityIndex, 0, 0);
            res.TM30_Rg = GetTM30Float(TM30DLL.tiwTM30GamutIndex, 0, 0);
#endif
            pgResults.SelectedObject = res;
		}

		private void GetSpectrum () {
			int pix;
			
			pix = (int)Math.Round(CAS4DLL.casGetDeviceParameter(casID, CAS4DLL.dpidVisiblePixels));
			CheckCASError(pix);

			//allocate the arrays for intensity values and lambdas
			spectrum = new double[pix];
			lambdas = new Double[pix];
			spectrumMax = -1E37;

			pix = (int)Math.Round(CAS4DLL.casGetDeviceParameter(casID, CAS4DLL.dpidDeadPixels));
			CheckCASError(pix);
			
			for (int i = 0; i < spectrum.Length; i++) {
				//get the intensities; don't forget about skipping dead pixels
				spectrum[i] = (float)CAS4DLL.casGetData(casID, i+pix);
				spectrumMax = Math.Max(spectrumMax, spectrum[i]);

				//get the wavelengths; don't forget about skipping dead pixels
				lambdas[i] = (float)CAS4DLL.casGetXArray(casID, i+pix);
			}
		}
		
		double GetMoment(int AMoment, double ACentroid) {
			//calculate second/third etc. moment
			//use spectrum data from lambda and spectrum arrays
			//for simplicity we use the complete spectral range

			double RadInt = 0;
			double WeighedInt = 0;
			double DeltaLambda;
			
			for (int i = 0; i < spectrum.Length; i++) {
				//calculate Delta-Lambda; for last pixel use difference to previous
				//for all others, use difference to next pixel
				//so called DeltaD integration as used in CAS DLL
				if (i == spectrum.Length-1) {
					DeltaLambda = lambdas[i]-lambdas[i-1];
				} else {
					DeltaLambda = lambdas[i+1]-lambdas[i]; 
				}
				
				//add intensities multiplied by DeltaLambda -> radiometric integral for quotient
				RadInt += spectrum[i] * DeltaLambda;
				//add intensities weighed with distance do centroid and power of AMoment
				WeighedInt += spectrum[i] * Math.Pow(lambdas[i]-ACentroid, AMoment) * DeltaLambda;
			}
			//return quotient of weighed integral and radiometric integral, if possible
			if (!RadInt.Equals(0)) {
				return WeighedInt / RadInt;
			} else {
				return 0;
			}
		}
		void MainFormShown(object sender, EventArgs e)
		{
			string dllPath;
			
			CAS4DLL.casGetDLLFileName(sb, sb.Capacity);
			dllPath = sb.ToString();
			CAS4DLL.casGetDLLVersionNumber(sb, sb.Capacity);
			lblDLLInfo.Text = string.Format("{0}, Version {1}", dllPath, sb.ToString());
			
			FillInterfaceCombo();
		}

		void CbInterfaceSelectedIndexChanged(object sender, EventArgs e)
		{
			//fill the parameter combo box
			int iface;
			int option;
			
			iface = SelectedInterface;
			
			cbInterfaceOption.BeginUpdate();
			try {
				cbInterfaceOption.Items.Clear();
				cbInterfaceOption.DisplayMember="Name";
				for (int i=0; i < CAS4DLL.casGetDeviceTypeOptions(iface); i++ ) {
					option = CAS4DLL.casGetDeviceTypeOption(iface, i);
					CAS4DLL.casGetDeviceTypeOptionName(iface, i, sb, sb.Capacity);
					cbInterfaceOption.Items.Add(new MyComboBoxItem(sb.ToString(), option));
				}
			} finally {
				cbInterfaceOption.EndUpdate();
			}
			
			//only enable the option combo box if there's actually a choice to be made
			cbInterfaceOption.Enabled = (cbInterfaceOption.Items.Count > 1);
			if (cbInterfaceOption.Items.Count > 0) cbInterfaceOption.SelectedIndex = 0;
		}
		
		void BtnBrowseForConfigClick(object sender, EventArgs e)
		{
			OFD.FileName = tbConfig.Text;
			OFD.FilterIndex = 1;
			if (OFD.ShowDialog() == DialogResult.OK) {
				tbConfig.Text = OFD.FileName;
			}
		}
		
		void BtnBrowseForCalibClick(object sender, EventArgs e)
		{
			OFD.FileName = tbCalib.Text;
			OFD.FilterIndex = 2;
			if (OFD.ShowDialog() == DialogResult.OK) {
				tbCalib.Text = OFD.FileName;
			}
		}
		
		void TbConfigTextChanged(object sender, EventArgs e)
		{
			
			btnInit.Enabled = (File.Exists(tbConfig.Text) && File.Exists(tbCalib.Text));//&& (btnDone.Enabled == false));
		}
		
		void BtnInitClick(object sender, EventArgs e)
		{
			CasDoneWhenNeeded();
			try {
				casID = CAS4DLL.casCreateDeviceEx(SelectedInterface, SelectedInterfaceOption);
				CheckCASError(casID);
				
				//setup config, calib
				CAS4DLL.casSetDeviceParameterString(casID, CAS4DLL.dpidConfigFileName, tbConfig.Text);
				CAS4DLL.casSetDeviceParameterString(casID, CAS4DLL.dpidCalibFileName, tbCalib.Text);
				
				CheckCASError(CAS4DLL.casInitialize(casID, CAS4DLL.InitForced));
				
				udIntTime.Minimum = (decimal)Math.Round(CAS4DLL.casGetDeviceParameter(casID, CAS4DLL.dpidIntTimeMin));
				udIntTime.Maximum = (decimal)Math.Round(CAS4DLL.casGetDeviceParameter(casID, CAS4DLL.dpidIntTimeMax));

				btnGetTOPInfo.Enabled = true;
				btnDarkCurrent.Enabled = true;
				btnMeasure.Enabled = true;
				btnDone.Enabled = true;
	
				TbConfigTextChanged(this, null);
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				CasDoneWhenNeeded();
			}
		}
		
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			CasDoneWhenNeeded();
		}
		
		void BtnDoneClick(object sender, EventArgs e)
		{
			CasDoneWhenNeeded();

			btnGetTOPInfo.Enabled = false;
			btnDarkCurrent.Enabled = false;
			btnMeasure.Enabled = false;
			btnDone.Enabled = false;

			TbConfigTextChanged(this, null);
		}
		
		void BtnDarkCurrentClick(object sender, EventArgs e)
		{
			try
			{
				SetParameter();
				MeasureDarkCurrent();
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void BtnMeasureClick(object sender, EventArgs e)
		{
			try
			{
				SetParameter();
				
				//check if we need to set the filter
				if ((int)Math.Round(CAS4DLL.casGetDeviceParameter(casID, CAS4DLL.dpidNeedDensityFilterChange))!=0) {
					CAS4DLL.casSetMeasurementParameter(casID, CAS4DLL.mpidDensityFilter, 
					                                   CAS4DLL.casGetMeasurementParameter(casID, CAS4DLL.mpidNewDensityFilter));
					CheckCASError();
				}
				//check if we need a background measurement
				if ((int)Math.Round(CAS4DLL.casGetDeviceParameter(casID, CAS4DLL.dpidNeedDarkCurrent))!=0) {
					MeasureDarkCurrent();
				}
				Measure();
				//now get spectrum and results; spectrum first so it is available to calc 2nd/3rd moment
				GetSpectrum();
				GetResults();
				pbSpectrum.Invalidate();
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void PbSpectrumPaint(object sender, PaintEventArgs e)
		{
			//paint the spectrum; no scaling, just the complete wavelength range and from 0..Max

			//clear background, rc is used later for painting too
			Rectangle rc = pbSpectrum.ClientRectangle;
    		e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), rc);
    		
			int cnt = spectrum.Length;
			//skip if no spectrum or max=0
    		if ((cnt > 0) &! (spectrumMax.Equals(0)))
    		{
    			PointF[] points = new PointF[spectrum.Length];
    			//factor for multiplying spectrum pixel into rc-Pixel
    			double xFactor = (double)rc.Width/cnt;
    				
    			for (int i = 0; i<cnt; i++) {
    				points[i].X = rc.Left+i*(float)xFactor;
    				points[i].Y = (float)(rc.Bottom - (spectrum[i]/spectrumMax)*(rc.Height));
    			}
    			
    			e.Graphics.DrawLines(new Pen(Color.Blue, 1), points);
    		}
		}
		
		void BtnGetTOPInfoClick(object sender, EventArgs e)
		{
			try {
				int topType = CheckCASError((int)Math.Round(CAS4DLL.casGetDeviceParameter(casID, CAS4DLL.dpidTOPType)));
				if (topType != CAS4DLL.ttNone) {
					CheckCASError(CAS4DLL.casGetDeviceParameterString(casID, CAS4DLL.dpidTOPSerialEx, sb, sb.Capacity));
					MessageBox.Show(string.Format("TOP type {0}, Serial '{1}'", topType, sb.ToString()), "TOP info", MessageBoxButtons.OK, MessageBoxIcon.Information);
				} else {
					MessageBox.Show("No TOP configured", "TOP info", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnDownload_Click(object sender, EventArgs e)
        {
            //recreate CASID with selected interface and interfaceOption
			CasDoneWhenNeeded();
			try {
				casID = CAS4DLL.casCreateDeviceEx(SelectedInterface, SelectedInterfaceOption);
				CheckCASError(casID);

                //get the serial
                CAS4DLL.casGetSerialNumberEx(casID, CAS4DLL.casSerialDevice, sb, sb.Capacity);
                CheckCASError(casID);

                //build a subpath in Documents folder, append 'CASCalibDir'
                string DestPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CASCalibDir");

                //if we managed to get a serial number, append the serial number to the path
                //by using a separate directory per spectrometer, we can simply choose the first
                //INI/ISC file pair in that directory and avoid using calibrations
                //that won't work with the spectrometer at hand
                if (sb.Length > 0) 
                    DestPath = Path.Combine(DestPath, sb.ToString());

                //now make sure the directory exists
                Directory.CreateDirectory(DestPath);

                //and pass it as dpidGetFilesFromDevice, telling the CAS DLL to download
                //the files into this directory
                CheckCASError(CAS4DLL.casSetDeviceParameterString(casID, CAS4DLL.dpidGetFilesFromDevice, DestPath));

                string[] foundConfigFiles = Directory.GetFiles(DestPath, "*.ini");
                if (foundConfigFiles.Length == 0)
                    throw new Exception("No .ini file found in directory " + DestPath);

                tbConfig.Text = foundConfigFiles[0];
                tbCalib.Text = Path.ChangeExtension(foundConfigFiles[0], ".isc");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //when we're done, we release the CASID again, since a new Init is required with the 
            //updated config/calib file paths
            BtnDoneClick(this, null);
        }

	}
}
