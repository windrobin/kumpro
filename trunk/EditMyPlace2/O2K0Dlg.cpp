// O2K0Dlg.cpp : 実装ファイル
//

#include "StdAfx.h"
#include "resource.h"
#include "O2K0Dlg.h"
#include "SHUt.h"
#include <algorithm>

#define MAX_CB_PIDL 4096U

// CO2K0Dlg ダイアログ

IMPLEMENT_DYNAMIC(CO2K0Dlg, CPropertyPage)

CO2K0Dlg::CO2K0Dlg(int offver)
	: CPropertyPage(CO2K0Dlg::IDD)
	, offver(offver)
{
	switch (offver) {
		case OFFVER_XP:
			m_strTitle.LoadString(IDS_OFFVER_XP);
			m_psp.dwFlags |= PSP_USETITLE;
			m_psp.pszTitle = m_strTitle;
			break;
		case OFFVER_2003:
			m_strTitle.LoadString(IDS_OFFVER_2003);
			m_psp.dwFlags |= PSP_USETITLE;
			m_psp.pszTitle = m_strTitle;
			break;
	}
}

CO2K0Dlg::~CO2K0Dlg() {
}

void CO2K0Dlg::DoDataExchange(CDataExchange* pDX) {
	CPropertyPage::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CO2K0Dlg, CPropertyPage)
	ON_COMMAND_RANGE(IDC_O1, IDC_O5, &CO2K0Dlg::OnBnClickedO1)
	ON_COMMAND_RANGE(IDC_ORDER1, IDC_ORDER101, OnOrderClicked)
	ON_COMMAND_RANGE(IDC_R1, IDC_R3, &CO2K0Dlg::OnBnClickedR1)
	ON_BN_CLICKED(IDC_REVERT, &CO2K0Dlg::OnBnClickedRevert)
	ON_BN_CLICKED(IDC_COMMIT, &CO2K0Dlg::OnBnClickedCommit)
	ON_BN_CLICKED(IDC_PREVIEW, &CO2K0Dlg::OnBnClickedPreview)
END_MESSAGE_MAP()

bool CO2K0Dlg::IDLUt::Parse(CString s, LPITEMIDLIST &pidlRoot) {
	BYTE bIdl[MAX_CB_PIDL];

	if (s.Left(6) == _T("<PIDL>")) {
		int cch = (s.GetLength() -6);
		int cbIn = 0;
		if (cch >= 2 && 0 == (cch & 1) && (cbIn = (cch >> 1)) < MAX_CB_PIDL) {
			int x = 0;
			for (; x < cbIn; x++) {
				TCHAR tc[3];
				tc[0] = s[6 +2*x +0];
				tc[1] = s[6 +2*x +1];
				tc[2] = 0;
				LPTSTR pszTerm = NULL;
				int val = _tcstol(tc, &pszTerm, 16);
				if (*pszTerm != 0)
					break;
				bIdl[x] = (BYTE)val;
			}
			if (x == cbIn && IDLUt::IsValid(reinterpret_cast<LPITEMIDLIST>(bIdl), cbIn)) {
				pidlRoot = IDLUt::Clone(reinterpret_cast<LPITEMIDLIST>(bIdl));
				return true;
			}
		}
	}
	else if (s.Left(10) == _T("ITEMIDLIST")) {
		int cch = s.GetLength();
		int wri = 0;
		int x = 10;
		while (x < cch) {
			if (s[x] == '{' || s[x] == '}') {
				x++;
				continue;
			}
			if (x +1 < cch) {
				if (wri >= MAX_CB_PIDL)
					break;
				TCHAR tc[3];
				tc[0] = s[x];
				tc[1] = s[x +1];
				tc[2] = 0;
				LPTSTR pszTerm = NULL;
				int val = _tcstol(tc, &pszTerm, 16);
				if (*pszTerm != 0)
					break;
				bIdl[wri] = (BYTE)val;

				wri++;
				x += 2;
				continue;
			}
			break;
		}
		if (x == cch && IDLUt::IsValid(reinterpret_cast<LPITEMIDLIST>(bIdl), wri)) {
			pidlRoot = IDLUt::Clone(reinterpret_cast<LPITEMIDLIST>(bIdl));
			return true;
		}
	}
	else {
		CComPtr<IShellFolder> psf;
		HRESULT hr;
		if (SUCCEEDED(hr = SHGetDesktopFolder(&psf))) {
			LPITEMIDLIST pidlSeled = NULL;
			hr = psf->ParseDisplayName(NULL, NULL, CT2W(s), NULL, &pidlSeled, NULL);
			if (SUCCEEDED(hr)) {
				pidlRoot = pidlSeled;
				return true;
			}
		}
	}

	return false;
}

