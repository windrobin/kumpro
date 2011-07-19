// StopMove.h : CStopMove �̐錾

#pragma once
#include "resource.h"       // ���C�� �V���{��

#include "NoMoveFolder.h"


#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "DCOM �̊��S�T�|�[�g���܂�ł��Ȃ� Windows Mobile �v���b�g�t�H�[���̂悤�� Windows CE �v���b�g�t�H�[���ł́A�P��X���b�h COM �I�u�W�F�N�g�͐������T�|�[�g����Ă��܂���BATL ���P��X���b�h COM �I�u�W�F�N�g�̍쐬���T�|�[�g���邱�ƁA����т��̒P��X���b�h COM �I�u�W�F�N�g�̎����̎g�p�������邱�Ƃ���������ɂ́A_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA ���`���Ă��������B���g�p�� rgs �t�@�C���̃X���b�h ���f���� 'Free' �ɐݒ肳��Ă���ADCOM Windows CE �ȊO�̃v���b�g�t�H�[���ŃT�|�[�g�����B��̃X���b�h ���f���Ɛݒ肳��Ă��܂����B"
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
