// JSOpenView.h : CJSOpenView �N���X�̃C���^�[�t�F�C�X
//


#pragma once


class CJSOpenView : public CHtmlView
{
protected: // �V���A��������̂ݍ쐬���܂��B
	CJSOpenView();
	DECLARE_DYNCREATE(CJSOpenView)

// ����
public:
	CJSOpenDoc* GetDocument() const;

// ����
public:

// �I�[�o�[���C�h
public:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual void OnInitialUpdate(); // �\�z��ɏ��߂ČĂяo����܂��B

// ����
public:
	virtual ~CJSOpenView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// �������ꂽ�A���b�Z�[�W���蓖�Ċ֐�
protected:
	DECLARE_MESSAGE_MAP()
public:
	virtual void OnNewWindow2(LPDISPATCH* ppDisp, BOOL* Cancel);
	afx_msg void OnFileWindowOpen();
	afx_msg void OnFileFix1();
};

#ifndef _DEBUG  // JSOpenView.cpp �̃f�o�b�O �o�[�W����
inline CJSOpenDoc* CJSOpenView::GetDocument() const
   { return reinterpret_cast<CJSOpenDoc*>(m_pDocument); }
#endif

