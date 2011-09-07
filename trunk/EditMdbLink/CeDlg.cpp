// CeDlg.cpp : 実装ファイル
//

#include "stdafx.h"
#include "EditMdbLink.h"
#include "CeDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CeDlg ダイアログ




CeDlg::CeDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CeDlg::IDD, pParent)
	, m_strMdb(_T(""))
	, m_strConns(_T(""))
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CeDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT_MDB, m_strMdb);
	DDX_Text(pDX, IDC_CONNS, m_strConns);
}

BEGIN_MESSAGE_MAP(CeDlg, CDialog)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_REF_MDB, &CeDlg::OnBnClickedRefMdb)
	ON_BN_CLICKED(IDC_OPEN, &CeDlg::OnBnClickedOpen)
	ON_BN_CLICKED(IDC_SAVE, &CeDlg::OnBnClickedSave)
END_MESSAGE_MAP()


// CeDlg メッセージ ハンドラ

BOOL CeDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// このダイアログのアイコンを設定します。アプリケーションのメイン ウィンドウがダイアログでない場合、
	//  Framework は、この設定を自動的に行います。
	SetIcon(m_hIcon, TRUE);			// 大きいアイコンの設定
	SetIcon(m_hIcon, FALSE);		// 小さいアイコンの設定

	return TRUE;  // フォーカスをコントロールに設定した場合を除き、TRUE を返します。
}

// ダイアログに最小化ボタンを追加する場合、アイコンを描画するための
//  下のコードが必要です。ドキュメント/ビュー モデルを使う MFC アプリケーションの場合、
//  これは、Framework によって自動的に設定されます。

void CeDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 描画のデバイス コンテキスト

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// クライアントの四角形領域内の中央
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// アイコンの描画
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// ユーザーが最小化したウィンドウをドラッグしているときに表示するカーソルを取得するために、
//  システムがこの関数を呼び出します。
HCURSOR CeDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CeDlg::OnBnClickedRefMdb() {
	if (!UpdateData())
		return;
	CFileDialog fd(true, _T("mdb"), m_strMdb, OFN_ENABLESIZING|OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_LONGNAMES|OFN_PATHMUSTEXIST, _T("*.mdb|*.mdb||"), this);
	if (fd.DoModal() == IDOK) {
		m_strMdb = fd.GetPathName();
		UpdateData(false);
		return;
	}
}

void CeDlg::OnBnClickedOpen() {
	if (!UpdateData())
		return;
	TRY {
		CDaoDatabase db;
		db.Open(m_strMdb, true, false, NULL);
		int cx = db.GetTableDefCount();
		CString text;
		for (int x = 0; x < cx; x++) {
			CDaoTableDefInfo inf;
			db.GetTableDefInfo(x, inf, AFX_DAO_PRIMARY_INFO);
			if (0 == (inf.m_lAttributes & dbAttachedODBC))
				continue;
			CDaoTableDef table(&db);
			table.Open(inf.m_strName);
			CString strConnect = table.GetConnect();
			text.AppendFormat(_T("%s=%s\r\n"), inf.m_strName, strConnect);
		}
		m_strConns = text;
		UpdateData(false);
	}
	CATCH_ALL(e) {
		TCHAR tcErr[1000] = {0};
		e->GetErrorMessage(tcErr, 1000);
		AfxMessageBox(tcErr, MB_ICONEXCLAMATION);
	}
	END_CATCH_ALL;
}

AFX_INLINE UINT AFXAPI HashKey(const CString &key) {

}

void CeDlg::OnBnClickedSave() {
	if (IDYES != AfxMessageBox(ID_QUERY_SAVE, MB_ICONEXCLAMATION|MB_YESNO))
		return;
	if (!UpdateData())
		return;
	CMap<CString, LPCTSTR, CString, LPCTSTR> mapTable2Conn;
	{
		CString s = m_strConns;
		s.Replace(_T("\r\n"), _T("\n"));
		s.Replace(_T("\r"), _T("\n"));
		int i = 0;
		while (i < s.GetLength()) {
			int k = s.Find(_T('\n'), i);
			if (k < 0)
				k = s.GetLength();

			{
				CString kv = s.Mid(i, k - i);
				int v = kv.Find(_T('='));
				if (v >= 0) {
					CString strKey = kv.Left(v);
					CString strVal = kv.Mid(v +1);
					mapTable2Conn[strKey] = strVal;
					printf("");
				}
			}

			i = k + 1;
		}
	}

	TRY {
		CDaoDatabase db;
		db.Open(m_strMdb, true, false, NULL);
		int cx = db.GetTableDefCount();
		int cnt = 0;
		for (int x = 0; x < cx; x++) {
			CDaoTableDefInfo inf;
			db.GetTableDefInfo(x, inf, AFX_DAO_PRIMARY_INFO);
			if (0 == (inf.m_lAttributes & dbAttachedODBC))
				continue;
			CString strNewConn;
			if (!mapTable2Conn.Lookup(inf.m_strName, strNewConn))
				continue;
			CDaoTableDef table(&db);
			table.Open(inf.m_strName);
			CString strConn = table.GetConnect();
			if (strConn == strNewConn)
				continue;
			table.SetConnect(strNewConn);
			table.RefreshLink();
			cnt++;
		}
		AfxMessageBox((cnt != 0) ? IDS_CONN_UPDATED : IDS_CONN_UPDATE_NOT_REQUIRED, MB_ICONINFORMATION);
	}
	CATCH_ALL(e) {
		TCHAR tcErr[1000] = {0};
		e->GetErrorMessage(tcErr, 1000);
		AfxMessageBox(tcErr, MB_ICONEXCLAMATION);
	}
	END_CATCH_ALL;
}
