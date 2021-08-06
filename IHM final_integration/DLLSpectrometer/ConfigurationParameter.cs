using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLSpectrometer
{
    // TODO: Utiliser plutot un record
    public class ConfigurationParameter
    {
        private double? integrationTimeFixed;
        private double? aRMinLevel;
        private double? aRMaxLevel;
        private double? aRMaxIntegrationTime;
        private double? aRAverages;

        public double? IntegrationTimeFixed
        {
            get { return this.integrationTimeFixed; }
            set { this.integrationTimeFixed = value; }
        }

        public double? ARMinLevel { get => aRMinLevel; set => aRMinLevel = value; }
        public double? ARMaxLevel { get => aRMaxLevel; set => aRMaxLevel = value; }
        public double? ARMaxIntegrationTime { get => aRMaxIntegrationTime; set => aRMaxIntegrationTime = value; }
        public double? ARAverages { get => aRAverages; set => aRAverages = value; }
    }
    
}
