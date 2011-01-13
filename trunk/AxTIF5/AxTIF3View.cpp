// AxTIF3View.cpp : CAxTIF3View クラスの実装
//

#include "stdafx.h"
#include "AxTIF3.h"

#include "AxTIF3Doc.h"
#include "AxTIF3View.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

const int cxBMPrev = 32;
const int cxBMNext = 32;

const int cxBMGear = 254;
const int cyBar = 24;

// CAxTIF3View

IMPLEMENT_DYNCREATE(CAxTIF3View, CView)

#ifndef WM_MOUSEHWHEEL
#define WM_MOUSEHWHEEL 0x020E
#endif // WM_MOUSEHWHEEL

BEGIN_MESSAGE_MAP(CAxTIF3View, CView)
	ON_WM_CREATE()
	ON_WM_SIZE()
	ON_WM_HSCROLL()
	ON_WM_VSCROLL()
	ON_WM_LBUTTONDOWN()
	ON_WM_RBUTTONDOWN()
	ON_WM_LBUTTONDBLCLK()
	ON_WM_RBUTTONDBLCLK()
	ON_WM_LBUTTONUP()
	ON_WM_MOUSEMOVE()
	ON_WM_ERASEBKGND()
	ON_WM_MOUSEWHEEL()
	ON_MESSAGE(WM_MOUSEHWHEEL, OnMouseHWheel)
	ON_WM_MOUSEACTIVATE()
END_MESSAGE_MAP()

// CAxTIF3View コンストラクション/デストラクション

CAxTIF3View::CAxTIF3View()
{
	// TODO: 構築コードをここに追加します。

}

CAxTIF3View::~CAxTIF3View()
{
}

BOOL CAxTIF3View::PreCreateWindow(CREATESTRUCT& cs)
{
	if (!CView::PreCreateWindow(cs))
		return false;

	cs.dwExStyle &= ~WS_EX_CLIENTEDGE;
	return true;
}

// CAxTIF3View 描画

