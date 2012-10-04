// Sf.h : CSf の宣言

#pragma once
#include "resource.h"       // メイン シンボル

#include "MyFav.h"

#include "Subf.h"

#define LevCallee 1 // 0 for doing output, 1 for no output

#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "DCOM の完全サポートを含んでいない Windows Mobile プラットフォームのような Windows CE プラットフォームでは、単一スレッド COM オブジェクトは正しくサポートされていません。ATL が単一スレッド COM オブジェクトの作成をサポートすること、およびその単一スレッド COM オブジェクトの実装の使用を許可することを強制するには、_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA を定義してください。ご使用の rgs ファイルのスレッド モデルは 'Free' に設定されており、DCOM Windows CE 以外のプラットフォームでサポートされる唯一のスレッド モデルと設定されていました。"
#endif



// CSf

class ATL_NO_VTABLE CSf :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CSf, &CLSID_Sf>,
	public IStorage,
	public IShellFolder2,
	public IPersistFolder2,
	public IPersistIDList,
	public ISf
{
protected:
	class IDLUt {
	public:
		static bool FromStr(CString s, LPITEMIDLIST &pidltar) {
			if (s.Left(5) != _T("PIDL:"))
				return false;
			int cb = (s.GetLength() - 5) / 2;
			pidltar = reinterpret_cast<LPITEMIDLIST>(CoTaskMemAlloc(cb));
			if (pidltar == NULL)
				return false;
			for (int x = 0; x < cb; x++) {
				TCHAR tc[3] = {s[5 +2*x], s[5 +2*x +1]};
				reinterpret_cast<LPBYTE>(pidltar)[x] = (BYTE)_tcstol(tc, NULL, 16);
			}
			return true;
		}

		static CString ToStr(LPCITEMIDLIST pidl) {
			CString s = _T("PIDL:");
			while (pidl != NULL) {
				int cb = pidl->mkid.cb;
				if (cb == 0)
					cb = 2;
				for (int x = 0; x < cb; x++)
					s.AppendFormat(_T("%02x"), ((LPBYTE)pidl)[x]);

				if (pidl->mkid.cb == 0)
					break;
				pidl = ILGetNext(pidl);
			}
			return s;
		}
	};
	class IDUt {
	public:
		static CString FromIID(const IID &riid) {
			LPOLESTR psz = NULL;
			StringFromIID(riid, &psz);
			CString s = CW2T(psz);
			CoTaskMemFree(psz);
			return s;
		}

		static CString WhatIID(const IID &riid) {
			CRegKey rk;
			LONG r;
			CString sIID = FromIID(riid);
			CString sKey; sKey.Format(_T("Interface\\%s"), static_cast<LPCTSTR>(sIID));
			CString s = sIID;
			if (0 == (r = rk.Open(HKEY_CLASSES_ROOT, sKey, KEY_READ))) {
				TCHAR tcDisp[1000 +1] = {0};
				ULONG n = 1000;
				if (0 == (r = rk.QueryStringValue(NULL, tcDisp, &n))) {
					s = _T("IID_");
					s += tcDisp;
				}
			}
			return s;
		}
	};

	class MUt {
	public:
		static CString GetRealPath(LPCITEMIDLIST pidl) {
			if (pidl != NULL)
				return CString(CW2T(reinterpret_cast<LPWSTR>(const_cast<PBYTE>(pidl->mkid.abID))));
			return CString();
		}
	};

	// IStorage : public IUnknown
public:
    virtual HRESULT STDMETHODCALLTYPE CreateStream( 
        /* [string][in] */ __RPC__in_string const OLECHAR *pwcsName,
        /* [in] */ DWORD grfMode,
        /* [in] */ DWORD reserved1,
        /* [in] */ DWORD reserved2,
        /* [out] */ __RPC__deref_out_opt IStream **ppstm)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual /* [local] */ HRESULT STDMETHODCALLTYPE OpenStream( 
        /* [string][in] */ const OLECHAR *pwcsName,
        /* [unique][in] */ void *reserved1,
        /* [in] */ DWORD grfMode,
        /* [in] */ DWORD reserved2,
        /* [out] */ IStream **ppstm)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual HRESULT STDMETHODCALLTYPE CreateStorage( 
        /* [string][in] */ __RPC__in_string const OLECHAR *pwcsName,
        /* [in] */ DWORD grfMode,
        /* [in] */ DWORD reserved1,
        /* [in] */ DWORD reserved2,
        /* [out] */ __RPC__deref_out_opt IStorage **ppstg)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual HRESULT STDMETHODCALLTYPE OpenStorage( 
        /* [string][unique][in] */ __RPC__in_opt_string const OLECHAR *pwcsName,
        /* [unique][in] */ __RPC__in_opt IStorage *pstgPriority,
        /* [in] */ DWORD grfMode,
        /* [unique][in] */ __RPC__deref_opt_in_opt SNB snbExclude,
        /* [in] */ DWORD reserved,
        /* [out] */ __RPC__deref_out_opt IStorage **ppstg)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual /* [local] */ HRESULT STDMETHODCALLTYPE CopyTo( 
        /* [in] */ DWORD ciidExclude,
        /* [size_is][unique][in] */ const IID *rgiidExclude,
        /* [annotation][unique][in] */ 
        __RPC__in_opt  SNB snbExclude,
        /* [unique][in] */ IStorage *pstgDest)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual HRESULT STDMETHODCALLTYPE MoveElementTo( 
        /* [string][in] */ __RPC__in_string const OLECHAR *pwcsName,
        /* [unique][in] */ __RPC__in_opt IStorage *pstgDest,
        /* [string][in] */ __RPC__in_string const OLECHAR *pwcsNewName,
        /* [in] */ DWORD grfFlags)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual HRESULT STDMETHODCALLTYPE Commit( 
        /* [in] */ DWORD grfCommitFlags)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual HRESULT STDMETHODCALLTYPE Revert( void)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual /* [local] */ HRESULT STDMETHODCALLTYPE EnumElements( 
        /* [in] */ DWORD reserved1,
        /* [size_is][unique][in] */ void *reserved2,
        /* [in] */ DWORD reserved3,
        /* [out] */ IEnumSTATSTG **ppenum)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual HRESULT STDMETHODCALLTYPE DestroyElement( 
        /* [string][in] */ __RPC__in_string const OLECHAR *pwcsName)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual HRESULT STDMETHODCALLTYPE RenameElement( 
        /* [string][in] */ __RPC__in_string const OLECHAR *pwcsOldName,
        /* [string][in] */ __RPC__in_string const OLECHAR *pwcsNewName)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual HRESULT STDMETHODCALLTYPE SetElementTimes( 
        /* [string][unique][in] */ __RPC__in_opt_string const OLECHAR *pwcsName,
        /* [unique][in] */ __RPC__in_opt const FILETIME *pctime,
        /* [unique][in] */ __RPC__in_opt const FILETIME *patime,
        /* [unique][in] */ __RPC__in_opt const FILETIME *pmtime)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual HRESULT STDMETHODCALLTYPE SetClass( 
        /* [in] */ __RPC__in REFCLSID clsid)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual HRESULT STDMETHODCALLTYPE SetStateBits( 
        /* [in] */ DWORD grfStateBits,
        /* [in] */ DWORD grfMask)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual HRESULT STDMETHODCALLTYPE Stat( 
        /* [out] */ __RPC__out STATSTG *pstatstg,
        /* [in] */ DWORD grfStatFlag)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		MessageBox(NULL, _T("Stat"), _T("MyFav"), MB_ICONINFORMATION);
		return E_ACCESSDENIED;
	}

	class SUt {
	public:
		static HRESULT Set(STRRET &r, LPCWSTR pwcs) {
			r.uType = STRRET_WSTR;
			size_t cch = wcslen(pwcs);
			if (NULL == (r.pOleStr = reinterpret_cast<LPWSTR>(CoTaskMemAlloc(2 * (1 + cch)))))
				return E_OUTOFMEMORY;
			wcsncpy_s(r.pOleStr, cch + 1, pwcs, cch);
			return S_OK;
		}

		static void Clear(STRRET &r) {
			if (r.uType == STRRET_WSTR) {
				CoTaskMemFree(r.pOleStr);
			}
			ZeroMemory(&r, sizeof(r));
		}
	};

	// IShellFolder2 : public IShellFolder
