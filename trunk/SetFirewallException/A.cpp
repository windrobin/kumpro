
#include <windows.h>
#include <stdio.h>
#include <atlbase.h>
#include <atlcom.h>

#include <objbase.h>
#include <netfw.h>

int helpYa() {
	fputs(
		"SetFirewallException set    port tcp 80 \"Web server\" \n"
		"SetFirewallException remove port tcp 80 \"\" \n"
		, stderr);
	return 1;
}

typedef enum {
	Set, Remove,
} Act;

typedef enum {
	Tcp, Udp,
} Sockett;

class BadCommandLine {
};

// http://msdn.microsoft.com/en-us/library/aa364726(VS.85).aspx

enum {
	Noerror = 0,
	NoNetFwMgr,
	No_get_LocalPolicy,
	No_get_CurrentProfile,
	No_get_GloballyOpenPorts,
	FailedRemove,
	NoNetFwOpenPort,
	No_put_Port,
	No_put_Protocol,
	No_put_Name,
	FailedAdd,
};

int SfeXP(Act act, Sockett sockett, int portno, LPCSTR pszDesc) {
	HRESULT hr;
	CComPtr<INetFwMgr> fwMgr;
	if (FAILED(hr = fwMgr.CoCreateInstance(__uuidof(NetFwMgr))))
		return NoNetFwMgr;

	CComPtr<INetFwPolicy> fwPolicy;
	if (FAILED(hr = fwMgr->get_LocalPolicy(&fwPolicy)))
		return No_get_LocalPolicy;

	CComPtr<INetFwProfile> fwProfile;
	if (FAILED(hr = fwPolicy->get_CurrentProfile(&fwProfile)))
		return No_get_CurrentProfile;

	NET_FW_IP_PROTOCOL ipProtocol = (sockett == Tcp) 
		? NET_FW_IP_PROTOCOL_TCP 
		: NET_FW_IP_PROTOCOL_UDP;

	if (act == Remove) {
		CComPtr<INetFwOpenPorts> fwOpenPorts;
		if (FAILED(hr = fwProfile->get_GloballyOpenPorts(&fwOpenPorts)))
			return No_get_GloballyOpenPorts;

		if (FAILED(hr = fwOpenPorts->Remove(portno, ipProtocol)))
			return FailedRemove;
	}
	else if (act == Set) {
		CComPtr<INetFwOpenPorts> fwOpenPorts;
		if (FAILED(hr = fwProfile->get_GloballyOpenPorts(&fwOpenPorts)))
			return No_get_GloballyOpenPorts;

		CComPtr<INetFwOpenPort> fwOpenPort;
		if (FAILED(hr = fwOpenPort.CoCreateInstance(__uuidof(NetFwOpenPort))))
			return NoNetFwOpenPort;

		if (FAILED(hr = fwOpenPort->put_Port(portno)))
			return No_put_Port;

		if (FAILED(hr = fwOpenPort->put_Protocol(ipProtocol)))
			return No_put_Protocol;

		CComBSTR fwBstrName = pszDesc;
		if (FAILED(hr = fwOpenPort->put_Name(fwBstrName)))
			return No_put_Name;

		if (FAILED(hr = fwOpenPorts->Add(fwOpenPort)))
			return FailedAdd;
	}

	return 0;
}

int main(int argc, char **argv) {
	HRESULT hr;
	if (FAILED(hr = CoInitialize(NULL)))
		return 1;
	int r = 0;
	try {
		if (argc < 6) 
			throw BadCommandLine();

		Act act;
		if (_stricmp(argv[1], "set") == 0)
			act = Set;
		else if (_stricmp(argv[1], "remove") == 0)
			act = Remove;
		else
			throw BadCommandLine();

		if (_stricmp(argv[2], "port") != 0)
			throw BadCommandLine();

		Sockett sockett;
		if (_stricmp(argv[3], "tcp") == 0)
			sockett = Tcp;
		else if (_stricmp(argv[3], "udp") == 0)
			sockett = Udp;
		else
			throw BadCommandLine();

		int portno = 0;
		if (sscanf(argv[4], "%d", &portno) != 1)
			throw BadCommandLine();

		r = SfeXP(act, sockett, portno, argv[5]);
	}
	catch (BadCommandLine) {
		r = helpYa();
	}
	CoUninitialize();
	return r;
}
