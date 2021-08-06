unit CAS4DLL;

interface

const
  ErrorNoError                     =   0;
  ErrorUnknown                     =  -1;
  ErrorTimeoutRWSNoData            =  -2;
  ErrorInvalidDeviceType           =  -3;
  ErrorAcquisition                 =  -4;
  ErrorAccuDataStream              =  -5;
  ErrorPrivilege                   =  -6;
  ErrorFIFOOverflow                =  -7;
  ErrorTimeoutEOSScan              =  -8; //ISA only
  ErrorTimeoutEOSDummyScan         =  -9;
  ErrorFifoFull                    = -10; //USB
  ErrorPixel1FinalCheck            = -11; //test for pixel1 on n+1. failed
  ErrorCCDTemperatureFail          = -13;
  ErrorAdrControl                  = -14;
  ErrorFloat                       = -15; //floating point error
  ErrorTriggerTimeout              = -16;
  ErrorAbortWaitTrigger            = -17;
  ErrorDarkArray                   = -18;
  ErrorNoCalibration               = -19;
  ErrorInterfaceVersion            = -20;
  ErrorCRI                         = -21;
  ErrorNoMultiTrack                = -25;
  ErrorInvalidTrack                = -26;
  ErrorDetectPixel                 = -31;
  ErrorSelectParamSet              = -32;
  ErrorI2CInit                     = -35;
  ErrorI2CBusy                     = -36;
  ErrorI2CNotAck                   = -37;
  ErrorI2CRelease                  = -38;
  ErrorI2CTimeOut                  = -39;
  ErrorI2CTransmission             = -40;
  ErrorI2CController               = -41; 
  ErrorDataNotAck                  = -42;
  ErrorNoExternalADC               = -52;
  ErrorShutterPos                  = -53;
  ErrorFilterPos                   = -54;
  ErrorConfigSerialMismatch        = -55;
  ErrorCalibSerialMismatch         = -56;
  ErrorInvalidParameter            = -57;
  ErrorGetFilterPos                = -58;
  ErrorParamOutOfRange             = -59;
  ErrorDeviceFileChecksum          = -60;
  ErrorInvalidEEPromType           = -61;
  ErrorDeviceFileTooLarge          = -62;
  ErrorNoCommunication             = -63;
  ErrorNoFilesOnIdentKey           = -64;
  ErrorExtraCalibFileInvalid       = -66;
  ErrorFeatureNotSupported         = -68;
  ErrorConfigUpToDate              = -70;
  ErrorCommunicationTimeout        = -73;
  ErrorTransmissionSerialMismatch  = -74;

  errCASOK                 = ErrorNoError;

  errCASError              = -1000;
  errCasNoConfig           = errCASError-3;
  errCASDriverMissing      = errCASError-6; //driver problem
  errCasDeviceNotFound     = errCASError-10; //invalid ADevice param


//Error handling
function casGetError(ADevice: Integer): Integer; stdcall;
function casGetErrorMessage(AError: Integer; ADest: PChar; AMaxLen: Integer): PChar; stdcall;

//Device Handles and Interfaces
const
  InterfaceISA         = 0;
  InterfacePCI         = 1;
  InterfaceTest        = 3;
  InterfaceUSB         = 5;
  InterfacePCIe        = 10;
  InterfaceEthernet    = 11;

function casCreateDevice: Integer; stdcall;
function casCreateDeviceEx(AInterfaceType: Integer; AInterfaceOption: Integer): Integer; stdcall;
function casChangeDevice(ADevice: Integer; AInterfaceType: Integer; AInterfaceOption: Integer): Integer; stdcall;
function casDoneDevice(ADevice: Integer): Integer; stdcall;

const
  aoAssignDevice     = 0;
  aoAssignParameters = 1;
  aoAssignComplete   = 2;

function casAssignDeviceEx(ASourceDevice, ADestDevice, AOption: Integer): Integer; stdcall;

function casGetDeviceTypes: Integer; stdcall;
function casGetDeviceTypeName(AInterfaceType: Integer; Dest: PChar; AMaxLen: Integer): PChar; stdcall;
function casGetDeviceTypeOptions(AInterfaceType: Integer): Integer; stdcall;
function casGetDeviceTypeOption(AInterfaceType, AIndex: Integer): Integer; stdcall;
function casGetDeviceTypeOptionName(AInterfaceType: Integer; AInterfaceOptionIndex: Integer; Dest: PChar; AMaxLen: Integer): PChar; stdcall;

//Initialization

const
  InitOnce       = 0;
  InitForced     = 1;
  InitNoHardware = 2;

function casInitialize(ADevice: Integer; Perform: Integer): Integer; stdcall;

//Instrument properties

