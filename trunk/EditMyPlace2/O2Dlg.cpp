
// O2Dlg.cpp : 実装ファイル
//

#include "StdAfx.h"
#include "resource.h"
#include "O2Dlg.h"
#include <algorithm>

// CO2Dlg ダイアログ

IMPLEMENT_DYNAMIC(CO2Dlg, CPropertyPage)

CO2Dlg::CO2Dlg()
	: CPropertyPage(CO2Dlg::IDD)
{

}

CO2Dlg::~CO2Dlg()
{
}

void CO2Dlg::DoDataExchange(CDataExchange* pDX)
{
	CPropertyPage::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_L, m_l);
}


BEGIN_MESSAGE_MAP(CO2Dlg, CPropertyPage)
	ON_NOTIFY(LVN_GETDISPINFO, IDC_L, &CO2Dlg::OnLvnGetdispinfoL)
END_MESSAGE_MAP()


// CO2Dlg メッセージ ハンドラ

static int s_cxIcon = GetSystemMetrics(SM_CXICON);
static int s_cyIcon = GetSystemMetrics(SM_CYICON);

BOOL CO2Dlg::OnInitDialog()
{
	CPropertyPage::OnInitDialog();

	VERIFY(m_il.Create(s_cxIcon, s_cyIcon, ILC_COLOR32|ILC_MASK, 0, 15));

	VERIFY(0 <= m_l.InsertColumn(0, _T("呼称"), LVCFMT_LEFT, 150));
	VERIFY(1 <= m_l.InsertColumn(1, _T("場所"), LVCFMT_LEFT, 200));

	m_l.SetExtendedStyle(
		m_l.GetExtendedStyle()
		| LVS_EX_FULLROWSELECT
		| LVS_EX_GRIDLINES
		| LVS_EX_ONECLICKACTIVATE
		| LVS_EX_UNDERLINEHOT
		| LVS_EX_DOUBLEBUFFER
		| LVS_EX_CHECKBOXES
		);

	m_l.SetImageList(&m_il, LVSIL_SMALL);

	Revert();

	return TRUE;  // return TRUE unless you set the focus to a control
	// 例外 : OCX プロパティ ページは必ず FALSE を返します。
}

void CO2Dlg::Revert() {
	m_oItems.RemoveAll();
	{
		CRegKey rkStd;
		LONG r;
		r = rkStd.Open(
			HKEY_CURRENT_USER,
			_T("Software\\Microsoft\\Office\\11.0\\Common\\Open Find\\Places\\StandardPlaces"),
			KEY_READ
			);
		if (r == 0) {
			ReadStd(rkStd, _T("Desktop"), CSIDL_DESKTOP);
			ReadStd(rkStd, _T("MyComputer"), CSIDL_DRIVES);
			ReadStd(rkStd, _T("MyDocuments"), CSIDL_PERSONAL);
			ReadStd(rkStd, _T("Publishing"), CSIDL_NETWORK);
			ReadStd(rkStd, _T("Recent"), CSIDL_RECENT);
		}
	}
	{
		CRegKey rkUser;
		LONG r;
		r = rkUser.Open(
			HKEY_CURRENT_USER,
			_T("Software\\Microsoft\\Office\\11.0\\Common\\Open Find\\Places\\UserDefinedPlaces"),
			KEY_READ
			);
		if (r == 0) {
			for (DWORD t=0; ; t++) {
				TCHAR tcSubkey[256 +1] = {_T("")};
				DWORD cb = 256;
				r = rkUser.EnumKey(t, tcSubkey, &cb, NULL);
				if (r == 0) {
					tcSubkey[cb] = 0;
					ReadUser(rkUser, tcSubkey);
					continue;
				}
				break;
			}
		}
	}
	Commit2lc();
}

void CO2Dlg::ReadUser(CRegKey &rkUser, LPCTSTR pszSubkey) {
	CRegKey rkSubkey;
	LONG r;
	r = rkSubkey.Open(
		rkUser,
		pszSubkey,
		KEY_READ
		);
	if (r == 0) {
		DWORD iIndex = 0;
		r = rkSubkey.QueryDWORDValue(_T("Index"), iIndex);
		if (r != 0) iIndex = 0;

		DWORD iShow = 1;
		r = rkSubkey.QueryDWORDValue(_T("Show"), iShow);
		if (r != 0) iShow = 1;

		BYTE buffPidl[4096];
		ULONG cbPidl = 4096;
		r = rkSubkey.QueryBinaryValue(_T("Pidl"), buffPidl, &cbPidl);
		if (r == 0) {
			OItem *p = new OItem(iIndex, iShow != 0);
			p->SetPidl(reinterpret_cast<LPITEMIDLIST>(buffPidl), cbPidl);
			m_oItems.Add(p);
		}
	}
}

void CO2Dlg::ReadStd(CRegKey &rkStd, LPCTSTR pszSubkey, int csidl) {
	CRegKey rk;
	LONG r;
	r = rk.Open(
		rkStd,
		pszSubkey,
		KEY_READ
		);
	if (r == 0) {
		DWORD iIndex = 0;
		r = rk.QueryDWORDValue(_T("Index"), iIndex);
		if (r != 0) iIndex = 0;

		DWORD iShow = 1;
		r = rk.QueryDWORDValue(_T("Show"), iShow);
		if (r != 0) iShow = 1;

		OItem *p = new OItem((int)iIndex, iShow != 0);
		p->ResolveDispName(csidl);
		p->AssocImageList(m_il);
		p->SetStdSubkey(pszSubkey);
		m_oItems.Add(p);
	}

	qsort(
		m_oItems.GetAt(0),
		m_oItems.GetCount(),
		4,
		OItem::Sort::byKg_qsort
		);
}

void CO2Dlg::Commit2lc() {
	int c = 0;
	while (m_oItems.GetCount() < m_l.GetItemCount()) {
		VERIFY(m_l.DeleteItem(m_l.GetItemCount() -1));
		c++;
	}
	while (m_oItems.GetCount() > m_l.GetItemCount()) {
		LVITEM lvi;
		ZeroMemory(&lvi, sizeof(lvi));
		lvi.mask = LVIF_IMAGE|LVIF_TEXT;
		lvi.iItem = m_l.GetItemCount();
		lvi.pszText = LPSTR_TEXTCALLBACK;
		lvi.iImage = I_IMAGECALLBACK;

		int r;
		r = m_l.InsertItem(&lvi);
		if (r < 0)
			break;
		c++;
	}
	for (int i = 0; i < m_oItems.GetCount(); i++) {
		if ((0 != m_l.GetCheck(i)) != m_oItems[i]->isVisible) {
			m_l.SetCheck(i, m_oItems[i]->isVisible);
			c++;
		}
	}
	if (c != 0) {
		m_l.Invalidate();
	}
}

void CO2Dlg::OnLvnGetdispinfoL(NMHDR *pNMHDR, LRESULT *pResult) {
	NMLVDISPINFO *p = reinterpret_cast<NMLVDISPINFO*>(pNMHDR);
	int i = p->item.iItem;
	if ((UINT)i < (UINT)m_oItems.GetCount()) {
		switch (p->item.iSubItem) {
			case 0:
				p->item.pszText = const_cast<LPTSTR>(static_cast<LPCTSTR>(m_oItems[i]->dispName));
				break;
			case 1:
				p->item.pszText = _T("...");
				break;
		}
		p->item.iImage = m_oItems[i]->iImage;
	}
	else {
		p->item.pszText = _T("");
		p->item.iImage = -1;
	}
	*pResult = 0;
}
