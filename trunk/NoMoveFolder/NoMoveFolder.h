

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0555 */
/* at Tue Jul 19 16:50:54 2011
 */
/* Compiler settings for .\NoMoveFolder.idl:
    Oicf, W1, Zp8, env=Win64 (32b run), target_arch=AMD64 7.00.0555 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __NoMoveFolder_h__
#define __NoMoveFolder_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IStopMove_FWD_DEFINED__
#define __IStopMove_FWD_DEFINED__
typedef interface IStopMove IStopMove;
#endif 	/* __IStopMove_FWD_DEFINED__ */


#ifndef __StopMove_FWD_DEFINED__
#define __StopMove_FWD_DEFINED__

#ifdef __cplusplus
typedef class StopMove StopMove;
#else
typedef struct StopMove StopMove;
#endif /* __cplusplus */

#endif 	/* __StopMove_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IStopMove_INTERFACE_DEFINED__
#define __IStopMove_INTERFACE_DEFINED__

/* interface IStopMove */
/* [unique][helpstring][nonextensible][uuid][object] */ 


EXTERN_C const IID IID_IStopMove;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("E3D82BF9-AE47-4ED9-A09A-9B1733DC6D0E")
    IStopMove : public IUnknown
    {
    public:
    };
    
#else 	/* C style interface */

    typedef struct IStopMoveVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IStopMove * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IStopMove * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IStopMove * This);
        
        END_INTERFACE
    } IStopMoveVtbl;

    interface IStopMove
    {
        CONST_VTBL struct IStopMoveVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IStopMove_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IStopMove_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IStopMove_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IStopMove_INTERFACE_DEFINED__ */



#ifndef __NoMoveFolderLib_LIBRARY_DEFINED__
#define __NoMoveFolderLib_LIBRARY_DEFINED__

/* library NoMoveFolderLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_NoMoveFolderLib;

EXTERN_C const CLSID CLSID_StopMove;

#ifdef __cplusplus

class DECLSPEC_UUID("D0B183F6-10EA-443A-A428-8D433A318CD1")
StopMove;
#endif
#endif /* __NoMoveFolderLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


