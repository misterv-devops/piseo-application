// CAS_DLL_Sample.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "CAS_DLL_Sample.h"

#include "MainFrm.h"
#include "ChildFrm.h"
#include "CAS_DLL_SampleDoc.h"
#include "CAS_DLL_SampleView.h"

#include "SpectrumDoc.h"
#include "SpectrumView.h"
#include "SpectrumFrame.h"
#include <commdlg.h>
#include "ConfDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


// make sure the cas4.lib file is in the path and the DLL is in a path where DLLs are
// If that is the case using the DLL is that easy. (make sure to set your compilers lib path
// correctly)

#include "cas4.h"


// Globals

CString configFilename;						// holds the configuration filename
CString calibFilename;    					// holds the calibration filename
int casInterface;						// holds the Interface # for the spectrometer
int casInterfaceOption;				// holds the Interface Option # for the spectrometer



/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleApp

BEGIN_MESSAGE_MAP(CCAS_DLL_SampleApp, CWinApp)
	//{{AFX_MSG_MAP(CCAS_DLL_SampleApp)
	ON_COMMAND(ID_APP_ABOUT, OnAppAbout)
	ON_COMMAND(IDD_SELECT_INTERFACE, OnSelectInterface)
	//}}AFX_MSG_MAP
	// Standard file based document commands
	ON_COMMAND(ID_FILE_NEW, CWinApp::OnFileNew)
	ON_COMMAND(ID_FILE_OPEN, CWinApp::OnFileOpen)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleApp construction

CCAS_DLL_SampleApp::CCAS_DLL_SampleApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
	casInterface = InterfaceTest;					// default is test interface
	casInterfaceOption = 0;
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CCAS_DLL_SampleApp object

CCAS_DLL_SampleApp theApp;



/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleApp initialization

