
#pragma once

// CSetterDlg �_�C�A���O

class CSetterDlg : public CDialog
{
public:
	CSetterDlg(CWnd* pParent = NULL);   // �W���R���X�g���N�^
	virtual ~CSetterDlg();

	void OnOK();

// �_�C�A���O �f�[�^
	enum { IDD = IDD_SETTER };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV �T�|�[�g

	DECLARE_MESSAGE_MAP()
public:
	int m_nKA;
	virtual BOOL OnInitDialog();
};
