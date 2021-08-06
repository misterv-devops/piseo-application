using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLSpectrometer
{
    public class ResultSpectro
    {
        private string cablibrationUnit;
        private double maxADCValue;
        private string radUnit;
        private double radValue;
        private string photUnit;
        private double photValue;

        public string CalibrationUnit
        {
            get { return this.cablibrationUnit; }
            set { this.cablibrationUnit = value; }
        }

        public double MaxADCValue
        {
            get { return this.maxADCValue; }
            set { this.maxADCValue = value; }
        }

        public string RadUnit
        {
            get { return this.radUnit; }
            set { this.radUnit = value; }
        }

        public double RadValue
        {
            get { return this.radValue; }
            set { this.radValue = value; }
        }

        public string PhotUnit
        {
            get { return this.photUnit; }
            set { this.photUnit = value; }
        }

        public double PhotValue
        {
            get { return this.photValue; }
            set { this.photValue = value; }
        }

        public ResultSpectro()
        {

        }
    }
}
