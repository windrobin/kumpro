
#include <windows.h>
#include <tchar.h>
#include <stdio.h>
#include <atlbase.h>
#include <atlcom.h>
#include <atlstr.h>
#include <shlobj.h>
#include <IntShCut.h>

int _tmain(int argc, TCHAR **argv) {
	if (argc < 5) {
		fprintf(stderr, "CreateInternetShortCut DD.url http://DDS/DD/ favicon.ico 0 ");
		return 1;
	}
	HRESULT hr;
	if (S_OK == (hr = CoInitialize(NULL))) {
		{
			CComPtr<IPersistFile> pPF;
			hr = pPF.CoCreateInstance(CLSID_InternetShortcut);
			if (S_OK == hr) {
				CComQIPtr<IUniformResourceLocator> pURL = pPF;
				if (pURL != NULL) {
					hr = pURL->SetURL(CT2W(argv[2]), IURL_SETURL_FL_GUESS_PROTOCOL);
				}
				CComQIPtr<IShellLink> pSL = pPF;
				if (pSL != NULL) {
					hr = pSL->SetIconLocation(CT2W(argv[3]), _ttoi(argv[4]));
				}
				hr = pPF->Save(CT2W(argv[1]), true);
			}
		}
		CoUninitialize();
	}
	return hr;
}