const
  //AWhat parameter constants for DeviceParameter methods below
  dpidIntTimeMin                 = 101;
  dpidIntTimeMax                 = 102;
  dpidDeadPixels                 = 103;
  dpidVisiblePixels              = 104;
  dpidPixels                     = 105;
  dpidParamSets                  = 106;
  dpidCurrentParamSet            = 107;
  dpidADCRange                   = 108;
  dpidADCBits                    = 109;
  dpidSerialNo                   = 110;
  dpidTOPSerial                  = 111; //TOP200 only, use dpidTOPSerialEx and dpidTOPType for others
  dpidTransmissionFileName       = 112;
  dpidConfigFileName             = 113;
  dpidCalibFileName              = 114;
  dpidCalibrationUnit            = 115;
  dpidAccessorySerial            = 116;
  dpidTriggerCapabilities        = 118;
  dpidAveragesMax                = 119;
  dpidFilterType                 = 120;
  dpidRelSaturationMin           = 123;
  dpidRelSaturationMax           = 124;
  dpidInterfaceVersion           = 125;
  dpidTriggerDelayTimeMax        = 126;
  dpidSpectrometerName           = 127;
  dpidNeedDarkCurrent            = 130;
  dpidNeedDensityFilterChange    = 131;
  dpidSpectrometerModel          = 132;
  dpidLine1FlipFlop              = 133;
  dpidTimer                      = 134;
  dpidInterfaceType              = 135;
  dpidInterfaceOption            = 136;
  dpidInitialized                = 137;
  dpidDCRemeasureReasons         = 138;
  dpidAbortWaitForTrigger        = 140;
  dpidGetFilesFromDevice         = 142;
  dpidTOPType                    = 143;
  dpidTOPSerialEx                = 144;
  dpidAutoRangeFilterMin         = 145;
  dpidAutoRangeFilterMax         = 146;
  dpidMultiTrackMaxCount         = 147;
  dpidSLCFileInfo                = 148;
  dpidCheckConfigFileSerial      = 149;
  dpidCheckCalibFileSerial       = 150;
  dpidExtraTransmissionsFileInfo = 152;
  dpidMultiTrackCount            = 153;
  dpidIntTimePossibleResolutions = 154;
  dpidCalibDate                  = 155;
  dpidTriggerDelayTimeMin        = 159;
  dpidWavelengthCalibrationType  = 160;
  dpidCheckReferenceSpectrumFile = 162;
  dpidFlashDurationMin           = 163;

  dpidDebugLogFile               = 204;
  dpidDebugLogLevel              = 205;
  dpidDebugMaxLogSize            = 206;
  
  
  //dpidTriggerCapabilities TriggerCapabilities constants
  tcoCanTrigger                   = $00000001;
  tcoTriggerDelay                 = $00000002;
  tcoTriggerOnlyWhenReady         = $00000004;
  tcoAutoRangeTriggering          = $00000008;
  tcoShowBusyState                = $00000010;
  tcoShowACQState                 = $00000020;
  tcoFlashOutput                  = $00000040;
  tcoFlashHardware                = $00000080;
  tcoFlashHardwareForEachAverage  = $00000100;
  tcoFlashSoftwareDelay           = $00000200;
  tcoFlashSoftwareDelayNegative   = $00000400;
  tcoFlashSoftware                = $00000800;
  tcoGetFlipFlopState             = $00001000;
  tcoQueryHasData                 = $00002000;
  tcoACQStatePolarity             = $00004000;
  tcoBusyStatePolarity            = $00008000;
  tcoFlashHardwareDelay           = $00010000;
  tcoFlashHardwareDelayNegative   = $00020000;
  tcoFlashHardwareDuration        = $00040000;

  //deprecated TriggerCapabilities; use the ones they are aliased to
  tcoFlashForEachAverage  = tcoFlashHardwareForEachAverage;
  tcoFlashDelay           = tcoFlashSoftwareDelay;
  tcoFlashDelayNegative   = tcoFlashSoftwareDelayNegative;

  //dpidDCRemeasureReasons flags
  todcrrNeedDarkCurrent   = $0001;
  todcrrCCDTemperature    = $0002;

  //TOPType constants; see dpidTOPType
  ttNone       = 0;
  ttTOP100     = 1;
  ttTOP200     = 2;
  ttTOP150     = 3;
  ttTOPLumiTOP = 4;

  //dpidDebugLogLevel constants
  DebugLogLevelErrors = 1;
  DebugLogLevelSaturation = 2;
  DebugLogLevelHardwareEvents = 3;
  DebugLogLevelParameterChanges = 4;
  DebugLogLevelAllMethodCalls = 10;
  
function casGetDeviceParameter(ADevice: Integer; AWhat: Integer): Double; stdcall;
function casSetDeviceParameter(ADevice: Integer; AWhat: Integer; AValue: Double): Integer; stdcall;
function casGetDeviceParameterString(ADevice: Integer; AWhat: Integer; ADest: PChar; ADestSize: Integer): Integer; stdcall;
function casSetDeviceParameterString(ADevice: Integer; AWhat: Integer; AValue: PChar): Integer; stdcall;

const
  casSerialComplete  = 0;
  casSerialAccessory = 1;
  casSerialExtInfo   = 2;
  casSerialDevice    = 3;
  casSerialTOP       = 4;

function casGetSerialNumberEx(ADevice: Integer; AWhat: Integer; Dest: PChar; AMaxLen: Integer): Integer; stdcall;

