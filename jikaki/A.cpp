
#include <windows.h>
#include <commctrl.h>
#include <richedit.h>
#include <shlobj.h>
#include <strsafe.h>

#include "resource.h"

#pragma comment(lib, "comctl32.lib")

#pragma comment(lib, "strsafe.lib")

HWND s_hWndTop = NULL;
HWND s_hWndRich = NULL;

HINSTANCE s_hInstApp = NULL;

class Stream {
public:
	PBYTE pb;
	int x, cx;

	int Read(PBYTE pbRead, int cb) {
		cb = min(cb, cx -x);

		for (int t=0; t<cb; t++, x++) {
			pbRead[t] = pb[x];
		}

		return cb;
	}
};

Stream s_st;

char s_fpRtf[256] = "";

DWORD CALLBACK FStreamIn(DWORD_PTR dwCookie, LPBYTE pbBuff, LONG cb, LONG *pcb) {
	if (ReadFile(reinterpret_cast<HANDLE>(dwCookie), pbBuff, cb, reinterpret_cast<DWORD *>(pcb), NULL))
		return 0;
	return GetLastError();
}

bool DoLoad() {
	HANDLE hf = CreateFile(s_fpRtf, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, 0, NULL);
	if (hf != INVALID_HANDLE_VALUE) {
		static EDITSTREAM s_esIn = {
			NULL, //DWORD_PTR dwCookie;		// User value passed to callback as first parameter 
			0, //DWORD	  dwError;		// Last error 
			FStreamIn, //EDITSTREAMCALLBACK pfnCallback;
		};
		s_esIn.dwCookie = reinterpret_cast<DWORD_PTR>(hf);
		s_esIn.dwError = 0;

		SendMessage(s_hWndRich, EM_STREAMIN, SF_RTF, reinterpret_cast<LPARAM>(&s_esIn));

		if (s_esIn.dwError == 0) {
			CloseHandle(hf);
			return true;
		}

		CloseHandle(hf);
	}
	return false;
}

DWORD CALLBACK FStreamOut(DWORD_PTR dwCookie, LPBYTE pbBuff, LONG cb, LONG *pcb) {
	if (WriteFile(reinterpret_cast<HANDLE>(dwCookie), pbBuff, cb, reinterpret_cast<DWORD *>(pcb), NULL))
		return 0;
	return GetLastError();
}

bool DoSave() {
	HANDLE hf = CreateFile(s_fpRtf, GENERIC_WRITE, FILE_SHARE_READ, NULL, CREATE_ALWAYS, 0, NULL);
	if (hf != INVALID_HANDLE_VALUE) {
		static EDITSTREAM s_esOut = {
			NULL, //DWORD_PTR dwCookie;		// User value passed to callback as first parameter 
			0, //DWORD	  dwError;		// Last error 
			FStreamOut, //EDITSTREAMCALLBACK pfnCallback;
		};
		s_esOut.dwCookie = reinterpret_cast<DWORD_PTR>(hf);
		s_esOut.dwError = 0;

		SendMessage(s_hWndRich, EM_STREAMOUT, SF_RTF, reinterpret_cast<LPARAM>(&s_esOut));

		if (s_esOut.dwError == 0) {
			CloseHandle(hf);
			return true;
		}

		CloseHandle(hf);
	}
	return false;
}

DWORD CALLBACK StreamIn(DWORD_PTR dwCookie, LPBYTE pbBuff, LONG cb, LONG *pcb) {
	if (pcb == NULL)
		return 1;
	if (pbBuff != NULL)
		*pcb = s_st.Read(pbBuff, cb);
	return 0;
}

bool DoInit() {
	HRSRC hrcRtf;
	HGLOBAL hRtf = LoadResource(s_hInstApp, hrcRtf = FindResource(s_hInstApp, MAKEINTRESOURCE(IDR_TEMPLATE_RTF), "RTF"));
	if (hRtf != NULL) {
		int cbRtf = (int)SizeofResource(s_hInstApp, hrcRtf);

		static EDITSTREAM s_esIn = {
			0, //DWORD_PTR dwCookie;		// User value passed to callback as first parameter 
			0, //DWORD	  dwError;		// Last error 
			StreamIn, //EDITSTREAMCALLBACK pfnCallback;
		};
		s_esIn.dwError = 0;

		PBYTE pbRtf = reinterpret_cast<PBYTE>(hRtf);
		s_st.pb = pbRtf;
		s_st.x = 0;
		s_st.cx = cbRtf;

		SendMessage(s_hWndRich, EM_STREAMIN, SF_RTF, reinterpret_cast<LPARAM>(&s_esIn));
		if (s_esIn.dwError == 0) {
			return true;
		}
	}
	return false;
}

