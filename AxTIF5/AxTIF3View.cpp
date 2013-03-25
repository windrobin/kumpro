// AxTIF3View.cpp : CAxTIF3View クラスの実装
//

#include "stdafx.h"
#include "AxTIF3.h"

#include "AxTIF3Doc.h"
#include "AxTIF3View.h"
#include "RUt.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

const int cxBMPrev = 32;
const int cxBMNext = 32;

const int cxBMGear = 254;
const int cyBar = 24;

// CAxTIF3View

IMPLEMENT_DYNAMIC(CAxTIF3View, CView)

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
	ON_COMMAND_RANGE(IDC_P6, IDC_MOVE, OnSelCmd)
	ON_UPDATE_COMMAND_UI_RANGE(IDC_MAG, IDC_MOVE, OnUpdateSelCmd)
END_MESSAGE_MAP()

// CAxTIF3View コンストラクション/デストラクション

CAxTIF3View::CAxTIF3View()
: m_ddcompat(0), m_slowzoom(15)
{
	RUt::GetInt(_T("HIRAOKA HYPERS TOOLS, Inc."), _T("AxTIF5"), _T("ddcompat"), reinterpret_cast<DWORD &>(m_ddcompat), 0);
	RUt::GetInt(_T("HIRAOKA HYPERS TOOLS, Inc."), _T("AxTIF5"), _T("slowzoom"), reinterpret_cast<DWORD &>(m_slowzoom), 15);
}

CAxTIF3View::~CAxTIF3View()
{
}

BOOL CAxTIF3View::PreCreateWindow(CREATESTRUCT& cs)
{
	if (!CView::PreCreateWindow(cs))
		return false;

	cs.style |= WS_CLIPCHILDREN|WS_CLIPSIBLINGS;
	cs.dwExStyle &= ~WS_EX_CLIENTEDGE;
	return true;
}

// CAxTIF3View 描画

inline int RUTo4(int v) {
	return (v+3)&(~3);
}

inline BYTE Lerp255(BYTE v0, BYTE v1, int f) {
	if (v0 == v1 || f == 0)
		return v0;
	return v0 + (v1 - v0) / 255 * f;
}

typedef struct BI256 {
	BITMAPINFOHEADER bmiHeader;
	RGBQUAD bmiColors[256];
}	BI256;