const
  coShutter                 = $00000001;
  coFilter                  = $00000002;
  coGetShutter              = $00000004;
  coGetFilter               = $00000008;
  coGetAccessories          = $00000010;
  coGetTemperature          = $00000020;
  coUseDarkcurrentArray     = $00000040;
  coUseTransmission         = $00000080;
  coAutorangeMeasurement    = $00000100;
  coAutorangeFilter         = $00000200;
  coCheckCalibConfigSerials = $00000400;
  coTOPHasFieldOfViewConfig = $00000800;
  coAutoRemeasureDC         = $00001000;
  coCanMultiTrack           = $00008000;
  coCanSwitchLEDOff         = $00010000;
  coLEDOffWhileMeasuring    = $00020000;
  coCheckConfigUpToDate     = $00040000;
  coCanAverageOnDevice      = $00080000;
  coCanMultiTrackSensorTemp = $00100000;

function casGetOptions(ADevice: Integer): Integer; stdcall;
procedure casSetOptionsOnOff(ADevice: Integer; AOptions: Integer; AOnOff: Integer); stdcall;
procedure casSetOptions(ADevice: Integer; AOptions: Integer); stdcall;

// Measurement commands
function casMeasure(ADevice: Integer): Integer; stdcall;

function casStart(ADevice: Integer): Integer; stdcall;
function casFIFOHasData(ADevice: Integer): Integer; stdcall;
function casGetFIFOData(ADevice: Integer): Integer; stdcall;

function casMeasureDarkCurrent(ADevice: Integer): Integer; stdcall;

const
  paPrepareMeasurement           =  1;
  paLoadCalibration              =  3;
  paCheckAccessories             =  4;
  paMultiTrackStart              =  5;
  paAutoRangeFindParameter       =  7;
  paCheckConfigUpToDate          = 12;
  paSearchForDevices             = 13;
  paPrepareShutDown              = 14;
  paRecalcSpectrum               = 16;
  paUpdateSpectralCalibration    = 19;
  paUpdateCompleteCalibration    = 20;
  paCheckAccessoriesTransmission = 21;
  paMeasureParamSets             = 24;
  paMeasureParamSetsDC           = 25;
  
function casPerformAction(ADevice: Integer; AID: Integer): Integer; stdcall; //deprecated, use casPerformActionEx
function casPerformActionEx(ADevice: Integer; AActionID, AParam1, AParam2: Integer; AParam3: Pointer): Integer; stdcall;

//Measurement Parameter

const
  //AWhat parameter constants for MeasurementParameter methods below
  mpidIntegrationTime          = 01;
  mpidAverages                 = 02;
  mpidTriggerDelayTime         = 03;
  mpidTriggerTimeout           = 04;
  mpidCheckStart               = 05;
  mpidCheckStop                = 06;
  mpidColormetricStart         = 07;
  mpidColormetricStop          = 08;
  mpidACQTime                  = 10;
  mpidMaxADCValue              = 11;
  mpidMaxADCPixel              = 12;
  mpidTriggerSource            = 14;
  mpidAmpOffset                = 15;
  mpidSkipLevel                = 16;
  mpidSkipLevelEnabled         = 17;
  mpidScanStartTime            = 18;
  mpidAutoRangeMaxIntTime      = 19;
  mpidAutoRangeLevel           = 20; //deprecated 01/2012
  mpidAutoRangeMinLevel        = 20;
  mpidDensityFilter            = 21;
  mpidCurrentDensityFilter     = 22;
  mpidNewDensityFilter         = 23;
  mpidLastDCAge                = 24;
  mpidRelSaturation            = 25;
  mpidPulseWidth               = 27;
  mpidRemeasureDCInterval      = 28;
  mpidFlashDelayTime           = 29;
  mpidTOPAperture              = 30;
  mpidTOPDistance              = 31;
  mpidTOPSpotSize              = 32;
  mpidTriggerOptions           = 33;
  mpidForceFilter              = 34;
  mpidFlashType                = 35;
  mpidFlashOptions             = 36;
  mpidACQStateLine             = 37;
  mpidACQStateLinePolarity     = 38;
  mpidBusyStateLine            = 39;
  mpidBusyStateLinePolarity    = 40;
  mpidAutoFlowTime             = 41;
  mpidCRIMode                  = 42;
  mpidObserver                 = 43;
  mpidTOPFieldOfView           = 44;
  mpidCurrentCCDTemperature    = 46;
  mpidLastCCDTemperature       = 47;
  mpidDCCCDTemperature         = 48;
  mpidAutoRangeMaxLevel        = 49;
  mpidMultiTrackAcqTime        = 50;
  mpidTimeSinceScanStart       = 51;
  mpidCMTTrackStart            = 52;
  mpidColormetricWidthLevel    = 54;
  mpidIntTimeResolution        = 55;
  mpidIntTimeAlignPeriod       = 56;
  mpidColormetricType          = 57;
  mpidAveragingOnDevice        = 58;
  mpidMeasured                 = 59;
  mpidCMTSensorTemp            = 61;
  mpidFlashDuration            = 63;
  mpidColormetricPeakDiffWidth = 67;

  //mpidColormetricType constants
  cmtDefaultColormetric = 0;
  cmtSWPColormetric = 100;

  //mpidTriggerOptions constants
  toAcceptOnlyWhenReady   =  1;
  toForEachAutoRangeTrial =  2;
  toShowBusyState         =  4;
  toShowACQState          =  8;

  //mpidFlashType constants
  ftNone     = 0;
  ftHardware = 1;
  ftSoftware = 2;

  //mpidFlashOptions constants
  foEveryAverage     = 1;

  //mpidTriggerSource
  trgSoftware = 0;
  trgFlipFlop = 3;

  //mpidCRIMode
  criDIN6169    = 0;
  criCIE13_3_95 = 1;

  //mpidObserver
  cieObserver1931 = 0;
  cieObserver1964 = 1;

