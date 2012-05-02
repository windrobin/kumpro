// SDDLViewerDlg.cpp : implementation file
//

#include "Stdafx.h"
#include "SDDLViewer.h"
#include "SDDLViewerDlg.h"

#pragma comment(lib, "Version.lib")
#pragma comment(lib, "Aclui.lib")

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
	BOOL OnInitDialog() {
		CDialog::OnInitDialog();

		CString strVer = _T("?");

		{
			HMODULE hMod = AfxGetResourceHandle();
			HRSRC hVeri = FindResource(hMod, MAKEINTRESOURCE(VS_VERSION_INFO), RT_VERSION);
			PVOID pvVeri = LockResource(LoadResource(hMod, hVeri));
			DWORD cbVeri = SizeofResource(hMod, hVeri);
			if (pvVeri != NULL && cbVeri != 0) {
				VS_FIXEDFILEINFO *pffi = NULL;
				UINT cb = 0;
				if (VerQueryValue(pvVeri, _T("\\"), reinterpret_cast<LPVOID *>(&pffi), &cb)) {
					if (pffi->dwSignature == 0xFEEF04BD) {
						strVer.Format(_T("%u.%u.%u.%u")
							, 0U +(WORD)(pffi->dwFileVersionMS >> 16)
							, 0U +(WORD)(pffi->dwFileVersionMS >> 0)
							, 0U +(WORD)(pffi->dwFileVersionLS >> 16)
							, 0U +(WORD)(pffi->dwFileVersionLS >> 0)
							);
					}
				}
			}
		}

		{
			CString text;
			m_wndVer.GetWindowText(text);
			text.Replace(_T("1.0"), strVer);
			m_wndVer.SetWindowText(text);
		}

		return true;
	}
protected:
	DECLARE_MESSAGE_MAP()
public:
	CStatic m_wndVer;
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_VER, m_wndVer);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()


// CvDlg dialog




CvDlg::CvDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CvDlg::IDD, pParent)
	, m_strSDDL(_T(""))
	, m_objName(_T("npf"))
	, m_dacl(true)
	, m_sacl(FALSE)
	, m_owner(FALSE)
	, m_group(FALSE)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CvDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT_SDDL, m_strSDDL);
	DDX_Control(pDX, IDC_SE, m_wndSe);
	DDX_Text(pDX, IDC_OBJNAME, m_objName);
	DDX_Check(pDX, IDC_DACL, m_dacl);
	DDX_Check(pDX, IDC_SACL, m_sacl);
	DDX_Check(pDX, IDC_OWNER, m_owner);
	DDX_Check(pDX, IDC_GROUP, m_group);
}

BEGIN_MESSAGE_MAP(CvDlg, CDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_PASTE, &CvDlg::OnBnClickedPaste)
	ON_BN_CLICKED(IDC_VIEW, &CvDlg::OnBnClickedView)
	ON_BN_CLICKED(IDC_GET, &CvDlg::OnBnClickedGet)
	ON_BN_CLICKED(IDC_COPY, &CvDlg::OnBnClickedCopy)
	ON_BN_CLICKED(IDC_EDIT, &CvDlg::OnBnClickedEdit)
END_MESSAGE_MAP()


// CvDlg message handlers

BOOL CvDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	m_wndSe.SetItemData(m_wndSe.AddString(_T("FILE_OBJECT")), SE_FILE_OBJECT);
	m_wndSe.SetItemData(m_wndSe.AddString(_T("SERVICE")), SE_SERVICE);
	m_wndSe.SetItemData(m_wndSe.AddString(_T("PRINTER")), SE_PRINTER);
	m_wndSe.SetItemData(m_wndSe.AddString(_T("REGISTRY_KEY")), SE_REGISTRY_KEY);
	m_wndSe.SetItemData(m_wndSe.AddString(_T("LMSHARE")), SE_LMSHARE);
	m_wndSe.SetItemData(m_wndSe.AddString(_T("KERNEL_OBJECT")), SE_KERNEL_OBJECT);
	m_wndSe.SetItemData(m_wndSe.AddString(_T("WINDOW_OBJECT")), SE_WINDOW_OBJECT);
	m_wndSe.SetItemData(m_wndSe.AddString(_T("DS_OBJECT")), SE_DS_OBJECT);
	m_wndSe.SetItemData(m_wndSe.AddString(_T("DS_OBJECT_ALL")), SE_DS_OBJECT_ALL);
	m_wndSe.SetItemData(m_wndSe.AddString(_T("PROVIDER_DEFINED_OBJECT")), SE_PROVIDER_DEFINED_OBJECT);
	m_wndSe.SetItemData(m_wndSe.AddString(_T("WMIGUID_OBJECT")), SE_WMIGUID_OBJECT);
	m_wndSe.SetItemData(m_wndSe.AddString(_T("REGISTRY_WOW64_32KEY")), SE_REGISTRY_WOW64_32KEY);

	m_wndSe.SetCurSel(m_wndSe.FindString(-1, _T("SERVICE")));

	UpdateData(false);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CvDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CvDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CvDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

