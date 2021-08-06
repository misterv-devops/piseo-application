// CAS_DLL_SampleView.cpp : implementation of the CCAS_DLL_SampleView class
//

#include "stdafx.h"
#include "CAS_DLL_Sample.h"

#include "CAS_DLL_SampleDoc.h"
#include "CAS_DLL_SampleView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleView

IMPLEMENT_DYNCREATE(CCAS_DLL_SampleView, CEditView)

BEGIN_MESSAGE_MAP(CCAS_DLL_SampleView, CEditView)
	//{{AFX_MSG_MAP(CCAS_DLL_SampleView)
		ON_REGISTERED_MESSAGE(UWM_PRINTF_MSG, OnPrintf)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleView construction/destruction

CCAS_DLL_SampleView::CCAS_DLL_SampleView()
{
	// TODO: add construction code here

}

CCAS_DLL_SampleView::~CCAS_DLL_SampleView()
{
}



/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleView drawing

void CCAS_DLL_SampleView::OnDraw(CDC* pDC)
{
	CCAS_DLL_SampleDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	// TODO: add draw code for native data here
}

/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleView diagnostics

#ifdef _DEBUG
void CCAS_DLL_SampleView::AssertValid() const
{
	CEditView::AssertValid();
}

void CCAS_DLL_SampleView::Dump(CDumpContext& dc) const
{
	CEditView::Dump(dc);
}

CCAS_DLL_SampleDoc* CCAS_DLL_SampleView::GetDocument() // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CCAS_DLL_SampleDoc)));
	return (CCAS_DLL_SampleDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleView message handlers


// receive message with ptr to CString in lParam
LRESULT CCAS_DLL_SampleView::OnPrintf(WPARAM, LPARAM lParam)
{
    CString* s = (CString*)lParam;

    printf( s );

    return 0;
}



// a very simple printf function that allows printing into the doc associated with the CEditView.
// CEditView has its own data storage. Use \r\n instead of single \n for line feed.
int CCAS_DLL_SampleView::printf( CString* s )
{
	int length;

	GetEditCtrl().SetSel( -1,0 );			// unselect just in case
	GetEditCtrl().ReplaceSel( *s );
	length = s->GetLength();
	delete s;					// careful, this is only possible since s was created on the heap !
	return length;
}      