void CAxTIF3View::OnDraw(CDC* pDC)
{
	CAxTIF3Doc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	CxImage *p = GetPic();
	if (p != NULL && pDC->RectVisible(m_rcPaint)) {
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

		ASSERT(p != NULL);
		bool fSmallMono = p->GetBpp() == 1 && ULONG(size.cx) < p->GetWidth() && ULONG(size.cy) < p->GetHeight();
		if (fSmallMono && m_slowzoom == 10) {
			BI256 bi;
			bi.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
			bi.bmiHeader.biWidth = cx;
			bi.bmiHeader.biHeight = cy;
			bi.bmiHeader.biPlanes = 1;
			bi.bmiHeader.biBitCount = 8;
			bi.bmiHeader.biCompression = BI_RGB;
			bi.bmiHeader.biClrUsed = 256;
			bi.bmiHeader.biClrImportant = 256;
			RGBQUAD c0 = p->GetPaletteColor(0);
			RGBQUAD c1 = p->GetPaletteColor(1);
			for (int c=0; c<256; c++) {
				bi.bmiColors[c].rgbReserved = 0;
				bi.bmiColors[c].rgbRed   = Lerp255(c0.rgbRed  , c1.rgbRed  , c);
				bi.bmiColors[c].rgbGreen = Lerp255(c0.rgbGreen, c1.rgbGreen, c);
				bi.bmiColors[c].rgbBlue  = Lerp255(c0.rgbBlue , c1.rgbBlue , c);
			}

			const int ocx = p->GetWidth();
			const int ocy = p->GetHeight();

			CRect rcDrawBox(
				xp,
				yp,
				xp +cx,
				yp +cy
				);

			CRect rcClip = m_rcPaint;
			if (pDC->IsKindOf(RUNTIME_CLASS(CPaintDC))) {
				CRect rcNeedPaint = STATIC_DOWNCAST(CPaintDC, pDC)->m_ps.rcPaint;
				if (!rcClip.IntersectRect(rcClip, &rcNeedPaint)) {
					rcClip.SetRectEmpty();
				}
			}

			const int ox = std::max<int>(0, rcClip.left -rcDrawBox.left);
			const int oy = std::max<int>(0, rcClip.top -rcDrawBox.top);
			const int ox1 = std::min<int>(cx, ox +rcClip.Width());
			const int oy1 = std::min<int>(cy, oy +rcClip.Height());

			CByteArray bits;
			int pitchSrc = RUTo4(cx);
			bits.SetSize(pitchSrc * cy);
			CUIntArray vecx;
			for (int x=0; x<cx; x++) vecx.Add(int(x / (float)cx * ocx));
			vecx.Add(ocx);
			CUIntArray vecy;
			for (int y=0; y<cy; y++) vecy.Add(int(y / (float)cy * ocy));
			vecy.Add(ocy);

			const int basx = vecx[ox];

			CUIntArray veca, vecc;
			veca.SetSize(cx);
			vecc.SetSize(cx);

			int cntpix = 0;

			for (int y=oy; y<oy1; y++) {
				int vy0 = vecy[y];
				int vy1 = vecy[y +1];
				for (; vy0<vy1; vy0++) {
					BYTE *pbSrc = p->GetBits(ocy -vy0 -1) +(basx >> 3);

					BYTE b = *pbSrc; pbSrc++;
					BYTE rest = 8 -(basx & 7);
					b <<= basx & 7;

					for (int x=ox; x<ox1; x++) {
						int vx0 = vecx[x];
						int vx1 = vecx[x +1];
						UINT va = 0, vc = 0;
						for (; vx0<vx1; vx0++) {
							if (rest == 0) {
								b = *pbSrc; pbSrc++;
								rest = 8;
							}

							if (b & 128) {
								++va;
							}
							++vc;

							b <<= 1;
							--rest;

							cntpix++;
						}
						veca[x] = va;
						vecc[x] = vc;
					}
				}
				BYTE *pbDst = bits.GetData() +pitchSrc * (cy -y -1) +ox;
				for (int x=ox; x<ox1; x++) {
					UINT vc = vecc[x];
					if (vc == 0) vc = 1;
					*pbDst = veca[x] * 255 / vc;
					pbDst++;
				}
			}
			_RPT1(_CRT_WARN, "# %d \n", cntpix);
			int r = SetDIBitsToDevice(
				pDC->GetSafeHdc(),
				xp,
				yp,
				cx,
				cy,
				0,
				0,
				0,
				cy,
				bits.GetData(),
				reinterpret_cast<PBITMAPINFO>(&bi),
				DIB_RGB_COLORS
				);
			printf("");
		}
		else if (fSmallMono && m_slowzoom == 20) {
			p->Draw(pDC->GetSafeHdc(), xp, yp, cx, cy, m_rcPaint, true);
		}
		else if (fSmallMono && m_slowzoom == 15) {
			int strMode = pDC->SetStretchBltMode(HALFTONE);
			CPoint pt = pDC->SetBrushOrg(xp, yp);

			int state = pDC->SaveDC();

			StretchDIBits(
				pDC->GetSafeHdc(),
				xp,
				yp,
				cx,
				cy,
				0,
				0,
				p->GetWidth(),
				p->GetHeight(),
				p->GetBits(),
				reinterpret_cast<BITMAPINFO *>(p->GetDIB()),
				DIB_RGB_COLORS,
				SRCCOPY
				);

			pDC->IntersectClipRect(m_rcPaint);

			pDC->RestoreDC(state);

			pDC->SetBrushOrg(pt);
			pDC->SetStretchBltMode(strMode);
		}
		else {
			p->Draw(pDC->GetSafeHdc(), xp, yp, cx, cy, m_rcPaint);
		}

		CRect rcBox(xp, yp, xp+cx, yp+cy);
		if (rcBox.IntersectRect(&rcBox, m_rcPaint)) {
			pDC->ExcludeClipRect(rcBox);
		}

		CBrush br;
		br.CreateSysColorBrush(COLOR_BTNSHADOW);
		pDC->FillRect(m_rcPaint, &br);

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
	if (pDC->RectVisible(m_rcFitWH)) {
		CDC dc;
		dc.CreateCompatibleDC(pDC);
		CBitmap* pOrg = dc.SelectObject(&m_bmFitWH);
		pDC->BitBlt(m_rcFitWH.left, m_rcFitWH.top, m_rcFitWH.Width(), m_rcFitWH.Height(), &dc, 0, 0, SRCCOPY);
		if (m_fit == FitWH) pDC->InvertRect(m_rcFitWH);
		dc.SelectObject(pOrg);
		pDC->ExcludeClipRect(m_rcFitWH);
	}
	if (pDC->RectVisible(m_rcFitW)) {
		CDC dc;
		dc.CreateCompatibleDC(pDC);
		CBitmap* pOrg = dc.SelectObject(&m_bmFitW);
		pDC->BitBlt(m_rcFitW.left, m_rcFitW.top, m_rcFitW.Width(), m_rcFitW.Height(), &dc, 0, 0, SRCCOPY);
		if (m_fit == FitW) pDC->InvertRect(m_rcFitW);
		dc.SelectObject(pOrg);
		pDC->ExcludeClipRect(m_rcFitW);
	}
	if (pDC->RectVisible(m_rcGear)) {
		CDC dc;
		dc.CreateCompatibleDC(pDC);
		CBitmap* pOrg = dc.SelectObject(&m_bmGear);
		pDC->BitBlt(m_rcGear.left, m_rcGear.top, m_rcGear.Width(), m_rcGear.Height(), &dc, 0, 0, SRCCOPY);
		dc.SelectObject(m_bmTrick);
		pDC->BitBlt(m_rcGear.left + 7 + GetTrickPos() - 7/2, m_rcGear.top + 7 - 12/2 + (cyBar-16)/2, 7, 12, &dc, 0, 0, SRCCOPY);
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
	if (pDC->RectVisible(m_rcMMSel)) {
		CDC dc;
		dc.CreateCompatibleDC(pDC);
		CBitmap* pOrg = dc.SelectObject(m_toolZoom ? &m_bmMagBtn : &m_bmMoveBtn);
		pDC->BitBlt(m_rcMMSel.left, m_rcMMSel.top, m_rcMMSel.Width(), m_rcMMSel.Height(), &dc, 0, 0, SRCCOPY);
		dc.SelectObject(pOrg);
		pDC->ExcludeClipRect(m_rcMMSel);
	}
	if (pDC->RectVisible(m_rcZoomVal)) {
		CDC dc;
		dc.CreateCompatibleDC(pDC);
		CBitmap* pOrg = dc.SelectObject(&m_bmZoomVal);
		pDC->BitBlt(m_rcZoomVal.left, m_rcZoomVal.top, m_rcZoomVal.Width(), m_rcZoomVal.Height(), &dc, 0, 0, SRCCOPY);
		dc.SelectObject(pOrg);

		pDC->SelectStockObject(DEFAULT_GUI_FONT);
		CString str;
		str.Format(_T("%u %%"), (int)(100 * Getzf()));

		//COLORREF lastBkClr = pDC->SetBkColor(GetSysColor(COLOR_WINDOW));
		UINT lastMode = pDC->SetTextAlign(TA_CENTER|TA_TOP);
		COLORREF lastTextClr = pDC->SetTextColor(RGB(0, 0, 0));
		CSize size =  pDC->GetTextExtent(str);
		CPoint pt = m_rcZoomVal.CenterPoint() - CPoint(0, size.cy / 2);
		pDC->ExtTextOut(pt.x, pt.y, ETO_CLIPPED, m_rcZoomVal, str, str.GetLength(), NULL);
		pDC->SetTextColor(lastTextClr);
		pDC->SetTextAlign(lastMode);
		//pDC->SetBkColor(lastBkClr);

		pDC->ExcludeClipRect(m_rcZoomVal);
	}


	{
		CBrush br;
		br.CreateSysColorBrush(COLOR_WINDOW);
		CRect rc;
		GetClientRect(rc); //pDC->GetClipBox(rc);
		pDC->FillRect(rc, &br);
	}
}


CxImage *CAxTIF3View::getPic(int frame) const {
	if (frame < 0)
		frame = m_iPage;
	CAxTIF3Doc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (pDoc != NULL) {
		size_t cx = pDoc->m_tifs.GetCount();
		if ((size_t)frame < cx)
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
		|| !m_bmFitWH.LoadBitmap(IDB_FITWH)
		|| !m_bmFitW.LoadBitmap(IDB_FITW)
		|| !m_bmMagBtn.LoadBitmap(IDB_MAGBTN)
		|| !m_bmMoveBtn.LoadBitmap(IDB_MOVEBTN)
		|| !m_bmZoomVal.LoadBitmap(IDB_ZOOMVAL)
		)
		return -1;

	m_fZoom = 1;
	ZeroMemory(&m_siH, sizeof(m_siH));
	m_siH.cbSize = sizeof(m_siH);
	ZeroMemory(&m_siV, sizeof(m_siV));
	m_siV.cbSize = sizeof(m_siV);
	m_toolZoom = true;
	m_fDrag = false;
	m_iPage = 0;
	m_fit = FitWH;
	m_ptClip = CPoint(0, 0);

	LayoutClient();

	return 0;
}

void CAxTIF3View::OnSize(UINT nType, int cx, int cy)
{
	CView::OnSize(nType, cx, cy);

	DoFit();
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
			this->ScrollWindowEx(-(newpos -oldpos), 0, m_rcPaint, m_rcPaint, NULL, NULL, SW_INVALIDATE);
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
			this->ScrollWindowEx(0, -(newpos -oldpos), m_rcPaint, m_rcPaint, NULL, NULL, SW_INVALIDATE);
			return;
		}
		return;
	}

	CView::OnVScroll(nSBCode, nPos, pScrollBar);
}

