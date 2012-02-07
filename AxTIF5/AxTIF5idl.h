

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0555 */
/* at Tue Feb 07 10:45:09 2012
 */
/* Compiler settings for .\AxTIF5.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 7.00.0555 
    protocol : dce , ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 440
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __AxTIF5idl_h__
#define __AxTIF5idl_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef ___DAxTIF5_FWD_DEFINED__
#define ___DAxTIF5_FWD_DEFINED__
typedef interface _DAxTIF5 _DAxTIF5;
#endif 	/* ___DAxTIF5_FWD_DEFINED__ */


#ifndef ___DAxTIF5Events_FWD_DEFINED__
#define ___DAxTIF5Events_FWD_DEFINED__
typedef interface _DAxTIF5Events _DAxTIF5Events;
#endif 	/* ___DAxTIF5Events_FWD_DEFINED__ */


#ifndef __AxTIF5_FWD_DEFINED__
#define __AxTIF5_FWD_DEFINED__

#ifdef __cplusplus
typedef class AxTIF5 AxTIF5;
#else
typedef struct AxTIF5 AxTIF5;
#endif /* __cplusplus */

#endif 	/* __AxTIF5_FWD_DEFINED__ */


#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __AxTIF5Lib_LIBRARY_DEFINED__
#define __AxTIF5Lib_LIBRARY_DEFINED__

/* library AxTIF5Lib */
/* [control][helpstring][helpfile][version][uuid] */ 


EXTERN_C const IID LIBID_AxTIF5Lib;

#ifndef ___DAxTIF5_DISPINTERFACE_DEFINED__
#define ___DAxTIF5_DISPINTERFACE_DEFINED__

/* dispinterface _DAxTIF5 */
/* [helpstring][uuid] */ 


EXTERN_C const IID DIID__DAxTIF5;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("56EC6F0A-34F6-4D86-9695-7B1E6FB61898")
    _DAxTIF5 : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _DAxTIF5Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            _DAxTIF5 * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            _DAxTIF5 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            _DAxTIF5 * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            _DAxTIF5 * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            _DAxTIF5 * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            _DAxTIF5 * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            _DAxTIF5 * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        END_INTERFACE
    } _DAxTIF5Vtbl;

    interface _DAxTIF5
    {
        CONST_VTBL struct _DAxTIF5Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _DAxTIF5_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define _DAxTIF5_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define _DAxTIF5_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define _DAxTIF5_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define _DAxTIF5_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define _DAxTIF5_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define _DAxTIF5_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___DAxTIF5_DISPINTERFACE_DEFINED__ */


#ifndef ___DAxTIF5Events_DISPINTERFACE_DEFINED__
#define ___DAxTIF5Events_DISPINTERFACE_DEFINED__

/* dispinterface _DAxTIF5Events */
/* [helpstring][uuid] */ 


EXTERN_C const IID DIID__DAxTIF5Events;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("CCBC4D8D-1D02-4CD2-9ED0-F96BDD8C4A69")
    _DAxTIF5Events : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _DAxTIF5EventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            _DAxTIF5Events * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            _DAxTIF5Events * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            _DAxTIF5Events * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            _DAxTIF5Events * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            _DAxTIF5Events * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            _DAxTIF5Events * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            _DAxTIF5Events * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        END_INTERFACE
    } _DAxTIF5EventsVtbl;

    interface _DAxTIF5Events
    {
        CONST_VTBL struct _DAxTIF5EventsVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _DAxTIF5Events_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define _DAxTIF5Events_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define _DAxTIF5Events_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define _DAxTIF5Events_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define _DAxTIF5Events_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define _DAxTIF5Events_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define _DAxTIF5Events_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___DAxTIF5Events_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_AxTIF5;

#ifdef __cplusplus

class DECLSPEC_UUID("05936E26-30E9-4210-84A6-A059B4512D14")
AxTIF5;
#endif
#endif /* __AxTIF5Lib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