BOOL CCAS_DLL_SampleApp::InitInstance()
{
	// Standard initialization
	// If you are not using these features and wish to reduce the size
	//  of your final executable, you should remove from the following
	//  the specific initialization routines you do not need.

	// Change the registry key under which our settings are stored.
	// TODO: You should modify this string to be something appropriate
	// such as the name of your company or organization.
	SetRegistryKey(_T("Local AppWizard-Generated Applications"));

	LoadStdProfileSettings();  // Load standard INI file options (including MRU)

	// Register the application's document templates.  Document templates
	//  serve as the connection between documents, frame windows and views.


	// create main MDI Frame window
	CMainFrame* pMainFrame = new CMainFrame;
	if (!pMainFrame->LoadFrame(IDR_MAINFRAME))
		return FALSE;
	m_pMainWnd = pMainFrame;


	CMultiDocTemplate* pDocTemplate;
	pDocTemplate = new CMultiDocTemplate(
		IDR_CAS_DLTYPE,
		RUNTIME_CLASS(CCAS_DLL_SampleDoc),
		RUNTIME_CLASS(CChildFrame), // custom MDI child frame
		RUNTIME_CLASS(CCAS_DLL_SampleView));
	AddDocTemplate(pDocTemplate);

	pDocTemplate->OpenDocumentFile(NULL);


	CMultiDocTemplate* pSpecDocTemplate;
	pSpecDocTemplate = new CMultiDocTemplate(
		IDR_CAS_SPECTRUMTYPE,
		RUNTIME_CLASS(CSpectrumDoc),
		RUNTIME_CLASS(CSpectrumFrame), // custom MDI child frame
		RUNTIME_CLASS(CSpectrumView));
	AddDocTemplate(pSpecDocTemplate);

	pSpecDocTemplate->OpenDocumentFile(NULL);


	// Parse command line for standard shell commands, DDE, file open
	// CCommandLineInfo cmdInfo;
	// ParseCommandLine(cmdInfo);

	// Dispatch commands specified on the command line
	// if (!ProcessShellCommand(cmdInfo))
	//	return FALSE;


	// The main window has been initialized, so show and update it.
	pMainFrame->ShowWindow(m_nCmdShow);
	pMainFrame->UpdateWindow();


	// MFC tech note 22
	// http://groups.google.de/groups?hl=de&lr=&ie=UTF-8&selm=52r8a5%244aj%40login.freenet.columbus.oh.us
	AfxGetMainWnd()->PostMessage(WM_COMMAND, ID_WINDOW_TILE_HORZ);



	// let the user choose the ini and isc files
	{
		CFileDialog fd(true, L"*.ini", NULL, OFN_FILEMUSTEXIST | OFN_HIDEREADONLY | OFN_PATHMUSTEXIST,

			_T("Config Files (*.ini)|*.ini|All Files|*.*||"), NULL);

		fd.m_ofn.lpstrTitle = L"Please choose a CONFIGURATION file:";

		if (fd.DoModal() != IDOK)
		{
			AfxGetMainWnd()->MessageBox(L"You must choose an existing ini file ! exiting...", L"Note:", MB_ICONEXCLAMATION);
			exit(0);
		}
		configFilename = fd.GetPathName();
	}

	{
		CFileDialog fd(true, L"*.isc", NULL, OFN_FILEMUSTEXIST | OFN_HIDEREADONLY | OFN_PATHMUSTEXIST,

			_T("Calibration Files (*.isc)|*.isc|All Files|*.*||"), NULL);

		fd.m_ofn.lpstrTitle = L"Please choose a CALIBRATION file:";


		if (fd.DoModal() != IDOK)
		{
			AfxGetMainWnd()->MessageBox(L"You must choose a calibration file ! exiting...", L"Note:", MB_ICONEXCLAMATION);
			exit(0);
		}
		calibFilename = fd.GetPathName();
	}

	CString* s;  s = new CString;				// the message receiver deletes the string
	s->Format(L"Using config file: %s\r\n", (LPCTSTR)configFilename);
	AfxGetMainWnd()->SendMessageToDescendants(UWM_PRINTF_MSG, 0, (LPARAM)s);


	s = new CString;
	s->Format(L"Using calibration file: %s\r\n", (LPCTSTR)calibFilename);
	AfxGetMainWnd()->SendMessageToDescendants(UWM_PRINTF_MSG, 0, (LPARAM)s);

	s = new CString;
	s->Format(L"Make sure Spectrum Window is focused for the menu to be active\r\n");
	AfxGetMainWnd()->SendMessageToDescendants(UWM_PRINTF_MSG, 0, (LPARAM)s);


	// make sure the user selects the cas Interface
	OnSelectInterface();

	return TRUE;
}


/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

	// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

	// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	// No message handlers
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
	// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

// App command to run the dialog
void CCAS_DLL_SampleApp::OnAppAbout()
{
	CAboutDlg aboutDlg;
	aboutDlg.DoModal();
}

/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleApp message handlers


void CCAS_DLL_SampleApp::OnSelectInterface()
{
	CConfDlg confDlg;
	confDlg.SelectedInterface = casInterface;			// write current interface and option to Dialog
	confDlg.SelectedInterfaceOption = casInterfaceOption;

	if (confDlg.DoModal() == IDOK) {

		casInterface = confDlg.SelectedInterface;			// extract interface and option from Dialog
		casInterfaceOption = confDlg.SelectedInterfaceOption;

		CString* s;
		s = new CString;
		wchar_t wbuf[256];
		casGetDeviceTypeNameW(casInterface, wbuf, 255);
		s->Format(L"Interface %s selected\r\n", wbuf);
		AfxGetMainWnd()->SendMessageToDescendants(UWM_PRINTF_MSG, 0, (LPARAM)s);

		// make CSpectrumDoc open the device properly
		AfxGetMainWnd()->SendMessageToDescendants(UWM_OPENCASDEVICE_MSG, 0, 0);
	}

}
