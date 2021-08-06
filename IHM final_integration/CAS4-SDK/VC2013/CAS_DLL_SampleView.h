// CAS_DLL_SampleView.h : interface of the CCAS_DLL_SampleView class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_CAS_DLL_SAMPLEVIEW_H__E35F6418_21B2_4AE4_B79C_BCBBA53FFA36__INCLUDED_)
#define AFX_CAS_DLL_SAMPLEVIEW_H__E35F6418_21B2_4AE4_B79C_BCBBA53FFA36__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000



// used together with the associated CDocument for simple text output.
class CCAS_DLL_SampleView : public CEditView
{
protected: // create from serialization only
	CCAS_DLL_SampleView();
	DECLARE_DYNCREATE(CCAS_DLL_SampleView)

// Attributes
public:
	CCAS_DLL_SampleDoc* GetDocument();

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CCAS_DLL_SampleView)
	public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	protected:
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CCAS_DLL_SampleView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:
	int printf( CString* s );

// Generated message map functions
protected:
	//{{AFX_MSG(CCAS_DLL_SampleView)
	    afx_msg LRESULT OnPrintf(WPARAM , LPARAM lParam);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in CAS_DLL_SampleView.cpp
inline CCAS_DLL_SampleDoc* CCAS_DLL_SampleView::GetDocument()
   { return (CCAS_DLL_SampleDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CAS_DLL_SAMPLEVIEW_H__E35F6418_21B2_4AE4_B79C_BCBBA53FFA36__INCLUDED_)
