// AxTIF3View.h : CAxTIF3View クラスのインターフェイス
//


#pragma once

class CAxTIF3Doc;

class CAxTIF3View : public CView
{
protected: // シリアル化からのみ作成します。
	DECLARE_DYNAMIC(CAxTIF3View)

// 属性
public:
	CAxTIF3View();
	CAxTIF3Doc* GetDocument() const;

// 操作
public:

// オーバーライド
public:
	virtual void OnDraw(CDC* pDC);  // このビューを描画するためにオーバーライドされます。
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:

// 実装
public:
	virtual ~CAxTIF3View();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// 生成された、メッセージ割り当て関数
protected:
	afx_msg void OnCancelEditSrvr();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);

public:
	CScrollBar m_sbH, m_sbV;
	afx_msg void OnSize(UINT nType, int cx, int cy);
	CRect m_rcPaint, m_rcGlass, m_rcMove, m_rcGear, m_rcGearOn, m_rcPrev, m_rcNext, m_rcDisp, m_rcAbout, m_rcFitWH, m_rcFitW;
	CRect m_rcMMSel, m_rcZoomVal;
	CxImage *getPic(int frame = -1) const;
	CxImage *GetPic(int frame = -1) {
		return getPic(frame);
	}
	const CxImage *GetPic(int frame = -1) const {
		return getPic(frame);
	}
	float m_fZoom;
	afx_msg void OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);
	SCROLLINFO m_siH, m_siV;
	afx_msg void OnVScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar);
	CBitmap m_bmMag, m_bmMove, m_bmGear, m_bmTrick, m_bmPrev, m_bmNext, m_bmAbout, m_bmFitWH, m_bmFitW, m_bmMagBtn, m_bmMoveBtn;
	CBitmap m_bmZoomVal;
	bool m_toolZoom;
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	void LayoutClient();
	void Zoomat(bool fIn, CPoint at);
	void Zoomat2(CPoint at);
	void ZoomatR(float zf, CPoint at);
	void SetzoomR(float zf);
	afx_msg void OnRButtonDown(UINT nFlags, CPoint point);
	//int m_trickPos; // (7,7)-(247,7) 0to240.
	CPoint GetCenterPos() const;
	CPoint GetAbsPosAt(CPoint pt) const;
	void SetCenterAt(CPoint pt, CPoint clientpt);
	int Newyp(int v) const {
		return max(m_siV.nMin, min(m_siV.nMax - (int)m_siV.nPage + 1, v));
	}
	int Newxp(int v) {
		return max(m_siH.nMin, min(m_siH.nMax - (int)m_siH.nPage + 1, v));
	}
	CPoint GetScrollOff() const {
		return CPoint(m_siH.nPos, m_siV.nPos);
	}
	CPoint m_ptBegin, m_ptScrollFrm;
	bool m_fDrag;
	int m_iPage;
	int CntPages();
	enum FitMode {
		FitNo, FitW, FitWH,
	};
	FitMode m_fit;
	void DoFit() { }
	class Fitrect {
	public:
		static CRect Fit(CRect rcMax, CSize rcBox) {
            if (rcMax.Height() == 0 || rcBox.cy == 0)
                return rcMax;
            float frMax = rcMax.Width() / (float)rcMax.Height();
            float frBox = rcBox.cx / (float)rcBox.cy;
            float centerx = ((float)rcMax.left + rcMax.right) / 2;
			float centery = ((float)rcMax.top + rcMax.bottom) / 2;
            if (frMax >= frBox) {
                // 縦長
                float v = float(rcBox.cx) * rcMax.Height() / rcBox.cy / 2;
                return CRect(
                    int(centerx - v),
                    int(rcMax.top),
                    int(centerx + v),
                    int(rcMax.bottom)
                    );
            }
            else {
                // 横長
                float v = float(rcBox.cy) * rcMax.Width() / rcBox.cx / 2;
                return CRect(
                    int(rcMax.left),
                    int(centery - v),
                    int(rcMax.right),
                    int(centery + v)
                    );
            }
        }
	};
	CSize GetZoomedSize();
	void SetFit(FitMode fit) {
		m_fit = fit;
		InvalidateRect(m_rcFitW);
		InvalidateRect(m_rcFitWH);

		InvalidateRect(m_rcGear,false);
	}
	float Getzf() const;
	void Setzf(float zf) {
		m_fZoom = zf;
		SetFit(FitNo);
		InvalidateRect(m_rcGear,false);
		InvalidateRect(m_rcZoomVal,false);
	}
	int GetTrickPos() {
		int v = z2tp(Getzf());
		return max(0, min(240, v));
	}
	LONG m_ddcompat, m_slowzoom;
	CRect GetZoomedDispRect() {
		CSize size = GetZoomedSize();
		int cx = size.cx;
		int cy = size.cy;

		int xp = -m_siH.nPos;
		int yp = -m_siV.nPos;

		if (cx < m_rcPaint.Width()) {
			xp = (m_rcPaint.Width() - cx) / 2;
		}
		if (cy < m_rcPaint.Height()) {
			yp = (m_rcPaint.Height() - cy) / 2;
		}
		return CRect(xp, yp, xp +cx, yp +cy);
	}
	CPoint m_ptClip;

	int z2tp(float f) const {
#if 1
		return (int)(log(f / 0.0625f) / log(2.0f) / 8 * 240);
#else
		if (f <= 0.0625f) return 0;
		if (f <= 0.125f) return 15;
		if (f <= 0.25f) return 30;
		if (f <= 0.5f) return 45;
		if (f <= 1.0f) return 60;
		if (f <= 2.0f) return 75;
		if (f <= 4.0f) return 90;
		if (f <= 8.0f) return 105;
		if (f <= 16.0f) return 120;
#endif
	}

	float tp2z(int t) const {
		return pow(2, t/30.0f) * 0.0625f;
	}

	afx_msg void OnLButtonDblClk(UINT nFlags, CPoint point);
	afx_msg void OnRButtonDblClk(UINT nFlags, CPoint point);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
	afx_msg BOOL OnMouseWheel(UINT nFlags, short zDelta, CPoint pt);
	afx_msg LRESULT OnMouseHWheel(WPARAM, LPARAM);
	afx_msg int OnMouseActivate(CWnd* pDesktopWnd, UINT nHitTest, UINT message);
	afx_msg void OnSelCmd(UINT nID);
	afx_msg void OnUpdateSelCmd(CCmdUI *pUI);
protected:
	virtual void PostNcDestroy();
	virtual void OnUpdate(CView* /*pSender*/, LPARAM /*lHint*/, CObject* /*pHint*/);
};

#ifndef _DEBUG  // AxTIF3View.cpp のデバッグ バージョン
inline CAxTIF3Doc* CAxTIF3View::GetDocument() const
   { return reinterpret_cast<CAxTIF3Doc*>(m_pDocument); }
#endif

