
#include <windows.h>
#include <atlbase.h>
#include <atlstr.h>
#include <mapi.h>

class Sess {
public:
	virtual ~Sess() { }
};

ULONG FAR PASCAL MyMAPILogon(
	ULONG_PTR ulUIParam,
	__in LPSTR lpszProfileName,
	__in LPSTR lpszPassword,
	FLAGS flFlags,
	ULONG ulReserved,
	LPLHANDLE lplhSession
) {
	if (lplhSession == NULL)
		return MAPI_E_FAILURE;
	*lplhSession = reinterpret_cast<LHANDLE>(new Sess());
	return SUCCESS_SUCCESS;
}

ULONG FAR PASCAL MyMAPILogoff(
	LHANDLE lhSession,
	ULONG_PTR ulUIParam,
	FLAGS flFlags,
	ULONG ulReserved
) {
	if (lhSession == NULL)
		return MAPI_E_INVALID_SESSION;
	delete reinterpret_cast<Sess *>(lhSession);
	return SUCCESS_SUCCESS;
}

ULONG FAR PASCAL MyMAPISendMail(
	LHANDLE lhSession,
	ULONG_PTR ulUIParam,
	lpMapiMessage pM,
	FLAGS flFlags,
	ULONG ulReserved
) {
	if (pM == NULL)
		return MAPI_E_FAILURE;
	UINT cx = pM->nFileCount;
	if (cx == 0)
		return SUCCESS_SUCCESS;

	//MessageBoxA(NULL, "MyEdMaxMAPI", "", MB_ICONSTOP);

	TCHAR tcfpMe[MAX_PATH] = {0};
	GetModuleFileName(GetModuleHandle(_T("MyEdMaxMAPI.dll")), tcfpMe, MAX_PATH);
	if (!PathRemoveFileSpec(tcfpMe))
		return MAPI_E_FAILURE;
	TCHAR tcDir[MAX_PATH] = {0};
	_tcscpy(tcDir, tcfpMe);
	TCHAR tcfpApp[MAX_PATH] = {0};
	PathCombine(tcfpApp, tcfpMe, _T("EdMax.exe"));

	TCHAR tcTmpdir[MAX_PATH] = {0};
	if (0 == GetTempPath(MAX_PATH, tcTmpdir))
		return MAPI_E_FAILURE;

	GUID id;
	HRESULT hr;
	if (FAILED(hr = CoCreateGuid(&id)))
		return MAPI_E_FAILURE;

	CString fnId;
	for (int x=0; x<16; x++)
		fnId.AppendFormat(_T("%02x"), 0U + reinterpret_cast<LPBYTE>(&id)[x]);

	TCHAR tcWorkdir[MAX_PATH] = {0};
	PathCombine(tcWorkdir, tcTmpdir, fnId);

	if (!CreateDirectory(tcWorkdir, NULL))
		return MAPI_E_FAILURE;

	CStringA s;
	s.AppendFormat((" /E"));
	for (UINT x=0; x<cx; x++) {
		CString fn1 = CA2T(pM->lpFiles[x].lpszFileName);
		CString fp1 = CA2T(pM->lpFiles[x].lpszPathName);

		for (size_t i=0; i<fn1.GetLength(); i++) {
			switch (fn1.GetAt(i)) {
				case _T(':'):
				case _T('\\'):
				case _T('/'):
				case _T('*'):
				case _T('?'):
				case _T('"'):
				case _T('<'):
				case _T('>'):
				case _T('|'):
					fn1.SetAt(i, _T('_'));
					break;
			}
		}

		if (fn1.GetLength() == 0) {
			fn1 = PathFindFileName(fp1);
		}

		TCHAR tcfp1[MAX_PATH] = {0};
		PathCombine(tcfp1, tcWorkdir, fn1);
		if (!CopyFile(fp1, tcfp1, false))
			return MAPI_E_FAILURE;			
		s.AppendFormat((" \"%s\""), CT2A(tcfp1));
	}

	STARTUPINFO si = {0};
	si.cb = sizeof(si);

	PROCESS_INFORMATION pi = {0};

	BOOL f = CreateProcess(
		tcfpApp,
		CA2T(s),
		NULL,
		NULL,
		false,
		0,
		NULL,
		tcDir,
		&si,
		&pi
		);

	if (!f)
		return MAPI_E_FAILURE;

	CloseHandle(pi.hProcess);
	CloseHandle(pi.hThread);

	return SUCCESS_SUCCESS;
}

ULONG FAR PASCAL MyMAPIAddress(
	LHANDLE lhSession,
	ULONG_PTR ulUIParam,
	__in LPSTR lpszCaption,
	ULONG nEditFields,
	__in LPSTR lpszLabels,
	ULONG nRecips,
	lpMapiRecipDesc lpRecips,
	FLAGS flFlags,
	ULONG ulReserved,
	LPULONG lpnNewRecips,
	lpMapiRecipDesc FAR *lppNewRecips
) {
	return MAPI_E_NOT_SUPPORTED;
}

ULONG FAR PASCAL MyMAPIDeleteMail(
	LHANDLE lhSession,
	ULONG_PTR ulUIParam,
	LPSTR lpszMessageID,
	FLAGS flFlags,
	ULONG ulReserved
) {
	return MAPI_E_NOT_SUPPORTED;
}

ULONG FAR PASCAL MyMAPIDetails(
	LHANDLE lhSession,
	ULONG_PTR ulUIParam,
	lpMapiRecipDesc lpRecip,
	FLAGS flFlags,
	ULONG ulReserved
) {
	return MAPI_E_NOT_SUPPORTED;
}

ULONG (FAR PASCAL MyMAPIFindNext)(
	LHANDLE lhSession,
	ULONG_PTR ulUIParam,
	LPSTR lpszMessageType,
	LPSTR lpszSeedMessageID,
	FLAGS flFlags,
	ULONG ulReserved,
	LPSTR lpszMessageID
) {
	return MAPI_E_NOT_SUPPORTED;
}

ULONG FAR PASCAL MyMAPIFreeBuffer(LPVOID pv) {
	return SUCCESS_SUCCESS;
}

ULONG (FAR PASCAL MyMAPIReadMail)(
	LHANDLE lhSession,
	ULONG_PTR ulUIParam,
	LPSTR lpszMessageID,
	FLAGS flFlags,
	ULONG ulReserved,
	lpMapiMessage FAR *lppMessage
) {
	return MAPI_E_NOT_SUPPORTED;
}

ULONG (FAR PASCAL MyMAPIResolveName)(
	LHANDLE lhSession,
	ULONG_PTR ulUIParam,
	LPSTR lpszName,
	FLAGS flFlags,
	ULONG ulReserved,
	lpMapiRecipDesc FAR *lppRecip
) {
	return MAPI_E_NOT_SUPPORTED;
}

ULONG (FAR PASCAL MyMAPISaveMail)(
	LHANDLE lhSession,
	ULONG_PTR ulUIParam,
	lpMapiMessage lpMessage,
	FLAGS flFlags,
	ULONG ulReserved,
	LPSTR lpszMessageID
) {
	return MAPI_E_NOT_SUPPORTED;
}

ULONG (FAR PASCAL MyMAPISendDocuments)(
	ULONG_PTR ulUIParam,
	LPSTR lpszDelimChar,
	LPSTR lpszFilePaths,
	LPSTR lpszFileNames,
	ULONG ulReserved
) {
	return MAPI_E_NOT_SUPPORTED;
}
