// JSOpen.h : JSOpen �A�v���P�[�V�����̃��C�� �w�b�_�[ �t�@�C��
//
#pragma once

#ifndef __AFXWIN_H__
	#error "PCH �ɑ΂��Ă��̃t�@�C�����C���N���[�h����O�� 'stdafx.h' ���C���N���[�h���Ă�������"
#endif

#include "resource.h"       // ���C�� �V���{��


// CJSOpenApp:
// ���̃N���X�̎����ɂ��ẮAJSOpen.cpp ���Q�Ƃ��Ă��������B
//

class CJSOpenApp : public CWinApp
{
public:
	CJSOpenApp();


// �I�[�o�[���C�h
public:
	virtual BOOL InitInstance();

// ����
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CJSOpenApp theApp;