class GMUt {
public:
	static HGLOBAL Cap(LPCSTR psz) {
		UINT cb = sizeof(char) * (strlen(psz) + 1);
		HGLOBAL hMem = GlobalAlloc(GHND, cb);
		if (hMem != NULL) {
			PVOID pv = GlobalLock(hMem);
			strcpy(reinterpret_cast<LPSTR>(pv), psz);
			GlobalUnlock(hMem);
		}
		return hMem;
	}
};

void CvDlg::OnBnClickedCopy() {
	if (!UpdateData())
		return;
	if (OpenClipboard()) {
		EmptyClipboard();
		SetClipboardData(CF_TEXT, GMUt::Cap(CT2A(m_strSDDL)));
		CloseClipboard();
	}
}

void CvDlg::OnBnClickedPaste() {
	if (!UpdateData())
		return;
	if (OpenClipboard()) {
		HGLOBAL hMem = GetClipboardData(CF_TEXT);
		LPVOID pv = GlobalLock(hMem);
		if (pv != NULL) {
			m_strSDDL = reinterpret_cast<LPCSTR>(pv);
			GlobalUnlock(pv);
		}
		CloseClipboard();

		UpdateData(false);
	}
}

#define MAX_ACC 100U

class SUt {
public:
	static BOOL ConvertSecurityDescriptorToStringSecurityDescriptor2(
		PSECURITY_DESCRIPTOR  SecurityDescriptor,
		DWORD RequestedStringSDRevision,
		SECURITY_INFORMATION SecurityInformation,
		CString &StringSecurityDescriptor
	) {
		LPWSTR pcw = NULL;
		if (ConvertSecurityDescriptorToStringSecurityDescriptor(SecurityDescriptor, RequestedStringSDRevision, SecurityInformation, &pcw, NULL)) {
			StringSecurityDescriptor = pcw;
			ATLVERIFY(NULL == LocalFree(pcw));
			return true;
		}
		return false;
	}
};

class CEaseSiAccess {
public:
	CAtlArray<SI_ACCESS> m_access;
	CAtlArray<CStringW> m_names;

	void Add(ACCESS_MASK mask, CStringW name, DWORD flags) {
		m_names.Add(name);
		SI_ACCESS si;
		si.pguid = NULL;
		si.mask = mask;
		si.pszName = const_cast<LPWSTR>(static_cast<LPCWSTR>(name));
		si.dwFlags = flags;
		m_access.Add(si);
	}
};

class CSettype {
public:
	CEaseSiAccess m_accEasy;