function casGetMeasurementParameter(ADevice: Integer; AWhat: Integer): Double; stdcall;
function casSetMeasurementParameter(ADevice: Integer; AWhat: Integer; AValue: Double): Integer; stdcall;
function casClearDarkCurrent(ADevice: Integer): Integer; stdcall;
function casDeleteParamSet(ADevice: Integer; AParamSet: Integer): Integer; stdcall;

//Filter and Shutter commands
const
  casShutterInvalid = -1;
  casShutterOpen    = 0;
  casShutterClose   = 1;

function casGetShutter(ADevice: Integer): Integer; stdcall;
procedure casSetShutter(ADevice: Integer; OnOff: Integer); stdcall;
function casGetFilterName(ADevice, AFilter: Integer; Dest: PChar; AMaxLen: Integer): PChar; stdcall;
function casGetDigitalOut(ADevice: Integer; APort: Integer): Integer; stdcall;
procedure casSetDigitalOut(ADevice: Integer; APort: Integer; OnOff: Integer); stdcall;
function casGetDigitalIn(ADevice: Integer; APort: Integer): Integer; stdcall;

//Calibration and Configuration Commands
procedure casCalculateCorrectedData(ADevice: Integer); stdcall;
procedure casConvoluteTransmission(ADevice: Integer); stdcall;

const
  gcfDensityFunction       = 0;
  gcfSensitivityFunction   = 1;
  gcfTransmissionFunction  = 2;
  gcfDensityFactor         = 3;
  gcfTOPApertureFactor     = 4;
  gcfTOPDistanceFactor     = 5;
    gcfTDDistanceMin   = -3;
    gcfTDDistanceMax   = -2;
    gcfTDCount         = -1;
    gcfTDExtraDistance = 1;
    gcfTDExtraFactor   = 2;
  gcfWLCalibrationChannel  = 6;
    gcfWLExtraFirstCoefficient     = -6;
    gcfWLExtraLastCoefficient      = -2;
    gcfWLCalibPointCount           = -1;
    gcfWLExtraCalibrationDelete    = 1;
    gcfWLExtraCalibrationDeleteAll = 2;
  gcfWLCalibrationAlias    = 7;
  gcfWLCalibrationSave     = 8; 
  gcfDarkArrayValues       = 9;
    gcfDarkArrayDepth   = -1;  //Extra
    gcfDarkArrayIntTime = -2;  //Extra
  gcfTOPParameter         = 11;
    gcfTOPApertureSize         = 0; //Extra
    gcfTOPSpotSizeDenominator  = 1;
    gcfTOPSpotSizeOffset       = 2;
  gcfLinearityFunction     = 12;
    gcfLinearityCounts = 0;
    gcfLinearityFactor = 1;
  gcfRawData               = 14;

  //obsolete (03/2010); backward compatibility after renaming
  gcfTop100Factor            = 4; //-> gcfTOPApertureFactor
  gcfTop100DistanceFactor    = 5; //-> gcfTOPDistanceFactor

function casGetCalibrationFactors(ADevice: Integer; What, Index, Extra: Integer): Double; stdcall;
procedure casSetCalibrationFactors(ADevice: Integer; What, Index, Extra: Integer; Value: Double); stdcall;
procedure casUpdateCalibrations(ADevice: Integer); stdcall;
procedure casSaveCalibration(ADevice: Integer; AFileName: PChar); stdcall;
procedure casClearCalibration(ADevice: Integer; What: Integer); stdcall;

//Measurement Results
function casGetData(ADevice: Integer; AIndex: Integer): Double; stdcall;
function casGetXArray(ADevice: Integer; AIndex: Integer): Double; stdcall;
function casGetDarkCurrent(ADevice: Integer; AIndex: Integer): Double; stdcall;
procedure casGetPhotInt(ADevice: Integer; var APhotInt: Double; AUnit: PChar; AUnitLen: Integer); stdcall;
procedure casGetRadInt(ADevice: Integer; var ARadInt: Double; AUnit: PChar; AUnitLen: Integer); stdcall;
function casGetCentroid(ADevice: Integer): Double; stdcall;
procedure casGetPeak(ADevice: Integer; var x, y: Double); stdcall;
function casGetWidth(ADevice: Integer): Double; stdcall;

const
  cLambdaWidth       = 0;
  cLambdaLow         = 1;
  cLambdaMiddle      = 2;
  cLambdaHigh        = 3;
  cLambdaOuterWidth  = 4;
  cLambdaOuterLow    = 5;
  cLambdaOuterMiddle = 6;
  cLambdaOuterHigh   = 7;

function casGetWidthEx(ADevice: Integer; What: Integer): Double; stdcall;// call only after casGetWidth
procedure casGetColorCoordinates(ADevice: Integer; var x, y, z, u, v1976, v1960: Double); stdcall;
function casGetCCT(ADevice: Integer): Double; stdcall;
function casGetCRI(ADevice: Integer; Index: Integer): Double; stdcall;
procedure casGetTriStimulus(ADevice: Integer; var X, Y, Z: Double); stdcall;

