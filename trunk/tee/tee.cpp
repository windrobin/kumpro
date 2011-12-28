
#include <windows.h>

#include <stdio.h>
#include <string.h>

#define MAXF 100
#define CBBUFF 10000

bool append = false;
int fcnt = 0;
HANDLE files[MAXF] = {NULL};
char buff[CBBUFF];

#define ISVALID(F) ((F) != NULL && (F) != INVALID_HANDLE_VALUE)

int main(int argc, char **argv) {
	for (int x=1; x<argc && fcnt<MAXF; x++) {
		if (strcmp(argv[x], "-a") == 0 || strcmp(argv[x], "--a") == 0) {
			append = true;
		}
		else if (strcmp(argv[x], "?") == 0 || strcmp(argv[x], "-?") == 0 || strcmp(argv[x], "--help") == 0) {
			fputs("tee.exe [-a] binary-files ...", stderr);
			return 1;
		}
		else {
			files[fcnt] = CreateFile(argv[x], GENERIC_WRITE, FILE_SHARE_READ, NULL, append ? OPEN_ALWAYS : CREATE_ALWAYS, 0, NULL);
			if (append && ISVALID(files[fcnt]))
				SetFilePointer(files[fcnt], 0, NULL, FILE_END);
			fcnt++;
		}
	}
	if (fcnt < MAXF) {
		files[fcnt] = GetStdHandle(STD_OUTPUT_HANDLE);
		fcnt++;
	}
	HANDLE hIn = GetStdHandle(STD_INPUT_HANDLE);
	DWORD cbRead = 0;
	while (ReadFile(hIn, buff, sizeof(buff), &cbRead, NULL)) {
		for (int i=0; i<fcnt; i++) {
			DWORD cbWritten = 0;
			if (ISVALID(files[i]))
				WriteFile(files[i], buff, cbRead, &cbWritten, NULL);
		}
	}
	for (int i=0; i<fcnt; i++) 
		if (ISVALID(files[i]))
			CloseHandle(files[i]);
	return 0;
}