void CAxTIF3View::OnDraw(CDC* pDC)
{
	CAxTIF3Doc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	CxImage *p = GetPic();
	if (p != NULL && pDC->RectVisible(m_rcPaint)) {
		int cx = (m_fZoom != 1) ? (int)(p->GetWidth() * m_fZoom) : (p->GetWidth());
		int cy = (m_fZoom != 1) ? (int)(p->GetHeight() * m_fZoom) : (p->GetHeight());

		int xp = -m_siH.nPos;
		int yp = -m_siV.nPos;

		if (cx < m_rcPaint.Width()) {
			xp = (m_rcPaint.Width() - cx) / 2;
		}
		if (cy < m_rcPaint.Height()) {
			yp = (m_rcPaint.Height() - cy) / 2;
		}

		ASSERT(p != NULL);
		p->Draw(pDC->GetSafeHdc(), xp, yp, cx, cy, m_rcPaint);

		CRgn rgnWaku;
		rgnWaku.CreateRectRgnIndirect(&m_rcPaint);
		CRgn rgnPic;
		rgnPic.CreateRectRgnIndirect(CRect(xp, yp, xp +cx, yp +cy));

		rgnWaku.CombineRgn(&rgnWaku, &rgnPic, RGN_DIFF);

		CBrush br;
		br.CreateSysColorBrush(COLOR_BTNSHADOW);
		pDC->FillRgn(&rgnWaku, &br);

		pDC->ExcludeClipRect(m_rcPaint);
	}

	if (pDC->RectVisible(m_rcGlass)) {
		CDC dc;
		dc.CreateCompatibleDC(pDC);
		CBitmap* pOrg = dc.SelectObject(&m_bmMag);
		pDC->BitBlt(m_rcGlass.left, m_rcGlass.top, m_rcGlass.Width(), m_rcGlass.Height(), &dc, 0, 0, SRCCOPY);
		if (m_toolZoom) pDC->InvertRect(m_rcGlass);
		dc.SelectObject(pOrg);
		pDC->ExcludeClipRect(m_rcGlass);
	}
	if (pDC->RectVisible(m_rcMove)) {
		CDC dc;
		dc.CreateCompatibleDC(pDC);
		CBitmap* pOrg = dc.SelectObject(&m_bmMove);
		pDC->BitBlt(m_rcMove.left, m_rcMove.top, m_rcMove.Width(), m_rcMove.Height(), &dc, 0, 0, SRCCOPY);
		if (!m_toolZoom) pDC->InvertRect(m_rcMove);
		dc.SelectObject(pOrg);
		pDC->ExcludeClipRect(m_rcMove);
	}
	if (pDC->RectVisible(m_rcGear)) {
		CDC dc;
		dc.CreateCompatibleDC(pDC);
		CBitmap* pOrg = dc.SelectObject(&m_bmGear);
		pDC->BitBlt(m_rcGear.left, m_rcGear.top, m_rcGear.Width(), m_rcGear.Height(), &dc, 0, 0, SRCCOPY);
		dc.SelectObject(m_bmTrick);
		pDC->BitBlt(m_rcGear.left + 7 + m_trickPos - 7/2, m_rcGear.top + 7 - 12/2 + (cyBar-16)/2, 7, 12, &dc, 0, 0, SRCCOPY);
		dc.SelectObject(pOrg);
		pDC->ExcludeClipRect(m_rcGear);
	}
	if (pDC->RectVisible(m_rcPrev)) {
		CDC dc;
		dc.CreateCompatibleDC(pDC);
		CBitmap* pOrg = dc.SelectObject(&m_bmPrev);
		pDC->BitBlt(m_rcPrev.left, m_rcPrev.top, m_rcPrev.Width(), m_rcPrev.Height(), &dc, 0, 0, SRCCOPY);
		dc.SelectObject(pOrg);
		pDC->ExcludeClipRect(m_rcPrev);
	}
	if (pDC->RectVisible(m_rcNext)) {
		CDC dc;
		dc.CreateCompatibleDC(pDC);
		CBitmap* pOrg = dc.SelectObject(&m_bmNext);
		pDC->BitBlt(m_rcNext.left, m_rcNext.top, m_rcNext.Width(), m_rcNext.Height(), &dc, 0, 0, SRCCOPY);
		dc.SelectObject(pOrg);
		pDC->ExcludeClipRect(m_rcNext);
	}
	if (pDC->RectVisible(m_rcDisp)) {
		pDC->SelectStockObject(DEFAULT_GUI_FONT);
		CString str; //str.Format(_T("%u"), 1+m_iPage);
		if (m_iPage < CntPages())
			str.Format(_T("%u/%u"), 1+m_iPage, CntPages());

		COLORREF lastBkClr = pDC->SetBkColor(GetSysColor(COLOR_WINDOW));
		UINT lastMode = pDC->SetTextAlign(TA_CENTER|TA_TOP);
		CSize size =  pDC->GetTextExtent(str);
		CPoint pt = m_rcDisp.CenterPoint() - CPoint(0, size.cy / 2);
		pDC->ExtTextOut(pt.x, pt.y, ETO_CLIPPED|ETO_OPAQUE, m_rcDisp, str, str.GetLength(), NULL);
		pDC->SetTextAlign(lastMode);
		pDC->SetBkColor(lastBkClr);

		pDC->ExcludeClipRect(m_rcDisp);
	}
	if (pDC->RectVisible(m_rcAbout)) {
		CDC dc;
		dc.CreateCompatibleDC(pDC);
		CBitmap* pOrg = dc.SelectObject(&m_bmAbout);
		pDC->BitBlt(m_rcAbout.left, m_rcAbout.top, m_rcAbout.Width(), m_rcAbout.Height(), &dc, 0, 0, SRCCOPY);
		dc.SelectObject(pOrg);
		pDC->ExcludeClipRect(m_rcAbout);
	}

	{
		CBrush br;
		br.CreateSysColorBrush(COLOR_WINDOW);
		CRect rc;
		pDC->GetClipBox(rc);
		pDC->FillRect(rc, &br);
	}
}