// CO2K0Dlg メッセージ ハンドラ

BOOL CO2K0Dlg::OnInitDialog() {
	CPropertyPage::OnInitDialog();

	Revert();

	return TRUE;  // return TRUE unless you set the focus to a control
	// 例外 : OCX プロパティ ページは必ず FALSE を返します。
}

bool CO2K0Dlg::Commit() {
	CRegKey rk;
	CString pathKey; pathKey.Format(_T("Software\\Microsoft\\Office\\%s\\Common\\Open Find\\Places"), GetVer());
	LONG r = rk.Open(
		HKEY_CURRENT_USER,
		pathKey,
		KEY_READ
		);
	if (r != 0)
		return false;

	WriteStd(_T("Desktop")     , IDC_V1, IDC_O1);
	WriteStd(_T("MyComputer")  , IDC_V2, IDC_O2);
	WriteStd(_T("MyDocuments") , IDC_V3, IDC_O3);
	WriteStd(_T("Publishing")  , IDC_V4, IDC_O4);
	WriteStd(_T("Recent")      , IDC_V5, IDC_O5);

	WriteUser(_T("Place0"), IDC_N1, 0, IDC_L1);
	WriteUser(_T("Place1"), IDC_N2, 0, IDC_L2);
	WriteUser(_T("Place2"), IDC_N3, 0, IDC_L3);

	return true;
}

void CO2K0Dlg::WriteUser(LPCTSTR pszSubkey, int idN, int idO, int idL) {
	CRegKey rk;
	CString pathKey; pathKey.Format(_T("Software\\Microsoft\\Office\\%s\\Common\\Open Find\\Places\\UserDefinedPlaces"), GetVer());
	pathKey.Append(_T("\\"));
	pathKey.Append(pszSubkey);
	LONG r = rk.Open(
		HKEY_CURRENT_USER,
		pathKey,
		KEY_READ|KEY_WRITE
		);
	if (r == 0) {
		{
			CEdit *pWndN = static_cast<CEdit *>(GetDlgItem(idN));
			if (pWndN != NULL) {
				CString s;
				pWndN->GetWindowText(s);

				rk.SetStringValue(_T("Name"), s);
			}
		}

		{
			CEdit *pWndL = static_cast<CEdit *>(GetDlgItem(idL));
			if (pWndL != NULL) {
				CString s;
				pWndL->GetWindowText(s);
				LPITEMIDLIST pidl = NULL;

				if (IDLUt::Parse(s, pidl)) {
					rk.SetBinaryValue(_T("Pidl"), pidl, IDLUt::GetSize(pidl));
					ILFree(pidl);
				}
				else {
					rk.DeleteValue(_T("Pidl"));
				}
			}
			else {
				rk.DeleteValue(_T("Pidl"));
			}
		}
	}
}

