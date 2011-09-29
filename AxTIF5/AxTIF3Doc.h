// AxTIF3Doc.h : CAxTIF3Doc クラスのインターフェイス
//


#pragma once

#define UPHINT_LOADED 1

class CAxTIF3SrvrItem;

class CAxTIF3Doc : public CDocument
{
protected: // シリアル化からのみ作成します。
	CAxTIF3Doc();
	DECLARE_DYNCREATE(CAxTIF3Doc)

// 属性
public:

// 操作
public:

// オーバーライド
protected:
public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);

// 実装
public:
	virtual ~CAxTIF3Doc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// 生成された、メッセージ割り当て関数
protected:
	DECLARE_MESSAGE_MAP()

public:
	CAutoPtrArray<CxImage> m_tifs;
};


