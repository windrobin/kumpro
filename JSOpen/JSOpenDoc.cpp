// JSOpenDoc.cpp : CJSOpenDoc クラスの実装
//

#include "stdafx.h"
#include "JSOpen.h"

#include "JSOpenDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CJSOpenDoc

IMPLEMENT_DYNCREATE(CJSOpenDoc, CDocument)

BEGIN_MESSAGE_MAP(CJSOpenDoc, CDocument)
END_MESSAGE_MAP()


// CJSOpenDoc コンストラクション/デストラクション

CJSOpenDoc::CJSOpenDoc()
{
	// TODO: この位置に 1 度だけ呼ばれる構築用のコードを追加してください。

}

CJSOpenDoc::~CJSOpenDoc()
{
}

BOOL CJSOpenDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: この位置に再初期化処理を追加してください。
	// (SDI ドキュメントはこのドキュメントを再利用します。)

	return TRUE;
}




// CJSOpenDoc シリアル化

void CJSOpenDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: 格納するコードをここに追加してください。
	}
	else
	{
		// TODO: 読み込むコードをここに追加してください。
	}
}


// CJSOpenDoc 診断

#ifdef _DEBUG
void CJSOpenDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CJSOpenDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG


// CJSOpenDoc コマンド
