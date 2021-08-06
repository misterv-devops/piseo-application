using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices; // Permet d'utiliser la "DLLImport".

namespace DLLSpectrometer
{
    public class Spectrometer
    {
        private class DLLSPECTROx64
        {
            private const string ModuleName = "CAS4x64.DLL";

            [DllImport(ModuleName)]
            public static extern int casGetError(int ADevice);

            [DllImport(ModuleName, CharSet = CharSet.Auto)]
            public static extern IntPtr casGetErrorMessage(int AError, StringBuilder ADest, int AMaxLen);

            [DllImport(ModuleName)]
            public static extern int casCreateDevice();

            [DllImport(ModuleName)]
            public static extern int casCreateDeviceEx(int AInterfaceType, int AInterfaceOption);

            [DllImport(ModuleName)]
            public static extern int casChangeDevice(int ADevice, int AInterfaceType, int AInterfaceOption);

            [DllImport(ModuleName)]
            public static extern int casDoneDevice(int ADevice);

            [DllImport(ModuleName)]
            public static extern int casAssignDeviceEx(int ASourceDevice, int ADestDevice, int AOption);

            [DllImport(ModuleName)]
            public static extern int casGetDeviceTypes();

            [DllImport(ModuleName, CharSet = CharSet.Auto)]
            public static extern IntPtr casGetDeviceTypeName(int AInterfaceType, StringBuilder ADest, int AMaxLen);

            [DllImport(ModuleName)]
            public static extern int casGetDeviceTypeOptions(int AInterfaceType);

            [DllImport(ModuleName)]
            public static extern int casGetDeviceTypeOption(int AInterfaceType, int AIndex);

            [DllImport(ModuleName, CharSet = CharSet.Auto)]
            public static extern IntPtr casGetDeviceTypeOptionName(int AInterfaceType, int AInterfaceOptionIndex, StringBuilder ADest, int AMaxLen);

            [DllImport(ModuleName)]
            public static extern int casInitialize(int ADevice, int Perform);

            [DllImport(ModuleName)]
            public static extern double casGetDeviceParameter(int ADevice, int AWhat);

            [DllImport(ModuleName)]
            public static extern int casSetDeviceParameter(int ADevice, int AWhat, double AValue);

            [DllImport(ModuleName, CharSet = CharSet.Auto)]
            public static extern int casGetDeviceParameterString(int ADevice, int AWhat, StringBuilder ADest, int AMaxLen);

            [DllImport(ModuleName, CharSet = CharSet.Auto)]
            public static extern int casSetDeviceParameterString(int ADevice, int AWhat, string AValue);

            [DllImport(ModuleName, CharSet = CharSet.Auto)]
            public static extern int casGetSerialNumberEx(int ADevice, int AWhat, StringBuilder ADest, int AMaxLen);

            [DllImport(ModuleName)]
            public static extern int casGetOptions(int ADevice);

            [DllImport(ModuleName)]
            public static extern void casSetOptionsOnOff(int ADevice, int AOptions, int AOnOff);

            [DllImport(ModuleName)]
            public static extern void casSetOptions(int ADevice, int AOptions);

            [DllImport(ModuleName)]
            public static extern int casMeasure(int ADevice);

            [DllImport(ModuleName)]
            public static extern int casStart(int ADevice);

            [DllImport(ModuleName)]
            public static extern double casGetMeasurementParameter(int ADevice, int AWhat);

            [DllImport(ModuleName)]
            public static extern int casSetMeasurementParameter(int ADevice, int AWhat, double AValue);

            [DllImport(ModuleName)]
            public static extern int casColorMetric(int ADevice);


            [DllImport(ModuleName, CharSet = CharSet.Auto)]
            public static extern void casGetRadInt(int ADevice, out double ARadInt, StringBuilder AUnit, int AUnitMaxLen);

            [DllImport(ModuleName, CharSet = CharSet.Auto)]
            public static extern void casGetPhotInt(int ADevice, out double APhotInt, StringBuilder AUnit, int AUnitMaxLen);

            [DllImport(ModuleName)]
            public static extern double casGetWidth(int ADevice);

            [DllImport(ModuleName)]
            public static extern double casGetWidthEx(int ADevice, int AWhat);

