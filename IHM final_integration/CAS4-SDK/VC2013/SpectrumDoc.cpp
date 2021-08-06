// SpectrumDoc.cpp : implementation file
//

#include "stdafx.h"
#include "math.h"
#include "CAS_DLL_Sample.h"
#include "SpectrumDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


#include "CAS_DLL_SampleDoc.h"
#include "CAS_DLL_Sample.h"
//#include <string.h>



// make sure the CAS4.lib file is in the path and the DLL is in a path where DLLs are
// If that is the case using the DLL is that easy. (make sure to set your compilers lib path
// correctly)

#include "CAS4.h"


// to test the CAS_TM30 calculation DLL with this sample, enable the define below and
// also make sure that CAS_TM30.h and the appropriate CAS_TM30x86.lib or CAS_TM30x64.lib file
// is added to the project
//#define TM30
#ifdef TM30
#include "CAS_TM30.h"
#endif


// IMPORTANT: also the .INI (Configuration) and ISC(Calibration) file need to be in the
// exe directory !!!




/////////////////////////////////////////////////////////////////////////////
// CSpectrumDoc

IMPLEMENT_DYNCREATE(CSpectrumDoc, CDocument)

CSpectrumDoc::CSpectrumDoc()
{
}


CSpectrumDoc::~CSpectrumDoc()
{
}


BEGIN_MESSAGE_MAP(CSpectrumDoc, CDocument)
	//{{AFX_MSG_MAP(CSpectrumDoc)
	ON_COMMAND(ID_MEASURE_GETSPECTRUM, OnMeasureGetSpectrum)
	ON_COMMAND(ID_MEASURE_GETDARKCURRENT, OnMeasureGetDarkcurrent)
	ON_COMMAND(ID_MEASURE_GETDEVICEOPTIONS, OnMeasureGetDeviceOptions)
	/*ON_COMMAND(ID_MEASURE_GETCOLORIMETRICVALUES, OnMeasureGetColormetricValues)*/
	ON_COMMAND(ID_MEASURE_MEASUREWITHEXTERNALTRIGGER, OnMeasureWithExternalTrigger)
	//ON_REGISTERED_MESSAGE(UWM_OPENCASDEVICE_MSG, OnOpenCasDevice)
	ON_COMMAND(ID_MEASURE_HIGHSPEED, OnMeasureHighspeed)
	ON_COMMAND(ID_MEASURE_HIGHSPEEDMEASUREMENTWITHEXTERNALTRIGGER, OnMeasureHighspeedWithExternalTrigger)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSpectrumDoc diagnostics

#ifdef _DEBUG
void CSpectrumDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CSpectrumDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CSpectrumDoc serialization





/////////////////////////////////////////////////////////////////////////////
// CSpectrumDoc commands

