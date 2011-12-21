// IEViewDoc.cpp : CDoc クラスの実装
//

#include "stdafx.h"
#include "IEView.h"

#include "IEViewDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CDoc

IMPLEMENT_DYNCREATE(CDoc, CDocument)

BEGIN_MESSAGE_MAP(CDoc, CDocument)
END_MESSAGE_MAP()

BEGIN_DISPATCH_MAP(CDoc, CDocument)
END_DISPATCH_MAP()

// メモ: VBA からタイプ セーフなバインドをサポートするために、IID_IIEView のサポートを追加します。
//  この IID は、.IDL ファイルのディスパッチ インターフェイスへアタッチされる 
//  GUID と一致しなければなりません。

// {30057643-DF70-4AC3-BF00-1CE1192DF309}
static const IID IID_IIEView =
{ 0x30057643, 0xDF70, 0x4AC3, { 0xBF, 0x0, 0x1C, 0xE1, 0x19, 0x2D, 0xF3, 0x9 } };

BEGIN_INTERFACE_MAP(CDoc, CDocument)
	INTERFACE_PART(CDoc, IID_IIEView, Dispatch)
END_INTERFACE_MAP()


// CDoc コンストラクション/デストラクション

CDoc::CDoc()
{
	// TODO: この位置に 1 度だけ呼ばれる構築用のコードを追加してください。

	EnableAutomation();

	AfxOleLockApp();
}

CDoc::~CDoc()
{
	AfxOleUnlockApp();
}

BOOL CDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: この位置に再初期化処理を追加してください。
	// (SDI ドキュメントはこのドキュメントを再利用します。)

	return TRUE;
}




// CDoc シリアル化

void CDoc::Serialize(CArchive& ar)
{
	AfxThrowNotSupportedException();

	if (ar.IsStoring())
	{
		// TODO: 格納するコードをここに追加してください。
	}
	else
	{
		// TODO: 読み込むコードをここに追加してください。
	}
}


// CDoc 診断

#ifdef _DEBUG
void CDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG


// CDoc コマンド

BOOL CDoc::OnOpenDocument(LPCTSTR lpszPathName) {
	return TRUE;
}