            [DllImport(ModuleName)]
            public static extern int casPerformAction(int ADevice, int AID);

            [DllImport(ModuleName)]
            public static extern int casPerformActionEx(int ADevice, int AActionID, int AParam1, int AParam2, IntPtr AParam3);

            [DllImport(ModuleName)]
            public static extern void casSetShutter(int ADevice, int OnOff);

            [DllImport(ModuleName)]
            public static extern int casMeasureDarkCurrent(int ADevice);

           
        }


        private int spectroID = -1;
        private StringBuilder sbAllResult = new StringBuilder(254);
        private StringBuilder sbError = new StringBuilder(254);
        private double radValue;
        private double photValue;

       
        public Spectrometer(string pInterface, string pPathOfConfigFile, string pPathOfCalibFile, string pPathOfTransmissionFile) //Constructeur
        {
            int interfaceSelected = -1;
            int interfaceOptionSelected = -1;

            // Utiliser une enum
            //STEP 1 => DEVICE 
            interfaceSelected = (int)SelectInterface(pInterface);

            this.spectroID = DLLSPECTROx64.casCreateDeviceEx(interfaceSelected, 051214420);
            CheckError(this.spectroID);
            //STEP 2 => CONFIG && INIT
            loadFilesAndInitialize(pPathOfConfigFile, pPathOfCalibFile, pPathOfTransmissionFile);
        }

        const int dpidTriggerCapabilities = 118;
        const int tcoShowACQState = 0x00000020;
        const int tcoCanTrigger = 0x00000001;

        public Spectrometer(string pInterface, string pPathOfConfigFile, string pPathOfCalibFile, string pPathOfTransmissionFile, bool pTrigger) //Constructeur
        {
            int interfaceSelected = -1;
            int interfaceOptionSelected = -1;

            // Utiliser une enum
            //STEP 1 => DEVICE 
            interfaceSelected = (int)SelectInterface(pInterface);

            this.spectroID = DLLSPECTROx64.casCreateDeviceEx(interfaceSelected, 051214420);
            CheckError(this.spectroID);
            //STEP 2 => CONFIG && INIT
            loadFilesAndInitialize(pPathOfConfigFile, pPathOfCalibFile, pPathOfTransmissionFile);

            //STEP 3 Verify DPID TRIGGER CAPALITIES
            string triggerState;
            int loCheckCapabilities = (int)DLLSPECTROx64.casGetDeviceParameter(this.spectroID, dpidTriggerCapabilities);
            if ((loCheckCapabilities & tcoCanTrigger) != 0 && pTrigger == true)
            {
                triggerState = "Le spectromètre peut être déclenché de manière externe";
                Console.WriteLine(triggerState);
            }
            else
            {
                triggerState = "Le spectromètre ne peut pas être déclenché de manière externe";
                Console.WriteLine(triggerState);
            }

        }


        private Interface SelectInterface(string pInterfaceSelect)
        {
            switch (pInterfaceSelect)
            {
                case "InterfaceISA":
                    return Interface.InterfaceISA;
                case "InterfacePCI":
                    return Interface.InterfacePCI;
                case "InterfaceTest":    
                    return Interface.InterfaceTest;
                case "InterfaceUSB":
                    return Interface.InterfaceUSB;
                case "InterfacePCIe":
                    return Interface.InterfacePCIe;
                case "InterfaceEthernet":
                    return Interface.InterfaceEthernet;
                default:
                    throw new ArgumentException("L'interface n'existe pas");
            }
          
        }