void CSpectrumDoc::OnMeasureGetSpectrum()
{

	int casDeviceHandle;				// the handle that we need for all other function calls
	CString s;							// to store some text messages

	int c;								// simple counter variable    
	int x;								// used to store different pixel number related stuff
	int deadPix;						// the number of dead pixels at the begining of the CAS array
	int vis;							// # of visible pixels


	// here we will demonstrate how a simple measurement is done using the CAS DLL.


	// the entire sequence of cas commands is secured by a simple message box exception
	try
	{

		printf(L"\r\n");

		// the device is opend after the app is started or a different device is selected. Init
		// Instance sends a message. See OpenCas( casDeviceHandle );

		casDeviceHandle = CASID;									// lets get the device handle from the variable

		// make sure to set the integration time
		printf(L"executing casSetMeasurementParameter with mpidIntegrationTime\r\n");
		casSetMeasurementParameter(casDeviceHandle, mpidIntegrationTime, 20); // set 20 ms


		// set number of averages for the measurement
		printf(L"executing casSetMeasurementParameter with mpidAverages\r\n");
		casSetMeasurementParameter(casDeviceHandle, mpidAverages, 1);	// no averaging


		// get # of dead pixels at the beginning of the detector area
		printf(L"executing casGetDeviceParameter with dpidDeadPixels\r\n");
		deadPix = int(casGetDeviceParameter(casDeviceHandle, dpidDeadPixels));
		s.Format(L"determinded DeadPixels: %d\r\n", deadPix);
		printf(s);

		// get number of Pixels the device supports
		x = int(casGetDeviceParameter(casDeviceHandle, dpidPixels));
		vis = int(casGetDeviceParameter(casDeviceHandle, dpidVisiblePixels));

		// you do not have to do a DC measurement before each spectrum, this is for demo only, if necessary you may however

		//casSetShutter( casDeviceHandle, casShutterClose ); 


		//casBackgroundMeasure( casDeviceHandle );


		//casSetShutter( casDeviceHandle, casShutterOpen ); 


		casMeasure(casDeviceHandle);


		SpecData.SetSize(vis);									// resize array that hold the spectrum data

		for (c = 0; c < vis; c++)										// transfer spectrum data to array
		{
			SpecData[c] = casGetData(casDeviceHandle, c + deadPix);
			// use casGetXArray to retrieve wave length information 
		}


		OnMeasureGetColormetricValues(casDeviceHandle);			// the On... is just in case we want to use it in the menu later...	

		printf(L"plotting spectrum...\r\n");


		// tell the spectrum view to display updated data
		UpdateAllViews(NULL);


	} // try // end of area protected by exception



	// exception handler to be used in case an error ocurred
	catch (CString str)
	{
		printf(L"ERROR: " + str);
	}


}

// send the message to the text output doc. The receiver deletes the CString.
int CSpectrumDoc::printf(const wchar_t* c_str, ...)
{
	va_list arguments;						// A place to store the list of arguments
	va_start(arguments, c_str);					// Initializing arguments to store all values

	int length;
	CString* s;

	s = new CString;

	s->FormatV(c_str, arguments);
	length = s->GetLength();

	AfxGetMainWnd()->SendMessageToDescendants(UWM_PRINTF_MSG, 0, (LPARAM)s);

	va_end(arguments);						// Cleans up the list
	return length;
}



void CSpectrumDoc::OnMeasureGetDarkcurrent()
{



	// here we will measure the CAS dark current and display the resulting dark spectrum



	int casDeviceHandle;				// the handle that we need for all other function calls
	CString s;							// to store some text messages

	int c;								// simple counter variable    
	int x;								// used to store different pixel number related stuff
	int deadPix;						// the number of dead pixels at the begining of the CAS array
	int vis;							// # of visible pixels


	// the entire sequence of cas commands is secured by a simple message box exception
	try
	{

		printf(L"\r\n");


		// the device is opend after the app is started or a different device is selected. Init
		// Instance sends a message. See OpenCas( casDeviceHandle );

		casDeviceHandle = CASID;									// lets get the device handle from the variable


		// make sure to set the integration time
		printf(L"executing casSetMeasurementParameter with mpidIntegrationTime\r\n");
		casSetMeasurementParameter(casDeviceHandle, mpidIntegrationTime, 20); // set 20 ms


		// set number of averages for the measurement
		printf(L"executing casSetMeasurementParameter with mpidAverages\r\n");
		casSetMeasurementParameter(casDeviceHandle, mpidAverages, 1);	// no averaging

		// get # of dead pixels at the beginning of the detector area
		printf(L"executing casGetDeviceParameter with dpidDeadPixels\r\n");
		deadPix = int(casGetDeviceParameter(casDeviceHandle, dpidDeadPixels));
		s.Format(L"determinded DeadPixels: %d\r\n", deadPix);
		printf(s);

		// get number of Pixels the device supports
		x = int(casGetDeviceParameter(casDeviceHandle, dpidPixels));
		vis = int(casGetDeviceParameter(casDeviceHandle, dpidVisiblePixels));

		casSetShutter(casDeviceHandle, casShutterClose);


		casMeasureDarkCurrent(casDeviceHandle);


		casSetShutter(casDeviceHandle, casShutterOpen);


		SpecData.SetSize(vis);									// resize array that hold the spectrum data

		for (c = 0; c < vis; c++)										// transfer spectrum data to array
		{
			SpecData[c] = casGetData(casDeviceHandle, c + deadPix);
			// use casGetXArray to retrieve wave length information 
		}

		printf(L"plotting spectrum...\r\n");


		// tell the spectrum view to display updated data
		UpdateAllViews(NULL);


	} // try // end of area protected by exception



	// exception handler to be used in case an error ocurred
	catch (CString str)
	{
		printf(L"ERROR: " + str);
	}
}



