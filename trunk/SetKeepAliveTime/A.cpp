
#include "StdAfx.h"
#include "resource.h"
#include "SetterDlg.h"

class CMyApp : public CWinApp {
public:
	BOOL InitInstance() {
		CSetterDlg wndDlg;
		wndDlg.DoModal();
		return false;
	}
};

CMyApp theApp;
