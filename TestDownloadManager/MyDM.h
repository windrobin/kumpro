// MyDM.h : CMyDM �̐錾

#pragma once
#include "resource.h"       // ���C�� �V���{��

#include "TestDownloadManager.h"


#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "DCOM �̊��S�T�|�[�g���܂�ł��Ȃ� Windows Mobile �v���b�g�t�H�[���̂悤�� Windows CE �v���b�g�t�H�[���ł́A�P��X���b�h COM �I�u�W�F�N�g�͐������T�|�[�g����Ă��܂���BATL ���P��X���b�h COM �I�u�W�F�N�g�̍쐬���T�|�[�g���邱�ƁA����т��̒P��X���b�h COM �I�u�W�F�N�g�̎����̎g�p�������邱�Ƃ���������ɂ́A_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA ���`���Ă��������B���g�p�� rgs �t�@�C���̃X���b�h ���f���� 'Free' �ɐݒ肳��Ă���ADCOM Windows CE �ȊO�̃v���b�g�t�H�[���ŃT�|�[�g�����B��̃X���b�h ���f���Ɛݒ肳��Ă��܂����B"
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
