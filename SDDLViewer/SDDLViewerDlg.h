// SDDLViewerDlg.h : header file
//

#pragma once

// CvDlg dialog
class CvDlg : public CDialog
{
// Construction
public:
	CvDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_SDDLVIEWER_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	CString m_strSDDL;
	afx_msg void OnBnClickedPaste();
	afx_msg void OnBnClickedView();
	CComboBox m_wndSe;
	afx_msg void OnBnClickedGet();
	afx_msg void OnBnClickedCopy();
	afx_msg void OnBnClickedSet();
	CString m_objName;
	BOOL m_dacl;
	BOOL m_sacl;
	BOOL m_owner;
	BOOL m_group;
	afx_msg void OnBnClickedEdit();
};
