
#include <windows.h>
#include <urlmon.h>

#include <atlbase.h>
#include <atlcom.h>

#include <tchar.h>

#include <stdio.h>

#pragma comment(lib, "urlmon.lib")

HANDLE s_hEvDone = CreateEvent(NULL, false, false, NULL);

class CBCB : public IBindStatusCallback {
	ULONG locks;
public:
	CBCB(): locks(0) {
	}

    virtual HRESULT STDMETHODCALLTYPE QueryInterface( 
        /* [in] */ REFIID riid,
		/* [iid_is][out] */ __RPC__deref_out void __RPC_FAR *__RPC_FAR *ppvObject) {
			if (ppvObject == NULL)
				return E_POINTER;
			if (riid == IID_IUnknown || riid == IID_IBindStatusCallback) {
				*ppvObject = static_cast<IBindStatusCallback *>(this);
			}
			else {
				*ppvObject = NULL;
				return E_NOINTERFACE;
			}
			AddRef();
			return S_OK;
	}

	virtual ULONG STDMETHODCALLTYPE AddRef( void) {
		return ++locks;
	}

	virtual ULONG STDMETHODCALLTYPE Release( void) {
		return --locks;
	}
public:
    virtual HRESULT STDMETHODCALLTYPE OnStartBinding( 
        /* [in] */ DWORD dwReserved,
		/* [in] */ __RPC__in_opt IBinding *pib) {
			fprintf(stderr, "# OnStartBinding %u %p \n", dwReserved, pib);
			return S_OK;
	}
    
    virtual HRESULT STDMETHODCALLTYPE GetPriority( 
		/* [out] */ __RPC__out LONG *pnPriority) {
			fprintf(stderr, "# GetPriority %p ( %u ) \n", pnPriority, *pnPriority);
			return S_OK;
	}
    
    virtual HRESULT STDMETHODCALLTYPE OnLowResource( 
		/* [in] */ DWORD reserved) {
			fprintf(stderr, "# GetPriority %u \n", reserved);
			return S_OK;
	}
    
    virtual HRESULT STDMETHODCALLTYPE OnProgress( 
        /* [in] */ ULONG ulProgress,
        /* [in] */ ULONG ulProgressMax,
        /* [in] */ ULONG ulStatusCode,
		/* [unique][in] */ __RPC__in_opt LPCWSTR szStatusText) {
			fprintf(stderr, "# OnProgress %lu %lu %lu %s \n"
				, ulProgress
				, ulProgressMax
				, ulStatusCode
				, CW2A(szStatusText)
				);
			if (ulStatusCode == BINDSTATUS_ENDDOWNLOADDATA)
				SetEvent(s_hEvDone);
			return S_OK;
	}
    
    virtual HRESULT STDMETHODCALLTYPE OnStopBinding( 
        /* [in] */ HRESULT hresult,
		/* [unique][in] */ __RPC__in_opt LPCWSTR szError) {
			fprintf(stderr, "# OnStopBinding 0x%u %s \n"
				, hresult
				, CW2A(szError)
				);
			return S_OK;
	}
    
    virtual /* [local] */ HRESULT STDMETHODCALLTYPE GetBindInfo( 
        /* [out] */ DWORD *grfBINDF,
		/* [unique][out][in] */ BINDINFO *pbindinfo) {
			fprintf(stderr, "# GetBindInfo %p ( %u ) %p \n"
				, grfBINDF, *grfBINDF
				, pbindinfo
				);
			return S_OK;
	}
    
    virtual /* [local] */ HRESULT STDMETHODCALLTYPE OnDataAvailable( 
        /* [in] */ DWORD grfBSCF,
        /* [in] */ DWORD dwSize,
        /* [in] */ FORMATETC *pformatetc,
		/* [in] */ STGMEDIUM *pstgmed) {
			fprintf(stderr, "# OnDataAvailable %u %u %p %p \n"
				, grfBSCF
				, dwSize
				, pformatetc
				, pstgmed
				);
			ReleaseStgMedium(pstgmed);
			if (grfBSCF == BSCF_DATAFULLYAVAILABLE)
				SetEvent(s_hEvDone);
			return S_OK;
	}
    
    virtual HRESULT STDMETHODCALLTYPE OnObjectAvailable( 
        /* [in] */ __RPC__in REFIID riid,
		/* [iid_is][in] */ __RPC__in_opt IUnknown *punk) {
			LPOLESTR psw = NULL;
			StringFromIID(riid, &psw);
			fprintf(stderr, "# OnObjectAvailable %p %p \n"
				, CW2A(psw)
				, punk
				);
			CoTaskMemFree(psw);
			SetEvent(s_hEvDone);
			return S_OK;
	}
};

int testIt(LPCTSTR pszUrl) {
	HRESULT hr;
	CComPtr<IBindStatusCallback> pCb = new CBCB();
	fputs("Call -> \n", stderr);
	hr = URLOpenPullStream(NULL, CT2W(pszUrl), 0, pCb);
	fputs("<- Call \n", stderr);
	if (FAILED(hr)) {
		fprintf(stderr, "# URLOpenStream failed 0x%08x \n", hr);
		return 1;
	}
	WaitForSingleObject(s_hEvDone, INFINITE);
	return 0;
}

int _tmain(int argc, TCHAR **argv) {
	if (argc < 2) {
		fputs("TestURLOpenStream http://research.microsoft.com/en-us/um/cambridge/projects/fsharp/manual/spec.pdf \n", stderr);
		return 1;
	}
	HRESULT hr;
	if (FAILED(hr = CoInitialize(NULL)))
		return 1;
	int r = testIt(argv[1]);
	CoUninitialize();
	return r;
}
