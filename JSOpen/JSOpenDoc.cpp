// JSOpenDoc.cpp : CJSOpenDoc �N���X�̎���
//

#include "stdafx.h"
#include "JSOpen.h"

#include "JSOpenDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CJSOpenDoc

IMPLEMENT_DYNCREATE(CJSOpenDoc, CDocument)

BEGIN_MESSAGE_MAP(CJSOpenDoc, CDocument)
END_MESSAGE_MAP()


// CJSOpenDoc �R���X�g���N�V����/�f�X�g���N�V����

CJSOpenDoc::CJSOpenDoc()
{
	// TODO: ���̈ʒu�� 1 �x�����Ă΂��\�z�p�̃R�[�h��ǉ����Ă��������B

}

CJSOpenDoc::~CJSOpenDoc()
{
}

BOOL CJSOpenDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: ���̈ʒu�ɍď�����������ǉ����Ă��������B
	// (SDI �h�L�������g�͂��̃h�L�������g���ė��p���܂��B)

	return TRUE;
}




// CJSOpenDoc �V���A����

void CJSOpenDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: �i�[����R�[�h�������ɒǉ����Ă��������B
	}
	else
	{
		// TODO: �ǂݍ��ރR�[�h�������ɒǉ����Ă��������B
	}
}


// CJSOpenDoc �f�f

#ifdef _DEBUG
void CJSOpenDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CJSOpenDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG


// CJSOpenDoc �R�}���h