void CAxTIF3View::OnRButtonDown(UINT nFlags, CPoint point) {
	if (GetFocus() == this) {
		if (m_toolZoom && m_rcPaint.PtInRect(point)) {
			bool fControl = 0 != (MK_CONTROL & nFlags);
			if (fControl) {
				SetFit(FitWH);

				DoFit();
				LayoutClient();
				InvalidateRect(m_rcPaint,false);
			}
			else {
				Zoomat(false, point);
			}
			return;
		}
	}

	CView::OnRButtonDown(nFlags, point);
}

#define SW2(val, A, B) ((val == (A)) ? (B) : (A))

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
		else if (m_rcFitWH.PtInRect(point)) {
			SetFit(SW2(m_fit, FitWH, FitNo));

			DoFit();
			LayoutClient();
			InvalidateRect(m_rcPaint,false);
		}
		else if (m_rcFitW.PtInRect(point)) {
			SetFit(SW2(m_fit, FitW, FitNo));

			DoFit();
			LayoutClient();
			InvalidateRect(m_rcPaint,false);
		}
		else if (m_toolZoom && m_rcPaint.PtInRect(point)) {
			bool fControl = 0 != (MK_CONTROL & nFlags);
			bool fShift = 0 != (MK_SHIFT & nFlags);
			if (fControl) {
				Zoomat2(point);
			}
			else {
				Zoomat(fShift ? false : true, point);
			}
		}
		else if (m_rcGearOn.PtInRect(point)) {
			int x = point.x - m_rcGearOn.left;
			SetzoomR(tp2z(x));
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
		else if (m_rcMMSel.PtInRect(point)) {
			CMenu aSel;
			aSel.CreatePopupMenu();
			aSel.AppendMenu(MF_BITMAP, IDC_MAG, &m_bmMag);
			aSel.AppendMenu(MF_BITMAP, IDC_MOVE, &m_bmMove);
			CPoint ptMenu = m_rcMMSel.TopLeft();
			ClientToScreen(&ptMenu);
			aSel.TrackPopupMenu(TPM_LEFTALIGN, ptMenu.x, ptMenu.y, GetParentFrame());
		}
		else if (m_rcZoomVal.PtInRect(point)) {
			CMenu aSel;
			aSel.LoadMenu(IDR_MENU_P);
			CMenu *pMenu = aSel.GetSubMenu(0);
			CPoint ptMenu = m_rcZoomVal.TopLeft();
			ClientToScreen(&ptMenu);
			if (pMenu != NULL)
				pMenu->TrackPopupMenu(TPM_LEFTALIGN, ptMenu.x, ptMenu.y, GetParentFrame());
		}
	}

	CView::OnLButtonDown(nFlags, point);
}

