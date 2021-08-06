; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=CSpectrumDoc
LastTemplate=CDialog
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "CAS_DLL_Sample.h"
LastPage=0

ClassCount=11
Class1=CCAS_DLL_SampleApp
Class2=CCAS_DLL_SampleDoc
Class3=CCAS_DLL_SampleView
Class4=CMainFrame

ResourceCount=12
Resource1=IDD_ABOUTBOX
Resource2=IDR_CAS_DLTYPE (English (U.S.))
Resource3=IDR_CAS_DLTYPE
Class5=CChildFrame
Class6=CAboutDlg
Resource4=IDD_ABOUTBOX (English (U.S.))
Resource5=IDR_MAINFRAME (English (U.S.))
Class7=CSpectrum
Class8=CSpectrumView
Class9=CSpectrumDoc
Class10=CSpectrumFrame
Resource6=IDR_MAINFRAME (English (U.S.) - A)
Resource7=IDD_DIALOG1
Class11=CConfDlg
Resource8=IDD_SELECT_INTERFACE (English (U.S.))
Resource9=IDR_MAINFRAME (Englisch (USA))
Resource10=IDD_ABOUTBOX (Englisch (USA))
Resource11=IDR_CAS_DLTYPE (Englisch (USA))
Resource12=IDD_SELECT_INTERFACE (Englisch (USA))

[CLS:CCAS_DLL_SampleApp]
Type=0
HeaderFile=CAS_DLL_Sample.h
ImplementationFile=CAS_DLL_Sample.cpp
Filter=N
BaseClass=CWinApp
VirtualFilter=AC
LastObject=CCAS_DLL_SampleApp

[CLS:CCAS_DLL_SampleDoc]
Type=0
HeaderFile=CAS_DLL_SampleDoc.h
ImplementationFile=CAS_DLL_SampleDoc.cpp
Filter=C
LastObject=CCAS_DLL_SampleDoc
BaseClass=CDocument
VirtualFilter=DC

[CLS:CCAS_DLL_SampleView]
Type=0
HeaderFile=CAS_DLL_SampleView.h
ImplementationFile=CAS_DLL_SampleView.cpp
Filter=C
LastObject=CCAS_DLL_SampleView
BaseClass=CEditView
VirtualFilter=VWC


[CLS:CMainFrame]
Type=0
HeaderFile=MainFrm.h
ImplementationFile=MainFrm.cpp
Filter=T
LastObject=ID_MEASURE_GETSPECTRUM


[CLS:CChildFrame]
Type=0
HeaderFile=ChildFrm.h
ImplementationFile=ChildFrm.cpp
Filter=M
LastObject=CChildFrame
BaseClass=CMDIChildWnd
VirtualFilter=mfWC


[CLS:CAboutDlg]
Type=0
HeaderFile=CAS_DLL_Sample.cpp
ImplementationFile=CAS_DLL_Sample.cpp
Filter=D
LastObject=CAboutDlg

[DLG:IDD_ABOUTBOX]
Type=1
ControlCount=4
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308352
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889
Class=CAboutDlg

[TB:IDR_MAINFRAME]
Type=1
Class=CMainFrame
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_SAVE
Command4=ID_EDIT_CUT
Command5=ID_EDIT_COPY
Command6=ID_EDIT_PASTE
Command7=ID_FILE_PRINT
CommandCount=8
Command8=ID_APP_ABOUT

[MNU:IDR_CAS_DLTYPE]
Type=1
Class=CCAS_DLL_SampleView
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_CLOSE
Command4=ID_FILE_SAVE
Command5=ID_FILE_SAVE_AS
Command9=ID_EDIT_CUT
Command10=ID_EDIT_COPY
Command11=ID_EDIT_PASTE
Command12=ID_VIEW_TOOLBAR
Command13=ID_VIEW_STATUS_BAR
Command14=ID_WINDOW_NEW
CommandCount=18
Command6=ID_FILE_MRU_FILE1
Command7=ID_APP_EXIT
Command8=ID_EDIT_UNDO
Command15=ID_WINDOW_CASCADE
Command16=ID_WINDOW_TILE_HORZ
Command17=ID_WINDOW_ARRANGE
Command18=ID_APP_ABOUT

[ACL:IDR_MAINFRAME]
Type=1
Class=CMainFrame
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_SAVE
Command5=ID_EDIT_CUT
Command6=ID_EDIT_COPY
Command7=ID_EDIT_PASTE
Command8=ID_EDIT_UNDO
Command9=ID_EDIT_CUT
Command10=ID_EDIT_COPY
Command11=ID_EDIT_PASTE
Command12=ID_NEXT_PANE
CommandCount=13
Command4=ID_EDIT_UNDO
Command13=ID_PREV_PANE