// loop through the available interfaces and print coresponding options
void CSpectrumDoc::OnMeasureGetDeviceOptions()
{

	char ch[256];						// this is a bit big, but to be safe...
	int c, d;
	int options;
	CString s, t;

	int nr_ifaces = casGetDeviceTypes();			// get number of interfaces

	printf(L"\r\n");

	for (c = 0; c < nr_ifaces; c++)
	{

		casGetDeviceTypeName(c, ch, 255);			// get Interface type name

		options = casGetDeviceTypeOptions(c);			// get # of options of interface
		CA2W wch(ch);
		s.Format(wch);
		t.Format(L", Interface id = %d\r\n", c);		// add Interface index to string 
		s += t;
		printf(s);

		if (options <= 0)
			printf(L"   No options\r\n");

		for (d = 0; d < options; d++)				// fetch options of each interface
		{
			casGetDeviceTypeOptionName(c, casGetDeviceTypeOption(c, d), ch, 255);
			printf(L"   ");					// some space
			CA2W wch(ch);
			printf(wch);
			printf(L"\r\n");
		}

		printf(L"\r\n");

	}
}



void CSpectrumDoc::OnMeasureGetColormetricValues(int casDevHandle)
{
	enum { MAXBUFSIZE = 256 };									// needs ome buffer because the cas... functions
	cas_wchar wunit[MAXBUFSIZE];

	CString s;
	double photInt;
	double radInt;
	int result;
	

	printf(L"\r\n");

	if (casDevHandle >= 0)										// make sure handle is valid
	{

		result = casColorMetric(casDevHandle);
		if (result >= 0)										// calculate colormetric values
		{
			casGetPhotIntW(casDevHandle, &photInt, wunit, MAXBUFSIZE - 1);		// all pChars are returned 0 terminated from the DLL
			printf(L"Photometric integral: %lg %s\r\n", photInt, wunit);

			casGetRadIntW(casDevHandle, &radInt, wunit, MAXBUFSIZE - 1);		// all pChars are returned 0 terminated from the DLL
			printf(L"Radiometric integral: %lg %s\r\n", radInt, wunit);
		}
		else
		{
			printf(L"casColorMetric returned error %d\r\n", result);
		}
#ifdef TM30
		result = tm30Perform(tiwCalculateTM30, casDevHandle, 0);
		if (result >= tiErrorNoError)
		{
			double tm30Result;
			result = tm30GetFloat(tiwTM30FidelityIndex, 0, 0, &tm30Result);
			if (result >= tiErrorNoError) printf(L"TM30 Rf: %.2f\r\n", tm30Result);
			result = tm30GetFloat(tiwTM30GamutIndex, 0, 0, &tm30Result);
			if (result >= tiErrorNoError) printf(L"TM30 Rg: %.2f\r\n", tm30Result);
		}
		else
		{
			printf(L"tm30Perform tiwCalculateTM30 returned error %d\r\n", result);
		}
#endif
	}
	else
	{
		printf(L"do a Measurement 1st to open the device\r\n");
	}
}



