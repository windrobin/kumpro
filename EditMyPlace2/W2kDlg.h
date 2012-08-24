
#pragma once

#include "ComboItem.h"

// CW2kDlg ダイアログ

class CW2kDlg : public CPropertyPage
{
	DECLARE_DYNAMIC(CW2kDlg)

protected:
	CImageList m_il;
	class CBExUt {
		CComboBoxEx &wndCBEx;
	public:
		CBExUt(CComboBoxEx &wndCBEx): wndCBEx(wndCBEx) {
		}
		int Add(LPCTSTR pszText, int iImage) {
			COMBOBOXEXITEM o;
			ZeroMemory(&o, sizeof(o));
			o.mask = CBEIF_IMAGE|CBEIF_TEXT|CBEIF_SELECTEDIMAGE;
			o.pszText = const_cast<LPTSTR>(pszText);
			o.iImage = iImage;
			o.iSelectedImage = iImage;
			o.iItem = wndCBEx.GetCount();

			return wndCBEx.InsertItem(&o);
		}
	};
	CArray<ComboItem, ComboItem> m_comboItems;
	void DoSelFolder(CComboBoxEx &wndCBEx, CEdit &wndfp);
	void Revert();
	bool Commit();

public:
	CW2kDlg();
	virtual ~CW2kDlg();

// ダイアログ データ
	enum { IDD = IDD_W2K };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート

	DECLARE_MESSAGE_MAP()
public:
	CComboBoxEx m_c1;
	virtual BOOL OnInitDialog();
	CComboBoxEx m_c2;
	CComboBoxEx m_c3;
	CComboBoxEx m_c4;
	CComboBoxEx m_c5;
	CEdit m_fp1;
	CEdit m_fp2;
	CEdit m_fp3;
	CEdit m_fp4;
	CEdit m_fp5;
	afx_msg void OnBnClickedR1(UINT nID);
	afx_msg void OnBnClickedRevert();
	afx_msg void OnBnClickedCommit();
	afx_msg void OnBnClickedRemove();
	virtual BOOL OnSetActive();
	afx_msg void OnBnClickedPreview();
	afx_msg void OnBnClickedReset();
};
