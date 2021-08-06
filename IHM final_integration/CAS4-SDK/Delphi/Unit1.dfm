object Form1: TForm1
  Left = 322
  Top = 218
  Caption = 'Form1'
  ClientHeight = 523
  ClientWidth = 511
  Color = clBtnFace
  Constraints.MinHeight = 557
  Constraints.MinWidth = 519
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  OnShow = FormShow
  DesignSize = (
    511
    523)
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 8
    Top = 11
    Width = 45
    Height = 13
    Caption = 'Interface:'
  end
  object Label2: TLabel
    Left = 227
    Top = 11
    Width = 34
    Height = 13
    Caption = 'Option:'
  end
  object Label3: TLabel
    Left = 19
    Top = 115
    Width = 95
    Height = 13
    Caption = 'IntegrationTime[ms]:'
  end
  object Chart1: TChart
    Left = 16
    Top = 144
    Width = 484
    Height = 179
    BackWall.Brush.Style = bsClear
    Legend.Visible = False
    Title.Text.Strings = (
      'Spectrum')
    LeftAxis.ExactDateTime = False
    View3D = False
    TabOrder = 0
    Anchors = [akLeft, akTop, akRight, akBottom]
    DefaultCanvas = 'TGDIPlusCanvas'
    ColorPaletteIndex = 0
    object lblSaturated: TLabel
      Left = 56
      Top = 12
      Width = 44
      Height = 13
      Caption = 'saturated'
      Color = clYellow
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clRed
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentColor = False
      ParentFont = False
      Visible = False
    end
    object Series1: TLineSeries
      Brush.BackColor = clDefault
      Pointer.InflateMargins = True
      Pointer.Style = psRectangle
      XValues.Name = 'Wavelength'
      XValues.Order = loAscending
      YValues.Name = 'Intensity'
      YValues.Order = loNone
    end
  end
  object Button1: TButton
    Left = 400
    Top = 61
    Width = 100
    Height = 25
    Action = actOpenConfig
    TabOrder = 2
  end
  object ecConfig: TEdit
    Left = 16
    Top = 61
    Width = 353
    Height = 21
    TabOrder = 3
  end
  object ecCalibration: TEdit
    Left = 16
    Top = 85
    Width = 353
    Height = 21
    TabOrder = 4
  end
  object Button2: TButton
    Left = 400
    Top = 85
    Width = 100
    Height = 25
    Action = actOpenCalibration
    TabOrder = 5
  end
  object Button3: TButton
    Left = 400
    Top = 109
    Width = 100
    Height = 25
    Action = actMeasure
    TabOrder = 6
  end
  object cbInterface: TComboBox
    Left = 59
    Top = 8
    Width = 145
    Height = 21
    Style = csDropDownList
    TabOrder = 7
    OnChange = cbInterfaceChange
  end
  object cbInterfaceOption: TComboBox
    Left = 267
    Top = 8
    Width = 145
    Height = 21
    Style = csDropDownList
    TabOrder = 8
    OnChange = cbInterfaceOptionChange
  end
  object Memo: TMemo
    Left = 16
    Top = 330
    Width = 484
    Height = 185
    Anchors = [akLeft, akRight, akBottom]
    TabOrder = 9
  end
  object meIntegrationTime: TEdit
    Left = 118
    Top = 112
    Width = 36
    Height = 21
    MaxLength = 5
    TabOrder = 10
    Text = '20'
  end
  object btnDownload: TButton
    Left = 400
    Top = 37
    Width = 100
    Height = 25
    Action = actDownload
    TabOrder = 1
  end
  object OD: TOpenDialog
    Filter = 'Config Files|*.ini|CalibrationFiles|*.isc|All Files|*.*'
    Left = 48
    Top = 168
  end
  object ActionList1: TActionList
    Left = 192
    Top = 168
    object actOpenConfig: TAction
      Caption = 'Load Config'
      OnExecute = actOpenConfigExecute
    end
    object actOpenCalibration: TAction
      Caption = 'Load Calibration'
      OnExecute = actOpenCalibrationExecute
    end
    object actMeasure: TAction
      Caption = 'Measure'
      OnExecute = actMeasureExecute
    end
    object actDownload: TAction
      AutoCheck = True
      Caption = 'Load from device'
      OnExecute = actDownloadExecute
    end
  end
end
