// Subf.h : CSubf �̐錾

#pragma once
#include "resource.h"       // ���C�� �V���{��

#include "MyFav.h"


#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "DCOM �̊��S�T�|�[�g���܂�ł��Ȃ� Windows Mobile �v���b�g�t�H�[���̂悤�� Windows CE �v���b�g�t�H�[���ł́A�P��X���b�h COM �I�u�W�F�N�g�͐������T�|�[�g����Ă��܂���BATL ���P��X���b�h COM �I�u�W�F�N�g�̍쐬���T�|�[�g���邱�ƁA����т��̒P��X���b�h COM �I�u�W�F�N�g�̎����̎g�p�������邱�Ƃ���������ɂ́A_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA ���`���Ă��������B���g�p�� rgs �t�@�C���̃X���b�h ���f���� 'Free' �ɐݒ肳��Ă���ADCOM Windows CE �ȊO�̃v���b�g�t�H�[���ŃT�|�[�g�����B��̃X���b�h ���f���Ɛݒ肳��Ă��܂����B"
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
