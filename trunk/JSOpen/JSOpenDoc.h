// JSOpenDoc.h : CJSOpenDoc �N���X�̃C���^�[�t�F�C�X
//


#pragma once


class CJSOpenDoc : public CDocument
{
protected: // �V���A��������̂ݍ쐬���܂��B
	CJSOpenDoc();
	DECLARE_DYNCREATE(CJSOpenDoc)

// ����
public:

// ����
public:

// �I�[�o�[���C�h
public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);

// ����
public:
	virtual ~CJSOpenDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// �������ꂽ�A���b�Z�[�W���蓖�Ċ֐�
protected:
	DECLARE_MESSAGE_MAP()
};


