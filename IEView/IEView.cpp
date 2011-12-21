// IEView.cpp : アプリケーションのクラス動作を定義します。
//

#include "stdafx.h"
#include "IEView.h"
#include "MainFrm.h"

#include "IEViewDoc.h"
#include "IEViewView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CApp

BEGIN_MESSAGE_MAP(CApp, CWinApp)
	ON_COMMAND(ID_APP_ABOUT, &CApp::OnAppAbout)
	// 標準のファイル基本ドキュメント コマンド
	ON_COMMAND(ID_FILE_NEW, &CWinApp::OnFileNew)
	ON_COMMAND(ID_FILE_OPEN, &CWinApp::OnFileOpen)
	// 標準の印刷セットアップ コマンド
	ON_COMMAND(ID_FILE_PRINT_SETUP, &CWinApp::OnFilePrintSetup)
END_MESSAGE_MAP()


// CApp コンストラクション

CApp::CApp()
{
	// TODO: この位置に構築用コードを追加してください。
	// ここに InitInstance 中の重要な初期化処理をすべて記述してください。
}


// 唯一の CApp オブジェクトです。

CApp theApp;
// この ID はこのアプリケーションが統計的に一意なものとなるように生成されました。
// 変更して特定な ID を指定することもできます。

// {6B8B1B23-FE04-4308-999A-B5E7064E849F}
static const CLSID clsid =
{ 0x6B8B1B23, 0xFE04, 0x4308, { 0x99, 0x9A, 0xB5, 0xE7, 0x6, 0x4E, 0x84, 0x9F } };

const GUID CDECL BASED_CODE _tlid =
		{ 0xFE4D822C, 0x9BFC, 0x451E, { 0x94, 0x99, 0xDA, 0x5D, 0xC9, 0xFF, 0x3B, 0x1E } };
const WORD _wVerMajor = 1;
const WORD _wVerMinor = 0;

CString g_strUrl = _T("http://www.google.com/");

// CApp 初期化

BOOL CApp::InitInstance()
{
	// アプリケーション マニフェストが visual スタイルを有効にするために、
	// ComCtl32.dll Version 6 以降の使用を指定する場合は、
	// Windows XP に InitCommonControlsEx() が必要です。さもなければ、ウィンドウ作成はすべて失敗します。
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	// アプリケーションで使用するすべてのコモン コントロール クラスを含めるには、
	// これを設定します。
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);

	CWinApp::InitInstance();

	// OLE ライブラリを初期化します。
	if (!AfxOleInit())
	{
		AfxMessageBox(IDP_OLE_INIT_FAILED);
		return FALSE;
	}
	AfxEnableControlContainer();
	// 標準初期化
	// これらの機能を使わずに最終的な実行可能ファイルの
	// サイズを縮小したい場合は、以下から不要な初期化
	// ルーチンを削除してください。
	// 設定が格納されているレジストリ キーを変更します。
	// TODO: 会社名または組織名などの適切な文字列に
	// この文字列を変更してください。
	SetRegistryKey(_T("アプリケーション ウィザードで生成されたローカル アプリケーション"));
	LoadStdProfileSettings(0);  // 標準の INI ファイルのオプションをロードします (MRU を含む)
	// アプリケーション用のドキュメント テンプレートを登録します。ドキュメント テンプレート
	//  はドキュメント、フレーム ウィンドウとビューを結合するために機能します。
	CSingleDocTemplate* pDocTemplate;
	pDocTemplate = new CSingleDocTemplate(
		IDR_MAINFRAME,
		RUNTIME_CLASS(CDoc),
		RUNTIME_CLASS(CMainFrame),       // メイン SDI フレーム ウィンドウ
		RUNTIME_CLASS(CVw));
	if (!pDocTemplate)
		return FALSE;
	AddDocTemplate(pDocTemplate);
	// ドキュメント テンプレートに COleTemplateServer を接続します。
	//  COleTemplateServer はドキュメント テンプレートで指定された
	//  情報を使って OLE コンテナに要求する代わりに新規ドキュメント
	//  を作成します。
	m_server.ConnectTemplate(clsid, pDocTemplate, TRUE);
		// メモ: SDI アプリケーションはコマンド ラインで /Embedding か /Automation
		//   が指定されている時だけサーバー オブジェクトを登録します。



	// DDE、file open など標準のシェル コマンドのコマンド ラインを解析します。
	CCommandLineInfo cmdInfo;
	ParseCommandLine(cmdInfo);

	g_strUrl = cmdInfo.m_strFileName;

	// アプリケーションが /Embedding または /Automation スイッチで起動されました。
	// アプリケーションをオートメーション サーバーとして実行します。
	if (cmdInfo.m_bRunEmbedded || cmdInfo.m_bRunAutomated)
	{
		// すべての OLE サーバー ファクトリを実行中として登録します。
		//  これによって、OLE ライブラリが、その他のアプリケーションからオブジェクトを作成できるようになります。
		COleTemplateServer::RegisterAll();

		// メイン ウィンドウを表示しません。
		return TRUE;
	}
	// アプリケーションが、/Unregserver または /Unregister スイッチで起動されました。タイプ ライブラリの
	// 登録を解除してください。他の登録解除は ProcessShellCommand() で発生します。
	else if (cmdInfo.m_nShellCommand == CCommandLineInfo::AppUnregister)
	{
		m_server.UpdateRegistry(OAT_DISPATCH_OBJECT, NULL, NULL, FALSE);
		AfxOleUnregisterTypeLib(_tlid, _wVerMajor, _wVerMinor);
	}
	// アプリケーションがスタンドアロンまたは/Register や /Regserver などのスイッチで起動されました。
	// タイプ ライブラリを含むレジストリ エントリを更新します。
	else
	{
		m_server.UpdateRegistry(OAT_DISPATCH_OBJECT);
		COleObjectFactory::UpdateRegistryAll();
		AfxOleRegisterTypeLib(AfxGetInstanceHandle(), _tlid);
	}

	// コマンド ラインで指定されたディスパッチ コマンドです。アプリケーションが
	// /RegServer、/Register、/Unregserver または /Unregister で起動された場合、False を返します。
	if (!ProcessShellCommand(cmdInfo))
		return FALSE;

	// メイン ウィンドウが初期化されたので、表示と更新を行います。
	m_pMainWnd->ShowWindow(SW_SHOW);
	m_pMainWnd->UpdateWindow();
	// 接尾辞が存在する場合にのみ DragAcceptFiles を呼び出してください。
	//  SDI アプリケーションでは、ProcessShellCommand の直後にこの呼び出しが発生しなければなりません。
	return TRUE;
}



