// CAS_DLL_SampleDoc.cpp : implementation of the CCAS_DLL_SampleDoc class
//

#include "stdafx.h"
#include "CAS_DLL_Sample.h"

#include "CAS_DLL_SampleDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


#include "CAS4.h"


/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleDoc

IMPLEMENT_DYNCREATE(CCAS_DLL_SampleDoc, CDocument)

BEGIN_MESSAGE_MAP(CCAS_DLL_SampleDoc, CDocument)
	//{{AFX_MSG_MAP(CCAS_DLL_SampleDoc)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleDoc construction/destruction

CCAS_DLL_SampleDoc::CCAS_DLL_SampleDoc()
{
	// TODO: add one-time construction code here

}

CCAS_DLL_SampleDoc::~CCAS_DLL_SampleDoc()
{
}


/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleDoc diagnostics

#ifdef _DEBUG
void CCAS_DLL_SampleDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CCAS_DLL_SampleDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleDoc commands



// set title to dll file name and version number
void CCAS_DLL_SampleDoc::SetTitle(LPCTSTR lpszTitle) 
{
	// TODO: Add your specialized code here and/or call the base class
	
    char fname[_MAX_FNAME];
    char ext[_MAX_EXT];
    char ch[_MAX_PATH];

    CString s, t;

    casGetDLLFileName( ch, _MAX_PATH );

	_splitpath_s(ch, NULL, 0, NULL, 0, fname, _MAX_FNAME, ext, _MAX_EXT);

    s = fname;  s += ext;
    
    t = "   Version ";
    t += casGetDLLVersionNumber( ch, _MAX_PATH );

    s += t;

    lpszTitle = s;
    
    CDocument::SetTitle(lpszTitle);
}



BOOL CCAS_DLL_SampleDoc::IsModified()
{
    return false;						// we don't want the save file dialog to bother on close 
}

