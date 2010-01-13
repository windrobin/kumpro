
#pragma once

// CSetterDlg ダイアログ

class CSetterDlg : public CDialog
{
public:
	CSetterDlg(CWnd* pParent = NULL);   // 標準コンストラクタ
	virtual ~CSetterDlg();

	void OnOK();

// ダイアログ データ
	enum { IDD = IDD_SETTER };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート

	DECLARE_MESSAGE_MAP()
public:
	int m_nKA;
	virtual BOOL OnInitDialog();
};