        private const int dpidTransmissionFileName = 112; ///<Returns or sets the path of thetransmission correction file (typical extension .isa); see <see cref="coUseTransmission"/>. Note that an invalid filename will not raise an error. The file is loaded during <see cref="casInitialize"/> or when <see cref="paUpdateCompleteCalibration"/> is called.
        private const int dpidConfigFileName = 113; ///<Returns or sets the path of the currently used configuration file (extension .ini). This file is required and loaded during <see cref="casInitialize"/> or <see cref="errCasNoConfig"/> will occur.
        private const int dpidCalibFileName = 114; ///<Returns or sets the path of the currently used calibration file (extension .isc). This file is required and loaded during <see cref="casInitialize"/> or <see cref="ErrorNoCalibration"/> will occur.
        public const int coUseTransmission = 0x00000080;
        private void loadFilesAndInitialize(string pPathOfConfigFile, string pPathOfCalibFile, string pPathOfTransmissionFile)
        {
            DLLSPECTROx64.casSetDeviceParameterString(this.spectroID, dpidConfigFileName, pPathOfConfigFile); // Charger le fichier de configuration.
            DLLSPECTROx64.casSetDeviceParameterString(this.spectroID, dpidCalibFileName, pPathOfCalibFile); // Charger le fichier de calibration.
            DLLSPECTROx64.casSetDeviceParameterString(this.spectroID, dpidTransmissionFileName, pPathOfTransmissionFile); // Charger le fichier de transmission.
            DLLSPECTROx64.casSetOptionsOnOff(this.spectroID, coUseTransmission,1); // Activer le fichier de transmission.
            initialize();
        }

        // TODO: Utiliser une enum
        private void initialize()
        {
            int loErrorInit = -1;
            loErrorInit = DLLSPECTROx64.casInitialize(this.spectroID, (int)InitPerform.InitForced);
            CheckError(loErrorInit);
        }



        public const int mpidAutoRangeMinLevel = 20;
        public const int mpidAutoRangeMaxLevel = 49;
        public const int mpidAutoRangeMaxIntTime = 19;
        public const int mpidAverages = 02;
        public const int coAutorangeMeasurement = 0x00000100;

        // TODO: Exposer une seule méthode 'configure'
        public const int mpidIntegrationTime = 01;
        public void setIntegrationTime(ConfigurationParameter pConfig) // STEP 4
        {
            if (pConfig.IntegrationTimeFixed.HasValue)
            {
                DLLSPECTROx64.casSetMeasurementParameter(this.spectroID, mpidIntegrationTime, pConfig.IntegrationTimeFixed.Value);//Définir le temps d'intégration fixe.
            }
            else if (pConfig.ARMinLevel.HasValue && pConfig.ARMaxLevel.HasValue && pConfig.ARMaxIntegrationTime.HasValue && pConfig.ARAverages.HasValue) 
            {
                DLLSPECTROx64.casSetMeasurementParameter(this.spectroID, mpidAutoRangeMinLevel, pConfig.ARMinLevel.Value);
                DLLSPECTROx64.casSetMeasurementParameter(this.spectroID, mpidAutoRangeMaxLevel, pConfig.ARMaxLevel.Value);
                DLLSPECTROx64.casSetMeasurementParameter(this.spectroID, mpidAutoRangeMaxIntTime, pConfig.ARMaxIntegrationTime.Value);
                DLLSPECTROx64.casSetMeasurementParameter(this.spectroID, mpidAverages, pConfig.ARAverages.Value);
                DLLSPECTROx64.casSetOptionsOnOff(this.spectroID, coAutorangeMeasurement, 1);
            }
        }

        public const int dpidNeedDensityFilterChange = 131;
        public const int mpidDensityFilter = 21;
        public const int mpidNewDensityFilter = 23;
        private void densityFilter()
        {
            if((int)DLLSPECTROx64.casGetDeviceParameter(this.spectroID,dpidNeedDensityFilterChange) != 0)
            {
                DLLSPECTROx64.casSetMeasurementParameter(this.spectroID, mpidDensityFilter, DLLSPECTROx64.casGetMeasurementParameter(this.spectroID, mpidNewDensityFilter));
            }
            
        }

        public const int dpidNeedDarkCurrent = 130;
        private void darkCurrentMeasure()
        {
            if ((int)DLLSPECTROx64.casGetDeviceParameter(this.spectroID, dpidNeedDarkCurrent) != 0)
            {
                DLLSPECTROx64.casMeasureDarkCurrent(this.spectroID);
            }
           
        }

        private void startMeasure()
        {
            DLLSPECTROx64.casMeasure(this.spectroID);
            DLLSPECTROx64.casColorMetric(this.spectroID);
        }