const
  ecvVisualEffect = 2;
  ecvUVA          = 3;
  ecvUVB          = 4;
  ecvUVC          = 5;
  ecvVIS          = 6;
  ecvCRICCT       = 7;
  ecvCDI          = 8;
  ecvDistance     = 9;
  ecvCalibMin     = 10;
  ecvCalibMax     = 11;
  ecvScotopicInt  = 12;

  ecvCRIFirst          = 100;
  ecvCRILast           = 116;
  ecvCRITriKXFirst     = 120;
  ecvCRITriKXLast      = 136;
  ecvCRITriKYFirst     = 140;
  ecvCRITriKYLast      = 156;
  ecvCRITriKZFirst     = 160;
  ecvCRITriKZLast      = 176;
  ecvCRITriRXordUFirst = 180;
  ecvCRITriRXordULast  = 196;
  ecvCRITriRYordVFirst = 200;
  ecvCRITriRYordVLast  = 216;
  ecvCRITriRZordWFirst = 220;
  ecvCRITriRZordWLast  = 236;

function casGetExtendedColorValues(ADevice: Integer; AWhat: Integer): Double; stdcall;

//Colormetric Calculation
function casColorMetric(ADevice: Integer): Integer; stdcall;
function casCalculateCRI(ADevice: Integer): Integer; stdcall;
function cmXYToDominantWavelength(x, y, IllX, IllY: Double; var LambdaDom, Purity: Double): Integer; stdcall;
function casCalculateLambdaDom(ADevice: Integer; IllX, IllY: Double; var LambdaDom, Purity: Double): Integer; stdcall;

//Utilities
function casGetDLLFileName(Dest: PChar; AMaxLen: Integer): PChar; stdcall;
function casGetDLLVersionNumber(Dest: PChar; AMaxLen: Integer): PChar; stdcall;
function casSaveSpectrum(ADevice: Integer; AFileName: PChar): Integer; stdcall;
function casGetExternalADCValue(ADevice: Integer; AIndex: Integer): Double; stdcall; //obsolete; use mpidCurrentCCDTemperature

const
  extNoError       = 0;
  extExternalError = 1;
  extFilterBlink   = 2;
  extShutterBlink  = 4;

procedure casSetStatusLED(ADevice: Integer; AWhat: Integer); stdcall;
function casNmToPixel(ADevice: Integer; nm: Double): Integer; stdcall;
function casPixelToNm(ADevice: Integer; APixel: Integer): Double; stdcall;
function casCalculateTOPParameter(ADevice: Integer; AAperture: Integer; ADistance: Double; var ASpotSize, AFieldOfView: Double): Integer; stdcall;

//MultiTrack
function casMultiTrackInit(ADevice: Integer; ATracks: Integer): Integer; stdcall;
function casMultiTrackDone(ADevice: Integer): Integer; stdcall;
function casMultiTrackCount(ADevice: Integer): Integer; stdcall; //obsolete; use dpidMultiTrackCount
function casMultiTrackCopyData(ADevice: Integer; ATrack: Integer): Integer; stdcall;
function casMultiTrackSaveData(ADevice: Integer; AFileName: PChar): Integer; stdcall;
function casMultiTrackLoadData(ADevice: Integer; AFileName: PChar): Integer; stdcall;

//Spectrum Manipulation
procedure casSetData(ADevice: Integer; AIndex: Integer; Value: Double); stdcall;
procedure casSetXArray(ADevice: Integer; AIndex: Integer; Value: Double); stdcall;
procedure casSetDarkCurrent(ADevice: Integer; AIndex: Integer; Value: Double); stdcall;
function casGetDataPtr(ADevice: Integer): Pointer; stdcall;
function casGetXPtr(ADevice: Integer): Pointer; stdcall;
procedure casLoadTestData(ADevice: Integer; AFileName: PChar); stdcall;

