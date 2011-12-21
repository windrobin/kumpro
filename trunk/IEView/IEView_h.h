

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0555 */
/* at Wed Dec 21 09:43:22 2011
 */
/* Compiler settings for .\IEView.idl:
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

#ifndef __IEView_h_h__
#define __IEView_h_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IIEView_FWD_DEFINED__
#define __IIEView_FWD_DEFINED__
typedef interface IIEView IIEView;
#endif 	/* __IIEView_FWD_DEFINED__ */


#ifndef __CDoc_FWD_DEFINED__
#define __CDoc_FWD_DEFINED__

#ifdef __cplusplus
typedef class CDoc CDoc;
#else
typedef struct CDoc CDoc;
#endif /* __cplusplus */

#endif 	/* __CDoc_FWD_DEFINED__ */


#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __IEView_LIBRARY_DEFINED__
#define __IEView_LIBRARY_DEFINED__

/* library IEView */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_IEView;

#ifndef __IIEView_DISPINTERFACE_DEFINED__
#define __IIEView_DISPINTERFACE_DEFINED__

/* dispinterface IIEView */
/* [uuid] */ 


EXTERN_C const IID DIID_IIEView;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("30057643-DF70-4AC3-BF00-1CE1192DF309")
    IIEView : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct IIEViewVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IIEView * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IIEView * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IIEView * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IIEView * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IIEView * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IIEView * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IIEView * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        END_INTERFACE
    } IIEViewVtbl;

    interface IIEView
    {
        CONST_VTBL struct IIEViewVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IIEView_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IIEView_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IIEView_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IIEView_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IIEView_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IIEView_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IIEView_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* __IIEView_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_CDoc;

#ifdef __cplusplus

class DECLSPEC_UUID("6B8B1B23-FE04-4308-999A-B5E7064E849F")
CDoc;
#endif
#endif /* __IEView_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


