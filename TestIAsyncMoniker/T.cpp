
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
	CComPtr<IBinding> pBind;

	static LPCSTR GetBSCF(DWORD grfBSCF) {
		switch (grfBSCF) {
			case BSCF_FIRSTDATANOTIFICATION: return "BSCF_FIRSTDATANOTIFICATION";
			case BSCF_INTERMEDIATEDATANOTIFICATION: return "BSCF_INTERMEDIATEDATANOTIFICATION";
			case BSCF_LASTDATANOTIFICATION: return "BSCF_LASTDATANOTIFICATION";
			case BSCF_DATAFULLYAVAILABLE: return "BSCF_DATAFULLYAVAILABLE";
			case BSCF_AVAILABLEDATASIZEUNKNOWN: return "BSCF_AVAILABLEDATASIZEUNKNOWN";
			case BSCF_SKIPDRAINDATAFORFILEURLS: return "BSCF_SKIPDRAINDATAFORFILEURLS";
			case BSCF_64BITLENGTHDOWNLOAD: return "BSCF_64BITLENGTHDOWNLOAD";
		}
		return "?";
	}

	static LPCSTR GetBINDSTATUS(ULONG ulStatusCode) {
		switch (ulStatusCode) {
			case BINDSTATUS_FINDINGRESOURCE: return "BINDSTATUS_FINDINGRESOURCE";
			case BINDSTATUS_CONNECTING: return "BINDSTATUS_CONNECTING";
			case BINDSTATUS_REDIRECTING: return "BINDSTATUS_REDIRECTING";
			case BINDSTATUS_BEGINDOWNLOADDATA: return "BINDSTATUS_BEGINDOWNLOADDATA";
			case BINDSTATUS_DOWNLOADINGDATA: return "BINDSTATUS_DOWNLOADINGDATA";
			case BINDSTATUS_ENDDOWNLOADDATA: return "BINDSTATUS_ENDDOWNLOADDATA";
			case BINDSTATUS_BEGINDOWNLOADCOMPONENTS: return "BINDSTATUS_BEGINDOWNLOADCOMPONENTS";
			case BINDSTATUS_INSTALLINGCOMPONENTS: return "BINDSTATUS_INSTALLINGCOMPONENTS";
			case BINDSTATUS_ENDDOWNLOADCOMPONENTS: return "BINDSTATUS_ENDDOWNLOADCOMPONENTS";
			case BINDSTATUS_USINGCACHEDCOPY: return "BINDSTATUS_USINGCACHEDCOPY";
			case BINDSTATUS_SENDINGREQUEST: return "BINDSTATUS_SENDINGREQUEST";
			case BINDSTATUS_CLASSIDAVAILABLE: return "BINDSTATUS_CLASSIDAVAILABLE";
			case BINDSTATUS_MIMETYPEAVAILABLE: return "BINDSTATUS_MIMETYPEAVAILABLE";
			case BINDSTATUS_CACHEFILENAMEAVAILABLE: return "BINDSTATUS_CACHEFILENAMEAVAILABLE";
			case BINDSTATUS_BEGINSYNCOPERATION: return "BINDSTATUS_BEGINSYNCOPERATION";
			case BINDSTATUS_ENDSYNCOPERATION: return "BINDSTATUS_ENDSYNCOPERATION";
			case BINDSTATUS_BEGINUPLOADDATA: return "BINDSTATUS_BEGINUPLOADDATA";
			case BINDSTATUS_UPLOADINGDATA: return "BINDSTATUS_UPLOADINGDATA";
			case BINDSTATUS_ENDUPLOADDATA: return "BINDSTATUS_ENDUPLOADDATA";
			case BINDSTATUS_PROTOCOLCLASSID: return "BINDSTATUS_PROTOCOLCLASSID";
			case BINDSTATUS_ENCODING: return "BINDSTATUS_ENCODING";
			case BINDSTATUS_VERIFIEDMIMETYPEAVAILABLE: return "BINDSTATUS_VERIFIEDMIMETYPEAVAILABLE";
			case BINDSTATUS_CLASSINSTALLLOCATION: return "BINDSTATUS_CLASSINSTALLLOCATION";
			case BINDSTATUS_DECODING: return "BINDSTATUS_DECODING";
			case BINDSTATUS_LOADINGMIMEHANDLER: return "BINDSTATUS_LOADINGMIMEHANDLER";
			case BINDSTATUS_CONTENTDISPOSITIONATTACH: return "BINDSTATUS_CONTENTDISPOSITIONATTACH";
			case BINDSTATUS_FILTERREPORTMIMETYPE: return "BINDSTATUS_FILTERREPORTMIMETYPE";
			case BINDSTATUS_CLSIDCANINSTANTIATE: return "BINDSTATUS_CLSIDCANINSTANTIATE";
			case BINDSTATUS_IUNKNOWNAVAILABLE: return "BINDSTATUS_IUNKNOWNAVAILABLE";
			case BINDSTATUS_DIRECTBIND: return "BINDSTATUS_DIRECTBIND";
			case BINDSTATUS_RAWMIMETYPE: return "BINDSTATUS_RAWMIMETYPE";
			case BINDSTATUS_PROXYDETECTING: return "BINDSTATUS_PROXYDETECTING";
			case BINDSTATUS_ACCEPTRANGES: return "BINDSTATUS_ACCEPTRANGES";
			case BINDSTATUS_COOKIE_SENT: return "BINDSTATUS_COOKIE_SENT";
			case BINDSTATUS_COMPACT_POLICY_RECEIVED: return "BINDSTATUS_COMPACT_POLICY_RECEIVED";
			case BINDSTATUS_COOKIE_SUPPRESSED: return "BINDSTATUS_COOKIE_SUPPRESSED";
			case BINDSTATUS_COOKIE_STATE_UNKNOWN: return "BINDSTATUS_COOKIE_STATE_UNKNOWN";
			case BINDSTATUS_COOKIE_STATE_ACCEPT: return "BINDSTATUS_COOKIE_STATE_ACCEPT";
			case BINDSTATUS_COOKIE_STATE_REJECT: return "BINDSTATUS_COOKIE_STATE_REJECT";
			case BINDSTATUS_COOKIE_STATE_PROMPT: return "BINDSTATUS_COOKIE_STATE_PROMPT";
			case BINDSTATUS_COOKIE_STATE_LEASH: return "BINDSTATUS_COOKIE_STATE_LEASH";
			case BINDSTATUS_COOKIE_STATE_DOWNGRADE: return "BINDSTATUS_COOKIE_STATE_DOWNGRADE";
			case BINDSTATUS_POLICY_HREF: return "BINDSTATUS_POLICY_HREF";
			case BINDSTATUS_P3P_HEADER: return "BINDSTATUS_P3P_HEADER";
			case BINDSTATUS_SESSION_COOKIE_RECEIVED: return "BINDSTATUS_SESSION_COOKIE_RECEIVED";
			case BINDSTATUS_PERSISTENT_COOKIE_RECEIVED: return "BINDSTATUS_PERSISTENT_COOKIE_RECEIVED";
			case BINDSTATUS_SESSION_COOKIES_ALLOWED: return "BINDSTATUS_SESSION_COOKIES_ALLOWED";
			case BINDSTATUS_CACHECONTROL: return "BINDSTATUS_CACHECONTROL";
			case BINDSTATUS_CONTENTDISPOSITIONFILENAME: return "BINDSTATUS_CONTENTDISPOSITIONFILENAME";
			case BINDSTATUS_MIMETEXTPLAINMISMATCH: return "BINDSTATUS_MIMETEXTPLAINMISMATCH";
			case BINDSTATUS_PUBLISHERAVAILABLE: return "BINDSTATUS_PUBLISHERAVAILABLE";
			case BINDSTATUS_DISPLAYNAMEAVAILABLE: return "BINDSTATUS_DISPLAYNAMEAVAILABLE";
			case BINDSTATUS_SSLUX_NAVBLOCKED: return "BINDSTATUS_SSLUX_NAVBLOCKED";
			case BINDSTATUS_SERVER_MIMETYPEAVAILABLE: return "BINDSTATUS_SERVER_MIMETYPEAVAILABLE";
			case BINDSTATUS_SNIFFED_CLASSIDAVAILABLE: return "BINDSTATUS_SNIFFED_CLASSIDAVAILABLE";
			case BINDSTATUS_64BIT_PROGRESS: return "BINDSTATUS_64BIT_PROGRESS";
		}
		return "?";
	}

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
		ULONG cx = --locks;
		if (cx == 0) {
			delete this;
		}
		return cx;
	}
