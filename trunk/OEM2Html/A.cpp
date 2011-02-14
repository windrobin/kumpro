
#include <windows.h>
#include <atlbase.h>
#include <atlcom.h>
#include <atlstr.h>
#include <mshtml.h>
#include <exdisp.h>
#include <shlguid.h>

#pragma comment(lib, "oleacc.lib")

UINT WM_HTML_GETOBJECT = RegisterWindowMessage(_T("WM_HTML_GETOBJECT"));

CString GetTempFilePath() {
	TCHAR tcDir[MAX_PATH] = {0};
	HRESULT hr;
	if (0 != GetTempPath(MAX_PATH, tcDir)) {
		GUID id;
		if (SUCCEEDED(hr = CoCreateGuid(&id))) {
			TCHAR tcFn[32 +1] = {0};
			for (int x=0; x<16; x++)
				_stprintf(tcFn +2*x, _T("%02x"), 0U +((BYTE *)&id)[x]);
			return CString(tcDir) + _T("\\") + tcFn;
		}
	}
	return _T("");
}

bool IsUTF8(const BYTE *pb, DWORD cb) {
	for (DWORD x=0; x<cb; ) {
		BYTE b0 = pb[0];
		BYTE b1 = (x +1 < cb) ? pb[1] : 0;
		BYTE b2 = (x +2 < cb) ? pb[2] : 0;
		BYTE b3 = (x +3 < cb) ? pb[3] : 0;
		if (b0 <= 0x7f) { x++; continue; }
		if (0xc2 <= b0 && b0 <= 0xdf && 0x80 <= b1 && b1 <= 0xbf) { x+=2; continue; }
		if (0xe0 <= b0 && b0 <= 0xef && 0x80 <= b1 && b1 <= 0xbf && 0x80 <= b2 && b2 <= 0xbf) { x+=3; continue; }
		if (0xf0 <= b0 && b0 <= 0xf7 && 0x80 <= b1 && b1 <= 0xbf && 0x80 <= b2 && b2 <= 0xbf && 0x80 <= b3 && b3 <= 0xbf) { x+=4; continue; }
		return false;
	}
	return true;
}

BOOL CALLBACK ECP(
	HWND hwnd,
    LPARAM lParam
) {
	const int cchClassName = 256;
	TCHAR tcClassName[cchClassName] = {0};
	HRESULT hr;
	if (GetClassName(hwnd, tcClassName, cchClassName) != 0 && _tcscmp(tcClassName, _T("Internet Explorer_Server")) == 0) {
		HWND hWndChild = hwnd;
		LRESULT lRes;
		SendMessageTimeout(hWndChild, WM_HTML_GETOBJECT, 0L, 0L, SMTO_ABORTIFHUNG, 1000, (DWORD*)&lRes );

		CComPtr<IHTMLDocument> spDoc;
		if (SUCCEEDED(hr = ObjectFromLresult(lRes, IID_IHTMLDocument, 0, reinterpret_cast<void **>(&spDoc)))) {
			CComQIPtr<IPersistStream> spPSt = spDoc;
			HRESULT hr;
			if (spPSt != NULL) {
				CComPtr<IStream> pSt;
				if (SUCCEEDED(hr = CreateStreamOnHGlobal(NULL, true, &pSt))) {
					hr = spPSt->Save(pSt, false);
					if (SUCCEEDED(hr)) {
						HGLOBAL hMem = NULL;
						if (SUCCEEDED(hr = GetHGlobalFromStream(pSt, &hMem))) {
							SIZE_T cbMem = GlobalSize(hMem);
							LPVOID pvMem = GlobalLock(hMem);

							CString strOut = GetTempFilePath() + _T(".html");

							FILE *f = _tfopen(strOut, _T("wb"));
							if (f != NULL) {
								if (IsUTF8((BYTE *)pvMem, cbMem)) {
									fwrite("\xEF\xBB\xBF", 3, 1, f);
								}

								fwrite(pvMem, cbMem, 1, f);
								fclose(f);

								ShellExecute(NULL, NULL, strOut, NULL, NULL, SW_SHOW);
							}
							GlobalUnlock(hMem);
						}
					}
					printf("");
				}
			}
		}
	}
	return true;
}

int WINAPI WinMain(
  HINSTANCE hInstance,      // 現在のインスタンスのハンドル
  HINSTANCE hPrevInstance,  // 以前のインスタンスのハンドル
  LPSTR lpCmdLine,          // コマンドライン
  int nCmdShow              // 表示状態
) {
	HRESULT hr;
	if (FAILED(hr = CoInitialize(NULL)))
		return 1;
	{
		HWND hWndPrev = NULL;
		do {
			HWND hWnd = FindWindowEx(NULL, hWndPrev, _T("Outlook Express Browser Class"), NULL);
			if (hWnd != NULL) {
				EnumChildWindows(hWnd, ECP, NULL);
			}
			hWndPrev = hWnd;
		} while (hWndPrev != NULL);
	}
	{
		HWND hWndPrev = NULL;
		do {
			HWND hWnd = FindWindowEx(NULL, hWndPrev, _T("ATH_Note"), NULL);
			if (hWnd != NULL) {
				EnumChildWindows(hWnd, ECP, NULL);
			}
			hWndPrev = hWnd;
		} while (hWndPrev != NULL);
	}
	CoUninitialize();
	return 0;
}
