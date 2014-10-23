// MyDM.h : CMyDM の宣言

#pragma once
#include "resource.h"       // メイン シンボル

#include "TestDownloadManager.h"


#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "DCOM の完全サポートを含んでいない Windows Mobile プラットフォームのような Windows CE プラットフォームでは、単一スレッド COM オブジェクトは正しくサポートされていません。ATL が単一スレッド COM オブジェクトの作成をサポートすること、およびその単一スレッド COM オブジェクトの実装の使用を許可することを強制するには、_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA を定義してください。ご使用の rgs ファイルのスレッド モデルは 'Free' に設定されており、DCOM Windows CE 以外のプラットフォームでサポートされる唯一のスレッド モデルと設定されていました。"
#endif



// CMyDM

class ATL_NO_VTABLE CMyDM :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CMyDM, &CLSID_MyDM>,
	public IDispatchImpl<IMyDM, &IID_IMyDM, &LIBID_TestDownloadManagerLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
	public IDownloadManager
{
public:
	CMyDM()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_MYDM)


BEGIN_COM_MAP(CMyDM)
	COM_INTERFACE_ENTRY(IMyDM)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(IDownloadManager)
END_COM_MAP()

public:

	HRESULT STDAPICALLTYPE Download(
		IMoniker *pmk,
		IBindCtx *pbc,
		DWORD dwBindVerb,
		LONG grfBINDF,
		BINDINFO *pBindInfo,
		LPCOLESTR pszHeaders,
		LPCOLESTR pszRedir,
		UINT uiCP
	) {
		LPOLESTR psz = NULL;
		HRESULT hr;
		hr = pmk->GetDisplayName(pbc, NULL, &psz);
		MessageBox(NULL, psz, NULL, MB_ICONINFORMATION);
		CoTaskMemFree(psz);
		CreateURLMoniker(NULL, NULL, NULL);
		CreateURLMonikerEx(NULL, NULL, NULL, 0);
		return S_OK;
	}

	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:

};

OBJECT_ENTRY_AUTO(__uuidof(MyDM), CMyDM)
