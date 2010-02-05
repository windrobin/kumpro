// Closer.h : CCloser の宣言

#pragma once
#include "resource.h"       // メイン シンボル

#include "Noime4ntr.h"

#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "DCOM の完全サポートを含んでいない Windows Mobile プラットフォームのような Windows CE プラットフォームでは、単一スレッド COM オブジェクトは正しくサポートされていません。ATL が単一スレッド COM オブジェクトの作成をサポートすること、およびその単一スレッド COM オブジェクトの実装の使用を許可することを強制するには、_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA を定義してください。ご使用の rgs ファイルのスレッド モデルは 'Free' に設定されており、DCOM Windows CE 以外のプラットフォームでサポートされる唯一のスレッド モデルと設定されていました。"
#endif

#include "L.h"

// CCloser

class Subclassed {
public:
	WNDPROC lpPrevWndFunc;
};
extern CRBMap<HWND, Subclassed> s_tree;
extern CMyMutex s_lck;

class ATL_NO_VTABLE CCloser :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCloser, &CLSID_Closer>,
	public IDispatchImpl<ICloser, &IID_ICloser, &LIBID_Noime4ntrLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
	public IObjectWithSite
{
public:
	CCloser()
	{

	}

DECLARE_REGISTRY_RESOURCEID(IDR_CLOSER)

DECLARE_NOT_AGGREGATABLE(CCloser)

BEGIN_COM_MAP(CCloser)
	COM_INTERFACE_ENTRY(ICloser)
	COM_INTERFACE_ENTRY2(IDispatch, IDispatchImpl)
	COM_INTERFACE_ENTRY(IObjectWithSite)
END_COM_MAP()

public:
	// IObjectWithSite ->

	DWORD m_iSiteCookie;

	CComQIPtr<IWebBrowser2> m_pSite;

	void siteClose() {
		if (m_pSite != NULL) {
			if (m_iSiteCookie != 0) {
				AtlUnadvise(m_pSite, DIID_DWebBrowserEvents2, m_iSiteCookie);

				m_iSiteCookie = 0;
			}
		}
	}

	void siteOpen() {
		if (m_pSite != NULL) {
			if (m_iSiteCookie == 0) {
				AtlAdvise(m_pSite, static_cast<IDispatchImpl *>(this), DIID_DWebBrowserEvents2, &m_iSiteCookie);
			}
		}
	}

    virtual HRESULT STDMETHODCALLTYPE SetSite( 
        /* [in] */ IUnknown *pUnkSite)
	{
		siteClose();

		m_pSite = pUnkSite;

		siteOpen();

		return S_OK;
	}
    
    virtual HRESULT STDMETHODCALLTYPE GetSite( 
        /* [in] */ REFIID riid,
        /* [iid_is][out] */ void **ppvSite)
	{
		if (ppvSite == NULL)
			return E_POINTER;
		if (m_pSite == NULL)
			return E_FAIL;
		if (riid == IID_IWebBrowser2) {
			*ppvSite = reinterpret_cast<void **>(static_cast<IWebBrowser2 *>(m_pSite));
			AddRef();
			return S_OK;
		}
		return E_NOINTERFACE;
	}

	// <- IObjectWithSite

	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct() {
		m_iSiteCookie = 0;
		return S_OK;
	}

	void FinalRelease() {
		siteClose();
	}

public:
	STDMETHOD(NavigateComplete2)(IDispatch* pDisp, VARIANT* URL);
};

OBJECT_ENTRY_AUTO(__uuidof(Closer), CCloser)
