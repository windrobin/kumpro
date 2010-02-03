
#pragma once

#define CITY_HIDDEN 0
#define CITY_CSIDL 1
#define CITY_REG_SZ 2
#define CITY_REG_ENVSZ 3

class ComboItem {
public:
	int city;
	int csidl;
	CString dispName;
	HICON hIcon, hIconTobeDestroyed;

	ComboItem() { }
	ComboItem(int city, int csidl, CString dispName)
		: city(city)
		, csidl(csidl)
		, dispName(dispName)
		, hIcon(hIcon)
	{

	}

	ComboItem &SetIco(HICON hIcon) {
		this->hIcon = hIcon;

		return *this;
	}

	ComboItem &ResolveName() {
		HRESULT hr;
		LPITEMIDLIST pidl = NULL;
		if (SUCCEEDED(hr = SHGetSpecialFolderLocation(NULL, csidl, &pidl))) {
			SHFILEINFO fi;
			ZeroMemory(&fi, sizeof(fi));
			DWORD_PTR r = SHGetFileInfo((LPCTSTR)pidl, 0, &fi, sizeof(fi), SHGFI_DISPLAYNAME|SHGFI_ICON|SHGFI_LARGEICON|SHGFI_PIDL);
			this->hIconTobeDestroyed = this->hIcon = fi.hIcon;
			this->dispName = fi.szDisplayName;
			CoTaskMemFree(pidl);
		}
		return *this;
	}
};

