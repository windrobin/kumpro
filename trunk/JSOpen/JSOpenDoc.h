// JSOpenDoc.h : CJSOpenDoc クラスのインターフェイス
//


#pragma once


class CJSOpenDoc : public CDocument
{
protected: // シリアル化からのみ作成します。
	CJSOpenDoc();
	DECLARE_DYNCREATE(CJSOpenDoc)

// 属性
public:

// 操作
public:

// オーバーライド
public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);

// 実装
public:
	virtual ~CJSOpenDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// 生成された、メッセージ割り当て関数
protected:
	DECLARE_MESSAGE_MAP()
};