void CAxTIF3View::SetzoomR(float zf) {
	CPoint posat = GetAbsPosAt(m_rcPaint.CenterPoint() + GetScrollOff());
	Setzf(zf);
	LayoutClient();
	SetCenterAt(posat, m_rcPaint.CenterPoint());
	InvalidateRect(m_rcPaint,false);
}

void CAxTIF3View::Zoomat(bool fIn, CPoint mouseat) {
	float zf = Getzf();
	ZoomatR(fIn ? zf * 2 : zf / 2, mouseat);
}

void CAxTIF3View::ZoomatR(float zf, CPoint mouseat) {
	CPoint clientpt = mouseat;
	CPoint dispat = GetZoomedDispRect().TopLeft();
	CPoint posat = GetAbsPosAt(clientpt - dispat);
	Setzf(max(0.0625f, min(16.0f, zf)));
	LayoutClient();
	SetCenterAt(posat, clientpt);
	InvalidateRect(m_rcPaint,false);
}

void CAxTIF3View::Zoomat2(CPoint mouseat) {
	CPoint clientpt = mouseat;
	CRect rcDisp = GetZoomedDispRect();
	CPoint dispat = rcDisp.TopLeft();
	CSize dispSize = rcDisp.Size();
	CPoint posat = GetAbsPosAt(clientpt - dispat);
	float curzf = Getzf();
	float fxl = (curzf * float(clientpt.x) / std::max<int>(clientpt.x - dispat.x, 1));
	float fxr = (curzf * float(m_rcPaint.Width() - clientpt.x) / std::max<int>(m_rcPaint.Width() - clientpt.x - dispat.x, 1));
	float fyt = (curzf * float(clientpt.y) / std::max<int>(clientpt.y - dispat.y, 1));
	float fyb = (curzf * float(m_rcPaint.Height() - clientpt.y) / std::max<int>(m_rcPaint.Height() - clientpt.y - dispat.y, 1));
	float zf = curzf;
	zf = max(zf, fxl);
	zf = max(zf, fxr);
	zf = max(zf, fyt);
	zf = max(zf, fyb);
	if (fabs(zf - curzf) < 0.001f) {
		zf *= 1.5f;
	}

	Setzf(max(0.0625f, min(16.0f, zf)));
	LayoutClient();
	SetCenterAt(posat, clientpt);
	InvalidateRect(m_rcPaint,false);
}

