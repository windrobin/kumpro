// IEView.h : IEView アプリケーションのメイン ヘッダー ファイル
//
#pragma once

#ifndef __AFXWIN_H__
	#error "PCH に対してこのファイルをインクルードする前に 'stdafx.h' をインクルードしてください"
#endif

#include "resource.h"       // メイン シンボル


// CApp:
// このクラスの実装については、IEView.cpp を参照してください。
//

class CApp : public CWinApp
{
public:
	CApp();


// オーバーライド
public:
	virtual BOOL InitInstance();

// 実装
	COleTemplateServer m_server;
		// ドキュメントを作成するためのサーバー オブジェクト
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CApp theApp;