void CSpectrumDoc::OnMeasureWithExternalTrigger()
{
	int casDeviceHandle;										// the handle that we need for all other function calls
	CString s;													// to store some text messages

	int c;														// simple counter variable    
	int x;														// used to store different pixel number related stuff
	int deadPix;												// the number of dead pixels at the begining of the CAS array
	int vis;													// # of visible pixels


	// here we will demonstrate how a simple measurement is done using the CAS DLL witrh external trigger


	// the entire sequence of cas commands is secured by a simple message box exception
	try
	{

		printf(L"\r\n");

		// the device is opend after the app is started or a different device is selected. Init
		// Instance sends a message. See OpenCas( casDeviceHandle );

		casDeviceHandle = CASID;									// lets get the device handle from the variable


		// make sure to set the integration time
		printf(L"executing casSetMeasurementParameter with mpidIntegrationTime\r\n");
		casSetMeasurementParameter(casDeviceHandle, mpidIntegrationTime,
			casGetDeviceParameter(casDeviceHandle, dpidIntTimeMin)); // set to minimum integration time


		// set number of averages for the measurement
		printf(L"executing casSetMeasurementParameter with mpidAverages\r\n");
		casSetMeasurementParameter(casDeviceHandle, mpidAverages, 1);	// no averaging


		// get # of dead pixels at the beginning of the detector area
		printf(L"executing casGetDeviceParameter with dpidDeadPixels\r\n");
		deadPix = int(casGetDeviceParameter(casDeviceHandle, dpidDeadPixels));
		s.Format(L"determinded DeadPixels: %d\r\n", deadPix);
		printf(s);

		// get number of Pixels the device supports
		x = int(casGetDeviceParameter(casDeviceHandle, dpidPixels));
		vis = int(casGetDeviceParameter(casDeviceHandle, dpidVisiblePixels));


		// make sure to set a timeout, otherwise there will be an exception. Timeout 10 sec.
		casSetMeasurementParameter(casDeviceHandle, mpidTriggerTimeout, 10000);

		// select external trigger, positive edge triggered, pin1 DB9
		casSetMeasurementParameter(casDeviceHandle, mpidTriggerSource, trgFlipFlop);

		// reset & arm trigger 
		casSetDeviceParameter(casDeviceHandle, dpidLine1FlipFlop, 0);


		// measurement will start on 1-going edge on pin 1 on DB9
		printf(L"now please put a 5V trigger pulse on pin1 on 9-pin SUBD connector (timeout 10 sec)\r\n");


		// enable the toShowBusyState and toShowACQState trigger options; ideally dpidTriggerCapabilities should be checked
		casSetMeasurementParameter(casDeviceHandle, mpidTriggerOptions,
			casGetMeasurementParameter(casDeviceHandle, mpidTriggerOptions) && toShowBusyState && toShowACQState);

		casMeasure(casDeviceHandle);

		SpecData.SetSize(vis);									// resize array that holds the spectrum data
		for (c = 0; c < vis; c++)										// transfer spectrum data to array
		{
			SpecData[c] = casGetData(casDeviceHandle, c + deadPix);
			// use casGetXArray to retrieve wave length information 
		}

		printf(L"plotting spectrum...\r\n");


		// restore old trigger condition
		casSetMeasurementParameter(casDeviceHandle, mpidTriggerSource, trgSoftware);



		// tell the spectrum view to display updated data
		UpdateAllViews(NULL);


	} // try // end of area protected by exception



	// exception handler to be used in case an error ocurred
	catch (CString str)
	{
		printf(L"ERROR: " + str);
	}



}



// returns a cas device handle
int CSpectrumDoc::CreateDeviceHandle()
{
	int handle;
	cas_wchar wbuf[256];
	CString s;

	// 1st step is always to create a CAS device using casCreateDeviceEx
	s.Format(L"executing casCreateDeviceEx with Interface=%d and Option=%d\r\n", casInterface, casInterfaceOption);
	printf(L"executing casCreateDeviceEx\r\n");
	handle = casCreateDeviceEx(casInterface, casInterfaceOption);	// casInterface and -Option are global
	if (handle < 0) {											// if < 0 then device could not be opened

		casGetErrorMessageW(handle, wbuf, 255);
		s.Format(L"casCreateDeviceEx returned error code %d = %s!\r\n", handle, wbuf);
		printf(s);
	}
	else {
		printf(L"casCreateDeviceEx returned OK\r\n");
	}

	return handle;
}



