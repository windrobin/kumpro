// InnerFrame.cpp : 実装ファイル
//

#include "stdafx.h"
#include "AxTIF5.h"
#include "InnerFrame.h"

#include "AxTIF3Doc.h"
#include "AxTIF3View.h"

// CInnerFrame

IMPLEMENT_DYNCREATE(CInnerFrame, CFrameWnd)

CInnerFrame::CInnerFrame()
{

}

CInnerFrame::~CInnerFrame()
{
}


BEGIN_MESSAGE_MAP(CInnerFrame, CFrameWnd)
	ON_WM_ERASEBKGND()
END_MESSAGE_MAP()


// CInnerFrame メッセージ ハンドラ

BOOL CInnerFrame::OnCreateClient(LPCREATESTRUCT lpcs, CCreateContext* pContext) {
	// TODO: ここに特定なコードを追加するか、もしくは基本クラスを呼び出してください。

	return CFrameWnd::OnCreateClient(lpcs, pContext);
}

void CInnerFrame::PostNcDestroy() {
//	CFrameWnd::PostNcDestroy();
}

BOOL CInnerFrame::PreCreateWindow(CREATESTRUCT& cs)
{
	if (!CFrameWnd::PreCreateWindow(cs))
		return false;

	cs.style |= WS_CLIPCHILDREN|WS_CLIPSIBLINGS;
	cs.dwExStyle &= ~WS_EX_CLIENTEDGE;
	return true;
}

BOOL CInnerFrame::OnEraseBkgnd(CDC* pDC) {
	return 1;
//	return CFrameWnd::OnEraseBkgnd(pDC);
}