[MNU:IDR_CAS_DLTYPE (English (U.S.))]
Type=1
Class=CCAS_DLL_SampleView
Command1=ID_APP_EXIT
Command2=ID_EDIT_UNDO
Command3=ID_EDIT_CUT
Command4=ID_EDIT_COPY
Command5=ID_EDIT_PASTE
Command6=ID_VIEW_TOOLBAR
Command7=ID_VIEW_STATUS_BAR
Command8=ID_WINDOW_CASCADE
Command9=ID_WINDOW_TILE_HORZ
Command10=ID_WINDOW_ARRANGE
Command11=ID_MEASURE_GETSPECTRUM
Command12=ID_MEASURE_GETDARKCURRENT
Command13=ID_MEASURE_MEASUREWITHEXTERNALTRIGGER
Command14=ID_MEASURE_GETDEVICEOPTIONS
Command15=ID_MEASURE_HIGHSPEED
Command16=ID_MEASURE_HIGHSPEEDMEASUREMENTWITHEXTERNALTRIGGER
Command17=IDD_SELECT_INTERFACE
Command18=ID_APP_ABOUT
CommandCount=18

[TB:IDR_MAINFRAME (English (U.S.))]
Type=1
Class=?
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_SAVE
Command4=ID_EDIT_CUT
Command5=ID_EDIT_COPY
Command6=ID_EDIT_PASTE
Command7=ID_FILE_PRINT
Command8=ID_APP_ABOUT
CommandCount=8

[ACL:IDR_MAINFRAME (English (U.S.))]
Type=1
Class=?
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_SAVE
Command4=ID_EDIT_UNDO
Command5=ID_EDIT_CUT
Command6=ID_EDIT_COPY
Command7=ID_EDIT_PASTE
Command8=ID_EDIT_UNDO
Command9=ID_EDIT_CUT
Command10=ID_EDIT_COPY
Command11=ID_EDIT_PASTE
Command12=ID_NEXT_PANE
Command13=ID_PREV_PANE
CommandCount=13

[DLG:IDD_ABOUTBOX (English (U.S.))]
Type=1
Class=CAboutDlg
ControlCount=4
Control1=IDC_STATIC,static,1342308480
Control2=IDC_STATIC,static,1342308352
Control3=IDOK,button,1342373889
Control4=IDC_STATIC,static,1342177283

[CLS:CSpectrum]
Type=0
HeaderFile=Spectrum.h
ImplementationFile=Spectrum.cpp
BaseClass=CDocument
Filter=N

[CLS:CSpectrumView]
Type=0
HeaderFile=SpectrumView.h
ImplementationFile=SpectrumView.cpp
BaseClass=CView
Filter=C
VirtualFilter=VWC
LastObject=CSpectrumView

[CLS:CSpectrumDoc]
Type=0
HeaderFile=SpectrumDoc.h
ImplementationFile=SpectrumDoc.cpp
BaseClass=CDocument
Filter=N
VirtualFilter=DC
LastObject=ID_MEASURE_HIGHSPEEDMEASUREMENTWITHEXTERNALTRIGGER

[CLS:CSpectrumFrame]
Type=0
HeaderFile=SpectrumFrame.h
ImplementationFile=SpectrumFrame.cpp
BaseClass=CMDIChildWnd
Filter=M
LastObject=CSpectrumFrame
VirtualFilter=mfWC

[MNU:IDR_MAINFRAME (English (U.S.))]
Type=1
Class=?
Command1=ID_APP_EXIT
Command2=ID_VIEW_TOOLBAR
Command3=ID_VIEW_STATUS_BAR
Command4=ID_APP_ABOUT
CommandCount=4

[MNU:IDR_MAINFRAME (English (U.S.) - A)]
Type=1
Class=?
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_MRU_FILE1
Command4=ID_APP_EXIT
Command5=ID_VIEW_TOOLBAR
Command6=ID_VIEW_STATUS_BAR
Command7=ID_APP_ABOUT
CommandCount=7

[ACL:IDR_CAS_DLTYPE (English (U.S.))]
Type=1
Class=?
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_SAVE
Command4=ID_EDIT_UNDO
Command5=ID_EDIT_CUT
Command6=ID_EDIT_COPY
Command7=ID_EDIT_PASTE
Command8=ID_EDIT_UNDO
Command9=ID_EDIT_CUT
Command10=ID_EDIT_COPY
Command11=ID_EDIT_PASTE
Command12=ID_NEXT_PANE
Command13=ID_PREV_PANE
CommandCount=13

