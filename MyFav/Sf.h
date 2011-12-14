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
	public IShellFolder,
	public IPersistFolder2,
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
				reinterpret_cast<LPBYTE>(pidltar)[x] = _tcstol(tc, NULL, 16);
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
        /* [out] */ __RPC__deref_out_opt IEnumIDList **ppenumIDList)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);

		ATLASSERT(ppenumIDList != NULL);

		*ppenumIDList = NULL;

		CComObject<CSubf> *pOb = NULL;
		HRESULT hr;
		if (FAILED(hr = CComObject<CSubf>::CreateInstance(&pOb)))
			return hr;

		pOb->AddRef();

		CRegKey rkP;
		LONG res;
		for (int side = 0; side < 2; side++) {
			if (ERROR_SUCCESS == (res = rkP.Open((side == 0) ? HKEY_CURRENT_USER : HKEY_LOCAL_MACHINE, _T("Software\\HIRAOKA HYPERS TOOLS, Inc.\\MyFav"), KEY_READ))) {
				{
					CRegKey rk;
					if (ERROR_SUCCESS == (res = rk.Open(rkP, _T("Favorites"), KEY_READ))) {
						DWORD y = 0;
						while (true) {
							TCHAR tcName[MAX_PATH +1] = {0}; // need null char
							DWORD cch = 1000;
							DWORD valueType = 0;
							BYTE abData[MAX_PATH +2] = {0}; // need null char
							DWORD cb = 1000;
							if (ERROR_SUCCESS != (res = RegEnumValue(rk.m_hKey, y, tcName, &cch, NULL, &valueType, abData, &cb)))
								break;
							if (valueType == REG_SZ) {
								pOb->folders.Add(CString(CT2W(reinterpret_cast<LPTSTR>(abData))));
							}
							else if (valueType == REG_EXPAND_SZ) {
								TCHAR tcDst[MAX_PATH];
								if (0 != ExpandEnvironmentStrings(reinterpret_cast<LPTSTR>(abData), tcDst, MAX_PATH)) {
									pOb->folders.Add(tcDst);
								}
							}
							++y;
						}
					}
				}
				{
					TCHAR tcRawDir[MAX_PATH + 1] = {0};
					ULONG n = MAX_PATH;
					if (ERROR_SUCCESS == (res = rkP.QueryStringValue(_T("Fav_Dir"), tcRawDir, &n))) {
						TCHAR tcDir[MAX_PATH +1] = {0};
						ExpandEnvironmentStrings(tcRawDir, tcDir, MAX_PATH);
						TCHAR tcFind[MAX_PATH +1] = {0};
						PathCombine(tcFind, tcDir, _T("*.lnk"));
						WIN32_FIND_DATA fd;
						HANDLE hhf = FindFirstFile(tcFind, &fd);
						if (hhf != NULL && hhf != INVALID_HANDLE_VALUE) {
							while (true) {
								CComPtr<IShellLink> psl;
								if (false
									//|| S_OK == (hr = psl.CoCreateInstance(CLSID_FolderShortcut))
									|| S_OK == (hr = psl.CoCreateInstance(CLSID_ShellLink))
								) {
									CComQIPtr<IPersistFile> ppf = psl;
									if (ppf != NULL) {
										TCHAR tcfplnk[MAX_PATH +1] = {0};
										PathCombine(tcfplnk, tcDir, fd.cFileName);
										if (S_OK == (hr = ppf->Load(tcfplnk, STGM_READ))) {
											TCHAR tctar[MAX_PATH +1] = {0};
											LPITEMIDLIST pidlLink = NULL;
											if (S_OK == (hr = psl->GetPath(tctar, MAX_PATH, NULL, 0))) {
												pOb->folders.Add(tctar);
											}
											else if (S_OK == (hr = psl->GetIDList(&pidlLink))) {
												pOb->folders.Add(IDLUt::ToStr(pidlLink));
												ILFree(pidlLink);
											}
										}
										printf("");
									}
								}

								if (!FindNextFile(hhf, &fd))
									break;
								continue;
							};
							FindClose(hhf);
						}
					}
				}
			}
		}

		*ppenumIDList = pOb;

		return S_OK;
	}
    
    virtual HRESULT STDMETHODCALLTYPE BindToObject( 
        /* [in] */ __RPC__in PCUIDLIST_RELATIVE pidl,
        /* [unique][in] */ __RPC__in_opt IBindCtx *pbc,
        /* [in] */ __RPC__in REFIID riid,
        /* [iid_is][out] */ __RPC__deref_out_opt void **ppv)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %s %s \n", __FUNCTION__, static_cast<LPCSTR>(CT2A(IDUt::WhatIID(riid))), static_cast<LPCSTR>(CT2A(MUt::GetRealPath(pidl))));

		ATLASSERT(ppv != NULL);
		*ppv = NULL;

		CComPtr<IShellFolder> psfDesk;
		LPITEMIDLIST pidltar = NULL;
		HRESULT hr = E_FAIL;
		if (S_OK == (hr = ResolvePIDL(pidl, psfDesk, pidltar, pbc))) {
			hr = psfDesk->BindToObject(pidltar, pbc, riid, ppv);
			ILFree(pidltar);
			ATLASSERT(!FAILED(hr));
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
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s \n", __FUNCTION__);

		short sResult = MUt::GetRealPath(pidl1).Compare(MUt::GetRealPath(pidl2));
		return MAKE_HRESULT(SEVERITY_SUCCESS, 0, (unsigned short)sResult);
	}
    
    virtual HRESULT STDMETHODCALLTYPE CreateViewObject( 
        /* [unique][in] */ __RPC__in_opt HWND hwndOwner,
        /* [in] */ __RPC__in REFIID riid,
        /* [iid_is][out] */ __RPC__deref_out_opt void **ppv)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %s \n", __FUNCTION__, static_cast<LPCSTR>(CT2A(IDUt::WhatIID(riid))));

		ATLASSERT(ppv != NULL);
		*ppv = NULL;

		return E_NOINTERFACE;
	}
    
    virtual HRESULT STDMETHODCALLTYPE GetAttributesOf( 
        /* [in] */ UINT cidl,
        /* [unique][size_is][in] */ __RPC__in_ecount_full_opt(cidl) PCUITEMID_CHILD_ARRAY apidl,
        /* [out][in] */ __RPC__inout SFGAOF *rgfInOut)
	{
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %u %s 0x%08x \n", __FUNCTION__, cidl, static_cast<LPCSTR>(CT2A(MUt::GetRealPath(apidl[0]))), *rgfInOut);

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
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %s %s \n", __FUNCTION__, static_cast<LPCSTR>(CT2A(IDUt::WhatIID(riid))), static_cast<LPCSTR>(CT2A(MUt::GetRealPath(apidl[0]))));

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
		ATLTRACE2(atlTraceCOM, LevCallee, "# %s %s \n", __FUNCTION__, static_cast<LPCSTR>(CT2A(MUt::GetRealPath(pidl))));

		CComPtr<IShellFolder> psfDesk;
		LPITEMIDLIST pidltar = NULL;
		HRESULT hr;
		if (S_OK == (hr = ResolvePIDL(pidl, psfDesk, pidltar))) {
			hr = psfDesk->GetDisplayNameOf(pidltar, uFlags, pName);
			ILFree(pidltar);
		}

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
	COM_INTERFACE_ENTRY(IPersist)
	COM_INTERFACE_ENTRY(IPersistFolder)
	COM_INTERFACE_ENTRY(IPersistFolder2)

	COM_INTERFACE_ENTRY_FUNC_BLIND(0, TraceQueryInterafce)
END_COM_MAP()

	static HRESULT WINAPI TraceQueryInterafce(void* pv, REFIID riid, LPVOID* ppv, DWORD_PTR dw) {
		ATLTRACE2(atlTraceQI, 1, "# %s %s \n", __FUNCTION__, static_cast<LPCSTR>(CT2A(IDUt::WhatIID(riid))));
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
