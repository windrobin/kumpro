

/* this ALWAYS GENERATED file contains the proxy stub code */


 /* File created by MIDL compiler version 7.00.0555 */
/* at Wed Dec 14 11:18:06 2011
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

#if defined(_M_AMD64)


#pragma warning( disable: 4049 )  /* more than 64k source lines */
#if _MSC_VER >= 1200
#pragma warning(push)
#endif

#pragma warning( disable: 4211 )  /* redefine extern to static */
#pragma warning( disable: 4232 )  /* dllimport identity*/
#pragma warning( disable: 4024 )  /* array to pointer mapping*/
#pragma warning( disable: 4152 )  /* function/data pointer conversion in expression */

#define USE_STUBLESS_PROXY


/* verify that the <rpcproxy.h> version is high enough to compile this file*/
#ifndef __REDQ_RPCPROXY_H_VERSION__
#define __REQUIRED_RPCPROXY_H_VERSION__ 475
#endif


#include "rpcproxy.h"
#ifndef __RPCPROXY_H_VERSION__
#error this stub requires an updated version of <rpcproxy.h>
#endif /* __RPCPROXY_H_VERSION__ */


#include "MyFav.h"

#define TYPE_FORMAT_STRING_SIZE   3                                 
#define PROC_FORMAT_STRING_SIZE   1                                 
#define EXPR_FORMAT_STRING_SIZE   1                                 
#define TRANSMIT_AS_TABLE_SIZE    0            
#define WIRE_MARSHAL_TABLE_SIZE   0            

typedef struct _MyFav_MIDL_TYPE_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ TYPE_FORMAT_STRING_SIZE ];
    } MyFav_MIDL_TYPE_FORMAT_STRING;

typedef struct _MyFav_MIDL_PROC_FORMAT_STRING
    {
    short          Pad;
    unsigned char  Format[ PROC_FORMAT_STRING_SIZE ];
    } MyFav_MIDL_PROC_FORMAT_STRING;

typedef struct _MyFav_MIDL_EXPR_FORMAT_STRING
    {
    long          Pad;
    unsigned char  Format[ EXPR_FORMAT_STRING_SIZE ];
    } MyFav_MIDL_EXPR_FORMAT_STRING;


static const RPC_SYNTAX_IDENTIFIER  _RpcTransferSyntax = 
{{0x8A885D04,0x1CEB,0x11C9,{0x9F,0xE8,0x08,0x00,0x2B,0x10,0x48,0x60}},{2,0}};


extern const MyFav_MIDL_TYPE_FORMAT_STRING MyFav__MIDL_TypeFormatString;
extern const MyFav_MIDL_PROC_FORMAT_STRING MyFav__MIDL_ProcFormatString;
extern const MyFav_MIDL_EXPR_FORMAT_STRING MyFav__MIDL_ExprFormatString;


extern const MIDL_STUB_DESC Object_StubDesc;


extern const MIDL_SERVER_INFO ISf_ServerInfo;
extern const MIDL_STUBLESS_PROXY_INFO ISf_ProxyInfo;


extern const MIDL_STUB_DESC Object_StubDesc;


extern const MIDL_SERVER_INFO ISubf_ServerInfo;
extern const MIDL_STUBLESS_PROXY_INFO ISubf_ProxyInfo;



#if !defined(__RPC_WIN64__)
#error  Invalid build platform for this stub.
#endif

static const MyFav_MIDL_PROC_FORMAT_STRING MyFav__MIDL_ProcFormatString =
    {
        0,
        {

			0x0
        }
    };

static const MyFav_MIDL_TYPE_FORMAT_STRING MyFav__MIDL_TypeFormatString =
    {
        0,
        {
			NdrFcShort( 0x0 ),	/* 0 */

			0x0
        }
    };


/* Object interface: IUnknown, ver. 0.0,
   GUID={0x00000000,0x0000,0x0000,{0xC0,0x00,0x00,0x00,0x00,0x00,0x00,0x46}} */


/* Object interface: ISf, ver. 0.0,
   GUID={0xBD8F99EB,0xF753,0x42D8,{0xA9,0x85,0x53,0x17,0x18,0x71,0x04,0x09}} */

#pragma code_seg(".orpc")
static const unsigned short ISf_FormatStringOffsetTable[] =
    {
    0
    };

static const MIDL_STUBLESS_PROXY_INFO ISf_ProxyInfo =
    {
    &Object_StubDesc,
    MyFav__MIDL_ProcFormatString.Format,
    &ISf_FormatStringOffsetTable[-3],
    0,
    0,
    0
    };