[DLG:IDD_DIALOG1]
Type=1
Class=?
ControlCount=7
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_COMBO1,combobox,1344340226
Control4=IDC_LIST1,listbox,1352728835
Control5=IDC_SPIN1,msctls_updown32,1342177312
Control6=IDC_STATIC,button,1342177287
Control7=IDC_LIST2,SysListView32,1350631424

[DLG:IDD_SELECT_INTERFACE (English (U.S.))]
Type=1
Class=CConfDlg
ControlCount=9
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_RADIO1,button,1342373897
Control4=IDC_RADIO2,button,1342242825
Control5=IDC_RADIO3,button,1342242825
Control6=IDC_RADIO4,button,1342242825
Control7=IDC_RADIO5,button,1342242825
Control8=IDC_STATIC,static,1342177280
Control9=IDC_RADIO6,button,1342242825

[CLS:CConfDlg]
Type=0
HeaderFile=ConfDlg.h
ImplementationFile=ConfDlg.cpp
BaseClass=CDialog
Filter=D
LastObject=CConfDlg
VirtualFilter=dWC

[TB:IDR_MAINFRAME (Englisch (USA))]
Type=1
Class=?
Command1=ID_FILE_NEW
Command2=ID_FILE_OPEN
Command3=ID_FILE_SAVE
Command4=ID_EDIT_CUT
Command5=ID_EDIT_COPY
Command6=ID_EDIT_PASTE
Command7=ID_FILE_PRINT
Command8=ID_APP_ABOUT
CommandCount=8

[MNU:IDR_MAINFRAME (Englisch (USA))]
Type=1
Class=?
Command1=ID_APP_EXIT
Command2=ID_EDIT_UNDO
Command3=ID_EDIT_CUT
Command4=ID_EDIT_COPY
Command5=ID_EDIT_PASTE
Command6=ID_VIEW_TOOLBAR
Command7=ID_VIEW_STATUS_BAR
Command8=ID_WINDOW_CASCADE
Command9=ID_WINDOW_TILE_HORZ
Command10=ID_WINDOW_ARRANGE
Command11=ID_APP_ABOUT
CommandCount=11

[MNU:IDR_CAS_DLTYPE (Englisch (USA))]
Type=1
Class=?
Command1=ID_APP_EXIT
Command2=ID_EDIT_UNDO
Command3=ID_EDIT_CUT
Command4=ID_EDIT_COPY
Command5=ID_EDIT_PASTE
Command6=ID_MEASURE_GETSPECTRUM
Command7=ID_MEASURE_GETDARKCURRENT
Command8=ID_MEASURE_MEASUREWITHEXTERNALTRIGGER
Command9=ID_MEASURE_GETDEVICEOPTIONS
Command10=ID_MEASURE_HIGHSPEED
Command11=ID_MEASURE_HIGHSPEEDMEASUREMENTWITHEXTERNALTRIGGER
Command12=IDD_SELECT_INTERFACE
Command13=ID_VIEW_TOOLBAR
Command14=ID_VIEW_STATUS_BAR
Command15=ID_WINDOW_CASCADE
Command16=ID_WINDOW_TILE_HORZ
Command17=ID_WINDOW_ARRANGE
Command18=ID_APP_ABOUT
CommandCount=18

[DLG:IDD_ABOUTBOX (Englisch (USA))]
Type=1
Class=?
ControlCount=4
Control1=IDC_STATIC,static,1342308480
Control2=IDC_STATIC,static,1342308352
Control3=IDOK,button,1342373889
Control4=IDC_STATIC,static,1342177283

[DLG:IDD_SELECT_INTERFACE (Englisch (USA))]
Type=1
Class=CConfDlg
ControlCount=6
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1342242816
Control3=IDC_STATIC,static,1342177280
Control4=IDC_CBINTERFACE,combobox,1344339971
Control5=IDC_CBINTERFACEOPTION,combobox,1344339971
Control6=IDC_STATIC,static,1342308352

[ACL:IDR_MAINFRAME (Englisch (USA))]
Type=1
Class=?
Command1=ID_EDIT_COPY
Command2=ID_FILE_NEW
Command3=ID_FILE_OPEN
Command4=ID_FILE_SAVE
Command5=ID_EDIT_PASTE
Command6=ID_EDIT_UNDO
Command7=ID_EDIT_CUT
Command8=ID_NEXT_PANE
Command9=ID_PREV_PANE
Command10=ID_MEASURE_GETSPECTRUM
Command11=ID_EDIT_COPY
Command12=ID_EDIT_PASTE
Command13=ID_EDIT_CUT
Command14=ID_EDIT_UNDO
CommandCount=14

