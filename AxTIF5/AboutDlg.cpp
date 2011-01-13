// AboutDlg.cpp : 実装ファイル
//

#include "stdafx.h"
#include "AxTIF5.h"
#include "AboutDlg.h"

#pragma comment(lib, "version.lib")

// CAboutDlg ダイアログ

IMPLEMENT_DYNAMIC(CAboutDlg, CDialog)

CAboutDlg::CAboutDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CAboutDlg::IDD, pParent)
{

}

CAboutDlg::~CAboutDlg()
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_CREDITS, m_credits);
	DDX_Control(pDX, IDC_VER, m_wndVer);
}


BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()


// CAboutDlg メッセージ ハンドラ

DWORD CALLBACK ESCallback(DWORD_PTR dwCookie, LPBYTE pbBuff, LONG cb, LONG *pcb) {
	TRY {
		*pcb = reinterpret_cast<CMemFile *>(dwCookie)->Read(pbBuff, cb);
		return 0;
	}
	CATCH_ALL(e) {
		return 1;
	}
	END_CATCH_ALL;
}

BOOL CAboutDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	{
		HMODULE hMod = AfxGetInstanceHandle();
		HRSRC hRtf = FindResource(hMod, MAKEINTRESOURCE(IDR_CREDITS), "RTF");
		LPVOID pvBuff = LockResource(LoadResource(hMod, hRtf));
		DWORD cbBuff = SizeofResource(hMod, hRtf);
		CMemFile f(reinterpret_cast<PBYTE>(pvBuff), cbBuff);

		EDITSTREAM st;
		ZeroMemory(&st, sizeof(st));
		st.dwCookie = reinterpret_cast<DWORD_PTR>(&f);
		st.dwError = 0;
		st.pfnCallback = ESCallback;
		m_credits.StreamIn(SF_RTF, st);
	}

	{
		CString strVer = _T("?");

		{
			HMODULE hMod = AfxGetResourceHandle();
			HRSRC hVeri = FindResource(hMod, MAKEINTRESOURCE(VS_VERSION_INFO), RT_VERSION);
			PVOID pvVeri = LockResource(LoadResource(hMod, hVeri));
			DWORD cbVeri = SizeofResource(hMod, hVeri);
			if (pvVeri != NULL && cbVeri != 0) {
				VS_FIXEDFILEINFO *pffi = NULL;
				UINT cb = 0;
				if (VerQueryValue(pvVeri, _T("\\"), reinterpret_cast<LPVOID *>(&pffi), &cb)) {
					if (pffi->dwSignature == 0xFEEF04BD) {
						strVer.Format(_T("%u.%u.%u")
							, 0U +(WORD)(pffi->dwFileVersionMS >> 16)
							, 0U +(WORD)(pffi->dwFileVersionMS >> 0)
							, 0U +(WORD)(pffi->dwFileVersionLS >> 16)
							, 0U +(WORD)(pffi->dwFileVersionLS >> 0)
							);
					}
				}
			}
		}

		{
			CString text;
			m_wndVer.GetWindowText(text);
			text.Replace(_T("###"), strVer);
			m_wndVer.SetWindowText(text);
		}
	}

	return TRUE;  // return TRUE unless you set the focus to a control
	// 例外 : OCX プロパティ ページは必ず FALSE を返します。
}
