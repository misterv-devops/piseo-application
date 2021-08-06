#if !defined(AFX_SPECTRUMFRAME_H__47CD691F_9FB1_40B7_AAFD_10C9AB12A011__INCLUDED_)
#define AFX_SPECTRUMFRAME_H__47CD691F_9FB1_40B7_AAFD_10C9AB12A011__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// SpectrumFrame.h : header file
//


//#include "CAS_DLL_SampleDoc.h"


/////////////////////////////////////////////////////////////////////////////
// CSpectrumFrame frame

class CSpectrumFrame : public CMDIChildWnd
{
	DECLARE_DYNCREATE(CSpectrumFrame)
protected:
	CSpectrumFrame();           // protected constructor used by dynamic creation

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSpectrumFrame)
	protected:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	//}}AFX_VIRTUAL

// Implementation
protected:
	virtual ~CSpectrumFrame();

#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

	// Generated message map functions
	//{{AFX_MSG(CSpectrumFrame)
		// NOTE - the ClassWizard will add and remove member functions here.

	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SPECTRUMFRAME_H__47CD691F_9FB1_40B7_AAFD_10C9AB12A011__INCLUDED_)