void CAxTIF3View::LayoutClient() {
	CRect rcH;
	m_sbH.GetWindowRect(rcH);
	CRect rcV;
	m_sbV.GetWindowRect(rcV);

	{
		m_rcGlass = 
		m_rcMove = 
		m_rcFitW = 
		m_rcFitWH =
		m_rcGear =
		m_rcGearOn =
		m_rcPrev =
		m_rcNext =
		m_rcDisp =
		m_rcAbout = 
		m_rcMMSel =
		m_rcZoomVal =
			CRect();
	}
	CRect rc;
	GetClientRect(&rc);
	rc.OffsetRect(m_ptClip);
	int curx=0;
	if (m_ddcompat == 0) {
		m_rcGlass.left = 0;
		m_rcGlass.bottom = rc.bottom;
		m_rcGlass.right = (curx += 24);
		m_rcGlass.top = m_rcGlass.bottom - cyBar;

		m_rcMove.left = curx;
		m_rcMove.bottom = rc.bottom;
		m_rcMove.right = (curx += 24);
		m_rcMove.top = m_rcMove.bottom - cyBar;

		m_rcFitW.left = curx;
		m_rcFitW.bottom = rc.bottom;
		m_rcFitW.right = (curx += 24);
		m_rcFitW.top = m_rcFitW.bottom - cyBar;

		m_rcFitWH.left = curx;
		m_rcFitWH.bottom = rc.bottom;
		m_rcFitWH.right = (curx += 24);
		m_rcFitWH.top = m_rcFitWH.bottom - cyBar;

		m_rcGear.left = curx;
		m_rcGear.bottom = rc.bottom;
		m_rcGear.right = (curx += cxBMGear);
		m_rcGear.top = m_rcGear.bottom - cyBar;

		m_rcGearOn = m_rcGear;
		m_rcGearOn.left += 7;
		m_rcGearOn.right -= 7;

		m_rcPrev.left = curx;
		m_rcPrev.bottom = rc.bottom;
		m_rcPrev.right = curx = (curx += cxBMPrev);
		m_rcPrev.top = rc.bottom - cyBar;

		m_rcNext.left = curx;
		m_rcNext.bottom = rc.bottom;
		m_rcNext.right = curx = (curx += cxBMNext);
		m_rcNext.top = rc.bottom - cyBar;

		m_rcDisp.left = curx ;
		m_rcDisp.bottom = rc.bottom;
		m_rcDisp.right = curx = (curx += 50);
		m_rcDisp.top = rc.bottom - cyBar;

		m_rcAbout.left = curx;
		m_rcAbout.bottom = rc.bottom;
		m_rcAbout.right = (curx += 24);
		m_rcAbout.top = rc.bottom - cyBar;
	}
	else {
		m_rcMMSel.left = curx;
		m_rcMMSel.bottom = rc.bottom;
		m_rcMMSel.right = (curx += 24);
		m_rcMMSel.top = rc.bottom - cyBar;

		m_rcZoomVal.left = curx;
		m_rcZoomVal.bottom = rc.bottom;
		m_rcZoomVal.right = (curx += 48);
		m_rcZoomVal.top = rc.bottom - cyBar;

		m_rcPrev.left = curx;
		m_rcPrev.bottom = rc.bottom;
		m_rcPrev.right = curx = (curx += cxBMPrev);
		m_rcPrev.top = rc.bottom - cyBar;

		m_rcDisp.left = curx ;
		m_rcDisp.bottom = rc.bottom;
		m_rcDisp.right = curx = (curx += 50);
		m_rcDisp.top = rc.bottom - cyBar;

		m_rcNext.left = curx;
		m_rcNext.bottom = rc.bottom;
		m_rcNext.right = curx = (curx += cxBMNext);
		m_rcNext.top = rc.bottom - cyBar;

		m_rcAbout.left = curx;
		m_rcAbout.bottom = rc.bottom;
		m_rcAbout.right = (curx += 24);
		m_rcAbout.top = rc.bottom - cyBar;
	}

	rc.bottom -= cyBar;

	m_sbH.MoveWindow(rc.left, rc.bottom - rcH.Height(), rc.right - rc.left - rcV.Width(), rcH.Height());
	m_sbV.MoveWindow(rc.right - rc.left - rcV.Width(), rc.top, rcV.Width(), rc.bottom - rc.top - rcH.Height());

	rc.right -= rcV.Width();
	rc.bottom -= rcH.Height();

	m_rcPaint = rc;

	CSize size = GetZoomedSize();

	int cxpic = std::max<int>(1, (int)(size.cx));
	int cypic = std::max<int>(1, (int)(size.cy));

	m_siH.fMask = SIF_PAGE|SIF_RANGE|SIF_DISABLENOSCROLL|SIF_POS;
	m_siH.nMin = 0;
	m_siH.nMax = cxpic -1;
	m_siH.nPage = m_rcPaint.Width();
	m_siH.nPos = Newxp(m_siH.nPos);
	m_sbH.SetScrollInfo(&m_siH);
	m_sbH.EnableScrollBar((m_siH.nMax <= (int)m_siH.nPage) ? ESB_DISABLE_BOTH : 0);

	m_siV.fMask = SIF_PAGE|SIF_RANGE|SIF_DISABLENOSCROLL|SIF_POS;
	m_siV.nMin = 0;
	m_siV.nMax = cypic -1;
	m_siV.nPage = m_rcPaint.Height();
	m_siV.nPos = Newyp(m_siV.nPos);
	m_sbV.SetScrollInfo(&m_siV);
	m_sbV.EnableScrollBar((m_siV.nMax <= (int)m_siV.nPage) ? ESB_DISABLE_BOTH : 0);
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
	float zf = Getzf();
	return CPoint(
		(int)(pt.x / zf),
		(int)(pt.y / zf)
		);
}

