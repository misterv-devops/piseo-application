// SpectrumFrame.cpp : implementation file
//

#include "stdafx.h"
#include "CAS_DLL_Sample.h"
#include "SpectrumFrame.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CSpectrumFrame

IMPLEMENT_DYNCREATE(CSpectrumFrame, CMDIChildWnd)

CSpectrumFrame::CSpectrumFrame()
{
}

CSpectrumFrame::~CSpectrumFrame()
{
}

BEGIN_MESSAGE_MAP(CSpectrumFrame, CMDIChildWnd)
	//{{AFX_MSG_MAP(CSpectrumFrame)
		// NOTE - the ClassWizard will add and remove mapping macros here.
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()





/////////////////////////////////////////////////////////////////////////////
// CChildFrame diagnostics

#ifdef _DEBUG
void CSpectrumFrame::AssertValid() const
{
	CMDIChildWnd::AssertValid();
}

void CSpectrumFrame::Dump(CDumpContext& dc) const
{
	CMDIChildWnd::Dump(dc);
}

#endif //_DEBUG






/////////////////////////////////////////////////////////////////////////////
// CSpectrumFrame message handlers


BOOL CSpectrumFrame::PreCreateWindow(CREATESTRUCT& cs) 
{
    if( CMDIChildWnd::PreCreateWindow(cs) )
	cs.style &= ~WS_SYSMENU;									// remove close etc. buttoms from windows
    else return false;
    
    return true;
}
