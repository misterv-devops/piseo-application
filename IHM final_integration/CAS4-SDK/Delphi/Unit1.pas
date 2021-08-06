unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ActnList, StdCtrls, TeEngine, Series, ExtCtrls, TeeProcs, Chart,
  Mask, VclTee.TeeGDIPlus, System.Actions;

type
  TForm1 = class(TForm)
    Chart1: TChart;
    lblSaturated: TLabel;
    Series1: TLineSeries;
    Button1: TButton;
    ecConfig: TEdit;
    OD: TOpenDialog;
    ecCalibration: TEdit;
    ActionList1: TActionList;
    actOpenConfig: TAction;
    actOpenCalibration: TAction;
    Button2: TButton;
    Button3: TButton;
    actMeasure: TAction;
    Label1: TLabel;
    cbInterface: TComboBox;
    Label2: TLabel;
    cbInterfaceOption: TComboBox;
    Memo: TMemo;
    Label3: TLabel;
    meIntegrationTime: TEdit;
    btnDownload: TButton;
    actDownload: TAction;
    procedure actOpenConfigExecute(Sender: TObject);
    procedure actOpenCalibrationExecute(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure cbInterfaceChange(Sender: TObject);
    procedure actMeasureExecute(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure cbInterfaceOptionChange(Sender: TObject);
    procedure actDownloadExecute(Sender: TObject);
  private
    { Private declarations }
    FCasID: Integer;
    procedure UpdateComboBoxes;

    procedure CheckError(AError: Integer);
    function GetSelectedInterfaceOption: Integer;
    procedure ReleaseCAS;
    procedure InitCAS;
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

uses
  Math,
  Types,
  IOUtils,
  CAS4DLL;

procedure TForm1.UpdateComboBoxes;
var
  i, DeviceTypes: Integer;
  Dest: array[Byte] of Char;
  s: string;
begin
  DeviceTypes:= casGetDeviceTypes;
  cbInterface.Clear;
  for i := 0 to DeviceTypes - 1 do    // Iterate
  begin
    casGetDeviceTypeName(i, Dest, Length(Dest)-1);
    s:= StrPas(Dest);
    if Length(s)>0 then
      cbInterface.Items.AddObject(s, Pointer(NativeInt(i)));
  end;    // for
  cbInterface.ItemIndex:= 0;
  cbInterfaceChange(nil)
end;

procedure TForm1.cbInterfaceChange(Sender: TObject);
var
  i, DeviceTypeOption, DeviceType, DeviceOptions: Integer;
  Dest: array[Byte] of Char;
begin
  DeviceType:= Integer(cbInterface.Items.Objects[cbInterface.ItemIndex]);
  cbInterfaceOption.Clear;
  DeviceOptions:= casGetDeviceTypeOptions(DeviceType);
  for i := 0 to DeviceOptions - 1 do
  begin
    DeviceTypeOption:= casGetDeviceTypeOption(DeviceType, i); //get the nth option
    casGetDeviceTypeOptionName(DeviceType, i, Dest, Length(Dest)-1); //get the nth name
    cbInterfaceOption.Items.AddObject(Dest, Pointer(NativeInt(DeviceTypeOption)));
  end;
  if cbInterfaceOption.Items.Count > 0 then
    cbInterfaceOption.ItemIndex:= 0;
  cbInterfaceOption.Enabled:= cbInterfaceOption.Items.Count > 0;

  ReleaseCAS;
end;

procedure TForm1.cbInterfaceOptionChange(Sender: TObject);
begin
  ReleaseCAS;
end;

procedure TForm1.ReleaseCAS;
begin
  if FCasID>=0 then
  begin
    CheckError(casDoneDevice(FCasID));
    FCasID:=-1;
  end;
end;

procedure TForm1.actOpenConfigExecute(Sender: TObject);
begin
  if ecConfig.Text <> '' then
    OD.FileName:= ecConfig.Text;

  OD.FilterIndex:=1;
  if OD.Execute then
    ecConfig.Text:= OD.FileName;
end;

procedure TForm1.actOpenCalibrationExecute(Sender: TObject);
begin
  if ecCalibration.Text <> '' then
    OD.FileName:= ecConfig.Text;

  OD.FilterIndex:=2;
  if OD.Execute then
    ecCalibration.Text:= OD.FileName;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  FCasID:= -1;
end;

procedure TForm1.FormShow(Sender: TObject);
var
  ach1, ach2: array[Byte] of Char;
begin
  casGetDLLFileName(ach2, 255);
  casGetDLLVersionNumber(ach1, 255);
  Memo.Lines.Add(Format('Using DLL %s from %s', [StrPas(ach1), StrPas(ach2)]));
  UpdateComboBoxes;
end;

function TForm1.GetSelectedInterfaceOption: Integer;
begin
  Result:=-1;
  if cbInterfaceOption.ItemIndex>=0 then
    Result:=Integer(cbInterfaceOption.Items.Objects[cbInterfaceOption.ItemIndex]) else
    if cbInterfaceOption.Items.Count>0 then //don't raise an exception if there are no device options available
      raise Exception.Create('Please select the device option first!');
end;

procedure TForm1.actDownloadExecute(Sender: TObject);
var
  DestPath: string;
  serial: array[Byte] of Char;
  foundConfigFiles: TStringDynArray;
begin
  try
    InitCAS;
  except
    //we deliberatly ignore exceptions here sinc we only
    //want to retrieve the serial number
  end;
  //get the serial
  FillChar(serial, Length(serial), 0);
  CheckError(casGetSerialNumberEx(FCASID, casSerialDevice, serial, Length(serial)-1));

  //build a subpath in Documents folder, append 'CASCalibDir'

  DestPath:= TPath.Combine(TPath.GetDocumentsPath, 'CASCalibDir');
  //if we managed to get a serial number, append the serial number to the path
  //by using a separate directory per spectrometer, we can simply choose the first
  //INI/ISC file pair in that directory and avoid using calibrations
  //that won't work with the spectrometer at hand
  if StrLen(serial) > 0 then
    DestPath:= TPath.Combine(DestPath, serial);

  //now make sure the directory exists
  TDirectory.CreateDirectory(DestPath);

  //and pass it as dpidGetFilesFromDevice, telling the CAS DLL to download
  //the files into this directory
  CheckError(casSetDeviceParameterString(FCASID, dpidGetFilesFromDevice, PChar(DestPath)));

  foundConfigFiles:= TDirectory.GetFiles(DestPath, '*.ini');
  if Length(foundConfigFiles) = 0 then
    raise Exception.CreateFmt('No .ini file found in directory "%s"', [DestPath]);

  ecConfig.Text:= foundConfigFiles[0];

  ecCalibration.Text:= ChangeFileExt(foundConfigFiles[0], '.isc');
end;

procedure TForm1.InitCAS;
begin
  if FCasID < 0 then
    FCasID:= casCreateDeviceEx(Integer(cbInterface.Items.Objects[cbInterface.ItemIndex]), GetSelectedInterfaceOption);

  //always set the config/calib filenames, if they actually changed,
  //the check for dpidInitialized will return 0
  CheckError(casSetDeviceParameterString(FCasID, dpidConfigFileName, PChar(ecConfig.Text)));
  CheckError(casSetDeviceParameterString(FCasID, dpidCalibFileName, PChar(ecCalibration.Text)));

  //we explicitly enable these options since they ensure that
  //a new calibration of the device is detected as well as a serial mismatch
  casSetOptionsOnOff(FCasID, coCheckCalibConfigSerials or coCheckConfigUpToDate, 1);
  CheckError(casGetError(FCasID));
  CheckError(casInitialize(FCasID, InitOnce));
end;

procedure TForm1.actMeasureExecute(Sender: TObject);
var
  i: Integer;
  Pixels: Integer;
  intTime, intTimeMin, intTimeMax: Integer;
begin
  InitCAS;

  //before setting mpidIntegrationTime, make sure it is within the
  //supported range, i.e. dpidIntTimeMin..dpidIntTimeMax
  intTime:= StrToInt(meIntegrationTime.Text);
  intTimeMin:= Round(casGetDeviceParameter(FCasID, dpidIntTimeMin));
  CheckError(casGetError(FCasID));
  intTimeMax:= Round(casGetDeviceParameter(FCasID, dpidIntTimeMax));
  if not InRange(intTime, intTimeMin, intTimeMax) then
  begin
    intTime:= EnsureRange(intTime, intTimeMin, intTimeMax);
    meIntegrationTime.Text:= IntToStr(intTime);
  end;
  CheckError(casSetMeasurementParameter(FCasID, mpidIntegrationTime, intTime));
  //averages should be checked against dpidAveragesMax, but 1 will always be ok
  CheckError(casSetMeasurementParameter(FCasID, mpidAverages, 1));

  if Round(casGetDeviceParameter(FCasID, dpidDCRemeasureReasons)) > 0 then
  begin
    casSetShutter(FCasID, 1);
    CheckError(casGetError(FCasID));
    try
      CheckError(casMeasureDarkCurrent(FCasID));
    finally
      casSetShutter(FCasID, 0);
      CheckError(casGetError(FCasID));
    end;
  end;

  CheckError(casMeasure(FCasID));
  Pixels:= Round(casGetDeviceParameter(FCasID, dpidPixels));

  Series1.Clear;
  for i:= 0 to  Pixels-1 do
    Series1.AddXY( casGetXArray(FCasID, i), casGetData(FCasID, i),'', clRed);
  lblSaturated.Visible:= casGetMeasurementParameter(FCasID, mpidMaxADCValue) >
    casGetDeviceParameter(FCasID, dpidADCRange);
end;

procedure TForm1.CheckError(AError: Integer);
var
  Dest: array[byte] of Char;
begin
  if AError < 0 then
  begin
    Memo.Lines.Add(casGetErrorMessage(AError, Dest, SizeOf(Dest)-1));
    raise Exception.CreateFmt('Error code %d: %s', [AError, Dest]);
  end;
end;

procedure TForm1.FormDestroy(Sender: TObject);
begin
  ReleaseCAS;
end;

end.

