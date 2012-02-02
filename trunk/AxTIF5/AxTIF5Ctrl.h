#pragma once

#include "InnerFrame.h"

// AxTIF5Ctrl.h : CAxTIF5Ctrl ActiveX コントロール クラスの宣言です。


// CAxTIF5Ctrl : 実装に関しては AxTIF5Ctrl.cpp を参照してください。

class CAxTIF5Ctrl : public COleControl
{
	DECLARE_DYNCREATE(CAxTIF5Ctrl)

// コンストラクタ
public:
	CAxTIF5Ctrl();

// オーバーライド
public:
	virtual void OnDraw(CDC* pdc, const CRect& rcBounds, const CRect& rcInvalid);
	virtual void DoPropExchange(CPropExchange* pPX);
	virtual void OnResetState();

	CString m_src, m_title;

// 実装
protected:
	~CAxTIF5Ctrl();

	virtual void OnShowToolBars();

	CInnerFrame m_frame;

	CComPtr<IMoniker> m_pimkDL;

	DECLARE_OLECREATE_EX(CAxTIF5Ctrl)    // クラス ファクトリ と guid
	DECLARE_OLETYPELIB(CAxTIF5Ctrl)      // GetTypeInfo
	DECLARE_PROPPAGEIDS(CAxTIF5Ctrl)     // プロパティ ページ ID
	DECLARE_OLECTLTYPE(CAxTIF5Ctrl)		// タイプ名とその他のステータス

// メッセージ マップ
	DECLARE_MESSAGE_MAP()

// ディスパッチ マップ
	DECLARE_DISPATCH_MAP()

	afx_msg void AboutBox();

// イベント マップ
	DECLARE_EVENT_MAP()

	DECLARE_INTERFACE_MAP()

	BEGIN_INTERFACE_PART(ObjectSafety, IObjectSafety)
        virtual HRESULT STDMETHODCALLTYPE GetInterfaceSafetyOptions( 
            /* [in] */ REFIID riid,
            /* [out] */ DWORD *pdwSupportedOptions,
            /* [out] */ DWORD *pdwEnabledOptions);
        
        virtual HRESULT STDMETHODCALLTYPE SetInterfaceSafetyOptions( 
            /* [in] */ REFIID riid,
            /* [in] */ DWORD dwOptionSetMask,
            /* [in] */ DWORD dwEnabledOptions);

		DWORD m_dwCurrentSafety;

		XObjectSafety(): m_dwCurrentSafety(0) {
		}
	END_INTERFACE_PART(ObjectSafety)

// ディスパッチ と イベント ID
public:
	enum {
	};
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnSize(UINT nType, int cx, int cy);
protected:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	void LoadFromMoniker(LPBC pibc, LPMONIKER pimkDL);
public:
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
	virtual BOOL OnSetExtent(LPSIZEL lpSizeL);
	afx_msg LRESULT OnIdleUpdateCmdUI(WPARAM, LPARAM);
};