void CO2K0Dlg::WriteStd(LPCTSTR pszSubkey, int idV, int idO) {
	CRegKey rk;
	CString pathKey; pathKey.Format(_T("Software\\Microsoft\\Office\\%s\\Common\\Open Find\\Places\\StandardPlaces"), GetVer());
	pathKey.Append(_T("\\"));
	pathKey.Append(pszSubkey);
	LONG r = rk.Open(
		HKEY_CURRENT_USER,
		pathKey,
		KEY_READ|KEY_WRITE
		);
	if (r == 0) {
		{
			DWORD iIndex = 0;

			class IfIdBtnIs {
			public:
				int idBtn;

				IfIdBtnIs(int idBtn): idBtn(idBtn) { }

				bool operator ()(OrdItem &r) {
					return r.idBtn == idBtn;
				}
			};

			OrdItem 
				*pEnd = m_ordItems.GetData() +m_ordItems.GetCount(), 
				*pSel = std::find_if(m_ordItems.GetData(), pEnd, IfIdBtnIs(idO));

			if (pSel != pEnd)
				iIndex = pSel->iIndex;

			rk.SetDWORDValue(_T("Index"), iIndex);
		}

		{
			DWORD iShow = 1;

			CButton *pWndV = static_cast<CButton *>(GetDlgItem(idV));
			if (pWndV != NULL) {
				iShow = (BST_CHECKED == pWndV->GetCheck()) ? 1 : 0;
			}

			rk.SetDWORDValue(_T("Show"), iShow);
		}
	}
}

void CO2K0Dlg::Revert() {
	m_ordItems.RemoveAll();

	ReadStd(_T("Desktop")     , CSIDL_DESKTOP  , IDC_P1, IDC_V1, IDC_O1);
	ReadStd(_T("MyComputer")  , CSIDL_DRIVES   , IDC_P2, IDC_V2, IDC_O2);
	ReadStd(_T("MyDocuments") , CSIDL_PERSONAL , IDC_P3, IDC_V3, IDC_O3);
	ReadStd(_T("Publishing")  , CSIDL_NETWORK  , IDC_P4, IDC_V4, IDC_O4);
	ReadStd(_T("Recent")      , CSIDL_RECENT   , IDC_P5, IDC_V5, IDC_O5);

	ReadUser(_T("Place0"), IDC_N1, 0, IDC_L1);
	ReadUser(_T("Place1"), IDC_N2, 0, IDC_L2);
	ReadUser(_T("Place2"), IDC_N3, 0, IDC_L3);

	Reorder();
}

void CO2K0Dlg::ReadStd(LPCTSTR pszSubkey, int csidl, int idP, int idV, int idBtn) {
	CRegKey rk;
	CString pathKey; pathKey.Format(_T("Software\\Microsoft\\Office\\%s\\Common\\Open Find\\Places\\StandardPlaces"), GetVer());
	pathKey.Append(_T("\\"));
	pathKey.Append(pszSubkey);
	LONG r = rk.Open(
		HKEY_CURRENT_USER,
		pathKey,
		KEY_READ
		);
	if (r == 0) {
		DWORD iIndex = 0;
		if (0 != (r = rk.QueryDWORDValue(_T("Index"), iIndex))) {
			iIndex = 0;
		}

		m_ordItems.Add(OrdItem(pathKey, iIndex, idBtn));

		DWORD iShow = 1;
		if (0 != (r = rk.QueryDWORDValue(_T("Show"), iShow))) {
			iShow = 1;
		}

		{
			CButton *pWndV = static_cast<CButton *>(GetDlgItem(idV));
			if (pWndV != NULL) {
				pWndV->SetCheck((iShow != 0) ? BST_CHECKED : BST_UNCHECKED);
			}
		}

		{
			CStatic *pWndP = static_cast<CStatic *>(GetDlgItem(idP));
			if (pWndP != NULL) {
				pWndP->SetIcon(SHUt::GetIco(csidl));
			}
		}
	}
}

