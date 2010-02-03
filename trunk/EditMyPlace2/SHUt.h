
#pragma once

class SHUt {
public:
	static HICON GetIco(int csidl) {
		HICON hIco = NULL;
		LPITEMIDLIST pidl = NULL;
		HRESULT hr;
		if (SUCCEEDED(hr = SHGetSpecialFolderLocation(NULL, csidl, &pidl))) {
			SHFILEINFO fi;
			ZeroMemory(&fi, sizeof(fi));
			DWORD_PTR r = SHGetFileInfo((LPCTSTR)pidl, 0, &fi, sizeof(fi), SHGFI_DISPLAYNAME|SHGFI_ICON|SHGFI_LARGEICON|SHGFI_PIDL);
			hIco = fi.hIcon;
			CoTaskMemFree(pidl);
		}

		return hIco;
	}
};
