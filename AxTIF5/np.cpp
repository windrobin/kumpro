
#include <afxwin.h>
#include <afxctl.h>

#include <objsafe.h>

#include <npapi.h>
#include <npupp.h>

#include "AxTIF5.h"
#include "ximage.h"
#include "AxTIF5Ctrl.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

extern CAxTIF5App theApp;

NPNetscapeFuncs *g_pNavigatorFuncs = NULL;

// https://developer.mozilla.org/en/Gecko_Plugin_API_Reference/Plug-in_Development_Overview

// http://www.podgoretsky.com/ftp/docs/Internet/Netscape%20Plug-Ins/ch8.htm#NP_GetEntryPoints

NPError NP_LOADDS NPP_New(
	NPMIMEType pluginType, 
	NPP instance,
	uint16 mode, 
	int16 argc, 
	char* argn[],
	char* argv[], 
	NPSavedData* saved)
{
	AFX_MANAGE_STATE(_afxModuleAddrThis);

	if (false
		|| strcmp(pluginType, "image/tiff") == 0
		|| strcmp(pluginType, "image/x-tiff") == 0
	) {
		TRY {
			instance->pdata = new CAxTIF5Ctrl();
			return NPERR_NO_ERROR;
		}
		CATCH_ALL(e) {
		}
		END_CATCH_ALL;
	}

	return NPERR_INVALID_PLUGIN_ERROR;
}

NPError NP_LOADDS NPP_Destroy(
	NPP instance, 
	NPSavedData** save)
{
	AFX_MANAGE_STATE(_afxModuleAddrThis);

	CAxTIF5Ctrl *p = reinterpret_cast<CAxTIF5Ctrl *>(instance->pdata);
	p->InternalRelease();

	return NPERR_NO_ERROR;
}

NPError NP_LOADDS NPP_SetWindow(NPP instance, NPWindow* window)
{
	AFX_MANAGE_STATE(_afxModuleAddrThis);

	CAxTIF5Ctrl *pCtrl = reinterpret_cast<CAxTIF5Ctrl *>(instance->pdata);

	HWND hwndParent = (HWND)window->window;
	if (hwndParent != NULL) {
		if (pCtrl->m_hWnd == NULL) {
			VERIFY(pCtrl->Create(AfxRegisterWndClass(CS_HREDRAW|CS_VREDRAW), _T("AxTIF5"), WS_CHILD|WS_VISIBLE|WS_CLIPCHILDREN|WS_CLIPSIBLINGS, CRect(0, 0, 0, 0), CWnd::FromHandle(hwndParent), 0));
		}
		//pCtrl->SetClip(CRect(window->clipRect.left, window->clipRect.top, window->clipRect.right, window->clipRect.bottom));

		pCtrl->MoveWindow(0, 0, window->width, window->height);
	}
	else {
		VERIFY(pCtrl->DestroyWindow());
	}
	return NPERR_NO_ERROR;
}

NPError NP_LOADDS NPP_NewStream(
	NPP instance, 
	NPMIMEType type,
    NPStream* stream, 
	NPBool seekable,
    uint16* stype)
{
	AFX_MANAGE_STATE(_afxModuleAddrThis);

	if (false
		|| strcmp(type, "image/tiff") == 0
		|| strcmp(type, "image/x-tiff") == 0
	) {
		TRY {
			*stype = NP_ASFILEONLY;
			return NPERR_NO_ERROR;
		}
		CATCH_ALL(e) {
		}
		END_CATCH_ALL;
	}

	return NPERR_INVALID_PLUGIN_ERROR;
}

NPError NP_LOADDS NPP_DestroyStream(
	NPP instance, 
	NPStream* stream,
    NPReason reason)
{
	return NPERR_NO_ERROR;
}

int32   NP_LOADDS NPP_WriteReady(
	NPP instance, 
	NPStream* stream)
{
	return 1000;
}

int32   NP_LOADDS NPP_Write(
	NPP instance, 
	NPStream* stream, 
	int32 offset,
    int32 len, 
	void* buffer)
{
	return NPERR_NO_ERROR;
}

void    NP_LOADDS NPP_StreamAsFile(
	NPP instance, 
	NPStream* stream,
    const char* fname)
{
	AFX_MANAGE_STATE(_afxModuleAddrThis);

	CAxTIF5Ctrl *pCtrl = reinterpret_cast<CAxTIF5Ctrl *>(instance->pdata);

	TRY {
		pCtrl->LoadFromFile(fname);
		pCtrl->Refresh();
		return;
	}
	CATCH_ALL(e) {
	}
	END_CATCH_ALL;

	return;
}

void    NP_LOADDS NPP_Print(
	NPP instance, 
	NPPrint* platformPrint)
{
}

int16   NP_LOADDS NPP_HandleEvent(
	NPP instance, 
	void* event)
{
	return 0;
}

void    NP_LOADDS NPP_URLNotify(
	NPP instance, 
	const char* url,
    NPReason reason, 
	void* notifyData)
{
}

NPError OSCALL NP_GetEntryPoints(NPPluginFuncs* pFuncs)
{
	if (pFuncs->size < sizeof(NPPluginFuncs))
		return NPERR_INVALID_FUNCTABLE_ERROR;

	pFuncs->version = (NP_VERSION_MAJOR << 8) | NP_VERSION_MINOR;
	pFuncs->newp		  = NPP_New;
	pFuncs->destroy       = NPP_Destroy;
	pFuncs->setwindow     = NPP_SetWindow; 
	pFuncs->newstream     = NPP_NewStream; 
	pFuncs->destroystream = NPP_DestroyStream;
	pFuncs->asfile        = NPP_StreamAsFile;
	pFuncs->writeready    = NPP_WriteReady; 
	pFuncs->write         = NPP_Write;
	pFuncs->print         = NPP_Print;
	pFuncs->event         = NULL;       /* reserved */

	return NPERR_NO_ERROR;
}

NPError OSCALL NP_Initialize(NPNetscapeFuncs* pFuncs)
{
	if (pFuncs->size < sizeof(NPNetscapeFuncs))
		return NPERR_INVALID_FUNCTABLE_ERROR;
	if (HIBYTE(pFuncs->version) > NP_VERSION_MAJOR)
		return NPERR_INCOMPATIBLE_VERSION_ERROR; 

	g_pNavigatorFuncs = pFuncs;

	return NPERR_NO_ERROR;
}

NPError OSCALL NP_Shutdown()
{
	g_pNavigatorFuncs = NULL;

	return NPERR_NO_ERROR;
}