public:
    virtual HRESULT STDMETHODCALLTYPE GetDefaultSearchGUID( 
        /* [out] */ __RPC__out GUID *pguid)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %s \n", __FUNCTION__
			, static_cast<LPCSTR>(CT2A(IDUt::FromIID(*pguid)))
			);
		return E_NOTIMPL;
	}
    
    virtual HRESULT STDMETHODCALLTYPE EnumSearches( 
        /* [out] */ __RPC__deref_out_opt IEnumExtraSearch **ppenum)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_NOTIMPL;
	}
    
    virtual HRESULT STDMETHODCALLTYPE GetDefaultColumn( 
        /* [in] */ DWORD dwRes,
        /* [out] */ __RPC__out ULONG *pSort,
        /* [out] */ __RPC__out ULONG *pDisplay)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_NOTIMPL;
	}

    
    virtual HRESULT STDMETHODCALLTYPE GetDefaultColumnState( 
        /* [in] */ UINT iColumn,
        /* [out] */ __RPC__out SHCOLSTATEF *pcsFlags)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %u \n", __FUNCTION__, iColumn);

		if (pcsFlags == NULL)
			return E_POINTER;

		switch (iColumn) {
		case 0:
			*pcsFlags = SHCOLSTATE_TYPE_STR | SHCOLSTATE_ONBYDEFAULT;
			return S_OK;
		case 1:
			*pcsFlags = SHCOLSTATE_TYPE_STR | SHCOLSTATE_ONBYDEFAULT;
			return S_OK;
		}

		return E_INVALIDARG;
	}

    
    virtual HRESULT STDMETHODCALLTYPE GetDetailsEx( 
        /* [unique][in] */ __RPC__in_opt PCUITEMID_CHILD pidl,
        /* [in] */ __RPC__in const SHCOLUMNID *p,
        /* [out] */ __RPC__out VARIANT *pv)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %s ( %s %u ) \n", __FUNCTION__
			, static_cast<LPCSTR>(CT2A(MUt::GetRealPath(pidl)))
			, static_cast<LPCSTR>(CT2A(IDUt::FromIID(p->fmtid)))
			, p->pid
			);

		if (pidl == NULL)
			return E_POINTER;
		if (p == NULL)
			return E_POINTER;
		if (pv == NULL)
			return E_POINTER;

		static const GUID FMTID_Link = PSGUID_LINK;

		HRESULT hr;

		if (false) { }
		else if (p->fmtid == FMTID_Storage && p->pid == PID_STG_NAME) {
			STRRET str;
			if (SUCCEEDED(hr = GetDisplayNameOf(pidl, SHGDN_NORMAL, &str))) {
				CComBSTR bstr;
				if (SUCCEEDED(hr = StrRetToBSTR(&str, pidl, &bstr))) {
					hr = CComVariant(bstr).Detach(pv);
					SUt::Clear(str);
					return hr;
				}
			}
		}
		else if (p->fmtid == FMTID_Storage && p->pid == PID_STG_WRITETIME) {
			return E_INVALIDARG;
		}
		else if (p->fmtid == FMTID_Link && p->pid == PID_LINK_TARGET) {
			CString s = MUt::GetRealPath(pidl);
			return CComVariant(s).Detach(pv);
		}

		return E_INVALIDARG;
	}

    
    virtual HRESULT STDMETHODCALLTYPE GetDetailsOf( 
        /* [unique][in] */ __RPC__in_opt PCUITEMID_CHILD pidl,
        /* [in] */ UINT iColumn,
        /* [out] */ __RPC__out SHELLDETAILS *psd)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %s %u \n", __FUNCTION__
			, static_cast<LPCSTR>(CT2A(MUt::GetRealPath(pidl)))
			, iColumn
			);

		if (psd == NULL)
			return E_POINTER;

		switch (iColumn) {
		case 0:
			psd->fmt = LVCFMT_LEFT;
			psd->cxChar = 15;
			return SUt::Set(psd->str, L"名称");
		case 1:
			psd->fmt = LVCFMT_LEFT;
			psd->cxChar = 30;
			return SUt::Set(psd->str, L"リンク先");
		}

		return E_INVALIDARG;
	}

    
    virtual HRESULT STDMETHODCALLTYPE MapColumnToSCID( 
        /* [in] */ UINT iColumn,
        /* [out] */ __RPC__out SHCOLUMNID *p)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %u \n", __FUNCTION__, iColumn);

		if (p == NULL)
			return E_POINTER;

		static const GUID FMTID_Link = PSGUID_LINK;

		switch (iColumn) {
		case 0:
			p->fmtid = FMTID_Storage;
			p->pid = PID_STG_NAME;
			return S_OK;
		case 1:
			p->fmtid = FMTID_Link;
			p->pid = PID_LINK_TARGET;
			return S_OK;
		}

		return E_INVALIDARG;
	}

    
	// IShellFolder : public IUnknown