//deprecated methods!!
function casGetInitialized(ADevice: Integer): Integer; stdcall;
function casGetDeviceType(ADevice: Integer): Integer; stdcall;
function casGetDeviceOption(ADevice: Integer ): Integer; stdcall;
function casGetAdcBits(ADevice: Integer): Integer; stdcall;
function casGetAdcRange(ADevice: Integer): Integer; stdcall;
function casGetSerialNumber(ADevice: Integer; Dest: PAnsiChar; AMaxLen: Integer): PAnsiChar; stdcall;
function casGetDeadPixels(ADevice: Integer): Integer; stdcall;
function casGetVisiblePixels(ADevice: Integer): Integer; stdcall;
function casGetPixels(ADevice: Integer): Integer; stdcall;
function casGetModel(ADevice: Integer): Integer; stdcall;
function casGetAmpOffset(ADevice: Integer): Double; stdcall;
function casGetIntTimeMin(ADevice: Integer): Integer; stdcall;
function casGetIntTimeMax(ADevice: Integer): Integer; stdcall;
function casBackgroundMeasure(ADevice: Integer): Integer; stdcall;
function casGetIntegrationTime(ADevice: Integer): Integer; stdcall;
procedure casSetIntegrationTime(ADevice: Integer; Value: Integer); stdcall;
function casGetAccumulations(ADevice: Integer): Integer; stdcall;
procedure casSetAccumulations(ADevice: Integer; Value: Integer); stdcall;
function casGetAutoIntegrationLevel(ADevice: Integer): Double; stdcall;
procedure casSetAutoIntegrationLevel(ADevice: Integer; ALevel: Double); stdcall;
function casGetAutoIntegrationTimeMax(ADevice: Integer): Integer; stdcall;
procedure casSetAutoIntegrationTimeMax(ADevice: Integer; AMaxTime: Integer); stdcall;
function casClearBackground(ADevice: Integer): Integer; stdcall;
function casGetNeedBackground(ADevice: Integer): Integer; stdcall;
procedure casSetNeedBackground(ADevice: Integer; AValue: Integer); stdcall;
function casGetTop100(ADevice: Integer): Integer; stdcall;
procedure casSetTop100(ADevice: Integer; AIndex: Integer); stdcall;
function casGetTop100Distance(ADevice: Integer): Double; stdcall;
procedure casSetTop100Distance(ADevice: Integer; ADistance: Double); stdcall;
function casGetFilter(ADevice: Integer): Integer; stdcall;
procedure casSetFilter(ADevice: Integer; AFilter: Integer); stdcall;
function casGetActualFilter(ADevice: Integer): Integer; stdcall;
function casGetNewDensityFilter(ADevice: Integer): Integer; stdcall;
procedure casSetNewDensityFilter(ADevice: Integer; AFilter: Integer); stdcall;
function casGetForceFilter(ADevice: Integer): Integer; stdcall;
procedure casSetForceFilter(ADevice: Integer; AForce: Integer); stdcall;
function casGetParamSets(ADevice: Integer): Integer; stdcall;
procedure casSetParamSets(ADevice: Integer; Value: Integer); stdcall;
function casGetParamSet(ADevice: Integer): Integer; stdcall;
procedure casSetParamSet(ADevice: Integer; Value: Integer); stdcall;
function casGetCalibrationFileName(ADevice: Integer; Dest: PAnsiChar; AMaxLen: Integer): PAnsiChar; stdcall;
procedure casSetCalibrationFileName(ADevice: Integer; Value: PAnsiChar); stdcall;
function casGetConfigFileName(ADevice: Integer; Dest: PAnsiChar; AMaxLen: Integer): PAnsiChar; stdcall;
procedure casSetConfigFileName(ADevice: Integer; Value: PAnsiChar); stdcall;
function casGetTransmissionFileName(ADevice: Integer; Dest: PAnsiChar; AMaxLen: Integer): PAnsiChar; stdcall;
procedure casSetTransmissionFileName(ADevice: Integer; Value: PAnsiChar); stdcall;
function casValidateConfigAndCalibFile(ADevice: Integer): Integer; stdcall;
function casGetCalibrationUnit(ADevice: Integer; Dest: PAnsiChar; AMaxLen: Integer): PAnsiChar; stdcall;
procedure casSetCalibrationUnit(ADevice: Integer; Value: PAnsiChar); stdcall;
function casGetBackground(ADevice: Integer; AIndex: Integer): Double; stdcall;
procedure casSetBackground(ADevice: Integer; AIndex: Integer; Value: Double); stdcall;
function casGetMaxAdcValue(ADevice: Integer): Integer; stdcall;
function casGetCheckStart(ADevice: Integer): Integer; stdcall;
procedure casSetCheckStart(ADevice: Integer; Value: Integer); stdcall;
function casGetCheckStop(ADevice: Integer): Integer; stdcall;
procedure casSetCheckStop(ADevice: Integer; Value: Integer); stdcall;
function casGetColormetricStart(ADevice: Integer): Double; stdcall;
procedure casSetColormetricStart(ADevice: Integer; Value: Double); stdcall;
function casGetColormetricStop(ADevice: Integer): Double; stdcall;
procedure casSetColormetricStop(ADevice: Integer; Value: Double); stdcall;
function casGetObserver: Integer; stdcall;
procedure casSetObserver(AObserver: Integer); stdcall;
function casGetSkipLevel(ADevice: Integer): Double; stdcall;
procedure casSetSkipLevel(ADevice: Integer; ASkipLevel: Double); stdcall;
function casGetSkipLevelEnabled(ADevice: Integer): Integer; stdcall;
procedure casSetSkipLevelEnabled(ADevice: Integer; ASkipLevel: Integer); stdcall;
function casGetTriggerSource(ADevice: Integer): Integer; stdcall;
procedure casSetTriggerSource(ADevice: Integer; Value: Integer); stdcall;
function casGetLine1FlipFlop(ADevice: Integer): Integer; stdcall;
procedure casSetLine1FlipFlop(ADevice: Integer; Value: Integer); stdcall;
function casGetTimeout(ADevice: Integer): Integer; stdcall;
procedure casSetTimeout(ADevice: Integer; Value: Integer); stdcall;
function casGetFlash(ADevice: Integer): Integer; stdcall;
procedure casSetFlash(ADevice: Integer; Value: Integer); stdcall;
function casGetFlashDelayTime(ADevice: Integer): Integer; stdcall;
procedure casSetFlashDelayTime(ADevice: Integer; Value: Integer); stdcall;
function casGetFlashOptions(ADevice: Integer): Integer; stdcall;
procedure casSetFlashOptions(ADevice: Integer; Value: Integer); stdcall;
function casGetDelayTime(ADevice: Integer): Integer; stdcall;
procedure casSetDelayTime(ADevice: Integer; Value: Integer); stdcall;
function casGetStartTime(ADevice: Integer): Integer; stdcall;
function casGetACQTime(ADevice: Integer): Integer; stdcall;
function casReadWatch(ADevice: Integer): Integer; stdcall;
function casStopTime(ADevice: Integer; ARefTime: Integer): Integer; stdcall;
procedure casMultiTrackCopySet(ADevice: Integer); stdcall;
function casMultiTrackReadData(ADevice: Integer; ATrack: Integer): Integer; stdcall;

