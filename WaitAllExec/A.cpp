
#include <windows.h>
#include <tchar.h>

const int MAXPROC = 100;

int _tmain(int argc, TCHAR **argv) {
	PROCESS_INFORMATION pi[MAXPROC];
	ZeroMemory(pi, sizeof(pi));

	int errlv = 0;

	for (int x = 1; x < argc && x < MAXPROC; x++) {
		STARTUPINFO si;
		ZeroMemory(&si, sizeof(si));
		si.cb = sizeof(si);

		BOOL f = CreateProcess(
			NULL,
			argv[x],
			NULL,
			NULL,
			false,
			0,
			NULL,
			NULL,
			&si,
			&pi[x]
			);
		if (!f) {
			pi[x] = PROCESS_INFORMATION();
			errlv |= 1 << x;
			errlv |= 1;
		}
	}

	for (int t = 1; t < argc && t < MAXPROC; t++) {
		WaitForSingleObject(pi[t].hProcess, INFINITE); 
		WaitForSingleObject(pi[t].hThread, INFINITE);
		DWORD lv = 0;
		if (GetExitCodeProcess(pi[t].hProcess, &lv)) {
			errlv |= 1 << t;
			errlv |= 1;
		}
		CloseHandle(pi[t].hThread);
		CloseHandle(pi[t].hProcess);
	}

	return errlv;
}
