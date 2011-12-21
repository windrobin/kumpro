// UrlDlg.cpp : 実装ファイル
//

#include "stdafx.h"
#include "IEView.h"
#include "UrlDlg.h"


// CUrlDlg ダイアログ

IMPLEMENT_DYNAMIC(CUrlDlg, CDialog)

CUrlDlg::CUrlDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CUrlDlg::IDD, pParent)
	, m_strUrl(_T(""))
{

}

CUrlDlg::~CUrlDlg()
{
}

void CUrlDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT_URL, m_strUrl);
}


BEGIN_MESSAGE_MAP(CUrlDlg, CDialog)
END_MESSAGE_MAP()


// CUrlDlg メッセージ ハンドラ
