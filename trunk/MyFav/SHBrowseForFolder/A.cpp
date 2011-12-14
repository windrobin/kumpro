
#include <windows.h>
#include <shlobj.h>

#include <tchar.h>

#include <atlbase.h>
#include <atlcom.h>

int _tmain() {
	HRESULT hr;
	if (FAILED(hr = OleInitialize(NULL)))
		return 1;
	BROWSEINFO bi;
	ZeroMemory(&bi, sizeof(bi));
	bi.ulFlags |= BIF_RETURNFSANCESTORS;
	bi.ulFlags |= BIF_RETURNONLYFSDIRS;
	//bi.ulFlags |= BIF_USENEWUI;
	SHBrowseForFolder(&bi);
	return 0;
}
