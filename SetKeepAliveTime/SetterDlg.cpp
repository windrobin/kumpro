
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
	ON_COMMAND_RANGE(IDC_H9, IDC_M15, &CSetterDlg::OnBnClickedH9)
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
				AfxMessageBox(IDS_SET_OK, MB_OK | MB_ICONINFORMATION);
			}
			else {
				AfxMessageBox(IDS_SET_ERR, MB_OK | MB_ICONINFORMATION);
			}
		}
		else {
			if (0 == (r = rk.SetDWORDValue(_T("KeepAliveTime"), m_nKA * 1000u * 60u))) {
				AfxMessageBox(IDS_SET_OK, MB_OK | MB_ICONINFORMATION);
			}
			else {
				AfxMessageBox(IDS_SET_ERR, MB_OK | MB_ICONINFORMATION);
			}
		}
	}
	else {
		AfxMessageBox(IDS_SET_ERR, MB_OK | MB_ICONINFORMATION);
	}
}

void CSetterDlg::OnBnClickedH9(UINT nID) {
	switch (nID) {
		case IDC_H9: m_nKA = 60 * 9; break;
		case IDC_H6: m_nKA = 60 * 6; break;
		case IDC_H2: m_nKA = 60 * 2; break;
		case IDC_M15: m_nKA = 15; break;
		default: return;
	}
	VERIFY(UpdateData(false));
}
