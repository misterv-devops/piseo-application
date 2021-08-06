#if !defined(AFX_SPECTRUMVIEW_H__CDA7A0C0_EE6A_4A24_A428_5A58BDA6B31C__INCLUDED_)
#define AFX_SPECTRUMVIEW_H__CDA7A0C0_EE6A_4A24_A428_5A58BDA6B31C__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// SpectrumView.h : header file
//

#include "SpectrumDoc.h"

/////////////////////////////////////////////////////////////////////////////
// CSpectrumView view

class CSpectrumView : public CView
{
protected:
	CSpectrumView();           // protected constructor used by dynamic creation
	DECLARE_DYNCREATE(CSpectrumView)

// Attributes
public:

// Operations
public:
	CSpectrumDoc* GetDocument( ) const;

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSpectrumView)
	protected:
	virtual void OnDraw(CDC* pDC);      // overridden to draw this view
	//}}AFX_VIRTUAL

// Implementation
protected:
	virtual ~CSpectrumView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

	// Generated message map functions
protected:
	//{{AFX_MSG(CSpectrumView)
		// NOTE - the ClassWizard will add and remove member functions here.
	afx_msg LRESULT OnOpenCasDevice(WPARAM wParam, LPARAM lParam);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SPECTRUMVIEW_H__CDA7A0C0_EE6A_4A24_A428_5A58BDA6B31C__INCLUDED_)
