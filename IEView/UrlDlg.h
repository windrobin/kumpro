#pragma once


// CUrlDlg ダイアログ

class CUrlDlg : public CDialog
{
	DECLARE_DYNAMIC(CUrlDlg)

public:
	CUrlDlg(CWnd* pParent = NULL);   // 標準コンストラクタ
	virtual ~CUrlDlg();

// ダイアログ データ
	enum { IDD = IDD_URLDLG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート

	DECLARE_MESSAGE_MAP()
public:
	CString m_strUrl;
};