public:
    virtual HRESULT STDMETHODCALLTYPE ParseDisplayName( 
        /* [unique][in] */ __RPC__in_opt HWND hwnd,
        /* [unique][in] */ __RPC__in_opt IBindCtx *pbc,
        /* [string][in] */ __RPC__in_string LPWSTR pszDisplayName,
        /* [annotation][unique][out][in] */ 
        __reserved  ULONG *pchEaten,
        /* [out] */ __RPC__deref_out_opt PIDLIST_RELATIVE *ppidl,
        /* [unique][out][in] */ __RPC__inout_opt ULONG *pdwAttributes)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual HRESULT STDMETHODCALLTYPE EnumObjects( 
        /* [unique][in] */ __RPC__in_opt HWND hwnd,
        /* [in] */ SHCONTF grfFlags,
        /* [out] */ __RPC__deref_out_opt IEnumIDList **ppenumIDList);
    
    virtual HRESULT STDMETHODCALLTYPE BindToObject( 
        /* [in] */ __RPC__in PCUIDLIST_RELATIVE pidl,
        /* [unique][in] */ __RPC__in_opt IBindCtx *pbc,
        /* [in] */ __RPC__in REFIID riid,
        /* [iid_is][out] */ __RPC__deref_out_opt void **ppv)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %s %s \n", __FUNCTION__
			, static_cast<LPCSTR>(CT2A(IDUt::WhatIID(riid)))
			, static_cast<LPCSTR>(CT2A(MUt::GetRealPath(pidl)))
			);

		ATLASSERT(ppv != NULL);
		*ppv = NULL;

		CComPtr<IShellFolder> psfDesk;
		LPITEMIDLIST pidltar = NULL;
		HRESULT hr = E_FAIL;
		if (S_OK == (hr = ResolvePIDL(pidl, psfDesk, pidltar, pbc))) {
			hr = psfDesk->BindToObject(pidltar, pbc, riid, ppv);
			ILFree(pidltar);
			//ATLASSERT(!FAILED(hr));
			if (SUCCEEDED(hr)) {
				ATLASSERT(riid != IID_IPropertyStoreCache);
				ATLASSERT(riid != IID_IPropertyStoreFactory);
				ATLASSERT(riid != IID_IPropertyStore);
			}
		}
		else ATLASSERT(false);

		return hr;
	}
    
    virtual HRESULT STDMETHODCALLTYPE BindToStorage( 
        /* [in] */ __RPC__in PCUIDLIST_RELATIVE pidl,
        /* [unique][in] */ __RPC__in_opt IBindCtx *pbc,
        /* [in] */ __RPC__in REFIID riid,
        /* [iid_is][out] */ __RPC__deref_out_opt void **ppv)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}
    
    virtual HRESULT STDMETHODCALLTYPE CompareIDs( 
        /* [in] */ LPARAM lParam,
        /* [in] */ __RPC__in PCUIDLIST_RELATIVE pidl1,
        /* [in] */ __RPC__in PCUIDLIST_RELATIVE pidl2)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %s %s \n", __FUNCTION__
			, static_cast<LPCSTR>(CT2A(MUt::GetRealPath(pidl1)))
			, static_cast<LPCSTR>(CT2A(MUt::GetRealPath(pidl2)))
			);

		short sResult = MUt::GetRealPath(pidl1).Compare(MUt::GetRealPath(pidl2));
		return MAKE_HRESULT(SEVERITY_SUCCESS, 0, (unsigned short)sResult);
	}
    
    virtual HRESULT STDMETHODCALLTYPE CreateViewObject( 
        /* [unique][in] */ __RPC__in_opt HWND hwndOwner,
        /* [in] */ __RPC__in REFIID riid,
        /* [iid_is][out] */ __RPC__deref_out_opt void **ppv)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %s \n", __FUNCTION__
			, static_cast<LPCSTR>(CT2A(IDUt::WhatIID(riid)))
			);

		HRESULT hr;

		if (ppv == NULL)
			return E_POINTER;

		*ppv = NULL;

		if (false) { }
		else if (riid == IID_IShellView) {
			SFV_CREATE fv;
			ZeroMemory(&fv, sizeof(fv));
			fv.cbSize = sizeof(fv);
			fv.pshf = this;
			AddRef();
			hr = SHCreateShellFolderView(&fv, reinterpret_cast<IShellView **>(ppv));
			Release();
		}
		else hr = E_FAIL;

		return hr;
	}
    
    virtual HRESULT STDMETHODCALLTYPE GetAttributesOf( 
        /* [in] */ UINT cidl,
        /* [unique][size_is][in] */ __RPC__in_ecount_full_opt(cidl) PCUITEMID_CHILD_ARRAY apidl,
        /* [out][in] */ __RPC__inout SFGAOF *rgfInOut)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %u %s 0x%08x \n", __FUNCTION__
			, cidl
			, static_cast<LPCSTR>(CT2A(MUt::GetRealPath(apidl[0])))
			, *rgfInOut
			);

		if (cidl != 1)
			return E_UNEXPECTED;

		ATLASSERT(rgfInOut != NULL);

		CComPtr<IShellFolder> psfDesk;
		LPITEMIDLIST pidltar = NULL;
		HRESULT hr = E_FAIL;
		if (S_OK == (hr = ResolvePIDL(apidl[0], psfDesk, pidltar))) {
			hr = psfDesk->GetAttributesOf(1, const_cast<LPCITEMIDLIST *>(&pidltar), rgfInOut);
			ILFree(pidltar);
		}
		else ATLASSERT(false);

		return S_OK;
	}
    
    virtual HRESULT STDMETHODCALLTYPE GetUIObjectOf( 
        /* [unique][in] */ __RPC__in_opt HWND hwndOwner,
        /* [in] */ UINT cidl,
        /* [unique][size_is][in] */ __RPC__in_ecount_full_opt(cidl) PCUITEMID_CHILD_ARRAY apidl,
        /* [in] */ __RPC__in REFIID riid,
        /* [annotation][unique][out][in] */ 
        __reserved  UINT *rgfReserved,
        /* [iid_is][out] */ __RPC__deref_out_opt void **ppv)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %s %s \n", __FUNCTION__
			, static_cast<LPCSTR>(CT2A(IDUt::WhatIID(riid)))
			, static_cast<LPCSTR>(CT2A(MUt::GetRealPath(apidl[0])))
			);

		ATLASSERT(ppv != NULL);
		*ppv = NULL;

		if (cidl != 1)
			return E_UNEXPECTED;

		CComPtr<IShellFolder> psfDesk;
		LPITEMIDLIST pidltar = NULL;
		HRESULT hr = E_FAIL;
		if (S_OK == (hr = ResolvePIDL(apidl[0], psfDesk, pidltar))) {
			hr = psfDesk->GetUIObjectOf(hwndOwner, 1, const_cast<LPCITEMIDLIST *>(&pidltar), riid, rgfReserved, ppv);
			ILFree(pidltar);
		}
		else ATLASSERT(false);

		return hr;
	}
    
    virtual HRESULT STDMETHODCALLTYPE GetDisplayNameOf( 
        /* [unique][in] */ __RPC__in_opt PCUITEMID_CHILD pidl,
        /* [in] */ SHGDNF uFlags,
        /* [out] */ __RPC__out STRRET *pName)
    {
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s 0x%08x \n", __FUNCTION__
			, static_cast<LPCSTR>(CT2A(MUt::GetRealPath(pidl)))
			, uFlags
			);

		CComPtr<IShellFolder> psfDesk;
		LPITEMIDLIST pidltar = NULL;
		HRESULT hr;
		if (S_OK == (hr = ResolvePIDL(pidl, psfDesk, pidltar))) {
			hr = psfDesk->GetDisplayNameOf(pidltar, uFlags, pName);
			ILFree(pidltar);
		}
		else ATLASSERT(false);

		return hr;
	}

	HRESULT ResolvePIDL(PCUITEMID_CHILD pidl, CComPtr<IShellFolder> &psfDesk, PUITEMID_CHILD &pidltar, LPBC pbc = NULL) {
		HRESULT hr = E_FAIL;
		psfDesk.Release();
		if (S_OK == (hr = SHGetDesktopFolder(&psfDesk))) {
			ULONG ch = 0;
			ULONG atts = 0;
			if (IDLUt::FromStr(MUt::GetRealPath(pidl), pidltar) || S_OK == (hr = psfDesk->ParseDisplayName(NULL, pbc, const_cast<LPWSTR>(static_cast<LPCWSTR>(CT2W(MUt::GetRealPath(pidl)))), &ch, &pidltar, &atts))) {
				LPITEMIDLIST pidlFull = ILCombine(pidltar, ILGetNext(pidl));
				ILFree(pidltar);
				pidltar = pidlFull;
				while (true) {
					if (pidltar->mkid.cb == 0 || ILGetNext(pidltar)->mkid.cb == 0)
						break;

					LPITEMIDLIST pidlChild = ILCloneFirst(pidltar);
					LPITEMIDLIST pidlSub = ILClone(ILGetNext(pidltar));

					CComPtr<IShellFolder> psfChild;
					hr = psfDesk->BindToObject(pidlChild, pbc, IID_IShellFolder, reinterpret_cast<void **>(&psfChild));

					psfDesk = psfChild;

					ILFree(pidlChild);
					ILFree(pidltar); pidltar = pidlSub;

					if (FAILED(hr))
						break;
					continue;
				}
			}
		}
	
		return hr;
	}

	HRESULT ResolvePIDL2(PCUITEMID_CHILD pidl, CComPtr<IShellFolder> &psfDesk, PUITEMID_CHILD &pidltar) {
		HRESULT hr = E_FAIL;
		psfDesk.Release();
		if (S_OK == (hr = SHGetDesktopFolder(&psfDesk))) {
			ULONG ch = 0;
			ULONG atts = 0;
			if (S_OK == (hr = psfDesk->ParseDisplayName(NULL, NULL, const_cast<LPWSTR>(static_cast<LPCWSTR>(CT2W(MUt::GetRealPath(pidl)))), &ch, &pidltar, &atts))) {
			}
		}
	
		return hr;
	}

    virtual /* [local] */ HRESULT STDMETHODCALLTYPE SetNameOf( 
        /* [annotation][unique][in] */ 
        __in_opt  HWND hwnd,
        /* [annotation][in] */ 
        __in  PCUITEMID_CHILD pidl,
        /* [annotation][string][in] */ 
        __in  LPCWSTR pszName,
        /* [annotation][in] */ 
        __in  SHGDNF uFlags,
        /* [annotation][out] */ 
        __deref_opt_out  PITEMID_CHILD *ppidlOut)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);
		return E_ACCESSDENIED;
	}

	// IPersist : public IUnknown
