
#include <windows.h>
#include <lm.h>

#pragma comment(lib, "netapi32.lib")

int nUsers() {
	SERVER_INFO_102 *pp = NULL;
	NET_API_STATUS r = NetServerGetInfo(NULL, 102, reinterpret_cast<LPBYTE *>(&pp));
	if (r == 0 && pp != NULL) {
		int res = pp->sv102_users;
		NetApiBufferFree(pp);
		return res;
	}
	return -1;
}
