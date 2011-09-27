#pragma once

// AxTIF5.h : AxTIF5.DLL のメイン ヘッダー ファイル

#if !defined( __AFXCTL_H__ )
#error "このファイルをインクルードする前に 'afxctl.h' をインクルードしてください。"
#endif

#include "resource.h"       // メイン シンボル


// CAxTIF5App : 実装に関しては AxTIF5.cpp を参照してください。

class CAxTIF5App : public COleControlModule
{
	DECLARE_MESSAGE_MAP()
public:
	BOOL InitInstance();
	int ExitInstance();
	afx_msg void OnAppAbout();
};

extern const GUID CDECL _tlid;
extern const WORD _wVerMajor;
extern const WORD _wVerMinor;

