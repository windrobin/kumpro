// AxTIF5Ctrl.cpp :  CAxTIF5Ctrl ActiveX コントロール クラスの実装

#include "stdafx.h"
#include "AxTIF5.h"
#include "AxTIF5Ctrl.h"
#include "AxTIF5PropPage.h"

#include "AxTIF3Doc.h"
#include "AxTIF3View.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

BEGIN_INTERFACE_MAP(CAxTIF5Ctrl, COleControl)
	INTERFACE_PART(CAxTIF5Ctrl, IID_IObjectSafety, ObjectSafety)
END_INTERFACE_MAP() 

ULONG FAR EXPORT CAxTIF5Ctrl::XObjectSafety::AddRef()
{
    METHOD_PROLOGUE(CAxTIF5Ctrl, ObjectSafety)
    return pThis->ExternalAddRef();
}

ULONG FAR EXPORT CAxTIF5Ctrl::XObjectSafety::Release()
{
    METHOD_PROLOGUE(CAxTIF5Ctrl, ObjectSafety)
    return pThis->ExternalRelease();
}

HRESULT FAR EXPORT CAxTIF5Ctrl::XObjectSafety::QueryInterface(
    REFIID iid, void FAR* FAR* ppvObj)
{
    METHOD_PROLOGUE(CAxTIF5Ctrl, ObjectSafety)
    return (HRESULT)pThis->ExternalQueryInterface(&iid, ppvObj);
}

const DWORD dwSupportedSafety = 0
	|INTERFACESAFE_FOR_UNTRUSTED_CALLER
	|INTERFACESAFE_FOR_UNTRUSTED_DATA;

HRESULT STDMETHODCALLTYPE CAxTIF5Ctrl::XObjectSafety::GetInterfaceSafetyOptions( // ATL's GetInterfaceSafetyOptions
    /* [in] */ REFIID riid,
    /* [out] */ DWORD *pdwSupportedOptions,
    /* [out] */ DWORD *pdwEnabledOptions)
{
    METHOD_PROLOGUE(CAxTIF5Ctrl, ObjectSafety)

	if (pdwSupportedOptions == NULL || pdwEnabledOptions == NULL)
		return E_POINTER;

	HRESULT hr;
	IUnknown* pUnk;
	// Check if we support this interface
	hr = pThis->ExternalQueryInterface(&riid, (void**)&pUnk);
	if (SUCCEEDED(hr))
	{
		// We support this interface so set the safety options accordingly
		pUnk->Release();	// Release the interface we just acquired
		*pdwSupportedOptions = dwSupportedSafety;
		*pdwEnabledOptions   = m_dwCurrentSafety;
	}
	else
	{
		// We don't support this interface
		*pdwSupportedOptions = 0;
		*pdwEnabledOptions   = 0;
	}
	return hr;
}

HRESULT STDMETHODCALLTYPE CAxTIF5Ctrl::XObjectSafety::SetInterfaceSafetyOptions( // ATL's SetInterfaceSafetyOptions
    /* [in] */ REFIID riid,
    /* [in] */ DWORD dwOptionSetMask,
    /* [in] */ DWORD dwEnabledOptions)
{
    METHOD_PROLOGUE(CAxTIF5Ctrl, ObjectSafety)

	IUnknown* pUnk;

	// Check if we support the interface and return E_NOINTEFACE if we don't
	if (FAILED(pThis->ExternalQueryInterface(&riid, (void**)&pUnk)))
		return E_NOINTERFACE;
	pUnk->Release();	// Release the interface we just acquired

	// If we are asked to set options we don't support then fail
	if (dwOptionSetMask & ~dwSupportedSafety)
		return E_FAIL;

	// Set the safety options we have been asked to
	m_dwCurrentSafety = (m_dwCurrentSafety  & ~dwOptionSetMask)  |  (dwOptionSetMask & dwEnabledOptions);		
	return S_OK;
}


IMPLEMENT_DYNCREATE(CAxTIF5Ctrl, COleControl)



// メッセージ マップ

BEGIN_MESSAGE_MAP(CAxTIF5Ctrl, COleControl)
	ON_OLEVERB(AFX_IDS_VERB_PROPERTIES, OnProperties)
	ON_WM_CREATE()
	ON_WM_SIZE()
	ON_WM_ERASEBKGND()
END_MESSAGE_MAP()



// ディスパッチ マップ

BEGIN_DISPATCH_MAP(CAxTIF5Ctrl, COleControl)
	DISP_FUNCTION_ID(CAxTIF5Ctrl, "AboutBox", DISPID_ABOUTBOX, AboutBox, VT_EMPTY, VTS_NONE)
END_DISPATCH_MAP()



// イベント マップ

BEGIN_EVENT_MAP(CAxTIF5Ctrl, COleControl)
END_EVENT_MAP()



// プロパティ ページ

// TODO: プロパティ ページを追加して、BEGIN 行の最後にあるカウントを増やしてください。
BEGIN_PROPPAGEIDS(CAxTIF5Ctrl, 1)
	PROPPAGEID(CAxTIF5PropPage::guid)
