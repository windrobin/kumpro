
#pragma once

class RUt {
public:
	static bool GetInt(LPCTSTR pszCom, LPCTSTR pszApp, LPCTSTR pszVal, DWORD &val, DWORD defval = 0) {
		CRegKey rk;
		LONG r;
		CString strKey; strKey.Format(
			_T("Software\\%s\\%s")
			, pszCom
			, pszApp
			);
		if (0 == (r = rk.Open(HKEY_CURRENT_USER, strKey, KEY_READ))) {
			if (0 == (r = rk.QueryDWORDValue(pszVal, val))) {
				return true;
			}
		}
		if (0 == (r = rk.Open(HKEY_LOCAL_MACHINE, strKey, KEY_READ))) {
			DWORD ddcompat = 0;
			if (0 == (r = rk.QueryDWORDValue(pszVal, val))) {
				return true;
			}
		}
		val = defval;
		return false;
	}
};