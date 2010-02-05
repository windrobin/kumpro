
#pragma once

class CMyMutex {
public:
	HANDLE hSync;

	CMyMutex() {
		hSync = CreateMutex(NULL, false, NULL);
	}
	~CMyMutex() {
		if (hSync != NULL) {
			CloseHandle(hSync);
		}
	}
};

class CMyAutoLockMutex {
	CMyMutex &ref;
public:
	CMyAutoLockMutex(CMyMutex &ref): ref(ref) {
		DWORD r = WaitForSingleObject(ref.hSync, INFINITE);
		ATLASSERT(r == WAIT_OBJECT_0);
	}
	~CMyAutoLockMutex() {
		ATLVERIFY(ReleaseMutex(ref.hSync));
	}
};