void CO2K0Dlg::ReadUser(LPCTSTR pszSubkey, int idN, int idBtn, int idL) {
	CRegKey rk;
	CString pathKey; pathKey.Format(_T("Software\\Microsoft\\Office\\%s\\Common\\Open Find\\Places\\UserDefinedPlaces"), GetVer());
	pathKey.Append(_T("\\"));
	pathKey.Append(pszSubkey);
	LONG r = rk.Open(
		HKEY_CURRENT_USER,
		pathKey,
		KEY_READ
		);
	if (r == 0) {
	//	DWORD iIndex = 0;
	//	if (0 != (r = rk.QueryDWORDValue(_T("Index"), iIndex))) {
	//		iIndex = 0;
	//	}
	//
	//	m_ordItems.Add(OrdItem(pathKey, iIndex, idBtn));

		{
			TCHAR tcName[1024 +1] = {_T("")};
			ULONG cch = 1024;
			rk.QueryStringValue(_T("Name"), tcName, &cch);
			tcName[cch] = 0;

			CStatic *pWndN = static_cast<CStatic *>(GetDlgItem(idN));
			if (pWndN != NULL) {
				pWndN->SetWindowText(tcName);
			}
		}

		{
			CEdit *pWndL = static_cast<CEdit *>(GetDlgItem(idL));
			if (pWndL != NULL) {
				BYTE idl[MAX_CB_PIDL];
				LPITEMIDLIST pidl = reinterpret_cast<LPITEMIDLIST>(idl);
				ULONG cbIdl = MAX_CB_PIDL;
				r = rk.QueryBinaryValue(_T("Pidl"), idl, &cbIdl);
				if (r == 0) {
					Ut::SetIdlTo(pidl, pWndL);
				}
			}
		}
	}
}

void CO2K0Dlg::Ut::SetIdlTo(LPITEMIDLIST pidl, CWnd *pWndL) {
	HRESULT hr;
	TCHAR tcDir[MAX_PATH] = {_T("")};
	hr = SHGetPathFromIDList(pidl, tcDir);
	if (SUCCEEDED(hr) && _tcslen(tcDir) != 0) {
		pWndL->SetWindowText(tcDir);
	}
	else {
		pWndL->SetWindowText(IDLUt::Format(pidl));
	}
}

void CO2K0Dlg::OnBnClickedO1(UINT nID) {
	CMenu m;
	if (m.CreatePopupMenu()) {
		for (int x=0; x<m_ordItems.GetCount(); x++) {
			CString s;
			s.Format(_T("%d"), 1 +x);
			m.AppendMenu(MF_STRING, IDC_ORDER1 +x, s);
		}
		DWORD pos = GetMessagePos();
		m_nID_OrderClick = nID;
		m.TrackPopupMenu(TPM_LEFTBUTTON, GET_X_LPARAM(pos), GET_Y_LPARAM(pos), this);
	}
}

void CO2K0Dlg::OnOrderClicked(UINT nID) {
	int iIndex = nID -IDC_ORDER1;
	if ((UINT)iIndex >= 100)
		return;

	for (int x=0; x<m_ordItems.GetCount(); x++) {
		if (m_ordItems[x].idBtn == m_nID_OrderClick) {
			int iCurIndex = m_ordItems[x].iIndex;
			m_ordItems[x].iIndex = (iCurIndex < iIndex) ? (iIndex +1) : (iIndex -1);
			break;
		}
	}

	Reorder();
}

void CO2K0Dlg::Reorder() {
	class Sort {
	public:
		static bool byIndexAsc(OrdItem &r1, OrdItem &r2) {
			return r1.iIndex < r2.iIndex;
		}
	};

	std::sort(
		m_ordItems.GetData(), 
		m_ordItems.GetData() +m_ordItems.GetCount(),
		Sort::byIndexAsc
		);
	for (int x=0; x<m_ordItems.GetCount(); x++) {
		m_ordItems[x].iIndex = x;
		
		CButton *pWndO = static_cast<CButton *>(GetDlgItem(m_ordItems[x].idBtn));
		if (pWndO != NULL) {
			CString s;
			s.Format(_T("%d"), 1 +x);
			pWndO->SetWindowText(s);
		}
	}
}