const
  {$IFDEF WIN64}
  ModulName = 'CAS4x64.dll';
  {$ELSE}
  ModulName = 'CAS4.dll';
  {$ENDIF}

implementation

uses
  Windows;

const
{$IFDEF UNICODE}
  ExportSuffix = 'W';
{$ELSE}
  ExportSuffix = '';
{$ENDIF}

//Error handling
function casGetError; external ModulName;
function casGetErrorMessage; external ModulName name 'casGetErrorMessage' + ExportSuffix;

//Device Handles and Interfaces
function casCreateDevice; external ModulName;
function casCreateDeviceEx; external ModulName;
function casChangeDevice; external ModulName;
function casDoneDevice; external ModulName;
function casAssignDeviceEx; external ModulName;

function casGetDeviceTypes; external ModulName;
function casGetDeviceTypeName; external ModulName name 'casGetDeviceTypeName' + ExportSuffix;
function casGetDeviceTypeOptions; external ModulName;
function casGetDeviceTypeOption; external ModulName;
function casGetDeviceTypeOptionName; external ModulName name 'casGetDeviceTypeOptionName' + ExportSuffix;

//Initialization
function casInitialize; external ModulName;

//Instrument properties
function casGetDeviceParameter; external ModulName;
function casSetDeviceParameter; external ModulName;
function casGetDeviceParameterString; external ModulName name 'casGetDeviceParameterString' + ExportSuffix;
function casSetDeviceParameterString; external ModulName name 'casSetDeviceParameterString' + ExportSuffix;
function casGetSerialNumberEx; external ModulName name 'casGetSerialNumberEx' + ExportSuffix;
function casGetOptions; external ModulName;
procedure casSetOptionsOnOff; external ModulName;
procedure casSetOptions; external ModulName;

// Measurement commands
function casMeasure; external ModulName;

function casStart; external ModulName;
function casFIFOHasData; external ModulName;
function casGetFIFOData; external ModulName;

function casMeasureDarkCurrent; external ModulName;

function casPerformAction; external ModulName;
function casPerformActionEx; external ModulName;

//Measurement Parameter
function casGetMeasurementParameter; external ModulName;
function casSetMeasurementParameter; external ModulName;
function casClearDarkCurrent; external ModulName;
function casDeleteParamSet; external ModulName;

//Filter and Shutter commands
function casGetShutter; external ModulName;
procedure casSetShutter; external ModulName;
function casGetFilterName; external ModulName name 'casGetFilterName' + ExportSuffix;
function casGetDigitalOut; external ModulName;
procedure casSetDigitalOut; external ModulName;
function casGetDigitalIn; external ModulName;

//Calibration and Configuration Commands
procedure casCalculateCorrectedData; external ModulName;
procedure casConvoluteTransmission; external ModulName;
function casGetCalibrationFactors; external ModulName;
procedure casSetCalibrationFactors; external ModulName;
procedure casUpdateCalibrations; external ModulName;
procedure casSaveCalibration; external ModulName name 'casSaveCalibration' + ExportSuffix;
procedure casClearCalibration; external ModulName;

//Measurement Results
function casGetData; external ModulName;
function casGetXArray; external ModulName;
function casGetDarkCurrent; external ModulName;
procedure casGetPhotInt; external ModulName name 'casGetPhotInt' + ExportSuffix;
procedure casGetRadInt; external ModulName name 'casGetRadInt' + ExportSuffix;
function casGetCentroid; external ModulName;
procedure casGetPeak; external ModulName;
function casGetWidth; external ModulName;
function casGetWidthEx; external ModulName;
procedure casGetColorCoordinates; external ModulName;
function casGetCCT; external ModulName;
function casGetCRI; external ModulName;
procedure casGetTriStimulus; external ModulName;
function casGetExtendedColorValues; external ModulName;

//Colormetric Calculation
function casColorMetric; external ModulName;
function casCalculateCRI; external ModulName;
function cmXYToDominantWavelength; external ModulName;
function casCalculateLambdaDom; external ModulName;

//Utilities
function casGetDLLFileName; external ModulName name 'casGetDLLFileName' + ExportSuffix;
function casGetDLLVersionNumber; external ModulName name 'casGetDLLVersionNumber' + ExportSuffix;
function casSaveSpectrum; external ModulName name 'casSaveSpectrum' + ExportSuffix;
function casGetExternalADCValue; external ModulName;
procedure casSetStatusLED; external ModulName;
function casNmToPixel; external ModulName;
function casPixelToNm; external ModulName;
function casCalculateTOPParameter; external ModulName;

