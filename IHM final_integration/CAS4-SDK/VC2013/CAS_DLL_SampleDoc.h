// CAS_DLL_SampleDoc.h : interface of the CCAS_DLL_SampleDoc class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_CAS_DLL_SAMPLEDOC_H__FA1EA2AE_AC46_4D33_AFCD_FFDA7043F3CA__INCLUDED_)
#define AFX_CAS_DLL_SAMPLEDOC_H__FA1EA2AE_AC46_4D33_AFCD_FFDA7043F3CA__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


#define UWM_PRINTF _T("UWM_PRINTF-{56604B4A-680F-4785-B9BF-157FCA93B7DF}")
static const UINT UWM_PRINTF_MSG = ::RegisterWindowMessage(UWM_PRINTF);



// used with the associated CEditView for simple text output.
class CCAS_DLL_SampleDoc : public CDocument
{
protected: // create from serialization only
	CCAS_DLL_SampleDoc();
	DECLARE_DYNCREATE(CCAS_DLL_SampleDoc)

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CCAS_DLL_SampleDoc)
	public:
	virtual void SetTitle(LPCTSTR lpszTitle);
	//}}AFX_VIRTUAL

// Implementation
public:
	BOOL IsModified();
	virtual ~CCAS_DLL_SampleDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CCAS_DLL_SampleDoc)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
    	

	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CAS_DLL_SAMPLEDOC_H__FA1EA2AE_AC46_4D33_AFCD_FFDA7043F3CA__INCLUDED_)