END_PROPPAGEIDS(CAxTIF5Ctrl)



// クラス ファクトリおよび GUID を初期化します。

IMPLEMENT_OLECREATE_EX(CAxTIF5Ctrl, "AXTIF5.AxTIF5Ctrl.1",
	0x5936e26, 0x30e9, 0x4210, 0x84, 0xa6, 0xa0, 0x59, 0xb4, 0x51, 0x2d, 0x14)



// タイプ ライブラリ ID およびバージョン

IMPLEMENT_OLETYPELIB(CAxTIF5Ctrl, _tlid, _wVerMajor, _wVerMinor)



// インターフェイス ID

const IID BASED_CODE IID_DAxTIF5 =
		{ 0x56EC6F0A, 0x34F6, 0x4D86, { 0x96, 0x95, 0x7B, 0x1E, 0x6F, 0xB6, 0x18, 0x98 } };
const IID BASED_CODE IID_DAxTIF5Events =
		{ 0xCCBC4D8D, 0x1D02, 0x4CD2, { 0x9E, 0xD0, 0xF9, 0x6B, 0xDD, 0x8C, 0x4A, 0x69 } };



// コントロールの種類の情報

static const DWORD BASED_CODE _dwAxTIF5OleMisc =
	OLEMISC_ACTIVATEWHENVISIBLE |
	OLEMISC_SETCLIENTSITEFIRST |
	OLEMISC_INSIDEOUT |
	OLEMISC_CANTLINKINSIDE |
	OLEMISC_RECOMPOSEONRESIZE;

IMPLEMENT_OLECTLTYPE(CAxTIF5Ctrl, IDS_AXTIF5, _dwAxTIF5OleMisc)



// CAxTIF5Ctrl::CAxTIF5CtrlFactory::UpdateRegistry -
// CAxTIF5Ctrl のシステム レジストリ エントリを追加または削除します。

BOOL CAxTIF5Ctrl::CAxTIF5CtrlFactory::UpdateRegistry(BOOL bRegister)
{
	// TODO: コントロールが apartment モデルのスレッド処理の規則に従っていることを
	// 確認してください。詳細については MFC のテクニカル ノート 64 を参照してください。
	// アパートメント モデルのスレッド処理の規則に従わないコントロールの場合は、6 番目
	// のパラメータを以下のように変更してください。
	// afxRegApartmentThreading を 0 に設定します。

	if (bRegister)
		return AfxOleRegisterControlClass(
			AfxGetInstanceHandle(),
			m_clsid,
			m_lpszProgID,
			IDS_AXTIF5,
			IDB_AXTIF5,
			afxRegApartmentThreading,
			_dwAxTIF5OleMisc,
			_tlid,
			_wVerMajor,
			_wVerMinor);
	else
		return AfxOleUnregisterClass(m_clsid, m_lpszProgID);
}



// CAxTIF5Ctrl::CAxTIF5Ctrl - コンストラクタ

CAxTIF5Ctrl::CAxTIF5Ctrl()
{
	InitializeIIDs(&IID_DAxTIF5, &IID_DAxTIF5Events);
}



// CAxTIF5Ctrl::~CAxTIF5Ctrl - デストラクタ

CAxTIF5Ctrl::~CAxTIF5Ctrl()
{
	// TODO: この位置にコントロールのインスタンス データの後処理用のコードを追加してください
}



// CAxTIF5Ctrl::OnDraw - 描画関数

void CAxTIF5Ctrl::OnDraw(
			CDC* pdc, const CRect& rcBounds, const CRect& rcInvalid)
{
#if 0
	if (!pdc)
		return;

	CRect rc = rcBounds;
	CBrush br;
	br.CreateHatchBrush(HS_BDIAGONAL, GetSysColor(COLOR_3DHILIGHT));
	pdc->FillRect(rc, &br);
	pdc->SelectStockObject(SYSTEM_FONT);
	pdc->DrawText(m_src, rc, DT_SINGLELINE|DT_CENTER|DT_VCENTER);
#endif
}

// CAxTIF5Ctrl::DoPropExchange - 永続性のサポート

void CAxTIF5Ctrl::DoPropExchange(CPropExchange* pPX)
{
	ExchangeVersion(pPX, MAKELONG(_wVerMinor, _wVerMajor));
	COleControl::DoPropExchange(pPX);

	PX_String(pPX, _T("src"), m_src);

	HRESULT hr;

	CComPtr<IBindCtx> pibc;
	if (SUCCEEDED(hr = CreateBindCtx(0, &pibc))) {
		IOleClientSite *pClientSite = GetClientSite();
		if (pClientSite != NULL) {
			CComPtr<IMoniker> pimkDL;
			if (pimkDL == NULL) {
				CComQIPtr<IBindHost> pBindHost = pClientSite;
				if (pBindHost != NULL) {
					if (SUCCEEDED(hr = pBindHost->CreateMoniker(CT2W(m_src), pibc, &pimkDL, 0))) {

					}
				}
			}
			if (pimkDL == NULL) {
				CComPtr<IMoniker> pimkName;
				if (SUCCEEDED(hr = pClientSite->GetMoniker(OLEGETMONIKER_FORCEASSIGN, OLEWHICHMK_CONTAINER, &pimkName))) {
					CComPtr<IBindCtx> pibc;
					CreateBindCtx(0, &pibc);
					ULONG chEaten = 0;
					if (SUCCEEDED(hr = pimkName->ParseDisplayName(pibc, pimkName, CT2W(m_src), &chEaten, &pimkDL))) {

					}
				}
			}

			if (pimkDL != NULL) {
				LPOLESTR pszDisplayName = NULL;
				if (SUCCEEDED(hr = pimkDL->GetDisplayName(pibc, NULL, &pszDisplayName))) {
					m_title = pszDisplayName;
					CoTaskMemFree(pszDisplayName);
				}
			}

			m_pimkDL = pimkDL;

			LoadFromMoniker(pibc, pimkDL);
		}
	}
}

