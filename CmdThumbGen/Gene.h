﻿// Gene.h : CGene の宣言

#pragma once
#include "resource.h"       // メイン シンボル

#include "CmdThumbGen.h"

// CGene

class ATL_NO_VTABLE CGene :
	public CComObjectRootEx<CComMultiThreadModel>,
	public CComCoClass<CGene, &CLSID_Gene>,
	public IPersistFile,
	public IExtractImage2,
	public IRunnableTask,
	public IInitializeWithFile,
	public IThumbnailProvider,
	public IGene
{
public:
	CGene()
	{
	}

DECLARE_REGISTRY_RESOURCEID(IDR_GENE)

BEGIN_COM_MAP(CGene)
	COM_INTERFACE_ENTRY(IPersist)
	COM_INTERFACE_ENTRY(IPersistFile)
	COM_INTERFACE_ENTRY(IExtractImage)
	COM_INTERFACE_ENTRY(IExtractImage2)
	COM_INTERFACE_ENTRY(IInitializeWithFile)
	COM_INTERFACE_ENTRY(IThumbnailProvider)
	COM_INTERFACE_ENTRY(IRunnableTask)

	COM_INTERFACE_ENTRY_AGGREGATE(IID_IMarshal, m_pFTM)

	COM_INTERFACE_ENTRY(IGene)
END_COM_MAP()

	DECLARE_PROTECT_FINAL_CONSTRUCT()

	CComPtr<IUnknown> m_pFTM;

	HRESULT FinalConstruct()
	{
		HRESULT hr;
		if (FAILED(hr = CoCreateFreeThreadedMarshaler(static_cast<IPersist *>(this), &m_pFTM)))
			return hr;

		return S_OK;
	}

	void FinalRelease()
	{
		m_pFTM.Release();
	}

public:
	// IPersist
		virtual HRESULT STDMETHODCALLTYPE GetClassID( 
			/* [out] */ CLSID *pClassID)
		{
			if (pClassID == NULL)
				return E_POINTER;

			*pClassID = GetObjectCLSID();
			return S_OK;
		}

	// IPersistFile
		virtual HRESULT STDMETHODCALLTYPE IsDirty( void)
		{
			return S_FALSE;
		}
		
		virtual HRESULT STDMETHODCALLTYPE Load( 
			/* [in] */ LPCOLESTR pszFileName,
			/* [in] */ DWORD dwMode)
		{
			ObjectLock lck(this);

			if (0 != (dwMode & (STGM_CREATE|STGM_CONVERT|STGM_TRANSACTED|STGM_DELETEONRELEASE)))
				return STG_E_INVALIDPARAMETER;

			m_targetPath = pszFileName;
			m_state = Waiting;

			return S_OK;
		}

		CString m_targetPath;
		
		virtual HRESULT STDMETHODCALLTYPE Save( 
			/* [unique][in] */ LPCOLESTR pszFileName,
			/* [in] */ BOOL fRemember) 
		{
			return E_NOTIMPL;
		}
		
		virtual HRESULT STDMETHODCALLTYPE SaveCompleted( 
			/* [unique][in] */ LPCOLESTR pszFileName)
		{
			return S_OK;
		}
		
		virtual HRESULT STDMETHODCALLTYPE GetCurFile( 
			/* [out] */ LPOLESTR *ppszFileName)
		{
			ObjectLock lck(this);

			if (ppszFileName == NULL)
				return E_POINTER;

			*ppszFileName = NULL;

			if (m_targetPath.IsEmpty())
				return S_FALSE;

			*ppszFileName = SUt::CopyW(CT2W(m_targetPath));

			return (*ppszFileName != NULL) ? S_OK : E_OUTOFMEMORY;
		}

		class SUt {
		public:
			static LPWSTR CopyW(LPCWSTR pcwSrc) {
				LPWSTR pcwRet = NULL;
				if (pcwSrc != NULL) {
					size_t cb = wcslen(pcwSrc + 1) << 1;
					LPWSTR pcwRet = reinterpret_cast<LPWSTR>(CoTaskMemAlloc(cb));
					if (pcwRet != NULL) {
						memcpy(pcwRet, pcwSrc, cb);
					}
				}
				return pcwRet;
			}
		};

		SIZE m_size;
		DWORD m_flags;

	// IInitializeWithFile : public IUnknown
	public:
		virtual HRESULT STDMETHODCALLTYPE Initialize( 
			/* [string][in] */ __RPC__in_string LPCWSTR pszFilePath,
			/* [in] */ DWORD grfMode)
		{
			ObjectLock lck(this);

			if (pszFilePath == NULL)
				return E_POINTER;

			m_targetPath = pszFilePath;
			m_state = Waiting;

			return S_OK;
		}

	// IExtractImage
		virtual HRESULT STDMETHODCALLTYPE GetLocation( 
			/* [size_is][out] */ LPWSTR pszPathBuffer,
			/* [in] */ DWORD cch,
			/* [unique][out][in] */ DWORD *pdwPriority,
			/* [in] */ const SIZE *prgSize,
			/* [in] */ DWORD dwRecClrDepth,
			/* [out][in] */ DWORD *pdwFlags)
		{
			ObjectLock lck(this);

			if (pszPathBuffer != NULL) {
				wcsncpy_s(pszPathBuffer, cch, CT2W(m_targetPath), cch);
			}
			if (pdwPriority == NULL)
				return E_POINTER;
			if (prgSize == NULL)
				return E_POINTER;
			if (pdwFlags == NULL)
				return E_POINTER;

			m_size = *prgSize;
			m_flags = *pdwFlags;

			if (0 != (m_flags & IEIFLAG_ASYNC))
				return E_PENDING;
			return S_OK;
		}

		typedef enum {
			Waiting, Running, Done,
		} State;

		State m_state;
		
		virtual HRESULT STDMETHODCALLTYPE Extract( 
			/* [out] */ HBITMAP *phBmpThumbnail)
		{
			ObjectLock lck(this);
			HRESULT hr;
			int errc;

			if (phBmpThumbnail == NULL)
				return E_POINTER;

			if (m_targetPath.IsEmpty())
				return E_FAIL;

			LPCTSTR pszExt = PathFindExtension(m_targetPath);
			if (pszExt == NULL)
				pszExt = _T(".");
			CString strCmdlForm;
			if (FAILED(hr = RUt::GetCommandLineForm(pszExt, strCmdlForm)))
				return hr;
			CString strTempFile;
			if (FAILED(hr = RUt::GetTempFilePath2(strTempFile)))
				return hr;
			DelTmp dt(strTempFile);

			TCHAR tcWorkdir[MAX_PATH] = {0};
			if (0 == GetTempPath(MAX_PATH, tcWorkdir))
				return errc = GetLastError(), HRESULT_FROM_WIN32(errc);

			DWORD_PTR pvArgs[] = {
				reinterpret_cast<DWORD_PTR>(static_cast<LPCTSTR>(m_targetPath)),
				reinterpret_cast<DWORD_PTR>(static_cast<LPCTSTR>(strTempFile)),
				m_size.cx,
				m_size.cy,
				m_flags,
				0,
				0,
				0,
				0,
				0,
			};

			TCHAR szCmdRun[2000 +1] = {0};
			if (0 == (FormatMessage(FORMAT_MESSAGE_FROM_STRING|FORMAT_MESSAGE_ARGUMENT_ARRAY, strCmdlForm, 0, 0, szCmdRun, 2000, (va_list *)pvArgs)))
				return errc = GetLastError(), HRESULT_FROM_WIN32(errc);

			STARTUPINFO si;
			ZeroMemory(&si, sizeof(si));
			si.cb = sizeof(si);
			si.dwFlags = STARTF_USESHOWWINDOW;
			si.wShowWindow = SW_SHOWMINNOACTIVE;

			PROCESS_INFORMATION pi;
			ZeroMemory(&pi, sizeof(pi));

			BOOL fCreated = CreateProcess(
				NULL,
				szCmdRun,
				NULL,
				NULL,
				false,
				0 |IDLE_PRIORITY_CLASS |CREATE_NO_WINDOW,
				NULL,
				tcWorkdir,
				&si,
				&pi
				);
			if (!fCreated)
				return errc = GetLastError(), HRESULT_FROM_WIN32(errc);

			m_state = Running;

			WaitForSingleObject(pi.hProcess, INFINITE);

			m_state = Done;

			DWORD errlv = 0;
			if (!GetExitCodeProcess(pi.hProcess, &errlv))
				errlv = 1;

			CloseHandle(pi.hProcess);
			CloseHandle(pi.hThread);

			HBITMAP hbm = NULL;

			if (errlv == 0) {
				hbm = reinterpret_cast<HBITMAP>(LoadImage(NULL, strTempFile, IMAGE_BITMAP, 0, 0, LR_CREATEDIBSECTION|LR_LOADFROMFILE|LR_VGACOLOR));
			}

			*phBmpThumbnail = hbm;
			return (*phBmpThumbnail != NULL) ? S_OK : E_FAIL;
		}

		class DelTmp {
		protected:
			CString fpTmp;
		public:
			DelTmp(CString fpTmp): fpTmp(fpTmp) {

			}
			~DelTmp() {
				ATLVERIFY(DeleteFile(fpTmp));
			}
		};

		class RUt {
		public:
			static HRESULT GetTempFilePath(CString &pStr) {
				int errc;
				TCHAR szDir[MAX_PATH] = {0};
				if (0 == GetTempPath(MAX_PATH, szDir))
					return errc = GetLastError(), HRESULT_FROM_WIN32(errc);
				TCHAR szPath[MAX_PATH] = {0};
				if (0 == GetTempFileName(szDir, _T("tmp"), 0, szPath))
					return errc = GetLastError(), HRESULT_FROM_WIN32(errc);

				pStr = szPath;
				return S_OK;
			}

			static HRESULT GetTempFilePath2(CString &pStr) {
				int errc;
				TCHAR szDir[MAX_PATH] = {0};
				if (0 == GetTempPath(MAX_PATH, szDir))
					return errc = GetLastError(), HRESULT_FROM_WIN32(errc);
				GUID tmpId;
				HRESULT hr;
				if (FAILED(hr = CoCreateGuid(&tmpId)))
					return hr;
				WCHAR wcId[MAX_PATH] = {0};
				if (0 == StringFromGUID2(tmpId, wcId, MAX_PATH))
					return E_OUTOFMEMORY;
				TCHAR szPath[MAX_PATH] = {0};
				PathCombine(szPath, szDir, CW2T(wcId));

				pStr = szPath;
				return S_OK;
			}

			static HRESULT GetCommandLineForm(LPCTSTR pszExt, CString &pStr) {
				LONG lerr;
				{
					CRegKey rkExt;
					CString strKey; strKey.Format(_T("Software\\HIRAOKA HYPERS TOOLS, Inc.\\CmdThumbGen\\FileExts\\%s")
						, pszExt);
					if (0 == (lerr = rkExt.Open(HKEY_CURRENT_USER, strKey, KEY_READ))) {
						TCHAR szRet[256 +1] = {0};
						ULONG cchRet = 256;
						if (0 == (lerr = rkExt.QueryStringValue(NULL, szRet, &cchRet))) {
							pStr = CString(szRet, cchRet);
							return NOERROR;
						}
					}
				}

				{
					CRegKey rkExt;
					CString strKey; strKey.Format(_T("Software\\HIRAOKA HYPERS TOOLS, Inc.\\CmdThumbGen\\FileExts\\%s")
						, pszExt);
					if (0 == (lerr = rkExt.Open(HKEY_LOCAL_MACHINE, strKey, KEY_READ))) {
						TCHAR szRet[256 +1] = {0};
						ULONG cchRet = 256;
						if (0 == (lerr = rkExt.QueryStringValue(NULL, szRet, &cchRet))) {
							pStr = CString(szRet, cchRet);
							return NOERROR;
						}
					}
				}

				return E_NOT_SET;
			}
		};

	// IExtractImage2
		virtual HRESULT STDMETHODCALLTYPE GetDateStamp( 
			/* [out] */ FILETIME *pDateStamp)
		{
			ObjectLock lck(this);
			HRESULT hr;
			int errc;
			CAtlFile f;
			if (SUCCEEDED(hr = f.Create(m_targetPath, GENERIC_READ, FILE_SHARE_READ|FILE_SHARE_WRITE, OPEN_EXISTING))) {
				if (GetFileTime(f, NULL, NULL, pDateStamp)) {
					f.Close();
					return S_OK;
				}
				else { errc = GetLastError(), hr = HRESULT_FROM_WIN32(errc); }
			}
			return hr;
		}

	// IRunnableTask
		virtual HRESULT STDMETHODCALLTYPE Run( void)
		{
			return NOERROR;
		}
		
		virtual HRESULT STDMETHODCALLTYPE Kill( 
			/* [annotation][in] */ 
			__in  BOOL bWait)
		{
			return E_NOTIMPL;
		}
		
		virtual HRESULT STDMETHODCALLTYPE Suspend( void)
		{
			return E_NOTIMPL;
		}
		
		virtual HRESULT STDMETHODCALLTYPE Resume( void)
		{
			return E_NOTIMPL;
		}
		
		virtual ULONG STDMETHODCALLTYPE IsRunning( void)
		{
			switch (m_state) {
				case Running:
					return IRTIR_TASK_RUNNING;
				case Done:
					return IRTIR_TASK_FINISHED;
			}

			return IRTIR_TASK_NOT_RUNNING;
		}

	// IThumbnailProvider : public IUnknown
	public:
		virtual HRESULT STDMETHODCALLTYPE GetThumbnail( 
			/* [in] */ UINT cx,
			/* [out] */ __RPC__deref_out_opt HBITMAP *phbmp,
			/* [out] */ __RPC__out WTS_ALPHATYPE *pdwAlpha)
		{
			ObjectLock lck(this);
			HRESULT hr;
			int errc;

			if (phbmp == NULL)
				return E_POINTER;

			if (m_targetPath.IsEmpty())
				return E_FAIL;

			LPCTSTR pszExt = PathFindExtension(m_targetPath);
			if (pszExt == NULL)
				pszExt = _T(".");
			CString strCmdlForm;
			if (FAILED(hr = RUt::GetCommandLineForm(pszExt, strCmdlForm)))
				return hr;
			CString strTempFile;
			if (FAILED(hr = RUt::GetTempFilePath2(strTempFile)))
				return hr;
			DelTmp dt(strTempFile);

			TCHAR tcWorkdir[MAX_PATH] = {0};
			if (0 == GetTempPath(MAX_PATH, tcWorkdir))
				return errc = GetLastError(), HRESULT_FROM_WIN32(errc);

			DWORD_PTR pvArgs[] = {
				reinterpret_cast<DWORD_PTR>(static_cast<LPCTSTR>(m_targetPath)),
				reinterpret_cast<DWORD_PTR>(static_cast<LPCTSTR>(strTempFile)),
				cx,
				cx,
				0, // m_flags
				0,
				0,
				0,
				0,
				0,
			};

			TCHAR szCmdRun[2000 +1] = {0};
			if (0 == (FormatMessage(FORMAT_MESSAGE_FROM_STRING|FORMAT_MESSAGE_ARGUMENT_ARRAY, strCmdlForm, 0, 0, szCmdRun, 2000, (va_list *)pvArgs)))
				return errc = GetLastError(), HRESULT_FROM_WIN32(errc);

			STARTUPINFO si;
			ZeroMemory(&si, sizeof(si));
			si.cb = sizeof(si);
			si.dwFlags = STARTF_USESHOWWINDOW;
			si.wShowWindow = SW_SHOWMINNOACTIVE;

			PROCESS_INFORMATION pi;
			ZeroMemory(&pi, sizeof(pi));

			BOOL fCreated = CreateProcess(
				NULL,
				szCmdRun,
				NULL,
				NULL,
				false,
				0 |IDLE_PRIORITY_CLASS |CREATE_NO_WINDOW,
				NULL,
				tcWorkdir,
				&si,
				&pi
				);
			if (!fCreated)
				return errc = GetLastError(), HRESULT_FROM_WIN32(errc);

			m_state = Running;

			WaitForSingleObject(pi.hProcess, INFINITE);

			m_state = Done;

			DWORD errlv = 0;
			if (!GetExitCodeProcess(pi.hProcess, &errlv))
				errlv = 1;

			CloseHandle(pi.hProcess);
			CloseHandle(pi.hThread);

			HBITMAP hbm = NULL;

			if (errlv == 0) {
				hbm = reinterpret_cast<HBITMAP>(LoadImage(NULL, strTempFile, IMAGE_BITMAP, 0, 0, LR_CREATEDIBSECTION|LR_LOADFROMFILE|LR_VGACOLOR));

			}

			if (pdwAlpha != NULL) {
				*pdwAlpha = WTSAT_UNKNOWN;

				if (hbm != NULL) {
					DIBSECTION dib;
					ZeroMemory(&dib, sizeof(dib));
					if (GetObject(hbm, sizeof(dib), &dib) == sizeof(dib)) {
						switch (dib.dsBmih.biBitCount) {
							case 32:
								*pdwAlpha = WTSAT_ARGB;
								break;
							default:
								*pdwAlpha = WTSAT_RGB;
								break;
						}
					}
				}
			}

			*phbmp = hbm;
			return (*phbmp != NULL) ? S_OK : E_FAIL;
		}
};

OBJECT_ENTRY_AUTO(__uuidof(Gene), CGene)