void CO2K0Dlg::OnBnClickedR1(UINT nID) {
	if (nID == IDC_R1) Browse(IDC_L1);
	if (nID == IDC_R2) Browse(IDC_L2);
	if (nID == IDC_R3) Browse(IDC_L3);
}

static int CALLBACK BCP(
	HWND hwnd,
	UINT uMsg,
	LPARAM lParam,
	LPARAM lpData
) {
	switch (uMsg) {
		case BFFM_INITIALIZED:
			if (lpData != 0)
				SendMessage(hwnd, BFFM_SETSELECTION, false, reinterpret_cast<LPARAM>(reinterpret_cast<LPITEMIDLIST>(lpData)));
			break;
	}
	return 1;
}

void CO2K0Dlg::Browse(int idL) {
	LPITEMIDLIST pidlRoot = NULL;

	CEdit *pWndL = static_cast<CEdit *>(GetDlgItem(idL));
	if (pWndL != NULL) {
		CString s;
		pWndL->GetWindowText(s);
		VERIFY(IDLUt::Parse(s, pidlRoot));
	}

	{
		CString strTitle;
		strTitle.LoadString(IDS_SEL_FOLDER);
		BROWSEINFO bi;
		ZeroMemory(&bi, sizeof(bi));
		bi.hwndOwner = GetSafeHwnd();
		bi.lParam = reinterpret_cast<LPARAM>(pidlRoot);
		bi.lpszTitle = strTitle;
		bi.pidlRoot = NULL;
		bi.lpfn = BCP;
		bi.ulFlags = 0
			|BIF_NEWDIALOGSTYLE
			|BIF_NONEWFOLDERBUTTON
			|BIF_RETURNONLYFSDIRS
			|BIF_RETURNFSANCESTORS
			;

		LPITEMIDLIST pidlNew = SHBrowseForFolder(&bi);
		if (pidlNew != NULL) {
			Ut::SetIdlTo(pidlNew, pWndL);
			CoTaskMemFree(pidlNew);
		}
	}
}

void CO2K0Dlg::OnBnClickedRevert() {
	Revert();
}

void CO2K0Dlg::OnBnClickedCommit() {
	if (Commit()) {
		AfxMessageBox(IDS_SAVE_DONE, MB_ICONINFORMATION);
	}
	else {
		AfxMessageBox(IDS_SAVE_FAILED, MB_ICONEXCLAMATION);
	}
}

BOOL CO2K0Dlg::OnSetActive() {
	return CPropertyPage::OnSetActive();
}

void CO2K0Dlg::OnBnClickedPreview() {
	CRegKey rk;
	CString pathKey; pathKey.Format(_T("SOFTWARE\\Microsoft\\Office\\%s\\Common\\InstallRoot"), GetVer());
	LONG r = rk.Open(
		HKEY_LOCAL_MACHINE,
		pathKey,
		KEY_READ
		);
	if (r == 0) {
		TCHAR tcDir[2048 +1] = {_T("")};
		ULONG cch = 2048;
		if (0 == (r = rk.QueryStringValue(_T("Path"), tcDir, &cch))) {
			tcDir[cch] = 0;

			CString exe = tcDir;
			exe += _T('\\');
			exe += _T("osa.exe");

			HINSTANCE hApp = ShellExecute(
				GetSafeHwnd(),
				_T("open"),
				exe,
				_T(" -f"),
				NULL,
				SW_SHOW
				);
			if ((UINT_PTR)hApp < 32) {
				AfxMessageBox(IDS_OSA_LAUNCH_FAILED, MB_ICONEXCLAMATION);
			}
		}
	}
	else {
		AfxMessageBox(IDS_OSA_LAUNCH_FAILED, MB_ICONEXCLAMATION);
	}
}
