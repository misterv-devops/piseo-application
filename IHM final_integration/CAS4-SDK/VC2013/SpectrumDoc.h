#if !defined(AFX_SPECTRUMDOC_H__7681B09C_30C1_4268_AA33_B811C9763290__INCLUDED_)
#define AFX_SPECTRUMDOC_H__7681B09C_30C1_4268_AA33_B811C9763290__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// SpectrumDoc.h : header file
//


#include <afxtempl.h>


/////////////////////////////////////////////////////////////////////////////
// CSpectrumDoc document

class CSpectrumDoc : public CDocument
{


    friend class CSpectrumView;

protected:
	CSpectrumDoc();           // protected constructor used by dynamic creation
	DECLARE_DYNCREATE(CSpectrumDoc)
	
	int CASID;												// the device handle, will be stored by open

	void TestLibFileCallingAllMethods();

// Attributes
public:

	CArray<double, double&> SpecData;			// here we will store the spec data

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSpectrumDoc)
	public:
	protected:
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CSpectrumDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

	// Generated message map functions
protected:
	int MeasureHighSpeed( int casDevHnd );
	void OpenCas( int& casDeviceHandle );
	int CreateDeviceHandle();
	int printf( const wchar_t* c_str, ... );

	//{{AFX_MSG(CSpectrumDoc)
	afx_msg void OnMeasureGetSpectrum();
	afx_msg void OnMeasureGetDarkcurrent();
	afx_msg void OnMeasureGetDeviceOptions();
	afx_msg void OnMeasureGetColormetricValues( int casDevHandle );
	afx_msg void OnMeasureWithExternalTrigger();
	afx_msg LRESULT OnOpenCasDevice(WPARAM, LPARAM lParam);
	afx_msg void OnMeasureHighspeed();
	afx_msg void OnMeasureHighspeedWithExternalTrigger();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SPECTRUMDOC_H__7681B09C_30C1_4268_AA33_B811C9763290__INCLUDED_)
