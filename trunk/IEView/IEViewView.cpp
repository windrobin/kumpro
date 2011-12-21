// IEViewView.cpp : CVw クラスの実装
//

#include "stdafx.h"
#include "IEView.h"

#include "IEViewDoc.h"
#include "IEViewView.h"

#include "UrlDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CVw

IMPLEMENT_DYNCREATE(CVw, CHtmlView)

BEGIN_MESSAGE_MAP(CVw, CHtmlView)
	// 標準印刷コマンド
	ON_COMMAND(ID_FILE_PRINT, &CHtmlView::OnFilePrint)
	ON_COMMAND(ID_FILE_URL, &CVw::OnFileUrl)
END_MESSAGE_MAP()

// CVw コンストラクション/デストラクション

CVw::CVw()
{
	// TODO: 構築コードをここに追加します。

}

CVw::~CVw()
{
}

BOOL CVw::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: この位置で CREATESTRUCT cs を修正して Window クラスまたはスタイルを
	//  修正してください。

	return CHtmlView::PreCreateWindow(cs);
}

extern CString g_strUrl;

void CVw::OnInitialUpdate()
{
	CHtmlView::OnInitialUpdate();

	Navigate2(g_strUrl,NULL,NULL);
}


// CVw 印刷



// CVw 診断

#ifdef _DEBUG
void CVw::AssertValid() const
{
	CHtmlView::AssertValid();
}

void CVw::Dump(CDumpContext& dc) const
{
	CHtmlView::Dump(dc);
}

CDoc* CVw::GetDocument() const // デバッグ以外のバージョンはインラインです。
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CDoc)));
	return (CDoc*)m_pDocument;
}
#endif //_DEBUG

// CVw メッセージ ハンドラ

void CVw::OnFileUrl() {
	CUrlDlg wndDlg(this);
	wndDlg.m_strUrl = GetLocationURL();
	if (wndDlg.DoModal() == IDOK) {
		Navigate2(g_strUrl = wndDlg.m_strUrl,NULL,NULL);
	}
}

void CVw::OnStatusTextChange(LPCTSTR lpszText) {
	CHtmlView::OnStatusTextChange(lpszText);
}

void CVw::OnNavigateComplete2(LPCTSTR strURL)
{
	// TODO: ここに特定なコードを追加するか、もしくは基本クラスを呼び出してください。

	CHtmlView::OnNavigateComplete2(strURL);
}
