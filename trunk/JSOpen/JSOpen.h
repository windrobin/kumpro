// JSOpen.h : JSOpen アプリケーションのメイン ヘッダー ファイル
//
#pragma once

#ifndef __AFXWIN_H__
	#error "PCH に対してこのファイルをインクルードする前に 'stdafx.h' をインクルードしてください"
#endif

#include "resource.h"       // メイン シンボル


// CJSOpenApp:
// このクラスの実装については、JSOpen.cpp を参照してください。
//

class CJSOpenApp : public CWinApp
{
public:
	CJSOpenApp();


// オーバーライド
public:
	virtual BOOL InitInstance();

// 実装
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CJSOpenApp theApp;