// アプリケーションのバージョン情報に使われる CAboutDlg ダイアログ

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// ダイアログ データ
	enum { IDD = IDD_ABOUTBOX };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート

// 実装
protected:
	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BOOL CAboutDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// http://social.msdn.microsoft.com/Forums/ja/windowsmobileja/thread/1ffe9217-8d62-4ad3-bb33-1c30d79fe951

	class UtVer {
	public:
		UtVer() {
			hMod = AfxGetResourceHandle();
			hVeri = FindResource(hMod, MAKEINTRESOURCE(VS_VERSION_INFO), RT_VERSION);
			pvVeri = LockResource(LoadResource(hMod, hVeri));
			cbVeri = SizeofResource(hMod, hVeri);
			if (pvVeri != NULL && cbVeri != 0) {
			}
		}

		CString GetTranslation() const {
			PVOID pvAny = NULL;
			UINT cb = 0;
			CString strRet;
			if (VerQueryValue(pvVeri, _T("\\VarFileInfo\\Translation"), &pvAny, &cb)) {
				DWORD nLangCodePage = *reinterpret_cast<LPDWORD>(pvAny);
				strRet.Format(_T("%04x%04x"), 0U +LOWORD(nLangCodePage), 0U +HIWORD(nLangCodePage));
			}
			return strRet;
		}

		CString GetProductName() const {
			return GetVerInfoStr(_T("ProductName"));
		}
		CString GetProductVersion() const {
			return GetVerInfoStr(_T("ProductVersion"));
		}
		CString GetLegalCopyright() const {
			return GetVerInfoStr(_T("LegalCopyright"));
		}
		CString GetCompanyName() const {
			return GetVerInfoStr(_T("CompanyName"));
		}

		CString GetVerInfoStr(LPCTSTR pszName) const {
			CString strPath;
			strPath.Format(_T("\\StringFileInfo\\%s\\%s"), static_cast<LPCTSTR>(GetTranslation()), pszName);
			LPWSTR pcwAny = NULL;
			UINT cb = 0;
			CString strRet;
			if (VerQueryValue(pvVeri, strPath, reinterpret_cast<LPVOID *>(&pcwAny), &cb)) {
				strRet = pcwAny;
			}
			return strRet;
		}

	#pragma comment(lib, "version.lib")

	protected:
		HMODULE hMod;
		HRSRC hVeri;
		PVOID pvVeri;
		DWORD cbVeri;
	};

	UtVer ver;

	{
		CString str; str.Format(_T("%s Version %s")
			, static_cast<LPCTSTR>(ver.GetProductName())
			, static_cast<LPCTSTR>(ver.GetProductVersion())
			);
		::SetWindowText(GetDlgItem(IDC_STATIC_UPPER)->GetSafeHwnd(), str);
	}
	{
		CString str; str.Format(_T("Copyright %s %s")
			, static_cast<LPCTSTR>(ver.GetLegalCopyright())
			, static_cast<LPCTSTR>(ver.GetCompanyName())
			);
		::SetWindowText(GetDlgItem(IDC_STATIC_LOWER)->GetSafeHwnd(), str);
	}

	return TRUE;  // return TRUE unless you set the focus to a control
	// 例外 : OCX プロパティ ページは必ず FALSE を返します。
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()

// ダイアログを実行するためのアプリケーション コマンド
void CApp::OnAppAbout()
{
	CAboutDlg aboutDlg;
	aboutDlg.DoModal();
}


// CApp メッセージ ハンドラ
