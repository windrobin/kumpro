
#pragma once

#include "BAUt.h"

class OItem {
	OItem(const OItem &) { }

public:
	CString dispName;
	int iImage;
	int iKg;
	HICON hIcon, hIconTobeDestroyed;
	bool isVisible;
	CString subkeyStd;
	CString subkeyUser;
	CByteArray buffPidl;

	OItem() { }
	OItem(int iKg, bool isVisible)
		: iKg(iKg)
		, hIcon(NULL)
		, hIconTobeDestroyed(NULL)
		, isVisible(isVisible)
	{

	}

	void ResolveDispName(int csidl) {
		LPITEMIDLIST pidl = NULL;
		HRESULT hr;
		if (SUCCEEDED(hr = SHGetSpecialFolderLocation(NULL, csidl, &pidl))) {
			SHFILEINFO fi;
			ZeroMemory(&fi, sizeof(fi));
			DWORD_PTR r = SHGetFileInfo((LPCTSTR)pidl, 0, &fi, sizeof(fi), SHGFI_DISPLAYNAME|SHGFI_ICON|SHGFI_LARGEICON|SHGFI_PIDL);
			this->hIconTobeDestroyed = this->hIcon = fi.hIcon;
			this->dispName = fi.szDisplayName;
			CoTaskMemFree(pidl);
		}
	}

	void AssocImageList(CImageList &il) {
		VERIFY(0 <= (iImage = il.Add(hIcon)));
	}

	void SetStdSubkey(CString subkeyStd) {
		this->subkeyStd = subkeyStd;
	}

	void SetUserSubkey(CString subkeyUser) {
		this->subkeyUser = subkeyUser;
	}

	void SetPidl(const void *pidl, int cb) {
		BAUt::Set(buffPidl, pidl, cb);

		{
			SHFILEINFO fi;
			ZeroMemory(&fi, sizeof(fi));
			DWORD_PTR r = SHGetFileInfo((LPCTSTR)buffPidl.GetData(), 0, &fi, sizeof(fi), SHGFI_DISPLAYNAME|SHGFI_ICON|SHGFI_LARGEICON|SHGFI_PIDL);
			this->hIconTobeDestroyed = this->hIcon = fi.hIcon;
			this->dispName = fi.szDisplayName;
		}
	}

	class Sort {
	public:
		static int byKg_qsort(const void *p1, const void *p2) {
			return byKg(*(const OItem *)p1, *(const OItem *)p2);
		}
		static int byKg(const OItem &x1, const OItem &x2) {
			return x1.iKg < x2.iKg;
		}
	};
};

// CO2Dlg ダイアログ

class CO2Dlg : public CPropertyPage
{
	DECLARE_DYNAMIC(CO2Dlg)

public:
	CO2Dlg();
	virtual ~CO2Dlg();

protected:
	CImageList m_il;
	CTypedPtrArray<CPtrArray, OItem *> m_oItems;
	void Commit2lc();
	void Revert();
	void ReadStd(CRegKey &rkStd, LPCTSTR pszSubkey, int csidl);
	void ReadUser(CRegKey &rkUser, LPCTSTR pszSubkey);

// ダイアログ データ
	enum { IDD = IDD_OFFICE2K };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	CListCtrl m_l;
	afx_msg void OnLvnGetdispinfoL(NMHDR *pNMHDR, LRESULT *pResult);
};
