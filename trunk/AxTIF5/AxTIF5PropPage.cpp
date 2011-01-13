// AxTIF5PropPage.cpp : CAxTIF5PropPage プロパティ ページ クラスの実装

#include "stdafx.h"
#include "AxTIF5.h"
#include "AxTIF5PropPage.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


IMPLEMENT_DYNCREATE(CAxTIF5PropPage, COlePropertyPage)



// メッセージ マップ

BEGIN_MESSAGE_MAP(CAxTIF5PropPage, COlePropertyPage)
END_MESSAGE_MAP()



// クラス ファクトリおよび GUID を初期化します。

IMPLEMENT_OLECREATE_EX(CAxTIF5PropPage, "AXTIF5.AxTIF5PropPage.1",
	0xd4df22ac, 0x3ab4, 0x41da, 0xa8, 0x6a, 0x15, 0x15, 0x74, 0xd8, 0x43, 0xd6)



// CAxTIF5PropPage::CAxTIF5PropPageFactory::UpdateRegistry -
// CAxTIF5PropPage のシステム レジストリ エントリを追加または削除します。

BOOL CAxTIF5PropPage::CAxTIF5PropPageFactory::UpdateRegistry(BOOL bRegister)
{
	if (bRegister)
		return AfxOleRegisterPropertyPageClass(AfxGetInstanceHandle(),
			m_clsid, IDS_AXTIF5_PPG);
	else
		return AfxOleUnregisterClass(m_clsid, NULL);
}



// CAxTIF5PropPage::CAxTIF5PropPage - コンストラクタ

CAxTIF5PropPage::CAxTIF5PropPage() :
	COlePropertyPage(IDD, IDS_AXTIF5_PPG_CAPTION)
{
}



// CAxTIF5PropPage::DoDataExchange - ページおよびプロパティ間でデータを移動します。

void CAxTIF5PropPage::DoDataExchange(CDataExchange* pDX)
{
	DDP_PostProcessing(pDX);
}



// CAxTIF5PropPage メッセージ ハンドラ
