

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 6.00.0366 */
/* at Fri Feb 05 11:18:05 2010
 */
/* Compiler settings for .\Noime4ntr.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 440
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

#ifndef __Noime4ntr_h__
#define __Noime4ntr_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ICloser_FWD_DEFINED__
#define __ICloser_FWD_DEFINED__
typedef interface ICloser ICloser;
#endif 	/* __ICloser_FWD_DEFINED__ */


#ifndef __Closer_FWD_DEFINED__
#define __Closer_FWD_DEFINED__

#ifdef __cplusplus
typedef class Closer Closer;
#else
typedef struct Closer Closer;
#endif /* __cplusplus */

#endif 	/* __Closer_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 

void * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void * ); 

#ifndef __ICloser_INTERFACE_DEFINED__
#define __ICloser_INTERFACE_DEFINED__

/* interface ICloser */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ICloser;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("EFC4506F-AD56-4D56-B248-93FE07FB37CC")
    ICloser : public IDispatch
    {
    public:
        virtual /* [hidden][helpstring][id] */ HRESULT STDMETHODCALLTYPE NavigateComplete2( 
            /* [in] */ IDispatch *pDisp,
            /* [in] */ VARIANT *URL) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ICloserVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ICloser * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ICloser * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ICloser * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ICloser * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ICloser * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ICloser * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ICloser * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [hidden][helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *NavigateComplete2 )( 
            ICloser * This,
            /* [in] */ IDispatch *pDisp,
            /* [in] */ VARIANT *URL);
        
        END_INTERFACE
    } ICloserVtbl;

    interface ICloser
    {
        CONST_VTBL struct ICloserVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICloser_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ICloser_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ICloser_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ICloser_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ICloser_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ICloser_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ICloser_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ICloser_NavigateComplete2(This,pDisp,URL)	\
    (This)->lpVtbl -> NavigateComplete2(This,pDisp,URL)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [hidden][helpstring][id] */ HRESULT STDMETHODCALLTYPE ICloser_NavigateComplete2_Proxy( 
    ICloser * This,
    /* [in] */ IDispatch *pDisp,
    /* [in] */ VARIANT *URL);


void __RPC_STUB ICloser_NavigateComplete2_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ICloser_INTERFACE_DEFINED__ */



#ifndef __Noime4ntrLib_LIBRARY_DEFINED__
#define __Noime4ntrLib_LIBRARY_DEFINED__

/* library Noime4ntrLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_Noime4ntrLib;

EXTERN_C const CLSID CLSID_Closer;

#ifdef __cplusplus

class DECLSPEC_UUID("3A01783B-18FE-494F-AAEA-914B34CD5844")
Closer;
#endif
#endif /* __Noime4ntrLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  VARIANT_UserSize(     unsigned long *, unsigned long            , VARIANT * ); 
unsigned char * __RPC_USER  VARIANT_UserMarshal(  unsigned long *, unsigned char *, VARIANT * ); 
unsigned char * __RPC_USER  VARIANT_UserUnmarshal(unsigned long *, unsigned char *, VARIANT * ); 
void                      __RPC_USER  VARIANT_UserFree(     unsigned long *, VARIANT * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


