#if !defined(AFX_CONFDLG_H__B5675D20_F3A2_416F_A3B6_C140BF665061__INCLUDED_)
#define AFX_CONFDLG_H__B5675D20_F3A2_416F_A3B6_C140BF665061__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ConfDlg.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CConfDlg dialog

class CConfDlg : public CDialog
{
// Construction
public:
	CConfDlg(CWnd* pParent = NULL);   // standard constructor

// Dialog Data
	//{{AFX_DATA(CConfDlg)
	enum { IDD = IDD_SELECT_INTERFACE };
	CComboBox	m_CBInterfaceOption;
	CComboBox	m_CBInterface;
	//}}AFX_DATA
	int SelectedInterface;
	int SelectedInterfaceOption;

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CConfDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(CConfDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSelchangeCBInterface();
	afx_msg void OnSelchangeCBInterfaceOption();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CONFDLG_H__B5675D20_F3A2_416F_A3B6_C140BF665061__INCLUDED_)