	void SettypeKey() {
		SetObjecttype(L"Key");

		m_accEasy.Add(FILE_GENERIC_READ, L"Generic Read", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(FILE_GENERIC_WRITE , L"Generic Write", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(FILE_GENERIC_EXECUTE, L"Generic Execute", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);

		m_accEasy.Add(STANDARD_RIGHTS_ALL, L"Standard Rights All", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);

		m_accEasy.Add(READ_CONTROL,	L"Read Control", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(WRITE_DAC,	L"Write DAC",	SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(WRITE_OWNER,	L"Write Owner",	SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(SYNCHRONIZE,	L"Synchronize",	SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);

		m_accEasy.Add(KEY_ALL_ACCESS, L"All Access", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);

		m_accEasy.Add(KEY_CREATE_LINK, L"Create Link", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(KEY_CREATE_SUB_KEY, L"Create Subkey", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(KEY_ENUMERATE_SUB_KEYS, L"Enum Subkeys", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(KEY_EXECUTE, L"Execute", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(KEY_NOTIFY, L"Notify",			SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(KEY_QUERY_VALUE, L"Query Value", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(KEY_READ, L"Read", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(KEY_SET_VALUE, L"Set Value", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(KEY_WOW64_32KEY, L"WOW64 32Key", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(KEY_WOW64_64KEY, L"WOW64 64Key", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(KEY_WRITE, L"Write", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
	}

	void SettypeFile() {
		SetObjecttype(L"File");

		m_accEasy.Add(FILE_GENERIC_READ, L"Generic Read", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(FILE_GENERIC_WRITE , L"Generic Write", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(FILE_GENERIC_EXECUTE, L"Generic Execute", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);

		m_accEasy.Add(STANDARD_RIGHTS_ALL, L"Standard Rights All", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);

		m_accEasy.Add(READ_CONTROL,	L"Read Control", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(WRITE_DAC,	L"Write DAC",	SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(WRITE_OWNER,	L"Write Owner",	SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(SYNCHRONIZE,	L"Synchronize",	SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);

		m_accEasy.Add(FILE_ALL_ACCESS,			L"All Access", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);

		m_accEasy.Add(FILE_READ_DATA, L"Read Data; List Directory",		SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(FILE_WRITE_DATA, L"Write Data; Add File",		SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(FILE_APPEND_DATA, L"Append Data; Add Subdir",				SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(FILE_READ_EA, L"Read EA",		SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(FILE_WRITE_EA, L"Write EA",			SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(FILE_EXECUTE, L"Execute", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(FILE_TRAVERSE, L"Traverse", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(FILE_DELETE_CHILD, L"Delete Child", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(FILE_READ_ATTRIBUTES, L"Read Atts", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(FILE_WRITE_ATTRIBUTES, L"Write Atts", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);
	}

	void SettypeService() {
		SetObjecttype(L"Service");

		m_accEasy.Add(STANDARD_RIGHTS_READ|SERVICE_QUERY_CONFIG|SERVICE_QUERY_STATUS|SERVICE_INTERROGATE|SERVICE_ENUMERATE_DEPENDENTS, 
			L"Generic Read", 0|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(STANDARD_RIGHTS_WRITE|SERVICE_CHANGE_CONFIG, 
			L"Generic Write", 0|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(STANDARD_RIGHTS_EXECUTE|SERVICE_START|SERVICE_STOP|SERVICE_PAUSE_CONTINUE|SERVICE_USER_DEFINED_CONTROL, 
			L"Generic Execute", 0|SI_ACCESS_SPECIFIC);

		m_accEasy.Add(STANDARD_RIGHTS_ALL, L"Standard Rights All", 0|SI_ACCESS_SPECIFIC);

		m_accEasy.Add(READ_CONTROL, L"Read Control", 0|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(WRITE_DAC, L"Write DAC", 0|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(WRITE_OWNER, L"Write Owner", 0|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(SYNCHRONIZE, L"Synchronize", 0|SI_ACCESS_SPECIFIC);

		m_accEasy.Add(SERVICE_ALL_ACCESS, L"All Access", SI_ACCESS_GENERAL|SI_ACCESS_SPECIFIC);

		m_accEasy.Add(SERVICE_QUERY_CONFIG, L"Query Config", 0|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(SERVICE_CHANGE_CONFIG, L"Change Config", 0|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(SERVICE_QUERY_STATUS, L"Query Status", 0|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(SERVICE_ENUMERATE_DEPENDENTS, L"Enumerate Dependents", 0|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(SERVICE_START, L"Start", 0|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(SERVICE_STOP, L"Stop", 0|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(SERVICE_PAUSE_CONTINUE, L"Pause Continue", 0|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(SERVICE_INTERROGATE, L"Interrogate", 0|SI_ACCESS_SPECIFIC);
		m_accEasy.Add(SERVICE_USER_DEFINED_CONTROL, L"User Defined Control", 0|SI_ACCESS_SPECIFIC);

		m_accEasy.Add(SERVICE_START|SERVICE_STOP|SERVICE_PAUSE_CONTINUE|SERVICE_USER_DEFINED_CONTROL, L"Control (Start, stop, etc)", SI_ACCESS_GENERAL);
	}

	virtual void SetObjecttype(LPCWSTR pcw) PURE;
};

class CSI : public CSettype, public ISecurityInformation {
public:
	ULONG m_life;
	CStringW m_strObjectName;

	CSI(): m_life(0) { }

	virtual void SetObjecttype(LPCWSTR pcw) {
		m_strObjectName = pcw;
	}

	// *** IUnknown methods ***
	STDMETHOD(QueryInterface) (THIS_ REFIID riid, LPVOID * ppvObj) {
		if (ppvObj == NULL)
			return E_POINTER;
		*ppvObj = NULL;
		if (riid == IID_ISecurityInformation || riid == IID_IUnknown) {
			*ppvObj = static_cast<ISecurityInformation *>(this);
		}
		else {
			return E_NOINTERFACE;
		}
		AddRef();
		return S_OK;
	}
	STDMETHOD_(ULONG,AddRef) (THIS)  {
		return ++m_life;
	}
	STDMETHOD_(ULONG,Release) (THIS) {
		ULONG c = --m_life;
		if (c == 0) {
			delete this;
		}
		return c;
	}

	// *** ISecurityInformation methods ***
	STDMETHOD(GetObjectInformation) (THIS_ PSI_OBJECT_INFO pObjectInfo ) {
		SI_OBJECT_INFO &r = *pObjectInfo;
		ZeroMemory(&r, sizeof(r));
		r.dwFlags = 0
			|SI_EDIT_ALL
			|SI_ADVANCED
			|SI_EDIT_PROPERTIES
			|SI_EDIT_EFFECTIVE
			|SI_NO_TREE_APPLY
			|SI_NO_ACL_PROTECT
			;
		r.hInstance = GetModuleHandle(NULL);
		r.pszObjectName = const_cast<LPWSTR>(static_cast<LPCWSTR>(m_strObjectName));

		return S_OK;
	}

	STDMETHOD(GetSecurity) (THIS_ SECURITY_INFORMATION RequestedInformation,
							PSECURITY_DESCRIPTOR *ppSecurityDescriptor,
							BOOL fDefault ) PURE;

	STDMETHOD(SetSecurity) (THIS_ SECURITY_INFORMATION SecurityInformation,
							PSECURITY_DESCRIPTOR pSecurityDescriptor ) PURE;

	class DCUt {
	public:
		template<typename IT, typename OT>
		static void PutValue(OT &rOut, const IT &rIn) {
			rOut = (OT)rIn;
			if (rOut != rIn) throw std::domain_error("precision lost");
		}
	};

	STDMETHOD(GetAccessRights) (THIS_ const GUID* pguidObjectType,
								DWORD dwFlags, // SI_EDIT_AUDITS, SI_EDIT_PROPERTIES
								PSI_ACCESS *ppAccess,
								ULONG *pcAccesses,
								ULONG *piDefaultAccess ) 
	{
		ATLASSERT(pguidObjectType == NULL || *pguidObjectType == GUID_NULL);

		*ppAccess = m_accEasy.m_access.GetData();
		DCUt::PutValue(*pcAccesses, m_accEasy.m_access.GetCount());
		*piDefaultAccess = 0;

		return S_OK;
	}

	STDMETHOD(MapGeneric) (THIS_ const GUID *pguidObjectType,
						   UCHAR *pAceFlags,
						   ACCESS_MASK *pMask) 
	{
		return S_OK;
	}

	STDMETHOD(GetInheritTypes) (THIS_ PSI_INHERIT_TYPE *ppInheritTypes,
								ULONG *pcInheritTypes ) 
	{
		if (ppInheritTypes == NULL || pcInheritTypes == NULL)
			return E_POINTER;

		*pcInheritTypes = 0;
		return S_OK;
	}

	STDMETHOD(PropertySheetPageCallback)(THIS_ HWND hwnd, UINT uMsg, SI_PAGE_TYPE uPage ) 
	{
		return S_OK;
	}
};

class CStrSI : public CSI {
public:
	CString m_strSDDLOrg;
	CString m_strSDDL;

	CStrSI(CString strSDDL): m_strSDDLOrg(strSDDL), m_strSDDL(strSDDL) { }

	STDMETHOD(GetSecurity) (THIS_ SECURITY_INFORMATION RequestedInformation,
							PSECURITY_DESCRIPTOR *ppSecurityDescriptor,
							BOOL fDefault )
	{
		if (ppSecurityDescriptor == NULL)
			return E_POINTER;
		if (ConvertStringSecurityDescriptorToSecurityDescriptor(
			fDefault ? m_strSDDLOrg : m_strSDDL, SDDL_REVISION_1, ppSecurityDescriptor, NULL
		)) {
			return S_OK;
		}
		int errc = GetLastError();
		return HRESULT_FROM_WIN32(errc);
	}

	STDMETHOD(SetSecurity) (THIS_ SECURITY_INFORMATION SecurityInformation,
							PSECURITY_DESCRIPTOR pSecurityDescriptor ) 
	{
		PSECURITY_DESCRIPTOR pSdRel = NULL;
		int errc = 0;
		if (ConvertStringSecurityDescriptorToSecurityDescriptor(m_strSDDL, SDDL_REVISION_1, &pSdRel, NULL)) {
			DWORD dwAbsoluteSDSize = 10000, dwDaclSize = 10000, dwSaclSize = 10000, dwOwnerSize = 10000, dwPrimaryGroupSize = 10000;
			PSECURITY_DESCRIPTOR pSd = LocalAlloc(LPTR, 10000);
			PACL pDacl = reinterpret_cast<PACL>(LocalAlloc(LPTR, 10000));
			PACL pSacl = reinterpret_cast<PACL>(LocalAlloc(LPTR, 10000));
			PSID pOwner = reinterpret_cast<PSID>(LocalAlloc(LPTR, 10000));
			PSID pPrimaryGroup = reinterpret_cast<PSID>(LocalAlloc(LPTR, 10000));
			SECURITY_INFORMATION fSi = 0;
			if (MakeAbsoluteSD(pSdRel, pSd, &dwAbsoluteSDSize, pDacl, &dwDaclSize, pSacl, &dwSaclSize, pOwner, &dwOwnerSize, pPrimaryGroup, &dwPrimaryGroupSize)) {
				if (SecurityInformation & OWNER_SECURITY_INFORMATION) {
					PSID pOwner;
					BOOL bDefaulted;
					ATLVERIFY(GetSecurityDescriptorOwner(pSecurityDescriptor, &pOwner, &bDefaulted));
					ATLVERIFY(SetSecurityDescriptorOwner(pSd, pOwner, bDefaulted));
				}
				if (SecurityInformation & GROUP_SECURITY_INFORMATION) {
					PSID pGroup;
					BOOL bDefaulted;
					ATLVERIFY(GetSecurityDescriptorGroup(pSecurityDescriptor, &pGroup, &bDefaulted));
					ATLVERIFY(SetSecurityDescriptorGroup(pSd, pGroup, bDefaulted));
				}
				if (SecurityInformation & DACL_SECURITY_INFORMATION) {
					PACL pDacl;
					BOOL bPresent, bDefaulted;
					ATLVERIFY(GetSecurityDescriptorDacl(pSecurityDescriptor, &bPresent, &pDacl, &bDefaulted));
					ATLVERIFY(SetSecurityDescriptorDacl(pSd, bPresent, pDacl, bDefaulted));
				}
				if (SecurityInformation & SACL_SECURITY_INFORMATION) {
					PACL pSacl;
					BOOL bPresent, bDefaulted;
					ATLVERIFY(GetSecurityDescriptorSacl(pSecurityDescriptor, &bPresent, &pSacl, &bDefaulted));
					ATLVERIFY(SetSecurityDescriptorSacl(pSd, bPresent, pSacl, bDefaulted));
				}
				LPWSTR pszSDDLNew = NULL;
				if (ConvertSecurityDescriptorToStringSecurityDescriptor(
					pSd, SDDL_REVISION_1, OWNER_SECURITY_INFORMATION|GROUP_SECURITY_INFORMATION|DACL_SECURITY_INFORMATION|SACL_SECURITY_INFORMATION, &pszSDDLNew, NULL
				)) {
					m_strSDDL = pszSDDLNew;
					LocalFree(pszSDDLNew);

					ATLVERIFY(NULL == LocalFree(pSd));
					ATLVERIFY(NULL == LocalFree(pDacl));
					ATLVERIFY(NULL == LocalFree(pSacl));
					ATLVERIFY(NULL == LocalFree(pOwner));
					ATLVERIFY(NULL == LocalFree(pPrimaryGroup));

					ATLVERIFY(NULL == LocalFree(pSdRel));
					return S_OK;
				}
				else 
					errc = GetLastError();
			}

			ATLVERIFY(NULL == LocalFree(pSd));
			ATLVERIFY(NULL == LocalFree(pDacl));
			ATLVERIFY(NULL == LocalFree(pSacl));
			ATLVERIFY(NULL == LocalFree(pOwner));
			ATLVERIFY(NULL == LocalFree(pPrimaryGroup));

			ATLVERIFY(NULL == LocalFree(pSdRel));
		}
		else 
			errc = GetLastError();

		return HRESULT_FROM_WIN32(errc);
	}
};

class CNamedSec {
public:
	PSID psidOwner, psidGroup;
	PACL pDacl, pSacl;
	PSECURITY_DESCRIPTOR pSd;

	CNamedSec() {
		psidOwner = psidGroup = NULL;
		pDacl = pSacl = NULL;
		pSd = NULL;
	}

	~CNamedSec() {
		Clear();
	}

	void Clear() {
		ATLVERIFY(NULL == LocalFree(pSd));

		psidOwner = psidGroup = NULL;
		pDacl = pSacl = NULL;
		pSd = NULL;
	}

	int Get(LPCTSTR psz, SE_OBJECT_TYPE se, SECURITY_INFORMATION sif) {
		Clear();
		return GetNamedSecurityInfo(psz, se, sif, &psidOwner, &psidGroup, &pDacl, &pSacl, &pSd);
	}

	PSECURITY_DESCRIPTOR Detach() {
		PSECURITY_DESCRIPTOR p = pSd;

		psidOwner = psidGroup = NULL;
		pDacl = pSacl = NULL;
		pSd = NULL;

		return p;
	}
};

class CNamedSI : public CSI {
public:
	CString m_objName;
	SE_OBJECT_TYPE m_se;
	SECURITY_INFORMATION m_sif;

	CNamedSI() { }

	STDMETHOD(GetSecurity) (THIS_ SECURITY_INFORMATION RequestedInformation,
							PSECURITY_DESCRIPTOR *ppSecurityDescriptor,
							BOOL fDefault )
	{
		if (ppSecurityDescriptor == NULL)
			return E_POINTER;

		CNamedSec sec;
		int errc;
		if (0 != (errc = sec.Get(m_objName, m_se, RequestedInformation))) {
			return HRESULT_FROM_WIN32(errc);
		}

		*ppSecurityDescriptor = sec.Detach();
		return S_OK;
	}

	STDMETHOD(SetSecurity) (THIS_ SECURITY_INFORMATION SecurityInformation,
							PSECURITY_DESCRIPTOR pSecurityDescriptor ) 
	{
		if (pSecurityDescriptor == NULL)
			return E_POINTER;

		if (AfxMessageBox(_T("Really save changes?"), MB_ICONEXCLAMATION|MB_YESNO) != IDYES)
			return E_ACCESSDENIED;

		PSID psidOwner, psidGroup;
		BOOL bOwnerDefaulted, bGroupDefaulted, bDaclPresent, bDaclDefaulted, bSaclPresent, bSaclDefaulted;
		PACL pDacl, pSacl;
		ATLVERIFY(GetSecurityDescriptorOwner(pSecurityDescriptor, &psidOwner, &bOwnerDefaulted));
		ATLVERIFY(GetSecurityDescriptorGroup(pSecurityDescriptor, &psidGroup, &bGroupDefaulted));
		ATLVERIFY(GetSecurityDescriptorDacl(pSecurityDescriptor, &bDaclPresent, &pDacl, &bDaclDefaulted));
		ATLVERIFY(GetSecurityDescriptorSacl(pSecurityDescriptor, &bSaclPresent, &pSacl, &bSaclDefaulted));

		int errc;
		if (0 != (errc = SetNamedSecurityInfo(CT2W(m_objName), m_se, SecurityInformation, psidOwner, psidGroup, pDacl, pSacl))) {
			return HRESULT_FROM_WIN32(errc);
		}

		return S_OK;
	}
};


void CvDlg::OnBnClickedView() {
	if (!UpdateData())
		return;

	CStrSI *pSi = new CStrSI(m_strSDDL);
	CComPtr<ISecurityInformation> pSiif = pSi;
	switch (m_wndSe.GetItemData(m_wndSe.GetCurSel())) {
		case SE_SERVICE: 
			pSi->SettypeService(); break;
		case SE_FILE_OBJECT:
		case SE_LMSHARE: 
			pSi->SettypeFile(); break;
		case SE_REGISTRY_KEY: 
		case SE_REGISTRY_WOW64_32KEY: 
			pSi->SettypeKey(); break;
	}
	if (EditSecurity(*this, pSiif)) {
		m_strSDDL = pSi->m_strSDDL;
		UpdateData(false);
	}
}

class EUt {
public:
	static CString Format(int errc) {
		PVOID pv = NULL;
		FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER|FORMAT_MESSAGE_IGNORE_INSERTS|FORMAT_MESSAGE_FROM_SYSTEM, NULL, errc, 0, reinterpret_cast<LPWSTR>(&pv), 0, NULL);
		CString s = reinterpret_cast<LPCTSTR>(pv);
		ATLVERIFY(NULL == LocalFree(pv));
		return s;
	}
};

void CvDlg::OnBnClickedGet() {
	if (!UpdateData())
		return;
	SE_OBJECT_TYPE se = (SE_OBJECT_TYPE)m_wndSe.GetItemData(m_wndSe.GetCurSel());
	SECURITY_INFORMATION sif = 0
		|(m_dacl ? DACL_SECURITY_INFORMATION : 0)
		|(m_group ? GROUP_SECURITY_INFORMATION : 0)
		|(m_owner ? OWNER_SECURITY_INFORMATION : 0)
		|(m_sacl ? SACL_SECURITY_INFORMATION : 0)
		;

	int errc;

	CNamedSec sec;
	if (0 != (errc = sec.Get(m_objName, se, sif))) {
		CString strMsg;
		strMsg.Format(_T("GetNamedSecurityInfo failed.\n\n%s"), EUt::Format(errc));
		AfxMessageBox(strMsg);
		return;
	}

	if (SUt::ConvertSecurityDescriptorToStringSecurityDescriptor2(sec.pSd, SDDL_REVISION_1, sif, m_strSDDL)) {
		UpdateData(false);
	}

}

void CvDlg::OnBnClickedEdit() {
	if (!UpdateData())
		return;
	SE_OBJECT_TYPE se = (SE_OBJECT_TYPE)m_wndSe.GetItemData(m_wndSe.GetCurSel());
	SECURITY_INFORMATION sif = 0
		|(m_dacl ? DACL_SECURITY_INFORMATION : 0)
		|(m_group ? GROUP_SECURITY_INFORMATION : 0)
		|(m_owner ? OWNER_SECURITY_INFORMATION : 0)
		|(m_sacl ? SACL_SECURITY_INFORMATION : 0)
		;

	CNamedSI *pSi = new CNamedSI();
	CComPtr<ISecurityInformation> pSiif = pSi;
	pSi->m_objName = m_objName;
	pSi->m_se = se;
	pSi->m_sif = sif;
	switch (m_wndSe.GetItemData(m_wndSe.GetCurSel())) {
		case SE_SERVICE: pSi->SettypeService(); break;
		default: AfxMessageBox(_T("Only NT Service edit for now."), MB_ICONEXCLAMATION); return;
	}
	pSi->m_strObjectName = m_objName;
	if (!EditSecurity(*this, pSiif))
		return;

	return;
}
