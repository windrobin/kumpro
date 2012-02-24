// Sf.cpp : CSf ‚ÌŽÀ‘•

#include "stdafx.h"
#include "Sf.h"


// CSf

HRESULT STDMETHODCALLTYPE CSf::EnumObjects( 
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

