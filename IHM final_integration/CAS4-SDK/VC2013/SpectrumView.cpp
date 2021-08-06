// SpectrumView.cpp : implementation file
//

#include "stdafx.h"
#include "CAS_DLL_Sample.h"
#include "SpectrumView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


#include <float.h>


/////////////////////////////////////////////////////////////////////////////
// CSpectrumView

IMPLEMENT_DYNCREATE(CSpectrumView, CView)

CSpectrumView::CSpectrumView()
{
}

CSpectrumView::~CSpectrumView()
{
}


BEGIN_MESSAGE_MAP(CSpectrumView, CView)
	//{{AFX_MSG_MAP(CSpectrumView)
		// NOTE - the ClassWizard will add and remove mapping macros here.
	ON_REGISTERED_MESSAGE(UWM_OPENCASDEVICE_MSG, OnOpenCasDevice)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()



/////////////////////////////////////////////////////////////////////////////
// CSpectrumView drawing

void CSpectrumView::OnDraw(CDC* pDC)
{
    

    double maxY, minY;						// used to scale properly
    double scalefactY;
    double scalefactX;
    int height;							// the height of the client area
    int c;							// simple counter
	int offset;

    CSpectrumDoc* pDoc = GetDocument();
	INT_PTR size = pDoc->SpecData.GetSize();

    if( size <=0 ) return;					// nothing to draw
    
    // get maximum of y Data
    maxY = -DBL_MAX;
    minY = DBL_MAX;

    for( c=0; c<size; c++ )
    {    
	double data = pDoc->SpecData.GetAt(c);
	if( data > maxY ) maxY = data;
	if( data < minY ) minY = data;
    }
    
    CRect rect;
    GetClientRect(rect);
    
    height = rect.Height();
    
    CPen penStroke;												// create Windows Pen and apply it
    penStroke.CreatePen(PS_SOLID, 1, RGB(0,0,0));
    pDC->SelectObject(&penStroke);

    scalefactY = (height) / (maxY - minY);
    scalefactX = (double)rect.Width() / (double)size;

	offset = (int)(minY * scalefactY);									// scale to window, round properly
    pDC->MoveTo( 0, int((height+offset-(pDoc->SpecData.GetAt(0) * scalefactY))+0.5) );

    for( c=1; c<size; c++ )
    {
		double data = pDoc->SpecData.GetAt(c);
		pDC->LineTo( int(c * scalefactX+0.5), int((height+offset-(data * scalefactY))+0.5) );
    }
}




LRESULT CSpectrumView::OnOpenCasDevice(WPARAM wParam, LPARAM lParam)
{
	GetDocument()->OnOpenCasDevice(wParam, lParam);
	return 0;
}



/////////////////////////////////////////////////////////////////////////////
// CSpectrumView diagnostics

#ifdef _DEBUG
void CSpectrumView::AssertValid() const
{
	CView::AssertValid();
}

void CSpectrumView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}
#endif //_DEBUG



/////////////////////////////////////////////////////////////////////////////
// CSpectrumView message handlers

CSpectrumDoc* CSpectrumView::GetDocument() const// non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CSpectrumDoc)));
	return (CSpectrumDoc*)m_pDocument;
}


