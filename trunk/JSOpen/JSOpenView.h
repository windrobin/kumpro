// JSOpenView.h : CJSOpenView クラスのインターフェイス
//


#pragma once


class CJSOpenView : public CHtmlView
{
protected: // シリアル化からのみ作成します。
	CJSOpenView();
	DECLARE_DYNCREATE(CJSOpenView)

// 属性
public:
	CJSOpenDoc* GetDocument() const;

// 操作
public:

// オーバーライド
public:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual void OnInitialUpdate(); // 構築後に初めて呼び出されます。

// 実装
public:
	virtual ~CJSOpenView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// 生成された、メッセージ割り当て関数
protected:
	DECLARE_MESSAGE_MAP()
public:
	virtual void OnNewWindow2(LPDISPATCH* ppDisp, BOOL* Cancel);
	afx_msg void OnFileWindowOpen();
	afx_msg void OnFileFix1();
};

#ifndef _DEBUG  // JSOpenView.cpp のデバッグ バージョン
inline CJSOpenDoc* CJSOpenView::GetDocument() const
   { return reinterpret_cast<CJSOpenDoc*>(m_pDocument); }
#endif

