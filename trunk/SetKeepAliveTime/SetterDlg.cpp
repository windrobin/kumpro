
// SetterDlg.cpp : 実装ファイル
//

#include "StdAfx.h"
#include "resource.h"
#include "SetterDlg.h"

// CSetterDlg ダイアログ

CSetterDlg::CSetterDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSetterDlg::IDD, pParent)
	, m_nKA(0)
{

}

CSetterDlg::~CSetterDlg() {
}

void CSetterDlg::DoDataExchange(CDataExchange* pDX) {
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_KA, m_nKA);
	DDV_MinMaxInt(pDX, m_nKA, 0, 57600);
}

BEGIN_MESSAGE_MAP(CSetterDlg, CDialog)
END_MESSAGE_MAP()

// CSetterDlg メッセージ ハンドラ

BOOL CSetterDlg::OnInitDialog() {
	CDialog::OnInitDialog();

	CRegKey rk;
	int r;
	if (0 == (r = rk.Open(HKEY_LOCAL_MACHINE, _T("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters"), KEY_READ))) {
		DWORD nKA = 0;
		if (0 == (r = rk.QueryDWORDValue(_T("KeepAliveTime"), nKA))) {
			m_nKA = nKA / 1000 / 60;
		}
	}

	UpdateData(false);

	return TRUE;  // return TRUE unless you set the focus to a control
	// 例外 : OCX プロパティ ページは必ず FALSE を返します。
}

void CSetterDlg::OnOK() {
	if (!UpdateData())
		return;
	CRegKey rk;
	int r;
	if (0 == (r = rk.Open(HKEY_LOCAL_MACHINE, _T("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters"), KEY_WRITE))) {
		if (m_nKA == 0) {
			if (0 == (r = rk.DeleteValue(_T("KeepAliveTime"))) || r == ERROR_FILE_NOT_FOUND) {
				AfxMessageBox(_T("設定しました。"), MB_OK | MB_ICONINFORMATION);
			}
			else {
				AfxMessageBox(_T("失敗しました。"), MB_OK | MB_ICONEXCLAMATION);
			}
		}
		else {
			if (0 == (r = rk.SetDWORDValue(_T("KeepAliveTime"), m_nKA * 1000u * 60u))) {
				AfxMessageBox(_T("設定しました。"), MB_OK | MB_ICONINFORMATION);
			}
			else {
				AfxMessageBox(_T("失敗しました。"), MB_OK | MB_ICONEXCLAMATION);
			}
		}
	}
	else {
		AfxMessageBox(_T("失敗しました。"), MB_OK | MB_ICONEXCLAMATION);
	}
}
