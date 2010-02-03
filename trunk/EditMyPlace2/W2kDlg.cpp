
// W2kDlg.cpp : 実装ファイル
//

#include "StdAfx.h"
#include "resource.h"
#include "W2kDlg.h"

// CW2kDlg ダイアログ

IMPLEMENT_DYNAMIC(CW2kDlg, CPropertyPage)

CW2kDlg::CW2kDlg()
	: CPropertyPage(CW2kDlg::IDD)
{

}

CW2kDlg::~CW2kDlg() {

}

void CW2kDlg::DoDataExchange(CDataExchange* pDX) {
	CPropertyPage::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_C1, m_c1);
	DDX_Control(pDX, IDC_C2, m_c2);
	DDX_Control(pDX, IDC_C3, m_c3);
	DDX_Control(pDX, IDC_C4, m_c4);
	DDX_Control(pDX, IDC_C5, m_c5);
	DDX_Control(pDX, IDC_FP1, m_fp1);
	DDX_Control(pDX, IDC_FP2, m_fp2);
	DDX_Control(pDX, IDC_FP3, m_fp3);
	DDX_Control(pDX, IDC_FP4, m_fp4);
	DDX_Control(pDX, IDC_FP5, m_fp5);
}

BEGIN_MESSAGE_MAP(CW2kDlg, CPropertyPage)
	ON_COMMAND_RANGE(IDC_R1, IDC_R5, &CW2kDlg::OnBnClickedR1)
	ON_BN_CLICKED(IDC_REVERT, &CW2kDlg::OnBnClickedRevert)
	ON_BN_CLICKED(IDC_COMMIT, &CW2kDlg::OnBnClickedCommit)
	ON_BN_CLICKED(IDC_REMOVE, &CW2kDlg::OnBnClickedRemove)
	ON_BN_CLICKED(IDC_PREVIEW, &CW2kDlg::OnBnClickedPreview)
END_MESSAGE_MAP()

// CW2kDlg メッセージ ハンドラ

int s_cxIcon = GetSystemMetrics(SM_CXICON);
int s_cyIcon = GetSystemMetrics(SM_CYICON);

BOOL CW2kDlg::OnInitDialog() {
	CPropertyPage::OnInitDialog();

	VERIFY(m_il.Create(s_cxIcon, s_cyIcon, ILC_COLOR32|ILC_MASK, 0, 15));

	m_comboItems.Add(ComboItem(CITY_HIDDEN    , 0, _T("(標準)")         ));
	m_comboItems.Add(ComboItem(CITY_REG_SZ    , 0, _T("(指定 フォルダパス)") ).SetIco(AfxGetApp()->LoadIcon(IDI_FOLDEROPEN)));
	m_comboItems.Add(ComboItem(CITY_REG_ENVSZ , 0, _T("(環境 フォルダパス)") ).SetIco(AfxGetApp()->LoadIcon(IDI_FOLDEROPEN)));
	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_PERSONAL   , _T("マイドキュメント")).ResolveName());
	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_DRIVES     , _T("マイコンピュータ")).ResolveName());
	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_NETWORK    , _T("マイネットワーク")).ResolveName());
	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_MYPICTURES , _T("マイピクチャ")).ResolveName());
	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_HISTORY    , _T("履歴")).ResolveName());
	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_RECENT     , _T("最近使ったファイル")).ResolveName());
	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_MYVIDEO    , _T("マイビデオ")).ResolveName());
	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_MYMUSIC    , _T("マイミュージック")).ResolveName());
	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_FAVORITES  , _T("お気に入り")).ResolveName());
	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_DESKTOP    , _T("デスクトップ")).ResolveName());

//	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_CONTROLS   , _T("コンパネ")).ResolveName());
//	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_TEMPLATES  , _T("テンプレート")).ResolveName());
//	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_NETHOOD    , _T("ネットワークプレース")).ResolveName());
	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_BITBUCKET  , _T("ごみ箱")).ResolveName());
//	m_comboItems.Add(ComboItem(CITY_CSIDL     , CSIDL_SENDTO     , _T("送る")).ResolveName());

	m_c1.SetImageList(&m_il);
	m_c2.SetImageList(&m_il);
	m_c3.SetImageList(&m_il);
	m_c4.SetImageList(&m_il);
	m_c5.SetImageList(&m_il);

	for (int x=0; x<m_comboItems.GetCount(); x++) {
		int iIco = m_il.Add(m_comboItems[x].hIcon);

		ATLVERIFY(CBExUt(m_c1).Add(m_comboItems[x].dispName, iIco) >= 0);
		ATLVERIFY(CBExUt(m_c2).Add(m_comboItems[x].dispName, iIco) >= 0);
		ATLVERIFY(CBExUt(m_c3).Add(m_comboItems[x].dispName, iIco) >= 0);
		ATLVERIFY(CBExUt(m_c4).Add(m_comboItems[x].dispName, iIco) >= 0);
		ATLVERIFY(CBExUt(m_c5).Add(m_comboItems[x].dispName, iIco) >= 0);
	}

	m_c1.SetCurSel(0);
	m_c2.SetCurSel(0);
	m_c3.SetCurSel(0);
	m_c4.SetCurSel(0);
	m_c5.SetCurSel(0);

	Revert();

	return TRUE;  // return TRUE unless you set the focus to a control
	// 例外 : OCX プロパティ ページは必ず FALSE を返します。
}

void CW2kDlg::OnBnClickedR1(UINT nID) {
	switch (nID) {
		case IDC_R1: DoSelFolder(m_c1, m_fp1); break;
		case IDC_R2: DoSelFolder(m_c2, m_fp2); break;
		case IDC_R3: DoSelFolder(m_c3, m_fp3); break;
		case IDC_R4: DoSelFolder(m_c4, m_fp4); break;
		case IDC_R5: DoSelFolder(m_c5, m_fp5); break;
	}
}

