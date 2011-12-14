// Subf.h : CSubf の宣言

#pragma once
#include "resource.h"       // メイン シンボル

#include "MyFav.h"


#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "DCOM の完全サポートを含んでいない Windows Mobile プラットフォームのような Windows CE プラットフォームでは、単一スレッド COM オブジェクトは正しくサポートされていません。ATL が単一スレッド COM オブジェクトの作成をサポートすること、およびその単一スレッド COM オブジェクトの実装の使用を許可することを強制するには、_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA を定義してください。ご使用の rgs ファイルのスレッド モデルは 'Free' に設定されており、DCOM Windows CE 以外のプラットフォームでサポートされる唯一のスレッド モデルと設定されていました。"
#endif



// CSubf

class ATL_NO_VTABLE CSubf :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CSubf, &CLSID_Subf>,
	public IEnumIDList,
	public ISubf
{
	// IEnumIDList : public IUnknown
public:
	virtual /* [local] */ HRESULT STDMETHODCALLTYPE Next( 
		/* [annotation][in] */ 
		__in  ULONG celt,
		/* [annotation][length_is][size_is][out] */ 
		__out_ecount_part(celt, *pceltFetched)  PITEMID_CHILD *rgelt,
		/* [annotation][out] */ 
		__out_opt __deref_out_range(0, celt)  ULONG *pceltFetched)
	{
		ATLASSERT(rgelt != NULL);

		if (celt > 1 && pceltFetched == NULL)
			return E_INVALIDARG;

		ULONG dummy = 0;
		if (pceltFetched == NULL)
			pceltFetched = &dummy;

		*pceltFetched = 0; // rgelt[*pceltFetched]

		while (celt != 0) {
			if (pos >= folders.GetSize())
				return S_FALSE;
			BYTE buff[1000] = {0}; // zeromem
			LPITEMIDLIST pidl = reinterpret_cast<LPITEMIDLIST>(buff);
			int cb = 2 + sizeof(WCHAR) * (folders[pos].GetLength() + 1); // including NULL char
			if (cb > 1000 - 2)
				return E_UNEXPECTED;
			pidl->mkid.cb = cb;
			memcpy(pidl->mkid.abID, static_cast<LPCWSTR>(folders[pos]), cb - 2);
			// at least, last 2 bytes set "0" for cb = 0 termination.
			rgelt[*pceltFetched] = ILClone(pidl);

			++pos;
			--celt;
			++*pceltFetched;
		}
		return S_OK;
	}
	
	virtual HRESULT STDMETHODCALLTYPE Skip( 
		/* [in] */ ULONG celt)
	{
		while (celt != 0) {
			if (pos >= folders.GetSize())
				return S_FALSE;

			--celt;
		}

		return S_OK;
	}
	
	virtual HRESULT STDMETHODCALLTYPE Reset( void)
	{
		pos = 0;
		return S_OK;
	}
	
	virtual HRESULT STDMETHODCALLTYPE Clone( 
		/* [out] */ __RPC__deref_out_opt IEnumIDList **ppenum)
	{
		return E_NOTIMPL;
	}

	CSimpleArray<CStringW> folders;
	int pos;

public:
	CSubf(): pos(0)
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_SUBF)


BEGIN_COM_MAP(CSubf)
	COM_INTERFACE_ENTRY(IEnumIDList)
	COM_INTERFACE_ENTRY(ISubf)
END_COM_MAP()



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

OBJECT_ENTRY_AUTO(__uuidof(Subf), CSubf)
