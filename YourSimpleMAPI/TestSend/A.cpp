
#include <windows.h>
#include <mapi.h>
#include <tchar.h>
#include <stdio.h>

int _tmain() {
	ULONG r;
	MapiMessage M = {0};
	M.lpszSubject = "表題";
	M.lpszNoteText = "本文1\n本文2\n本文3";
	MapiRecipDesc R[3] = {0}; 
	M.lpRecips = R;
	R[0].lpszAddress = "ku@digitaldolphins.jp";
	R[0].lpszName = "KU";
	R[0].ulRecipClass = MAPI_TO;
	
	R[2].lpszAddress = "test@test";
	R[2].lpszName = "test user";
	R[2].ulRecipClass = MAPI_TO;

	R[1].lpszAddress = "cctest@test";
	R[1].lpszName = "cc test user";
	R[1].ulRecipClass = MAPI_CC;

	M.nRecipCount = 3;
	MapiFileDesc F[1] = {0};
	F[0].lpszPathName = "C:\\A\\A.png";
	F[0].lpszFileName = "Photo.png";
	M.lpFiles = F;
	M.nFileCount = 0;
	HMODULE hMod = LoadLibrary(_T("MAPI32.dll"));
	LPMAPILOGON pfnMAPILogon = (LPMAPILOGON)GetProcAddress(hMod, "MAPILogon");
	LHANDLE h = NULL;
	r = pfnMAPILogon(
		0,
		NULL,
		NULL,
		0,
		0,
		&h
		);
	printf("MAPILogon %u %u \n", r, h);
	LPMAPISENDMAIL pfnMAPISendMail = (LPMAPISENDMAIL)GetProcAddress(hMod, "MAPISendMail");
	r = pfnMAPISendMail(
		h,
		0,
		&M,
		0,
		0
		);
	printf("MAPISendMail %u \n", r);
	return 0;
}
