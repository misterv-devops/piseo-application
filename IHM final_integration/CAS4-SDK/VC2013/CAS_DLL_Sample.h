/***********************************************************************************************
 *                                                                                             *
 *  $RCSfile: CAS_DLL_Sample.h,v $                                                             *
 *                                                                                             *
 *              Sample Application demonstrating the usage of the CAS DLL                      *
 *                                                                                             *
 *  Copyright:  (C) 2004..2010 Instrument Systems Optische Messtechnik GmbH, Munich, Germany   *
 *                                                                                             *
 *  Authors:    Christopher Lang, Sebastian Modersohn                                          *
 *                                                                                             *
 ***********************************************************************************************
 *                                                                                             *
 *  $Id: CAS_DLL_Sample.h,v 1.4 2004/11/05 17:30:00 cl Exp $                                   *
 *                                                                                             *
 *  See CSpectrumDoc:                                                                          *
 *                                                                                             *
 *		OnTestMeasureMeasureRegular();                                                         *
 *		OnTestMeasureDarkcurrent();                                                            *
 *		OnRealdeviceMeasureGetDarkcurrent();                                                   *
 *		OnRealdeviceMeasureGetSpectrum();                                                      *
 *		OnRealdeviceMeasureGetDeviceOptions();                                                 *
 *		OnTestMeasureGetColormetricValues();                                                   *
 *                                                                                             *
 *  Make sure the Spectrum Window stays focussed for the menue to work.                        *
 *                                                                                             *
 *                                                                                             *
 ***********************************************************************************************/

// Change history:
 									      
/*
 *
 * $Log: CAS_DLL_Sample.h,v $
 * Revision 2.0  2010/06/07 17:05:00  sm
 * Update to CAS DLL version 4
 * introduced casInterfaceOption, redesigned ConfDlg 
 * to dynamic list of interfaces and options
 * replaced calls of deprecated methods
 *
 * $Log: CAS_DLL_Sample.h,v $
 * Revision 1.4  2004/11/05 17:30:00  cl
 * added high speed measurement
 *
 * Revision 1.3  2004/07/16 10:36:35  cl
 * seperated open device calls
 *
 * Revision 1.2  2004/07/13 17:09:09  cl
 * added file name dialogs
 *
 * Revision 1.1.1.1  2004/07/13 08:20:41  cl
 * initial import of cas dll sample application
 *
 *
 */


 
/* some doxygen directives */
/** \file C_xy.z */

// CAS_DLL_Sample.h : main header file for the CAS_DLL_SAMPLE application
//

#if !defined(AFX_CAS_DLL_SAMPLE_H__7FEE6F32_CECA_4DDD_A1E9_7AA68A74529E__INCLUDED_)
#define AFX_CAS_DLL_SAMPLE_H__7FEE6F32_CECA_4DDD_A1E9_7AA68A74529E__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CCAS_DLL_SampleApp:
// See CAS_DLL_Sample.cpp for the implementation of this class
//

#define UWM_OPENCASDEVICE _T("UWM_OPENCASDEVICE-{24EE6DEA-112E-4e2c-B067-88F38D63F050}")
static const UINT UWM_OPENCASDEVICE_MSG = ::RegisterWindowMessage(UWM_OPENCASDEVICE);


extern CString configFilename;						// holds the configuration filename
extern CString calibFilename;    					// holds the calibration filename
extern int casInterface;							// holds the Interface # for the spectrometer
extern int casInterfaceOption;						// holds the Interface Option # for the spectrometer



class CCAS_DLL_SampleApp : public CWinApp
{
public:
	CCAS_DLL_SampleApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CCAS_DLL_SampleApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation
	//{{AFX_MSG(CCAS_DLL_SampleApp)
	afx_msg void OnAppAbout();
	afx_msg void OnSelectInterface();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};



/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CAS_DLL_SAMPLE_H__7FEE6F32_CECA_4DDD_A1E9_7AA68A74529E__INCLUDED_)
