// Closer.cpp : CCloser の実装

#include "StdAfx.h"
#include "Closer.h"

// CCloser

CRBMap<HWND, Subclassed> s_tree;
CMyMutex s_lck;

static bool IsTar(HWND hWnd) {
	TCHAR tc[256] = {_T("")};
	GetClassName(hWnd, tc, 256);
	tc[255] = 0;
	if (_tcsncmp(tc, _T("ATL:"), 4) == 0) {
		HWND hwndTop = FindWindowEx(hWnd, NULL, _T("_NTRTBLAYERCLASS"), NULL);
		return hwndTop != NULL;
	}
	return false;
}

extern CAtlModule &GetAtlModule();

void CloseIME(HWND hwnd) {
#pragma comment(lib, "imm32")
#if 1
	ImmAssociateContext(hwnd, NULL);
#else
	HIMC hIMC;
	hIMC = ImmGetContext(hwnd);
	ImmSetOpenStatus(hIMC, false);
	ImmReleaseContext(hwnd, hIMC);
#endif
}

#if 0

void CancelIME(HWND hwnd) {
	HIMC hIMC;
	hIMC = ImmGetContext(hwnd);
	ImmNotifyIME(hIMC, NI_COMPOSITIONSTR, CPS_CANCEL, 0);
	ImmReleaseContext(hwnd, hIMC);
}

LRESULT CALLBACK MyNoimeWP(
	HWND hwnd,
	UINT uMsg,
	WPARAM wParam,
	LPARAM lParam
) {
	switch (uMsg) {
		case WM_IME_NOTIFY:
			{
				if (wParam == IMN_SETOPENSTATUS) {
					CloseIME(hwnd);
				}
				break;
			}
		case WM_IME_COMPOSITION:
			{
				CancelIME(hwnd);
				break;
			}
		case WM_NCDESTROY:
			{
				CMyAutoLockMutex alck(s_lck);

				if (s_tree.RemoveKey(hwnd)) {
					GetAtlModule().Unlock();
				}
				break;
			}
	}

	WNDPROC lpPrevWndFunc = NULL;

	{
		CMyAutoLockMutex alck(s_lck);

		Subclassed instance;

		if (s_tree.Lookup(hwnd, instance)) {
			lpPrevWndFunc = instance.lpPrevWndFunc;
		}
	}

	LRESULT res = (lpPrevWndFunc != NULL)
		? CallWindowProc(lpPrevWndFunc, hwnd, uMsg, wParam, lParam)
		: DefWindowProc(hwnd, uMsg, wParam, lParam);

	switch (uMsg) {
		case WM_IME_NOTIFY:
			{
				if (wParam == IMN_SETOPENSTATUS) {
					CloseIME(hwnd);
				}
				break;
			}
	}

	return res;
}

#endif

BOOL CALLBACK ECP(
	HWND hwnd,
	LPARAM lParam
) {
	if (IsTar(hwnd)) {
#if 1
		CloseIME(hwnd);
#else
		CMyAutoLockMutex alck(s_lck);

		Subclassed instance;
		if (s_tree.Lookup(hwnd, instance)) {

		}
		else {
			instance.lpPrevWndFunc = reinterpret_cast<WNDPROC>(GetWindowLong(hwnd, GWL_WNDPROC));

			if (0 != SetWindowLong(hwnd, GWL_WNDPROC, reinterpret_cast<LONG>(MyNoimeWP))) {
				s_tree.SetAt(hwnd, instance);

				GetAtlModule().Lock();
			}
		}
#endif
	}
	return true;
}

STDMETHODIMP CCloser::NavigateComplete2(IDispatch* pDisp, VARIANT* URL) {
	if (m_pSite != NULL) {
		SHANDLE_PTR hwndIt = NULL;
		HRESULT hr = m_pSite->get_HWND(&hwndIt);
		if (SUCCEEDED(hr)) {
			EnumChildWindows(reinterpret_cast<HWND>(hwndIt), ECP, 0);
		}
	}

	return S_OK;
}