//MultiTrack
function casMultiTrackInit; external ModulName;
function casMultiTrackDone; external ModulName;
function casMultiTrackCount; external ModulName;
function casMultiTrackCopyData; external ModulName;
function casMultiTrackSaveData; external ModulName name 'casMultiTrackSaveData' + ExportSuffix;
function casMultiTrackLoadData; external ModulName name 'casMultiTrackLoadData' + ExportSuffix;

//Spectrum Manipulation
procedure casSetData; external ModulName;
procedure casSetXArray; external ModulName;
procedure casSetDarkCurrent; external ModulName;
function casGetDataPtr; external ModulName;
function casGetXPtr; external ModulName;
procedure casLoadTestData; external ModulName name 'casLoadTestData' + ExportSuffix;

//deprecated; see documentation for replacement methods
function casGetInitialized; external ModulName;
function casGetDeviceType; external ModulName;
function casGetDeviceOption; external ModulName;
function casGetAdcBits; external ModulName;
function casGetAdcRange; external ModulName;
function casGetSerialNumber; external ModulName;
function casGetDeadPixels; external ModulName;
function casGetVisiblePixels; external ModulName;
function casGetPixels; external ModulName;
function casGetModel; external ModulName;
function casGetAmpOffset; external ModulName;
function casGetIntTimeMin; external ModulName;
function casGetIntTimeMax; external ModulName;
function casBackgroundMeasure; external ModulName name 'casMeasureDarkCurrent';
function casGetIntegrationTime; external ModulName;
procedure casSetIntegrationTime; external ModulName;
function casGetAccumulations; external ModulName;
procedure casSetAccumulations; external ModulName;
function casGetAutoIntegrationLevel; external ModulName;
procedure casSetAutoIntegrationLevel; external ModulName;
function casGetAutoIntegrationTimeMax; external ModulName;
procedure casSetAutoIntegrationTimeMax; external ModulName;
function casClearBackground; external ModulName name 'casClearDarkCurrent';
function casGetNeedBackground; external ModulName;
procedure casSetNeedBackground; external ModulName;
function casGetTop100; external ModulName;
procedure casSetTop100; external ModulName;
function casGetTop100Distance; external ModulName;
procedure casSetTop100Distance; external ModulName;
function casGetFilter; external ModulName;
procedure casSetFilter; external ModulName;
function casGetActualFilter; external ModulName;
function casGetNewDensityFilter; external ModulName;
procedure casSetNewDensityFilter; external ModulName;
function casGetForceFilter; external ModulName;
procedure casSetForceFilter; external ModulName;
function casGetParamSets; external ModulName;
procedure casSetParamSets; external ModulName;
function casGetParamSet; external ModulName;
procedure casSetParamSet; external ModulName;
function casGetCalibrationFileName; external ModulName;
procedure casSetCalibrationFileName; external ModulName;
function casGetConfigFileName; external ModulName;
procedure casSetConfigFileName; external ModulName;
function casGetTransmissionFileName; external ModulName;
procedure casSetTransmissionFileName; external ModulName;
function casValidateConfigAndCalibFile; external ModulName;
function casGetCalibrationUnit; external ModulName;
procedure casSetCalibrationUnit; external ModulName;
function casGetBackground; external ModulName name 'casGetDarkCurrent';
procedure casSetBackground; external ModulName name 'casSetDarkCurrent';
function casGetMaxAdcValue; external ModulName;
function casGetCheckStart; external ModulName;
procedure casSetCheckStart; external ModulName;
function casGetCheckStop; external ModulName;
procedure casSetCheckStop; external ModulName;
function casGetColormetricStart; external ModulName;
procedure casSetColormetricStart; external ModulName;
function casGetColormetricStop; external ModulName;
procedure casSetColormetricStop; external ModulName;
function casGetObserver; external ModulName;
procedure casSetObserver; external ModulName;
function casGetSkipLevel; external ModulName;
procedure casSetSkipLevel; external ModulName;
function casGetSkipLevelEnabled; external ModulName;
procedure casSetSkipLevelEnabled; external ModulName;
function casGetTriggerSource; external ModulName;
procedure casSetTriggerSource; external ModulName;
function casGetLine1FlipFlop; external ModulName;
procedure casSetLine1FlipFlop; external ModulName;
function casGetTimeout; external ModulName;
procedure casSetTimeout; external ModulName;
function casGetFlash; external ModulName;
procedure casSetFlash; external ModulName;
function casGetFlashDelayTime; external ModulName;
procedure casSetFlashDelayTime; external ModulName;
function casGetFlashOptions; external ModulName;
procedure casSetFlashOptions; external ModulName;
function casGetDelayTime; external ModulName;
procedure casSetDelayTime; external ModulName;
function casGetStartTime; external ModulName;
function casGetACQTime; external ModulName;
function casReadWatch; external ModulName;
function casStopTime; external ModulName;
procedure casMultiTrackCopySet; external ModulName;
function casMultiTrackReadData; external ModulName;

end.