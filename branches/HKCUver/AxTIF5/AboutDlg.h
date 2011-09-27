#pragma once
#include "afxcmn.h"
#include "afxwin.h"


// CAboutDlg ダイアログ

class CAboutDlg : public CDialog
{
	DECLARE_DYNAMIC(CAboutDlg)

public:
	CAboutDlg(CWnd* pParent = NULL);   // 標準コンストラクタ
	virtual ~CAboutDlg();

// ダイアログ データ
	enum { IDD = IDD_ABOUTBOX_AXTIF5 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	CRichEditCtrl m_credits;
	CStatic m_wndVer;
};