        public const int paPrepareMeasurement = 1;
        public const int mpidTriggerSource = 14;
        public const int trgFlipFlop = 3;
        const int dpidTriggerDelayTimeMin = 159;
        const int dpidTriggerDelayTimeMax = 126;
        const int mpidTriggerDelayTime = 03;
        const int mpidTriggerTimeout = 04;
        const int tcoTriggerOnlyWhenReady = 0x00000004;
        const int toAcceptOnlyWhenReady = 1;
        public const int mpidTriggerOptions = 33;

        public void syncTriggerPower(double pTimeOut)
        {
            //int checkTriggerOption;
            
            //checkTriggerOption = (int)DLLSPECTROx64.casGetMeasurementParameter(this.spectroID, mpidTriggerOptions);

            DLLSPECTROx64.casSetMeasurementParameter(this.spectroID, mpidTriggerSource, trgFlipFlop);
            
            /* if((checkTriggerOption & tcoTriggerOnlyWhenReady) != 0) {*/DLLSPECTROx64.casSetMeasurementParameter(this.spectroID, mpidTriggerOptions, toAcceptOnlyWhenReady);/*}*/

            int loErrorTimeout = DLLSPECTROx64.casSetMeasurementParameter(this.spectroID, mpidTriggerTimeout, pTimeOut);
            CheckError(loErrorTimeout);

        }

        private void setTriggerDelayTime(double pTriggerDelayTime)
        {
            double loTriggerDelayTimeMin;
            double loTriggerDelayTimeMax;

            loTriggerDelayTimeMin = DLLSPECTROx64.casGetDeviceParameter(this.spectroID, dpidTriggerDelayTimeMin);
            loTriggerDelayTimeMax = DLLSPECTROx64.casGetDeviceParameter(this.spectroID, dpidTriggerDelayTimeMax);

            //if ((pTriggerDelayTime >= loTriggerDelayTimeMin) && (pTriggerDelayTime <= loTriggerDelayTimeMax))
            //{
                DLLSPECTROx64.casSetMeasurementParameter(this.spectroID, mpidTriggerDelayTime, pTriggerDelayTime);
            //}
               
        }

        private void SetPreMeasurement()
        {
            DLLSPECTROx64.casPerformActionEx(this.spectroID, paPrepareMeasurement, 0, 0, (IntPtr)0);
        }


        public const int dpidCalibrationUnit = 115;
        public const int mpidMaxADCValue = 11;

        public void MeasureWithoutTrigger()
        {
                densityFilter();
                darkCurrentMeasure();
                SetPreMeasurement();
                startMeasure();
                
                
        }

        public void MeasureWithTrigger(double pTimeOut)
        {
            densityFilter();
            darkCurrentMeasure();
            SetPreMeasurement();
            syncTriggerPower(pTimeOut);
            startMeasure();
           

        }

        public ResultSpectro GetResultSpectro()
        {

            ResultSpectro pMySpectrumResult = new ResultSpectro();

            DLLSPECTROx64.casGetDeviceParameterString(this.spectroID, dpidCalibrationUnit, this.sbAllResult, this.sbAllResult.Capacity);
            pMySpectrumResult.CalibrationUnit = this.sbAllResult.ToString();

            pMySpectrumResult.MaxADCValue = DLLSPECTROx64.casGetMeasurementParameter(this.spectroID, mpidMaxADCValue);

            DLLSPECTROx64.casGetRadInt(this.spectroID, out this.radValue, this.sbAllResult, this.sbAllResult.Capacity);
            pMySpectrumResult.RadUnit = this.sbAllResult.ToString();
            pMySpectrumResult.RadValue = this.radValue;

            DLLSPECTROx64.casGetPhotInt(this.spectroID, out this.photValue, this.sbAllResult, this.sbAllResult.Capacity);
            pMySpectrumResult.PhotUnit = this.sbAllResult.ToString();
            pMySpectrumResult.PhotValue = this.photValue;

            return pMySpectrumResult;
        }


        private void CheckError(int pError)
        {
            if(pError < 0)
            {
                DLLSPECTROx64.casGetErrorMessage(pError, sbError, sbError.Capacity);
                throw new Exception(string.Format("CAS DLL error ({0}): {1}", pError, sbError.ToString()));
            }
        }
 

    }
}
