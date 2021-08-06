// ConfDlg.cpp : implementation file
//

#include "stdafx.h"
#include "CAS_DLL_Sample.h"
#include "ConfDlg.h"
#include "CAS4.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CConfDlg dialog


CConfDlg::CConfDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CConfDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CConfDlg)
	//}}AFX_DATA_INIT
}


void CConfDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CConfDlg)
	DDX_Control(pDX, IDC_CBINTERFACEOPTION, m_CBInterfaceOption);
	DDX_Control(pDX, IDC_CBINTERFACE, m_CBInterface);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CConfDlg, CDialog)
	//{{AFX_MSG_MAP(CConfDlg)
	ON_CBN_SELCHANGE(IDC_CBINTERFACE, OnSelchangeCBInterface)
	ON_CBN_SELCHANGE(IDC_CBINTERFACEOPTION, OnSelchangeCBInterfaceOption)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CConfDlg message handlers

BOOL CConfDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	int i;
	int count;
	int idx;
	int SelectedIdx;
	char buf[256];
	CString cs;

	SelectedIdx = -1;
	count = casGetDeviceTypes();
	for (i = 0; i < count; i++) {
		cs = casGetDeviceTypeName(i, buf, 255);
		if (strlen(buf)>0) {
			idx = m_CBInterface.AddString(cs);
			m_CBInterface.SetItemData(idx, i);

			if (i == SelectedInterface) {
				SelectedIdx = idx;
			}
		}
	}

	if (SelectedIdx >= 0) {
		m_CBInterface.SetCurSel(SelectedIdx);
		OnSelchangeCBInterface();
	}

	return TRUE;
}

void CConfDlg::OnSelchangeCBInterface()
{
	int iface;
	int SelectedOptionIdx;

	m_CBInterfaceOption.ResetContent();

	iface = m_CBInterface.GetCurSel();
	if (iface >= 0) {
		SelectedInterface = int(m_CBInterface.GetItemData(iface));

		int i;
		int count;
		int optionID;
		char buf[256];
		CString cs;

		SelectedOptionIdx = -1;
		count = casGetDeviceTypeOptions(SelectedInterface);
		for (i = 0; i < count; i++) {
			//options are not equivalent to indices!
			optionID = casGetDeviceTypeOption(SelectedInterface, i);
			if (optionID == SelectedInterfaceOption) SelectedOptionIdx = i;

			cs = casGetDeviceTypeOptionName(SelectedInterface, i, buf, 255);
			m_CBInterfaceOption.AddString(cs);
			m_CBInterfaceOption.SetItemData(i, optionID);
		}

		if (SelectedOptionIdx >= 0) {
			m_CBInterfaceOption.SetCurSel(SelectedOptionIdx);
		}
		else {
			if (count > 0) {
				SelectedInterfaceOption = int(m_CBInterfaceOption.GetItemData(0));
				m_CBInterfaceOption.SetCurSel(0);
			}
			else {
				SelectedInterfaceOption = 0;
			}
		}
		//disable Interface Option, if there's less than 2 options
		m_CBInterfaceOption.EnableWindow(count > 1);
	}
}

void CConfDlg::OnSelchangeCBInterfaceOption()
{
	SelectedInterfaceOption = m_CBInterfaceOption.GetCurSel();
	if (SelectedInterfaceOption >= 0) {
		SelectedInterfaceOption = int(m_CBInterfaceOption.GetItemData(SelectedInterfaceOption));
	}
	else {
		SelectedInterfaceOption = 0;
	}

}