CxImage *CAxTIF3View::GetPic(int frame) {
	if (frame < 0)
		frame = m_iPage;
	CAxTIF3Doc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (pDoc != NULL) {
		UINT cx = pDoc->m_tifs.GetCount();
		if ((UINT)frame < cx)
			return pDoc->m_tifs[frame];
	}
	return NULL;
}

// OLE Server サポート

// 以下に示すコマンド ハンドラは組み込み先編集を中止するための標準的な
//  キーボード ユーザー インターフェイスを用意しています。ここではコンテナ
//  (サーバーではない)が非アクティブ化を引き起こします。


// CAxTIF3View 診断

#ifdef _DEBUG
void CAxTIF3View::AssertValid() const
{
	CView::AssertValid();
}

void CAxTIF3View::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CAxTIF3Doc* CAxTIF3View::GetDocument() const // デバッグ以外のバージョンはインラインです。
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CAxTIF3Doc)));
	return (CAxTIF3Doc*)m_pDocument;
}
#endif //_DEBUG


// CAxTIF3View メッセージ ハンドラ

int CAxTIF3View::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CView::OnCreate(lpCreateStruct) == -1)
		return -1;

	if (false
		|| !m_sbH.Create(WS_CHILDWINDOW|WS_VISIBLE|SBS_HORZ|SBS_BOTTOMALIGN, CRect(), this, IDC_HORZ)
		|| !m_sbV.Create(WS_CHILDWINDOW|WS_VISIBLE|SBS_VERT|SBS_RIGHTALIGN, CRect(), this, IDC_VERT))
		return -1;

	if (false
		|| !m_bmMag.LoadBitmap(IDB_MAG)
		|| !m_bmMove.LoadBitmap(IDB_MOVE)
		|| !m_bmGear.LoadBitmap(IDB_GEAR)
		|| !m_bmTrick.LoadBitmap(IDB_TRICK)
		|| !m_bmPrev.LoadBitmap(IDB_PREV)
		|| !m_bmNext.LoadBitmap(IDB_NEXT)
		|| !m_bmAbout.LoadBitmap(IDB_ABOUT)
		)
		return -1;

	m_fZoom = 1;
	ZeroMemory(&m_siH, sizeof(m_siH));
	m_siH.cbSize = sizeof(m_siH);
	ZeroMemory(&m_siV, sizeof(m_siV));
	m_siV.cbSize = sizeof(m_siV);
	m_toolZoom = true;
	m_trickPos = z2tp(m_fZoom);
	m_fDrag = false;
	m_iPage = 0;

	LayoutClient();

	return 0;
}

void CAxTIF3View::OnSize(UINT nType, int cx, int cy)
{
	CView::OnSize(nType, cx, cy);

	LayoutClient();
}

void CAxTIF3View::OnHScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar)
{
	if (pScrollBar == &m_sbH) {
		int oldpos = m_sbH.GetScrollPos();
		int newpos = oldpos;

		switch (nSBCode) {
			case SB_LEFT: newpos = m_siH.nMin; break;
			case SB_LINELEFT: newpos--; break;
			case SB_LINERIGHT: newpos++; break;
			case SB_PAGELEFT: newpos -= m_siH.nPage; break;
			case SB_PAGERIGHT: newpos += m_siH.nPage; break;
			case SB_RIGHT: newpos = m_siH.nMax; break;
			case SB_THUMBPOSITION: newpos = nPos; break;
			case SB_THUMBTRACK: newpos = nPos; break;
		}

		newpos = Newxp(newpos);

		if (oldpos != newpos) {
			m_siH.nPos = newpos;
			m_sbH.SetScrollPos(newpos);
			this->InvalidateRect(m_rcPaint, false);
			return;
		}
		return;
	}

	CView::OnHScroll(nSBCode, nPos, pScrollBar);
}