public:
    virtual HRESULT STDMETHODCALLTYPE GetClassID( 
        /* [out] */ __RPC__out CLSID *pClassID)
	{
		//!toomany ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);

		ATLASSERT(pClassID != NULL);
		*pClassID = CLSID_Sf;
		return S_OK;
	}

	// IPersistFolder : public IPersist
public:
    virtual HRESULT STDMETHODCALLTYPE Initialize( 
        /* [in] */ __RPC__in PCIDLIST_ABSOLUTE pidl)
	{
		//!toomany ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);

		ATLASSERT(pidl != NULL);
		ATLASSERT(pidlView == NULL);
		pidlView = ILClone(pidl);
		return S_OK;
	}

	LPITEMIDLIST pidlView;
    
	// IPersistFolder2 : public IPersistFolder
public:
    virtual HRESULT STDMETHODCALLTYPE GetCurFolder( 
        /* [out] */ __RPC__deref_out_opt PIDLIST_ABSOLUTE *ppidl)
	{
		//!toomany ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);

		ATLASSERT(ppidl != NULL);
		*ppidl = ILClone(pidlView);
		return S_OK;
	}

	// IPersistIDList : public IPersist
public:
    virtual HRESULT STDMETHODCALLTYPE SetIDList( 
        /* [in] */ __RPC__in PCIDLIST_ABSOLUTE pidl)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);

		if (pidl == NULL)
			return E_POINTER;

		pidlView = ILClone(pidl);
		return S_OK;
	}
    
    virtual HRESULT STDMETHODCALLTYPE GetIDList( 
        /* [out] */ __RPC__deref_out_opt PIDLIST_ABSOLUTE *ppidl)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);

		if (ppidl == NULL)
			return E_POINTER;
		
		*ppidl = ILClone(pidlView);
		return S_OK;
	}