// CAxTIF5Ctrl::OnResetState - コントロールを既定の状態にリセットします。

void CAxTIF5Ctrl::OnResetState()
{
	COleControl::OnResetState();  // DoPropExchange を呼び出して既定値にリセット

	// TODO: この位置にコントロールの状態をリセットする処理を追加してください
}



// CAxTIF5Ctrl::AboutBox - "バージョン情報" ボックスをユーザーに表示します。

void CAxTIF5Ctrl::AboutBox()
{
	CDialog dlgAbout(IDD_ABOUTBOX_AXTIF5);
	dlgAbout.DoModal();
}



// CAxTIF5Ctrl メッセージ ハンドラ

void CAxTIF5Ctrl::OnShowToolBars() {
	//AfxMessageBox(m_src, MB_ICONINFORMATION);
}

int CAxTIF5Ctrl::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (COleControl::OnCreate(lpCreateStruct) == -1)
		return -1;

	CCreateContext a;

	a.m_pCurrentDoc = STATIC_DOWNCAST(CAxTIF3Doc,RUNTIME_CLASS(CAxTIF3Doc)->CreateObject());
	a.m_pCurrentFrame = NULL;
	a.m_pLastView = NULL;
	a.m_pNewDocTemplate = NULL;
	a.m_pNewViewClass = RUNTIME_CLASS(CAxTIF3View);

	m_frame.DestroyWindow();

	BOOL f = m_frame.Create(
		AfxRegisterWndClass(CS_HREDRAW|CS_VREDRAW, 0, 0, 0),
		NULL,
		WS_CHILDWINDOW,
		CRect(0, 0, 100, 100),
		this,
		0,
		0,
		&a
		);
	if (f) {
		m_frame.InitialUpdateFrame(a.m_pCurrentDoc, true);

		HRESULT hr;
		CComPtr<IBindCtx> pibc;
		if (SUCCEEDED(hr =CreateBindCtx(0, &pibc))) {
			LoadFromMoniker(pibc, m_pimkDL);
		}
	}
	else return -1;

	return 0;
}

void CAxTIF5Ctrl::OnSize(UINT nType, int cx, int cy)
{
	COleControl::OnSize(nType, cx, cy);

	m_frame.SetWindowPos(NULL, 0, 0, cx, cy, SWP_NOACTIVATE|SWP_NOMOVE|SWP_NOOWNERZORDER|SWP_NOZORDER);
}

BOOL CAxTIF5Ctrl::PreCreateWindow(CREATESTRUCT& cs)
{
	if (!COleControl::PreCreateWindow(cs))
		return false;

	cs.style |= WS_CLIPCHILDREN|WS_CLIPSIBLINGS;
	cs.dwExStyle &= ~WS_EX_CLIENTEDGE;
	return true;
}

void CAxTIF5Ctrl::LoadFromMoniker(LPBC pibc, LPMONIKER pimkDL) {
	HRESULT hr;

	if (pimkDL != NULL) {
		CDocument *document = m_frame.GetActiveDocument();
		if (document != NULL && m_frame.GetSafeHwnd() != NULL) {
			CComPtr<IStream> pSt;
			if (SUCCEEDED(hr = pimkDL->BindToStorage(pibc, NULL, IID_IStream, reinterpret_cast<void **>(&pSt)))) {
				COleStreamFile fIn;
				fIn.Attach(pSt);

				CArchive ar(&fIn, CArchive::load);

				document->Serialize(ar);
				document->UpdateAllViews(NULL);
			}
		}
	}
}

BOOL CAxTIF5Ctrl::OnEraseBkgnd(CDC* pDC) {
	return 1;
//	return COleControl::OnEraseBkgnd(pDC);
}

BOOL CAxTIF5Ctrl::OnSetExtent(LPSIZEL lpSizeL)
{
//	if (m_frame.GetSafeHwnd() != NULL)
//		m_frame.SetWindowPos(NULL, 0, 0, lpSizeL->cx, lpSizeL->cy, SWP_NOACTIVATE|SWP_NOMOVE|SWP_NOOWNERZORDER|SWP_NOZORDER);

	return COleControl::OnSetExtent(lpSizeL);
}
