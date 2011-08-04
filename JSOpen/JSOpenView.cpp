// JSOpenView.cpp : CJSOpenView �N���X�̎���
//

#include "stdafx.h"
#include "JSOpen.h"

#include "JSOpenDoc.h"
#include "JSOpenView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CJSOpenView

IMPLEMENT_DYNCREATE(CJSOpenView, CHtmlView)

BEGIN_MESSAGE_MAP(CJSOpenView, CHtmlView)
	ON_COMMAND(ID_FILE_WINDOW_OPEN, &CJSOpenView::OnFileWindowOpen)
	ON_COMMAND(ID_FILE_FIX1, &CJSOpenView::OnFileFix1)
END_MESSAGE_MAP()

// CJSOpenView �R���X�g���N�V����/�f�X�g���N�V����

CJSOpenView::CJSOpenView()
{
	// TODO: �\�z�R�[�h�������ɒǉ����܂��B

}

CJSOpenView::~CJSOpenView()
{
}

BOOL CJSOpenView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: ���̈ʒu�� CREATESTRUCT cs ���C������ Window �N���X�܂��̓X�^�C����
	//  �C�����Ă��������B

	return CHtmlView::PreCreateWindow(cs);
}

void CJSOpenView::OnInitialUpdate()
{
	CHtmlView::OnInitialUpdate();

	Navigate2(_T("http://typhoon.yahoo.co.jp/weather/jp/typhoon/typha.html"),NULL,NULL);
}


// CJSOpenView �f�f

#ifdef _DEBUG
void CJSOpenView::AssertValid() const
{
	CHtmlView::AssertValid();
}

void CJSOpenView::Dump(CDumpContext& dc) const
{
	CHtmlView::Dump(dc);
}

CJSOpenDoc* CJSOpenView::GetDocument() const // �f�o�b�O�ȊO�̃o�[�W�����̓C�����C���ł��B
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CJSOpenDoc)));
	return (CJSOpenDoc*)m_pDocument;
}
#endif //_DEBUG


// CJSOpenView ���b�Z�[�W �n���h��

void CJSOpenView::OnNewWindow2(LPDISPATCH* ppDisp, BOOL* Cancel)
{
	// TODO: �����ɓ���ȃR�[�h��ǉ����邩�A�������͊�{�N���X���Ăяo���Ă��������B

	CHtmlView::OnNewWindow2(ppDisp, Cancel);
}

_COM_SMARTPTR_TYPEDEF(IHTMLWindow2, __uuidof(IHTMLWindow2));

void CJSOpenView::OnFileWindowOpen() {
	HRESULT hr;
	CString str;
	try {
		CComQIPtr<IHTMLDocument2> document = GetHtmlDocument();
		if (document != NULL) {
			IHTMLWindow2Ptr window;
			document->get_parentWindow(&window);
			if (window != NULL) {
				CComBSTR url;
				document->get_URL(&url);

				CComPtr<IHTMLWindow2> newwin;
				if (FAILED(hr = window->open(url, CComBSTR(L"_blank"), CComBSTR(), false, &newwin))) {
					_com_issue_errorex(hr, window, IID_IHTMLWindow2);
				}
				printf("");
			}
		}
	}
	catch (_com_error err) {
		AfxMessageBox(CString(err.ErrorMessage()), MB_ICONEXCLAMATION);
	}
}

void CJSOpenView::OnFileFix1()
{
	ShellExecute(*this, _T("open"), _T("B8DA6310-E19B-11D0-933C-00A0C90DCAA9.reg"), NULL, NULL, SW_SHOW);
}