void CSpectrumDoc::OpenCas(int& casDeviceHandle)
{

	enum { MAXBUFSIZE = 256 };									// needs some buffer because the cas... functions
	cas_wchar wbuf[MAXBUFSIZE];

	int result;

	CString s;
	


	// get the spectrometer device handle
	casDeviceHandle = CreateDeviceHandle();						// get the device from global			


	// this will set the configuraton filename. Make sure to add the path name but at least  ".\"  !!!
	if (configFilename != "") {
		printf(L"executing casSetConfigFileName\r\n");
		casSetDeviceParameterStringW(casDeviceHandle, dpidConfigFileName, configFilename.GetString());
	}
	else {
		throw(L"You must choose an ini file !\r\nexiting...");
	}



	// this will set the calibration filename. IMPORTANT: make sure to add the correct path
	// or at least .\ ( \ needs to be escaped with \ !)
	if (calibFilename != "") {
		printf(L"executing casSetCalibrationFileName\r\n");
		casSetDeviceParameterStringW(casDeviceHandle, dpidCalibFileName, calibFilename.GetString());
	}
	else {
		throw("You must choose an isc file !\r\nexiting...");
	}



	// finally we will initialize the hardware of the cas device
	printf(L"executing casInitialize\r\n");
	result = casInitialize(casDeviceHandle, InitForced);
	if (result < 0) {											// if < 0 then device could not be initialized

		casGetErrorMessageW(result, wbuf, MAXBUFSIZE - 1);
		s.Format(L"Error %d='%s' initialising CAS device !", result, wbuf);
		printf(s);
	}
	else {
		printf(L"casInitialize returned OK\r\n");
	}

}



LRESULT CSpectrumDoc::OnOpenCasDevice(WPARAM, LPARAM lParam)
{
	int tempID;

	if (CASID >= 0)											// if a device was created
	{
		tempID = CASID;
		CASID = -1;
		casDoneDevice(tempID);								// close the device just in case if it is open
		tempID = -1;
		printf(L"casDoneDevice\r\n");
	}

	OpenCas(tempID);
	CASID = tempID;											// store the handle

	TestLibFileCallingAllMethods();

	return 0;
}


void CSpectrumDoc::OnMeasureHighspeed()
{
	int casDeviceHandle;				// the handle that we need for all other function calls
	CString s;							// to store some text messages

	int c;							// simple counter variable
	int x;							// used to store different pixel number related stuff
	int deadPix;						// the number of dead pixels at the begining of the CAS array
	int vis;							// # of visible pixels


	// here we will demonstrate how a high speed measurement is done using the CAS DLL

	try								// protect block by exception	
	{
		printf(L"\r\n");


		// the device is opend after the app is started or a different device is selected. Init
		// Instance sends a message. See OpenCas( casDeviceHandle );

		casDeviceHandle = CASID;				// lets get the device handle from the variable


		// make sure to set the integration time
		printf(L"executing casSetMeasurementParameter with mpidIntegrationTime\r\n");
		casSetMeasurementParameter(casDeviceHandle, mpidIntegrationTime, 20); // set 20 ms


		// set number of averages for the measurement
		printf(L"executing casSetMeasurementParameter with mpidAverages\r\n");
		casSetMeasurementParameter(casDeviceHandle, mpidAverages, 1);	// no averaging


		// get # of dead pixels at the beginning of the detector area
		printf(L"executing casGetDeviceParameter with dpidDeadPixels\r\n");
		deadPix = int(casGetDeviceParameter(casDeviceHandle, dpidDeadPixels));
		s.Format(L"determinded DeadPixels: %d\r\n", deadPix);
		printf(s);

		// get number of Pixels the device supports
		x = int(casGetDeviceParameter(casDeviceHandle, dpidPixels));
		vis = int(casGetDeviceParameter(casDeviceHandle, dpidVisiblePixels));


		// you do not have to do a DC measurement before each spectrum, this is for demo only.
		//casSetShutter( casDeviceHandle, casShutterClose ); 
		//casBackgroundMeasure( casDeviceHandle );
		//casSetShutter( casDeviceHandle, casShutterOpen ); 


		MeasureHighSpeed(casDeviceHandle);			// do the measurement manually


		// and get the data

		SpecData.SetSize(vis);									// resize array that hold the spectrum data

		for (c = 0; c < vis; c++)										// transfer spectrum data to array
		{
			SpecData[c] = casGetData(casDeviceHandle, c + deadPix);
			// use casGetXArray to retrieve wave length information 
		}


		OnMeasureGetColormetricValues(casDeviceHandle);			// the On... is just in case we want to use it in the menu later...	

		printf(L"plotting spectrum...\r\n");


		// tell the spectrum view to display updated data
		UpdateAllViews(NULL);


	} // try // end of area protected by exception



	// exception handler to be used in case an error ocurred
	catch (CString str)
	{
		printf(L"ERROR: " + str);
	}

}