LRESULT CALLBACK WindowProc(
	HWND hwnd,
    UINT uMsg,
    WPARAM wParam,
    LPARAM lParam
)
{
	switch (uMsg) {
		case WM_CREATE:
			{
				s_hWndRich = CreateWindowEx(
					0,
					RICHEDIT_CLASS,
					NULL,
					WS_CHILD|WS_VISIBLE|WS_TABSTOP|WS_GROUP |WS_VSCROLL|WS_HSCROLL |ES_MULTILINE|ES_AUTOHSCROLL|ES_AUTOVSCROLL|ES_WANTRETURN,
					0, 0, 0, 0, hwnd, NULL, s_hInstApp, NULL);
				if (s_hWndRich == NULL)
					return (LRESULT)-1;
				if (!DoLoad()) {
					DoInit();
				}
				goto _resize;
			}
		case WM_SIZE:
_resize:
			{
				RECT rc;
				if (GetClientRect(hwnd, &rc)) {
					MoveWindow(s_hWndRich, 0, 0, rc.right -rc.left, rc.bottom -rc.top, true);
				}
				break;
			}
		case WM_CLOSE:
			{
				DoSave();
				break;
			}
		case WM_DESTROY:
			{
				PostQuitMessage(0);
				break;
			}
		case WM_COMMAND:
			{
				switch (wParam) {
				case ID_NEW:
					DoInit();
					break;
				case ID_SAVE:
					DoSave();
					break;
				case ID_LOAD:
					DoLoad();
					break;
				}
				break;
			}
		case WM_ACTIVATE:
			{
				switch (wParam) {
				case WA_ACTIVE:
				case WA_CLICKACTIVE:
					SetFocus(s_hWndRich);
					return 0;
				}
				break;
			}
	}
	return DefWindowProc(hwnd, uMsg, wParam, lParam);
}

WNDCLASS s_wndcls = {
	CS_OWNDC,    //UINT        style;
    WindowProc, //WNDPROC     lpfnWndProc;
    0, //int         cbClsExtra;
    0, //int         cbWndExtra;
    0, //HINSTANCE   hInstance;
    0, //HICON       hIcon;
    0, //HCURSOR     hCursor;
    (HBRUSH)COLOR_WINDOW, //HBRUSH      hbrBackground;
    NULL, //LPCSTR      lpszMenuName;
    "jikaki.window", //LPCSTR      lpszClassName;
};

INITCOMMONCONTROLSEX s_initCtrls = {
	sizeof(INITCOMMONCONTROLSEX), //DWORD dwSize;
    ICC_STANDARD_CLASSES, //DWORD dwICC;
};

int JIKAKI() {
	s_hInstApp = GetModuleHandle(NULL);

	HRESULT hr;
	if (FAILED(hr = CoInitialize(NULL)))
		return 1;

	{
		if (SHGetSpecialFolderPath(NULL, s_fpRtf, CSIDL_MYDOCUMENTS, true)) {
			StringCbCat(s_fpRtf, sizeof(s_fpRtf), "\\jikaki.rtf");
		}
	}

	if (NULL == LoadLibrary("RICHED20.DLL"))
		return 1;

	InitCommonControlsEx(&s_initCtrls);

	s_wndcls.hInstance = s_hInstApp;
	s_wndcls.hIcon = LoadIcon(s_hInstApp, MAKEINTRESOURCE(1));
	s_wndcls.hCursor = LoadCursor(NULL, IDC_ARROW);

	if (NULL == RegisterClass(&s_wndcls))
		return 1;

	s_hWndTop = CreateWindowEx(
		0, 
		s_wndcls.lpszClassName, "éöèëÇ´", 
		WS_OVERLAPPEDWINDOW, 
		0, 0, CW_USEDEFAULT, CW_USEDEFAULT, 
		NULL, LoadMenu(s_hInstApp, MAKEINTRESOURCE(IDR_TOPMENU)), s_hInstApp, NULL
		);
	if (s_hWndTop == NULL)
		return 1;

	ShowWindow(s_hWndTop, SW_SHOWDEFAULT);

	MSG wm;
	while (GetMessage(&wm, NULL, 0, 0)) {
		TranslateMessage(&wm);
		DispatchMessage(&wm);
	}

	return 0;
}