public:
	CSf()
	{
		pidlView = NULL;
	}

DECLARE_REGISTRY_RESOURCEID(IDR_SF)


BEGIN_COM_MAP(CSf)
	COM_INTERFACE_ENTRY(ISf)
//	COM_INTERFACE_ENTRY(IStorage)
	COM_INTERFACE_ENTRY(IShellFolder)
	COM_INTERFACE_ENTRY(IShellFolder2)
	COM_INTERFACE_ENTRY2(IPersist, IPersistFolder)
	COM_INTERFACE_ENTRY(IPersistFolder)
	COM_INTERFACE_ENTRY(IPersistFolder2)
	COM_INTERFACE_ENTRY(IPersistIDList)

	COM_INTERFACE_ENTRY_FUNC_BLIND(0, TraceQueryInterafce)
END_COM_MAP()

	static HRESULT WINAPI TraceQueryInterafce(void* pv, REFIID riid, LPVOID* ppv, DWORD_PTR dw) {
		ATLTRACE2(atlTraceQI, 0, "# %s %s \n", __FUNCTION__
			, static_cast<LPCSTR>(CT2A(IDUt::WhatIID(riid)))
			);
		return S_FALSE;
	}


	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
		ILFree(pidlView); pidlView = NULL;
	}

public:

};

OBJECT_ENTRY_AUTO(__uuidof(Sf), CSf)