void CAxTIF3View::OnVScroll(UINT nSBCode, UINT nPos, CScrollBar* pScrollBar)
{
	if (pScrollBar == &m_sbV) {
		int oldpos = m_sbV.GetScrollPos();
		int newpos = oldpos;

		switch (nSBCode) {
			case SB_TOP: newpos = m_siV.nMin; break;
			case SB_LINEUP: newpos--; break;
			case SB_LINEDOWN: newpos++; break;
			case SB_PAGEUP: newpos -= m_siV.nPage; break;
			case SB_PAGEDOWN: newpos += m_siV.nPage; break;
			case SB_BOTTOM: newpos = m_siV.nMax; break;
			case SB_THUMBPOSITION: newpos = nPos; break;
			case SB_THUMBTRACK: newpos = nPos; break;
		}

		newpos = Newyp(newpos);

		if (oldpos != newpos) {
			m_siV.nPos = newpos;
			m_sbV.SetScrollPos(newpos);
			this->InvalidateRect(m_rcPaint, false);
			return;
		}
		return;
	}

	CView::OnVScroll(nSBCode, nPos, pScrollBar);
}

void CAxTIF3View::OnRButtonDown(UINT nFlags, CPoint point) {
	if (GetFocus() == this) {
		if (m_toolZoom && m_rcPaint.PtInRect(point)) {
			Zoomat(false, point);
			return;
		}
	}

	CView::OnRButtonDown(nFlags, point);
}

void CAxTIF3View::OnLButtonDown(UINT nFlags, CPoint point) {
	if (GetFocus() == this) {
		if (m_rcGlass.PtInRect(point)) {
			m_toolZoom = true;
			InvalidateRect(m_rcGlass,false);InvalidateRect(m_rcMove,false);
		}
		else if (m_rcMove.PtInRect(point)) {
			m_toolZoom = false;
			InvalidateRect(m_rcGlass,false);InvalidateRect(m_rcMove,false);
		}
		else if (m_toolZoom && m_rcPaint.PtInRect(point)) {
			Zoomat(true, point);
		}
		else if (m_rcGearOn.PtInRect(point)) {
			CPoint posat = GetAbsPosAt(m_rcPaint.CenterPoint() + GetScrollOff());
			int x = point.x - m_rcGearOn.left;
			m_fZoom = tp2z(x);
			m_trickPos = z2tp(m_fZoom);
			InvalidateRect(m_rcGear,false);
			LayoutClient();
			SetCenterAt(posat, m_rcPaint.CenterPoint());
			InvalidateRect(m_rcPaint,false);
		}
		else if (!m_toolZoom && m_rcPaint.PtInRect(point)) {
			m_ptBegin = point;
			m_ptScrollFrm = CPoint(m_siH.nPos, m_siV.nPos);
			m_fDrag = true;

			SetCapture();
		}
		else if (m_rcPrev.PtInRect(point)) {
			if (m_iPage > 0) {
				m_iPage--;
				LayoutClient();
				InvalidateRect(m_rcPaint,false);
				InvalidateRect(m_rcDisp,false);
			}
		}
		else if (m_rcNext.PtInRect(point)) {
			if (m_iPage +1 < CntPages()) {
				m_iPage++;
				LayoutClient();
				InvalidateRect(m_rcPaint,false);
				InvalidateRect(m_rcDisp,false);
			}
		}
		else if (m_rcAbout.PtInRect(point)) {
			AfxGetApp()->OnCmdMsg(ID_APP_ABOUT, 0, NULL, NULL);
		}
	}

	CView::OnLButtonDown(nFlags, point);
}

void CAxTIF3View::Zoomat(bool fIn, CPoint mouseat) {
	CPoint clientpt = mouseat;
	CPoint posat = GetAbsPosAt(GetScrollOff() + clientpt);
	if (fIn) {
		m_fZoom = min(16.0f, m_fZoom * 2);
	}
	else {
		m_fZoom = max(0.0625f, m_fZoom / 2);
	}
	m_trickPos = z2tp(m_fZoom);
	InvalidateRect(m_rcGear,false);
	LayoutClient();
	SetCenterAt(posat, clientpt);
	InvalidateRect(m_rcPaint,false);
}

