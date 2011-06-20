#pragma once

#include "AxTIF3View.h"

// CInnerFrame フレーム

class CInnerFrame : public CFrameWnd
{
	DECLARE_DYNCREATE(CInnerFrame)
protected:

public:
	CInnerFrame();           // 動的生成で使用される protected コンストラクタ
	virtual ~CInnerFrame();

	CAxTIF3View m_vw;

protected:
	DECLARE_MESSAGE_MAP()
	virtual BOOL OnCreateClient(LPCREATESTRUCT lpcs, CCreateContext* pContext);
	virtual void PostNcDestroy();
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
public:
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
};


