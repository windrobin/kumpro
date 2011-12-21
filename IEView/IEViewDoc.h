// IEViewDoc.h : CDoc クラスのインターフェイス
//


#pragma once


class CDoc : public CDocument
{
protected: // シリアル化からのみ作成します。
	CDoc();
	DECLARE_DYNCREATE(CDoc)

// 属性
public:
	CString m_strUrl;

// 操作
public:

// オーバーライド
public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);

// 実装
public:
	virtual ~CDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// 生成された、メッセージ割り当て関数
protected:
	DECLARE_MESSAGE_MAP()

	// 生成された OLE ディスパッチ割り当て関数

	DECLARE_DISPATCH_MAP()
	DECLARE_INTERFACE_MAP()
public:
	virtual BOOL OnOpenDocument(LPCTSTR lpszPathName);
};


