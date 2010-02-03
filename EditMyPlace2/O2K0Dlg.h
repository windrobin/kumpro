
#pragma once

// CO2K0Dlg ダイアログ

class OrdItem {
public:
	CString pathKey;
	int iIndex;
	UINT idBtn;

	OrdItem() { }
	OrdItem(CString pathKey, DWORD iIndex, UINT idBtn)
		: pathKey(pathKey)
		, iIndex(iIndex)
		, idBtn(idBtn)
	{

	}
};

#define OFFVER_2000 MAKEWORD(0, 9)
#define OFFVER_XP   MAKEWORD(0,10)
#define OFFVER_2003 MAKEWORD(0,11)

class CO2K0Dlg : public CPropertyPage
{
	DECLARE_DYNAMIC(CO2K0Dlg)

public:
	CO2K0Dlg(int offver);
	virtual ~CO2K0Dlg();

protected:
	int offver;
	void Revert();
	void ReadStd(LPCTSTR pszSubkey, int csidl, int idP, int idV, int idBtn);
	CArray<OrdItem, OrdItem> m_ordItems;
	void ReadUser(LPCTSTR pszSubkey, int idN, int idBtn, int idL);
	UINT m_nID_OrderClick;
	void Reorder();
	void Browse(int idL);
	class Ut {
	public:
		static void SetIdlTo(LPITEMIDLIST pidl, CWnd *p);
	};
	class IDLUt {
	protected:
		class IDLSt {
			const BYTE *pb;
			UINT x;
			UINT cx;
		public:
			IDLSt(const BYTE *pb, UINT cb)
				: pb(pb)
				, x(0)
				, cx(cb)
			{

			}

			bool ReadByte(BYTE *pv) {
				if (x +1 <= cx) {
					if (pv != NULL)
						*pv = pb[x];
					x += 1;
					return true;
				}
				return false;
			}

			bool ReadUShort(USHORT *pv) {
				if (x +2 <= cx) {
					if (pv != NULL)
						*pv = *(USHORT *)&pb[x];
					x += 2;
					return true;
				}
				return false;
			}

			bool Skip(UINT cb) {
				if (x +cb <= cx) {
					x += cb;
					return true;
				}
				return false;
			}
		};
	public:
		static LPITEMIDLIST Clone(LPITEMIDLIST pidl) {
			return ILClone(pidl);
		}
		static void Free(LPITEMIDLIST pidl) {
			return ILFree(pidl);
		}
		static CString Format(LPCITEMIDLIST pidl) {
			IDLSt st((const BYTE *)pidl, 65536);

			if (pidl != NULL) {
				CString s = _T("ITEMIDLIST");
				PBYTE pb = (PBYTE)pidl;
				while (true) {
					int cb = *(USHORT *)pb;
					int cbWritten = max(cb, 2);
					s += _T('{');
					for (int x = 0; x < cbWritten; x++) {
						s.AppendFormat(_T("%02X"), pb[x]);
					}
					s += _T('}');
					pb += cb;
					if (cb != 0)
						continue;
					break;
				}
				return s;
			}
			else {
				return CString();
			}
		}
		static bool Parse(CString s, LPITEMIDLIST &ppidl);
		static bool IsValid(LPCITEMIDLIST pidl, UINT cbTotal) {
			IDLSt st((const BYTE *)pidl, cbTotal);

			if (pidl != NULL) {
				while (true) {
					USHORT cb = 0;
					if (!st.ReadUShort(&cb))
						return false;
					if (cb == 0)
						break;
					if (cb < 2)
						return false;
					if (!st.Skip(cb -2))
						return false;
				}
			}
			return true;
		}
		static UINT GetSize(LPCITEMIDLIST pidl) {
#if 1
			return ILGetSize(pidl);
#else
			UINT cbTotal = 0;
			if (pidl) {
				cbTotal += sizeof(pidl->mkid.cb);    // Terminating null character
				while (pidl) {
					cbTotal += pidl->mkid.cb;
					pidl = GetNextItemID(pidl);
				}
			}
			return cbTotal;
#endif
		}
		static LPITEMIDLIST GetNextItemID(LPCITEMIDLIST pidl)  { 
			// Check for valid pidl.
			if(pidl == NULL)
				return NULL;

			// Get the size of the specified item identifier. 
			int cb = pidl->mkid.cb; 

			// If the size is zero, it is the end of the list. 
			if (cb == 0) 
				return NULL; 

			// Add cb to pidl (casting to increment by bytes). 
			pidl = (LPITEMIDLIST) (((LPBYTE) pidl) + cb); 

			// Return NULL if it is null-terminating, or a pidl otherwise. 
			return (pidl->mkid.cb == 0) ? NULL : (LPITEMIDLIST) pidl; 
		} 
	};
	bool Commit();
	void WriteStd(LPCTSTR pszSubkey, int idV, int idO);
	void WriteUser(LPCTSTR pszSubkey, int idN, int idO, int idL);
	CString GetVer() { 
		switch (offver) {
			case OFFVER_2000:
				return _T("9.0");
			case OFFVER_XP:
				return _T("10.0");
			case OFFVER_2003:
				return _T("11.0");
		}
		return _T("?");
	}
	CString m_strTitle;

// ダイアログ データ
	enum { IDD = IDD_O2K0 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	afx_msg void OnBnClickedO1(UINT nID);
	afx_msg void OnOrderClicked(UINT nID);
	afx_msg void OnBnClickedR1(UINT nID);
	afx_msg void OnBnClickedRevert();
	afx_msg void OnBnClickedCommit();
	virtual BOOL OnSetActive();
	afx_msg void OnBnClickedPreview();
};
