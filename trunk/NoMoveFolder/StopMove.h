// StopMove.h : CStopMove の宣言

#pragma once
#include "resource.h"       // メイン シンボル

#include "NoMoveFolder.h"


#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "DCOM の完全サポートを含んでいない Windows Mobile プラットフォームのような Windows CE プラットフォームでは、単一スレッド COM オブジェクトは正しくサポートされていません。ATL が単一スレッド COM オブジェクトの作成をサポートすること、およびその単一スレッド COM オブジェクトの実装の使用を許可することを強制するには、_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA を定義してください。ご使用の rgs ファイルのスレッド モデルは 'Free' に設定されており、DCOM Windows CE 以外のプラットフォームでサポートされる唯一のスレッド モデルと設定されていました。"
#endif



// CStopMove

class ATL_NO_VTABLE CStopMove :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CStopMove, &CLSID_StopMove>,
	public ICopyHookW
{
public:
	CStopMove()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_STOPMOVE)


BEGIN_COM_MAP(CStopMove)
	COM_INTERFACE_ENTRY(ICopyHook)
END_COM_MAP()


    STDMETHOD_(UINT,CopyCallback) (THIS_ HWND hwnd, UINT wFunc, UINT wFlags, LPCWSTR pszSrcFile, DWORD dwSrcAttribs,
                                   LPCWSTR pszDestFile, DWORD dwDestAttribs) 
	{
		switch (wFunc) {
			case FO_MOVE:
				{
					WCHAR wcdir[MAX_PATH] = {0};
					wcscpy_s(wcdir, pszSrcFile);
					TCHAR wcfp[MAX_PATH] = {0};
					PathCombineW(wcfp, wcdir, L".nomove");
					if (0 == (GetFileAttributesW(wcfp) & FILE_ATTRIBUTE_DIRECTORY)) {
						return IDNO;
					}
					break;
				}
			case FO_DELETE:
				{
					WCHAR wcdir[MAX_PATH] = {0};
					wcscpy_s(wcdir, pszSrcFile);
					TCHAR wcfp[MAX_PATH] = {0};
					PathCombineW(wcfp, wcdir, L".nodelete");
					if (0 == (GetFileAttributesW(wcfp) & FILE_ATTRIBUTE_DIRECTORY)) {
						return IDNO;
					}
					break;
				}
			case FO_RENAME:
				{
					WCHAR wcdir[MAX_PATH] = {0};
					wcscpy_s(wcdir, pszSrcFile);
					TCHAR wcfp[MAX_PATH] = {0};
					PathCombineW(wcfp, wcdir, L".norename");
					if (0 == (GetFileAttributesW(wcfp) & FILE_ATTRIBUTE_DIRECTORY)) {
						return IDNO;
					}
					break;
				}
		}
		return IDYES;
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

OBJECT_ENTRY_AUTO(__uuidof(StopMove), CStopMove)
