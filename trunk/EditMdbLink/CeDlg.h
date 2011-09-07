// CeDlg.h : ヘッダー ファイル
//

#pragma once
#include "afxwin.h"


// CeDlg ダイアログ
class CeDlg : public CDialog
{
// コンストラクション
public:
	CeDlg(CWnd* pParent = NULL);	// 標準コンストラクタ

// ダイアログ データ
	enum { IDD = IDD_EDITMDBLINK_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV サポート


// 実装
protected:
	HICON m_hIcon;

	// 生成された、メッセージ割り当て関数
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedRefMdb();
	CString m_strMdb;
	afx_msg void OnBnClickedOpen();
	CString m_strConns;
	afx_msg void OnBnClickedSave();
};