// use this way of measuring instead of casMeasure if you want to write your code as time efficient as possible.
// this way of measuring also allows your code to stay in control and do something else during the actual measurement.
int CSpectrumDoc::MeasureHighSpeed(int casDevHnd)
{

	// now the high speed portion of the code starts, it is actually straight forward

	printf(L"set digital output #1 to 0\r\n");		// in case we want to use this pin as a trigger for an external handler
	casSetDigitalOut(casDevHnd, 2, 0);		// set digital output pin 1 to 0

	printf(L"manually starting measurement with casStart\r\n");
	casStart(casDevHnd);				// manually start the measurement
	while (!casFIFOHasData(casDevHnd)); 		// idle until integration time is over, you should protect this by timeout to avoid an endless loop in the case of error

	///////////////////////////////////////////////////////
	// ok, integration is over you may disconnect your DUT
	// this could be triggered by toggling a digital output pin
	///////////////////////////////////////////////////////

	casSetDigitalOut(casDevHnd, 2, 1);		// set digital output pin 1 to 1

	// printf( "set digital output #1 to 1\r\n"  );


	// printf( "waiting until data is transfered from CCD\r\n"  );
	casGetFIFOData(casDevHnd);
	casSetDigitalOut(casDevHnd, 2, 0);		// all data were read from PCI card to RAM

	return 0;
}



void CSpectrumDoc::OnMeasureHighspeedWithExternalTrigger()
{

	int casDeviceHandle;										// the handle that we need for all other function calls
	CString s;													// to store some text messages

	int c;														// simple counter variable    
	int x;														// used to store different pixel number related stuff
	int deadPix;												// the number of dead pixels at the begining of the CAS array
	int vis;													// # of visible pixels

	try
	{

		printf(L"\r\n");

		// the device is opend after the app is started or a different device is selected. Init
		// Instance sends a message. See OpenCas( casDeviceHandle );

		casDeviceHandle = CASID;									// lets get the device handle from the variable


		// make sure to set the integration time
		printf(L"executing casSetMeasurementParameter with mpidIntegrationTime\r\n");
		casSetMeasurementParameter(casDeviceHandle, mpidIntegrationTime,
			casGetDeviceParameter(casDeviceHandle, dpidIntTimeMin)); // set to minimum integration time


		// set number of averages for the measurement
		printf(L"executing casSetMeasurementParameter with mpidAverages\r\n");
		casSetMeasurementParameter(casDeviceHandle, mpidAverages, 1);	// no averaging


		// get # of dead pixels at the beginning of the detector area
		printf(L"executing casGetDeviceParameter with dpidDeadPixels\r\n");
		deadPix = int(casGetDeviceParameter(casDeviceHandle, dpidDeadPixels));
		s.Format(L"determinded DeadPixels: %d\r\n", deadPix);
		printf(s);

		// get number of Pixels the device supports
		x = int(casGetDeviceParameter(casDeviceHandle, dpidPixels));
		vis = int(casGetDeviceParameter(casDeviceHandle, dpidVisiblePixels));

		// make sure to set a timeout, otherwise there will be an exception
		casSetMeasurementParameter(casDeviceHandle, mpidTriggerTimeout, 10000);

		// select external trigger, positive edge triggered, pin1 DB9
		casSetMeasurementParameter(casDeviceHandle, mpidTriggerSource, trgFlipFlop);

		// reset trigger
		casSetDeviceParameter(casDeviceHandle, dpidLine1FlipFlop, 0);

		printf(L"now please put a 5V trigger pulse on pin1 on 9-pin SUBD connector (timeout 10 sec)\r\n");


		MeasureHighSpeed(casDeviceHandle);						// do the measurement manually	

		SpecData.SetSize(vis);									// resize array that holds the spectrum data
		for (c = 0; c < vis; c++)										// transfer spectrum data to array
		{
			SpecData[c] = casGetData(casDeviceHandle, c + deadPix);
			// use casGetXArray to retrieve wave length information 
		}

		printf(L"plotting spectrum...\r\n");


		// restore old trigger condition
		casSetMeasurementParameter(casDeviceHandle, mpidTriggerSource, trgSoftware);


		// tell the spectrum view to display updated data
		UpdateAllViews(NULL);


	} // try // end of area protected by exception



	// exception handler to be used in case an error ocurred
	catch (CString str)
	{
		printf(L"ERROR: " + str);
	}

}