public:
    virtual HRESULT STDMETHODCALLTYPE OnStartBinding( 
        /* [in] */ DWORD dwReserved,
		/* [in] */ __RPC__in_opt IBinding *pib) {
			pBind = pib;
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
			fprintf(stderr, "# OnProgress %lu %lu %s %s \n"
				, ulProgress
				, ulProgressMax
				, GetBINDSTATUS(ulStatusCode)
				, CW2A(szStatusText)
				);
			//if (ulStatusCode == BINDSTATUS_ENDDOWNLOADDATA)
			//	SetEvent(s_hEvDone);
			return S_OK;
	}
    
    virtual HRESULT STDMETHODCALLTYPE OnStopBinding( 
        /* [in] */ HRESULT hresult,
		/* [unique][in] */ __RPC__in_opt LPCWSTR szError) {
			pBind = NULL;
			fprintf(stderr, "# OnStopBinding 0x%u %s \n"
				, hresult
				, CW2A(szError)
				);
			SetEvent(s_hEvDone);
			return S_OK;
	}
    
    virtual /* [local] */ HRESULT STDMETHODCALLTYPE GetBindInfo( 
        /* [out] */ DWORD *grfBINDF,
		/* [unique][out][in] */ BINDINFO *pbindinfo) {
			fprintf(stderr, "# GetBindInfo %p ( %u ) %p \n"
				, grfBINDF, *grfBINDF
				, pbindinfo
				);
			*grfBINDF |= BINDF_ASYNCHRONOUS | BINDF_NEEDFILE;
			return S_OK;
	}
    
    virtual /* [local] */ HRESULT STDMETHODCALLTYPE OnDataAvailable( 
        /* [in] */ DWORD grfBSCF,
        /* [in] */ DWORD dwSize,
        /* [in] */ FORMATETC *pformatetc,
		/* [in] */ STGMEDIUM *pstgmed) {
			fprintf(stderr, "# OnDataAvailable %s %u %p %p \n"
				, GetBSCF(grfBSCF)
				, dwSize
				, pformatetc
				, pstgmed
				);
			ReleaseStgMedium(pstgmed);
			//if (grfBSCF == BSCF_DATAFULLYAVAILABLE)
			//	SetEvent(s_hEvDone);
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
	CComPtr<IBindCtx> pbc;

	if (FAILED(hr = CreateAsyncBindCtx(0, pCb, NULL, &pbc))) {
		return 1;
	}

#if 0
	BIND_OPTS bo;
	ZeroMemory(&bo, sizeof(bo));
	bo.cbStruct = sizeof(bo);
	bo.grfFlags = BIND_MAYBOTHERUSER;
	bo.dwTickCountDeadline = 1;
	if (FAILED(hr = pbc->SetBindOptions(&bo)))
		return 1;
#endif

	CComPtr<IMoniker> pmk;
	if (FAILED(hr = CreateURLMoniker(NULL, CT2W(pszUrl), &pmk))) {
		return 1;
	}
	hr = IsAsyncMoniker(pmk);
	if (hr == S_FALSE) {
		fputs("not asynchronous moniker \n", stderr);
	}
	else if (hr == S_OK) {
		fputs("asynchronous moniker \n", stderr);
	}
	CComPtr<IStream> pSt;
	fputs("BindToStorage  -> \n", stderr);
	hr = pmk->BindToStorage(pbc, pmk, IID_IStream, reinterpret_cast<void **>(&pSt));
	fputs("<- BindToStorage  \n", stderr);

	fprintf(stderr, "BindToStorage 0x%08x \n", hr);
	if (FAILED(hr)) {
		return 1;
	}

	while (true) {
		DWORD r = MsgWaitForMultipleObjects(1, &s_hEvDone, false, INFINITE, QS_ALLINPUT);
		if (r == WAIT_OBJECT_0)
			break;
		if (r == WAIT_OBJECT_0 +1) {
			MSG wm;
			while (PeekMessage(&wm, NULL, 0, 0, PM_REMOVE)) {
				DispatchMessage(&wm);
			}
		}
		else break;
	}
	return 0;
}

int _tmain(int argc, TCHAR **argv) {
	if (argc < 2) {
		fputs("TestIAsyncMoniker http://research.microsoft.com/en-us/um/cambridge/projects/fsharp/manual/spec.pdf \n", stderr);
		return 1;
	}
	HRESULT hr = CoInitialize(NULL);
	if (FAILED(hr))
		return 1;
	int r = testIt(argv[1]);
	CoUninitialize();
	return r;
}
