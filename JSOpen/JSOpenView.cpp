// JSOpenView.cpp : CJSOpenView クラスの実装
//

#include "stdafx.h"
#include "JSOpen.h"

#include "JSOpenDoc.h"
#include "JSOpenView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CJSOpenView

IMPLEMENT_DYNCREATE(CJSOpenView, CHtmlView)

BEGIN_MESSAGE_MAP(CJSOpenView, CHtmlView)
	ON_COMMAND(ID_FILE_WINDOW_OPEN, &CJSOpenView::OnFileWindowOpen)
	ON_COMMAND(ID_FILE_FIX1, &CJSOpenView::OnFileFix1)
END_MESSAGE_MAP()

// CJSOpenView コンストラクション/デストラクション

CJSOpenView::CJSOpenView()
{
	// TODO: 構築コードをここに追加します。

}

CJSOpenView::~CJSOpenView()
{
}

BOOL CJSOpenView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: この位置で CREATESTRUCT cs を修正して Window クラスまたはスタイルを
	//  修正してください。

	return CHtmlView::PreCreateWindow(cs);
}

void CJSOpenView::OnInitialUpdate()
{
	CHtmlView::OnInitialUpdate();

	Navigate2(_T("http://typhoon.yahoo.co.jp/weather/jp/typhoon/typha.html"),NULL,NULL);
}


// CJSOpenView 診断

#ifdef _DEBUG
void CJSOpenView::AssertValid() const
{
	CHtmlView::AssertValid();
}

void CJSOpenView::Dump(CDumpContext& dc) const
{
	CHtmlView::Dump(dc);
}

CJSOpenDoc* CJSOpenView::GetDocument() const // デバッグ以外のバージョンはインラインです。
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CJSOpenDoc)));
	return (CJSOpenDoc*)m_pDocument;
}
#endif //_DEBUG


// CJSOpenView メッセージ ハンドラ

void CJSOpenView::OnNewWindow2(LPDISPATCH* ppDisp, BOOL* Cancel)
{
	// TODO: ここに特定なコードを追加するか、もしくは基本クラスを呼び出してください。

	CHtmlView::OnNewWindow2(ppDisp, Cancel);
}

_COM_SMARTPTR_TYPEDEF(IHTMLWindow2, __uuidof(IHTMLWindow2));

void CJSOpenView::OnFileWindowOpen() {
	HRESULT hr;
	CString str;
	try {
		CComQIPtr<IHTMLDocument2> document = GetHtmlDocument();
		if (document != NULL) {
			IHTMLWindow2Ptr window;
			document->get_parentWindow(&window);
			if (window != NULL) {
				CComBSTR url;
				document->get_URL(&url);

				CComPtr<IHTMLWindow2> newwin;
				if (FAILED(hr = window->open(url, CComBSTR(L"_blank"), CComBSTR(), false, &newwin))) {
					_com_issue_errorex(hr, window, IID_IHTMLWindow2);
				}
				printf("");
			}
		}
	}
	catch (_com_error err) {
		AfxMessageBox(CString(err.ErrorMessage()), MB_ICONEXCLAMATION);
	}
}

void CJSOpenView::OnFileFix1()
{
	ShellExecute(*this, _T("open"), _T("B8DA6310-E19B-11D0-933C-00A0C90DCAA9.reg"), NULL, NULL, SW_SHOW);
}