CPoint CAxTIF3View::GetAbsPosAt(CPoint pt) const {
	float zf = Getzf();
	return CPoint(
		(int)(pt.x / zf),
		(int)(pt.y / zf)
		);
}

void CAxTIF3View::SetCenterAt(CPoint pt, CPoint clientpt) {
	float zf = Getzf();
	{
		int xp = Newxp((int)(pt.x * zf - m_rcPaint.Width()/2 + (m_rcPaint.Width()/2 - clientpt.x) ));
		if (xp != m_siH.nPos)
			m_sbH.SetScrollPos(m_siH.nPos = xp);
	}
	{
		int yp = Newyp((int)(pt.y * zf - m_rcPaint.Height()/2 + (m_rcPaint.Height()/2 - clientpt.y) ));
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
	return static_cast<int>(GetDocument()->m_tifs.GetCount());
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

CSize CAxTIF3View::GetZoomedSize() {
	CxImage *p = GetPic();
	if (p != NULL) {
		CSize size = CSize(p->GetWidth(), p->GetHeight());
		int rx = p->GetXDPI();
		int ry = p->GetYDPI();
		if (rx == ry || rx < 1 || ry < 1) {

		}
		else if (rx > ry) {
			size.cx = int(size.cx * (float(ry) / rx));
			//size.cy = int(size.cy * (float(rx) / ry));
		}
		else {
			size.cy = int(size.cy * (float(rx) / ry));
			//size.cx = int(size.cx * (float(ry) / rx));
		}
		switch (m_fit) {
		case FitNo:
			return CSize((int)(size.cx * m_fZoom), (int)(size.cy * m_fZoom));
		case FitW:
			if (m_rcPaint.Width() < size.cx) {
				return CSize(
					m_rcPaint.Width(),
					(size.cx != 0)
						? (int)(size.cy * (m_rcPaint.Width() / (float)size.cx))
						: 0
					);
			}
			return size;
		case FitWH:
			if (m_rcPaint.Width() < size.cx || m_rcPaint.Height() < size.cy) {
				return Fitrect::Fit(m_rcPaint, size).Size();
			}
			return size;
		}
	}
	return CSize(0,0);
}

float CAxTIF3View::Getzf() const {
	const CxImage *p = GetPic();
	if (p != NULL) {
		CSize size = CSize(p->GetWidth(), p->GetHeight());
		switch (m_fit) {
		default:
		case FitNo:
			return m_fZoom;
		case FitW:
			if (m_rcPaint.Width() < size.cx) {
				return (size.cx != 0)
					? (m_rcPaint.Width() / (float)size.cx)
					: 1
					;
			}
			return 1;
		case FitWH:
			if (m_rcPaint.Width() < size.cx || m_rcPaint.Height() < size.cy) {
				CSize newSize = Fitrect::Fit(m_rcPaint, size).Size();
				float fx = (size.cx != 0) ? ((float)newSize.cx / size.cx) : 0;
				float fy = (size.cy != 0) ? ((float)newSize.cy / size.cy) : 0;
				if (fx != 0) return fx;
				if (fy != 0) return fy;
				return 1;
			}
			return 1;
		}
	}
	return 1;
}

void CAxTIF3View::PostNcDestroy() {
	return;
//	CView::PostNcDestroy();
}

void CAxTIF3View::OnSelCmd(UINT nID) {
	switch (nID) {
		case IDC_MAG: m_toolZoom = true; break;
		case IDC_MOVE: m_toolZoom = false; break;

		case IDC_P6   : SetzoomR(0.06f); return;
		case IDC_P12  : SetzoomR(0.12f); return;
		case IDC_P25  : SetzoomR(0.25f); return;
		case IDC_P50  : SetzoomR(0.5f); return;
		case IDC_P100 : SetzoomR(1); return;
		case IDC_P200 : SetzoomR(2); return;
		case IDC_P400 : SetzoomR(4); return;
		case IDC_P800 : SetzoomR(8); return;
		case IDC_P1600: SetzoomR(16); return;

		default: return;
	}
	InvalidateRect(m_rcMMSel,false);
}

void CAxTIF3View::OnUpdateSelCmd(CCmdUI *pUI) {
	switch (pUI->m_nID) {
		case IDC_MAG: pUI->SetCheck(m_toolZoom); break;
		case IDC_MOVE: pUI->SetCheck(!m_toolZoom); break;
	}
}

void CAxTIF3View::OnUpdate(CView* /*pSender*/, LPARAM lHint, CObject* /*pHint*/) {
	switch (lHint) {
		case UPHINT_LOADED:
			LayoutClient();
			break;
	}
}