void CSpectrumDoc::TestLibFileCallingAllMethods()
{
	int tempCASID = -1;
	int BuffSize = 256;
	char buff[256] = { 0 };
	cas_wchar wbuff[256] = { 0 };
	
	tempCASID = casCreateDevice();
	casDoneDevice(tempCASID);
	tempCASID = casCreateDeviceEx(0, 0);
	casChangeDevice(tempCASID, 0, 0);
	casDoneDevice(tempCASID);
	casGetError(CASID);
	casGetErrorMessage(CASID, buff, BuffSize);
	casGetErrorMessageA(CASID, buff, BuffSize);
	casGetErrorMessageW(CASID, wbuff, BuffSize);
	casGetXArray(CASID, 0);
	casSetXArray(CASID, 0, 0);
	casGetData(CASID, 0);
	casSetData(CASID, 0, 0);
	casGetDarkCurrent(CASID, 0);
	casSetDarkCurrent(CASID, 0, 0);
	casNmToPixel(CASID, 0);
	casPixelToNm(CASID, 0);
	casGetShutter(CASID);
	casSetShutter(CASID, 0);
	casGetFilter(CASID);
	casSetFilter(CASID, 0);
	casGetFilterName(CASID, 0, buff, BuffSize);
	casGetFilterNameA(CASID, 0, buff, BuffSize);
	casGetFilterNameW(CASID, 0, wbuff, BuffSize);
	casGetDigitalOut(CASID, 0);
	casSetDigitalOut(CASID, 0, 0);
	casGetDigitalIn(CASID, 0);
	casInitialize(CASID, 0);
	casMeasureDarkCurrent(CASID);
	casClearDarkCurrent(CASID);
	casMeasure(CASID);
	casGetFIFOData(CASID);
	casStart(CASID);
	casFIFOHasData(CASID);
	casReadWatch(CASID);
	casStopTime(CASID, 0);
	casDeleteParamSet(CASID, 1);
	casGetWidth(CASID);
	casGetWidthEx(CASID, cLambdaWidth);
	casColorMetric(CASID);
	double tempFloat;
	casGetColorCoordinates(CASID, &tempFloat, &tempFloat, &tempFloat, &tempFloat, &tempFloat, &tempFloat);
	casGetTriStimulus(CASID, &tempFloat, &tempFloat, &tempFloat);
	casGetPeak(CASID, &tempFloat, &tempFloat);
	casGetCentroid(CASID);
	casGetRadInt(CASID, &tempFloat, buff, BuffSize);
	casGetRadIntA(CASID, &tempFloat, buff, BuffSize);
	casGetRadIntW(CASID, &tempFloat, wbuff, BuffSize);
	casGetPhotInt(CASID, &tempFloat, buff, BuffSize);
	casGetPhotIntA(CASID, &tempFloat, buff, BuffSize);
	casGetPhotIntW(CASID, &tempFloat, wbuff, BuffSize);
	casGetCCT(CASID);
	casCalculateCRI(CASID);
	casGetCRI(CASID, 1);
	casGetExtendedColorValues(CASID, ecvCDI);
	cmXYToDominantWavelength(0, 0, 0, 0, &tempFloat, &tempFloat);
	casLoadTestData(CASID, buff);
	casLoadTestDataA(CASID, buff);
	casLoadTestDataW(CASID, wbuff);
	casMultiTrackInit(CASID, 0);
	casMultiTrackDone(CASID);
	casMultiTrackReadData(CASID, 0);
	casMultiTrackCopyData(-1, 0);
	casMultiTrackCount(CASID);
	casMultiTrackCopySet(-1);
	casMultiTrackSaveData(-1, buff);
	casMultiTrackSaveDataA(-1, buff);
	casMultiTrackSaveDataW(-1, wbuff);
	casMultiTrackLoadData(-1, buff);
	casMultiTrackLoadDataA(-1, buff);
	casMultiTrackLoadDataW(-1, wbuff);
	casSaveSpectrum(CASID, buff);
	casSaveSpectrumA(CASID, buff);
	casSaveSpectrumW(CASID, wbuff);
	casGetDLLFileName(buff, BuffSize);
	casGetDLLFileNameA(buff, BuffSize);
	casGetDLLFileNameW(wbuff, BuffSize);
	casGetDLLVersionNumber(buff, BuffSize);
	casGetDLLVersionNumberA(buff, BuffSize);
	casGetDLLVersionNumberW(wbuff, BuffSize);
	casGetDeviceTypeOptions(InterfaceTest);
	casGetDeviceTypeOption(InterfaceTest, 0);
	casGetDeviceTypeOptionName(InterfaceTest, 0, buff, BuffSize);
	casGetDeviceTypeOptionNameA(InterfaceTest, 0, buff, BuffSize);
	casGetDeviceTypeOptionNameW(InterfaceTest, 0, wbuff, BuffSize);
	casGetDeviceTypes();
	casGetDeviceTypeName(InterfaceTest, buff, BuffSize);
	casGetDeviceTypeNameA(InterfaceTest, buff, BuffSize);
	casGetDeviceTypeNameW(InterfaceTest, wbuff, BuffSize);
	casGetCalibrationFactors(CASID, gcfDarkArrayIntTime, 0, 0);
	casSetCalibrationFactors(-1, 0, 0, 0, 0);
	casUpdateCalibrations(-1);
	casClearCalibration(-1, 0);
	casSaveCalibration(-1, buff);
	casSaveCalibrationA(-1, buff);
	casSaveCalibrationW(-1, wbuff);
	casGetExternalADCValue(CASID, 0);
	casGetSerialNumberEx(CASID, casSerialAccessory, buff, BuffSize);
	casGetSerialNumberExA(CASID, casSerialAccessory, buff, BuffSize);
	casGetSerialNumberExW(CASID, casSerialAccessory, wbuff, BuffSize);
	casGetOptions(CASID);
	casSetOptions(-1, 0);
	casSetOptionsOnOff(CASID, 0, 0);
	casSetAdvOptions("", 0);
	casSetStatusLED(CASID, 0);
	casGetMeasurementParameter(CASID, mpidIntegrationTime);
	casSetMeasurementParameter(CASID, mpidIntegrationTime, 20);
	casGetDeviceParameter(CASID, dpidAbortWaitForTrigger);
	casSetDeviceParameter(CASID, dpidDebugLogLevel, 5);
	buff[0] = 0;
	wbuff[0] = 0;
	casSetDeviceParameterString(CASID, dpidDebugLogFile, buff);
	casSetDeviceParameterStringA(CASID, dpidDebugLogFile, buff);
	casSetDeviceParameterStringW(CASID, dpidDebugLogFile, wbuff);
	casGetDeviceParameterString(CASID, dpidDebugLogFile, buff, BuffSize);
	casGetDeviceParameterStringA(CASID, dpidDebugLogFile, buff, BuffSize);
	casGetDeviceParameterStringW(CASID, dpidDebugLogFile, wbuff, BuffSize);
	casAssignDeviceEx(-1, -1, aoAssignComplete);
	casPerformAction(CASID, -1);
	casPerformActionEx(CASID, -1, -1, -1, nullptr);
	casCalculateTOPParameter(CASID, 0, 0, &tempFloat, &tempFloat);
	casCalculateLambdaDom(1, 3, 4, &tempFloat, &tempFloat);
}
