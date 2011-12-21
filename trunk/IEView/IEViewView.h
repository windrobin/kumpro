// IEViewView.h : CVw クラスのインターフェイス
//


#pragma once


class CVw : public CHtmlView
{
protected: // シリアル化からのみ作成します。
	CVw();
	DECLARE_DYNCREATE(CVw)

// 属性
public:
	CDoc* GetDocument() const;

// 操作
public:

// オーバーライド
public:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual void OnInitialUpdate(); // 構築後に初めて呼び出されます。

// 実装
public:
	virtual ~CVw();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// 生成された、メッセージ割り当て関数
protected:
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnFileUrl();
	virtual void OnStatusTextChange(LPCTSTR lpszText);
	virtual void OnNavigateComplete2(LPCTSTR strURL);
};

#ifndef _DEBUG  // IEViewView.cpp のデバッグ バージョン
inline CDoc* CVw::GetDocument() const
   { return reinterpret_cast<CDoc*>(m_pDocument); }
#endif

