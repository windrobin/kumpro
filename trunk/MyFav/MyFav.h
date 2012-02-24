

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0555 */
/* at Fri Feb 24 11:22:07 2012
 */
/* Compiler settings for .\MyFav.idl:
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

#ifndef __MyFav_h__
#define __MyFav_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ISf_FWD_DEFINED__
#define __ISf_FWD_DEFINED__
typedef interface ISf ISf;
#endif 	/* __ISf_FWD_DEFINED__ */


#ifndef __ISubf_FWD_DEFINED__
#define __ISubf_FWD_DEFINED__
typedef interface ISubf ISubf;
#endif 	/* __ISubf_FWD_DEFINED__ */


#ifndef __Sf_FWD_DEFINED__
#define __Sf_FWD_DEFINED__

#ifdef __cplusplus
typedef class Sf Sf;
#else
typedef struct Sf Sf;
#endif /* __cplusplus */

#endif 	/* __Sf_FWD_DEFINED__ */


#ifndef __Subf_FWD_DEFINED__
#define __Subf_FWD_DEFINED__

#ifdef __cplusplus
typedef class Subf Subf;
#else
typedef struct Subf Subf;
#endif /* __cplusplus */

#endif 	/* __Subf_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"
#include "shobjidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __ISf_INTERFACE_DEFINED__
#define __ISf_INTERFACE_DEFINED__

/* interface ISf */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ISf;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("BD8F99EB-F753-42D8-A985-531718710409")
    ISf : public IUnknown
    {
    public:
    };
    
#else 	/* C style interface */

    typedef struct ISfVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISf * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISf * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISf * This);
        
        END_INTERFACE
    } ISfVtbl;

    interface ISf
    {
        CONST_VTBL struct ISfVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISf_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISf_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISf_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISf_INTERFACE_DEFINED__ */


#ifndef __ISubf_INTERFACE_DEFINED__
#define __ISubf_INTERFACE_DEFINED__

/* interface ISubf */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ISubf;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("56F18A76-3FE6-409B-9364-9138C8D6FA05")
    ISubf : public IUnknown
    {
    public:
    };
    
#else 	/* C style interface */

    typedef struct ISubfVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISubf * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISubf * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISubf * This);
        
        END_INTERFACE
    } ISubfVtbl;

    interface ISubf
    {
        CONST_VTBL struct ISubfVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISubf_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISubf_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISubf_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISubf_INTERFACE_DEFINED__ */



#ifndef __MyFavLib_LIBRARY_DEFINED__
#define __MyFavLib_LIBRARY_DEFINED__

/* library MyFavLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_MyFavLib;

EXTERN_C const CLSID CLSID_Sf;

#ifdef __cplusplus

class DECLSPEC_UUID("2BC11758-C9EF-4E3B-905C-F412A6527BF5")
Sf;
#endif

EXTERN_C const CLSID CLSID_Subf;

#ifdef __cplusplus

class DECLSPEC_UUID("3F8AA1BB-C52A-4BAE-A635-744D7CECF9D4")
Subf;
#endif
#endif /* __MyFavLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


