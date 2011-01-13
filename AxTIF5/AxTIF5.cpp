// AxTIF5.cpp : CAxTIF5App および DLL 登録の実装

#include "stdafx.h"
#include "AxTIF5.h"
#include "AboutDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

BEGIN_MESSAGE_MAP(CAxTIF5App, COleControlModule)
	ON_COMMAND(ID_APP_ABOUT, OnAppAbout)
END_MESSAGE_MAP()

CAxTIF5App theApp;

const GUID CDECL BASED_CODE _tlid =
		{ 0x3C358B7C, 0xA227, 0x42C7, { 0xA2, 0x26, 0x89, 0xC5, 0xCD, 0xD6, 0x92, 0xC6 } };
const WORD _wVerMajor = 1;
const WORD _wVerMinor = 0;



// CAxTIF5App::InitInstance - DLL 初期化

BOOL CAxTIF5App::InitInstance()
{
//TODO: call AfxInitRichEdit2() to initialize richedit2 library.
	BOOL bInit = COleControlModule::InitInstance();

	if (bInit)
	{
		bInit = AfxInitRichEdit2();
	}

	return bInit;
}



// CAxTIF5App::ExitInstance - DLL 終了

int CAxTIF5App::ExitInstance()
{
	// TODO: この位置にモジュールの終了処理を追加してください。

	return COleControlModule::ExitInstance();
}



// DllRegisterServer - エントリをシステム レジストリに追加します。

STDAPI DllRegisterServer(void)
{
	AFX_MANAGE_STATE(_afxModuleAddrThis);

	if (!AfxOleRegisterTypeLib(AfxGetInstanceHandle(), _tlid))
		return ResultFromScode(SELFREG_E_TYPELIB);

	if (!COleObjectFactoryEx::UpdateRegistryAll(TRUE))
		return ResultFromScode(SELFREG_E_CLASS);

	return NOERROR;
}



// DllUnregisterServer - エントリをレジストリから削除します。

STDAPI DllUnregisterServer(void)
{
	AFX_MANAGE_STATE(_afxModuleAddrThis);

	if (!AfxOleUnregisterTypeLib(_tlid, _wVerMajor, _wVerMinor))
		return ResultFromScode(SELFREG_E_TYPELIB);

	if (!COleObjectFactoryEx::UpdateRegistryAll(FALSE))
		return ResultFromScode(SELFREG_E_CLASS);

	return NOERROR;
}

void CAxTIF5App::OnAppAbout() {
	CAboutDlg wndDlg;
	wndDlg.DoModal();
}
