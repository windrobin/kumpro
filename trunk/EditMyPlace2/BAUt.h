
#pragma once

class BAUt {
public:
	static void Set(CByteArray &vec, const void *pv, int cb) {
		vec.SetSize(cb);
		CopyMemory(vec.GetData(), pv, cb);
	}
};