void CAxTIF3View::LayoutClient() {
	CRect rcH;
	m_sbH.GetWindowRect(rcH);
	CRect rcV;
	m_sbV.GetWindowRect(rcV);

	CRect rc;
	GetClientRect(&rc);
	int curx=0;
	m_rcGlass.left = 0;
	m_rcGlass.bottom = rc.bottom;
	m_rcGlass.right = curx = m_rcGlass.left + 24;
	m_rcGlass.top = m_rcGlass.bottom - cyBar;

	m_rcMove.left = curx;
	m_rcMove.bottom = rc.bottom;
	m_rcMove.right = curx = m_rcMove.left + 24;
	m_rcMove.top = m_rcMove.bottom - cyBar;

	m_rcGear.left = curx;
	m_rcGear.bottom = rc.bottom;
	m_rcGear.right = curx = m_rcGear.left + cxBMGear;
	m_rcGear.top = m_rcGear.bottom - cyBar;

	m_rcGearOn = m_rcGear;
	m_rcGearOn.left += 7;
	m_rcGearOn.right -= 7;

	m_rcPrev.left = curx;
	m_rcPrev.bottom = rc.bottom;
	m_rcPrev.right = curx = m_rcPrev.left + cxBMPrev;
	m_rcPrev.top = rc.bottom - cyBar;

	m_rcNext.left = curx;
	m_rcNext.bottom = rc.bottom;
	m_rcNext.right = curx = m_rcNext.left + cxBMNext;
	m_rcNext.top = rc.bottom - cyBar;

	m_rcDisp.left = curx ;
	m_rcDisp.bottom = rc.bottom;
	m_rcDisp.right = curx = m_rcDisp.left + 50;
	m_rcDisp.top = rc.bottom - cyBar;

	m_rcAbout.left = curx;
	m_rcAbout.bottom = rc.bottom;
	m_rcAbout.right = m_rcAbout.left + 24;
	m_rcAbout.top = rc.bottom - cyBar;

	rc.bottom -= cyBar;

	m_sbH.MoveWindow(rc.left, rc.bottom - rcH.Height(), rc.right - rc.left - rcV.Width(), rcH.Height());
	m_sbV.MoveWindow(rc.right - rc.left - rcV.Width(), rc.top, rcV.Width(), rc.bottom - rc.top - rcH.Height());

	rc.right -= rcV.Width();
	rc.bottom -= rcH.Height();

	m_rcPaint = rc;

	CSize size(0, 0);
	CxImage *p = GetPic();
	if (p != NULL)
		size = CSize(p->GetWidth(), p->GetHeight());

	int cxpic = (int)(size.cx * m_fZoom);
	int cypic = (int)(size.cy * m_fZoom);

	m_siH.fMask = SIF_PAGE|SIF_RANGE|SIF_DISABLENOSCROLL|SIF_POS;
	m_siH.nMin = 0;
	m_siH.nMax = cxpic -1;
	m_siH.nPage = m_rcPaint.Width();
	m_siH.nPos = Newxp(m_siH.nPos);
	m_sbH.SetScrollInfo(&m_siH);
	//m_sbH.EnableScrollBar((m_siH.nMax <= (int)m_siH.nPage) ? ESB_DISABLE_BOTH : 0);

	m_siV.fMask = SIF_PAGE|SIF_RANGE|SIF_DISABLENOSCROLL|SIF_POS;
	m_siV.nMin = 0;
	m_siV.nMax = cypic -1;
	m_siV.nPage = m_rcPaint.Height();
	m_siV.nPos = Newyp(m_siV.nPos);
	m_sbV.SetScrollInfo(&m_siV);
	//m_sbV.EnableScrollBar((m_siV.nMax <= (int)m_siV.nPage) ? ESB_DISABLE_BOTH : 0);
}

void CAxTIF3View::OnLButtonDblClk(UINT nFlags, CPoint point)
{
	CAxTIF3View::OnLButtonDown(nFlags, point);
//
//	CView::OnLButtonDblClk(nFlags, point);
}