static const MIDL_SERVER_INFO ISf_ServerInfo = 
    {
    &Object_StubDesc,
    0,
    MyFav__MIDL_ProcFormatString.Format,
    &ISf_FormatStringOffsetTable[-3],
    0,
    0,
    0,
    0};
CINTERFACE_PROXY_VTABLE(3) _ISfProxyVtbl = 
{
    0,
    &IID_ISf,
    IUnknown_QueryInterface_Proxy,
    IUnknown_AddRef_Proxy,
    IUnknown_Release_Proxy
};

const CInterfaceStubVtbl _ISfStubVtbl =
{
    &IID_ISf,
    &ISf_ServerInfo,
    3,
    0, /* pure interpreted */
    CStdStubBuffer_METHODS
};


/* Object interface: ISubf, ver. 0.0,
   GUID={0x56F18A76,0x3FE6,0x409B,{0x93,0x64,0x91,0x38,0xC8,0xD6,0xFA,0x05}} */

#pragma code_seg(".orpc")
static const unsigned short ISubf_FormatStringOffsetTable[] =
    {
    0
    };

static const MIDL_STUBLESS_PROXY_INFO ISubf_ProxyInfo =
    {
    &Object_StubDesc,
    MyFav__MIDL_ProcFormatString.Format,
    &ISubf_FormatStringOffsetTable[-3],
    0,
    0,
    0
    };


static const MIDL_SERVER_INFO ISubf_ServerInfo = 
    {
    &Object_StubDesc,
    0,
    MyFav__MIDL_ProcFormatString.Format,
    &ISubf_FormatStringOffsetTable[-3],
    0,
    0,
    0,
    0};
CINTERFACE_PROXY_VTABLE(3) _ISubfProxyVtbl = 
{
    0,
    &IID_ISubf,
    IUnknown_QueryInterface_Proxy,
    IUnknown_AddRef_Proxy,
    IUnknown_Release_Proxy
};

const CInterfaceStubVtbl _ISubfStubVtbl =
{
    &IID_ISubf,
    &ISubf_ServerInfo,
    3,
    0, /* pure interpreted */
    CStdStubBuffer_METHODS
};

static const MIDL_STUB_DESC Object_StubDesc = 
    {
    0,
    NdrOleAllocate,
    NdrOleFree,
    0,
    0,
    0,
    0,
    0,
    MyFav__MIDL_TypeFormatString.Format,
    1, /* -error bounds_check flag */
    0x50002, /* Ndr library version */
    0,
    0x700022b, /* MIDL Version 7.0.555 */
    0,
    0,
    0,  /* notify & notify_flag routine table */
    0x1, /* MIDL flag */
    0, /* cs routines */
    0,   /* proxy/server info */
    0
    };

const CInterfaceProxyVtbl * const _MyFav_ProxyVtblList[] = 
{
    ( CInterfaceProxyVtbl *) &_ISubfProxyVtbl,
    ( CInterfaceProxyVtbl *) &_ISfProxyVtbl,
    0
};

const CInterfaceStubVtbl * const _MyFav_StubVtblList[] = 
{
    ( CInterfaceStubVtbl *) &_ISubfStubVtbl,
    ( CInterfaceStubVtbl *) &_ISfStubVtbl,
    0
};

PCInterfaceName const _MyFav_InterfaceNamesList[] = 
{
    "ISubf",
    "ISf",
    0
};


#define _MyFav_CHECK_IID(n)	IID_GENERIC_CHECK_IID( _MyFav, pIID, n)

int __stdcall _MyFav_IID_Lookup( const IID * pIID, int * pIndex )
{
    IID_BS_LOOKUP_SETUP

    IID_BS_LOOKUP_INITIAL_TEST( _MyFav, 2, 1 )
    IID_BS_LOOKUP_RETURN_RESULT( _MyFav, 2, *pIndex )
    
}

const ExtendedProxyFileInfo MyFav_ProxyFileInfo = 
{
    (PCInterfaceProxyVtblList *) & _MyFav_ProxyVtblList,
    (PCInterfaceStubVtblList *) & _MyFav_StubVtblList,
    (const PCInterfaceName * ) & _MyFav_InterfaceNamesList,
    0, /* no delegation */
    & _MyFav_IID_Lookup, 
    2,
    2,
    0, /* table of [async_uuid] interfaces */
    0, /* Filler1 */
    0, /* Filler2 */
    0  /* Filler3 */
};
#if _MSC_VER >= 1200
#pragma warning(pop)
#endif


#endif /* defined(_M_AMD64)*/

