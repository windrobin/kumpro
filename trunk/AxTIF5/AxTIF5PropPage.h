#pragma once

// AxTIF5PropPage.h : CAxTIF5PropPage プロパティ ページ クラスの宣言です。


// CAxTIF5PropPage : 実装に関しては AxTIF5PropPage.cpp を参照してください。

class CAxTIF5PropPage : public COlePropertyPage
{
	DECLARE_DYNCREATE(CAxTIF5PropPage)
	DECLARE_OLECREATE_EX(CAxTIF5PropPage)

// コンストラクタ
public:
	CAxTIF5PropPage();

// ダイアログ データ
	enum { IDD = IDD_PROPPAGE_AXTIF5 };

// 実装
protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート

// メッセージ マップ
protected:
	DECLARE_MESSAGE_MAP()
};

