
#include "StdAfx.h"

#include "resource.h"

#include "W2kDlg.h"
#include "O2K0Dlg.h"

class CMySheet : public CPropertySheet {
	DECLARE_MESSAGE_MAP()
public:
	explicit CMySheet(
		UINT nIDCaption, 
		CWnd* pParentWnd = NULL,
		UINT iSelectPage = 0
	)
	: CPropertySheet(nIDCaption, pParentWnd, iSelectPage)
	{

	}
};

BEGIN_MESSAGE_MAP(CMySheet, CPropertySheet)
END_MESSAGE_MAP()

class CMyApp : public CWinApp {
public:
	BOOL InitInstance() {
		HRESULT hr;
		if (SUCCEEDED(hr = CoInitialize(NULL))) {
			CW2kDlg w2kDlg;
			CO2K0Dlg off2000(OFFVER_2000);
			CO2K0Dlg off2003(OFFVER_2003);
			CMySheet wndSheet(IDS_TITLE);
			wndSheet.AddPage(&w2kDlg);
			wndSheet.AddPage(&off2000);
			wndSheet.AddPage(&off2003);
			wndSheet.DoModal();

			CoUninitialize();
		}
		return false;
	}
};

CMyApp theApp;