int CALLBACK BCP(
	HWND hwnd,
	UINT uMsg,
	LPARAM lParam,
	LPARAM lpData
) {
	return 1;
}

void CW2kDlg::DoSelFolder(CComboBoxEx &wndCBEx, CEdit &wndfp) {
	BROWSEINFO bi;
	ZeroMemory(&bi, sizeof(bi));

	bi.hwndOwner = *this;
	bi.lParam = reinterpret_cast<LPARAM>(this);
	bi.lpfn = BCP;
	bi.ulFlags = 0
		|BIF_NEWDIALOGSTYLE
		|BIF_NONEWFOLDERBUTTON
		|BIF_RETURNONLYFSDIRS
		|BIF_RETURNFSANCESTORS
		;

	LPITEMIDLIST pidl = SHBrowseForFolder(&bi);
	if (pidl != NULL) {
		TCHAR tc[MAX_PATH +1] = {_T("")};
		if (SHGetPathFromIDList(pidl, tc)) {
			wndfp.SetWindowText(tc);
			wndfp.SetFocus();
			wndfp.SetSel(0, wndfp.GetWindowTextLength());
		}
		else {
			wndfp.SetWindowText(NULL);
		}

		wndCBEx.SetCurSel(1);

		CoTaskMemFree(pidl);
	}
}

void CW2kDlg::OnBnClickedRevert() {
	Revert();
}

void CW2kDlg::Revert() {
	CRegKey rkPB;
	LONG r;
	r = rkPB.Open(
		HKEY_CURRENT_USER, 
		_T("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Comdlg32\\PlacesBar"), 
		KEY_READ
		);
	if (r == 0) {
		for (int x=0; x<5; x++) {
			CComboBoxEx *pWnd = static_cast<CComboBoxEx *>(GetDlgItem(IDC_C1 +x));
			if (pWnd == NULL)
				continue;
			CComboBoxEx &wndCBEx = *pWnd;

			CEdit *pfp = static_cast<CEdit *>(GetDlgItem(IDC_FP1 +x));
			if (pfp == NULL)
				continue;

			BYTE buff[2048 +1] = {0};
			DWORD cb = 2048;
			DWORD nType = 0;
			CString s;
			s.Format(_T("Place%d"), x);
			r = rkPB.QueryValue(s, &nType, buff, &cb);
			if (r == ERROR_FILE_NOT_FOUND) {
				wndCBEx.SetCurSel(0);
			}
			else if (nType == REG_DWORD) {
				DWORD csidl = *reinterpret_cast<DWORD *>(buff);
				for (int t=0; t<m_comboItems.GetCount(); t++) {
					if (m_comboItems[t].city == CITY_CSIDL && m_comboItems[t].csidl == csidl) {
						wndCBEx.SetCurSel(t);
						break;
					}
				}
				pfp->SetWindowText(_T(""));
			}
			else if (nType == REG_SZ) {
				buff[cb] = 0;
				wndCBEx.SetCurSel(1);
				pfp->SetWindowText(reinterpret_cast<LPTSTR>(buff));
			}
			else if (nType == REG_EXPAND_SZ) {
				buff[cb] = 0;
				wndCBEx.SetCurSel(2);
				pfp->SetWindowText(reinterpret_cast<LPTSTR>(buff));
			}
		}
	}
}

void CW2kDlg::OnBnClickedCommit() {
	if (Commit()) {
		AfxMessageBox(IDS_SAVE_DONE, MB_ICONINFORMATION);
	}
	else {
		AfxMessageBox(IDS_SAVE_FAILED, MB_ICONEXCLAMATION);
	}
}

bool CW2kDlg::Commit() {
	CRegKey rkPB;
	LONG r;
	r = rkPB.Create(
		HKEY_CURRENT_USER, 
		_T("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Comdlg32\\PlacesBar"), 
		0,
		0,
		KEY_WRITE,
		NULL,
		NULL
		);
	if (r == 0) {
		for (int x=0; x<5; x++) {
			CComboBoxEx *pWnd = static_cast<CComboBoxEx *>(GetDlgItem(IDC_C1 +x));
			if (pWnd == NULL)
				continue;
			CComboBoxEx &wndCBEx = *pWnd;

			CEdit *pfp = static_cast<CEdit *>(GetDlgItem(IDC_FP1 +x));
			if (pfp == NULL)
				continue;

			int i = wndCBEx.GetCurSel();

			CString s;
			s.Format(_T("Place%d"), x);

			if ((UINT)i >= (UINT)m_comboItems.GetCount())
				continue;

			switch (m_comboItems[i].city) {
				case CITY_HIDDEN:
					VERIFY(0 == (r = rkPB.DeleteValue(s)));
					break;
				case CITY_REG_SZ:
				case CITY_REG_ENVSZ:
					{
						CString strVal;
						pfp->GetWindowText(strVal);

						VERIFY(0 == (r = rkPB.SetStringValue(s, strVal, (m_comboItems[i].city == CITY_REG_SZ) ? REG_SZ : REG_EXPAND_SZ)));
						break;
					}
				default:
					VERIFY(0 == (r = rkPB.SetDWORDValue(s, m_comboItems[i].csidl)));
					break;
			}
		}

		return true;
	}
	return false;
}

void CW2kDlg::OnBnClickedRemove() { }

BOOL CW2kDlg::OnSetActive() {
	return CPropertyPage::OnSetActive();
}

void CW2kDlg::OnBnClickedPreview() {
	Commit();

	CFileDialog wndDlg(
		true, 
		NULL,
		NULL, 
		OFN_EXPLORER, 
		_T("test|*.*||"), 
		this
		);
	wndDlg.DoModal();
}