void CAxTIF3View::OnRButtonDblClk(UINT nFlags, CPoint point)
{
	CAxTIF3View::OnRButtonDown(nFlags, point);
//
//	CView::OnRButtonDblClk(nFlags, point);
}

CPoint CAxTIF3View::GetCenterPos() const {
	CPoint pt = m_rcPaint.CenterPoint();
	return CPoint(
		(int)(pt.x / m_fZoom),
		(int)(pt.y / m_fZoom)
		);
}

CPoint CAxTIF3View::GetAbsPosAt(CPoint pt) const {
	return CPoint(
		(int)(pt.x / m_fZoom),
		(int)(pt.y / m_fZoom)
		);
}

void CAxTIF3View::SetCenterAt(CPoint pt, CPoint clientpt) {
	{
		int xp = Newxp((int)(pt.x * m_fZoom - m_rcPaint.Width()/2 + (m_rcPaint.Width()/2 - clientpt.x) ));
		if (xp != m_siH.nPos)
			m_sbH.SetScrollPos(m_siH.nPos = xp);
	}
	{
		int yp = Newyp((int)(pt.y * m_fZoom - m_rcPaint.Height()/2 + (m_rcPaint.Height()/2 - clientpt.y) ));
		if (yp != m_siV.nPos)
			m_sbV.SetScrollPos(m_siV.nPos = yp);
	}
}

void CAxTIF3View::OnLButtonUp(UINT nFlags, CPoint point)
{
	if (m_fDrag) {
		m_fDrag = false;
		if (GetCapture() == this)
			ReleaseCapture();
	}

	CView::OnLButtonUp(nFlags, point);
}

void CAxTIF3View::OnMouseMove(UINT nFlags, CPoint point)
{
	if (m_fDrag && 0 != (nFlags & MK_LBUTTON) && GetCapture() == this) {
		CPoint pt = m_ptScrollFrm + m_ptBegin - point;

		bool moved = false;

		int xp = Newxp(pt.x);
		if (xp != m_siH.nPos)
			m_sbH.SetScrollPos(m_siH.nPos = xp), moved=true;

		int yp = Newyp(pt.y);
		if (yp != m_siV.nPos)
			m_sbV.SetScrollPos(m_siV.nPos = yp), moved=true;

		if (moved)
			InvalidateRect(m_rcPaint,false);
	}

	CView::OnMouseMove(nFlags, point);
}

int CAxTIF3View::CntPages() {
	return GetDocument()->m_tifs.GetCount();
}

BOOL CAxTIF3View::OnEraseBkgnd(CDC* pDC) {
	return 1;
//	return CView::OnEraseBkgnd(pDC);
}

BOOL CAxTIF3View::OnMouseWheel(UINT nFlags, short zDelta, CPoint pt) {
	{
		int oldpos = m_sbV.GetScrollPos();
		int newpos = oldpos;

		newpos = Newyp(newpos -zDelta);

		if (oldpos != newpos) {
			m_siV.nPos = newpos;
			m_sbV.SetScrollPos(newpos);
			this->InvalidateRect(m_rcPaint, false);
			return true;
		}
	}

	return CView::OnMouseWheel(nFlags, zDelta, pt);
}

// http://msdn.microsoft.com/en-us/library/ms645614(VS.85).aspx
LRESULT CAxTIF3View::OnMouseHWheel(WPARAM wp, LPARAM) {
	short zDelta = HIWORD(wp);
	{
		int oldpos = m_sbH.GetScrollPos();
		int newpos = oldpos;

		newpos = Newxp(newpos +zDelta);

		if (oldpos != newpos) {
			m_siH.nPos = newpos;
			m_sbH.SetScrollPos(newpos);
			this->InvalidateRect(m_rcPaint, false);
			return 1;
		}
	}
	return 0;
}

int CAxTIF3View::OnMouseActivate(CWnd* pDesktopWnd, UINT nHitTest, UINT message) {
	return CView::OnMouseActivate(pDesktopWnd, nHitTest, message);
}
