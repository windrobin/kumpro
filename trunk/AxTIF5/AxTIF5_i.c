

/* this ALWAYS GENERATED file contains the IIDs and CLSIDs */

/* link this file in with the server and any clients */


 /* File created by MIDL compiler version 6.00.0366 */
/* at Thu Jan 13 13:40:34 2011
 */
/* Compiler settings for .\AxTIF5.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


#ifdef __cplusplus
extern "C"{
#endif 


#include <rpc.h>
#include <rpcndr.h>

#ifdef _MIDL_USE_GUIDDEF_

#ifndef INITGUID
#define INITGUID
#include <guiddef.h>
#undef INITGUID
#else
#include <guiddef.h>
#endif

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        DEFINE_GUID(name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8)

#else // !_MIDL_USE_GUIDDEF_

#ifndef __IID_DEFINED__
#define __IID_DEFINED__

typedef struct _IID
{
    unsigned long x;
    unsigned short s1;
    unsigned short s2;
    unsigned char  c[8];
} IID;

#endif // __IID_DEFINED__

#ifndef CLSID_DEFINED
#define CLSID_DEFINED
typedef IID CLSID;
#endif // CLSID_DEFINED

#define MIDL_DEFINE_GUID(type,name,l,w1,w2,b1,b2,b3,b4,b5,b6,b7,b8) \
        const type name = {l,w1,w2,{b1,b2,b3,b4,b5,b6,b7,b8}}

#endif !_MIDL_USE_GUIDDEF_

MIDL_DEFINE_GUID(IID, LIBID_AxTIF5Lib,0x3C358B7C,0xA227,0x42C7,0xA2,0x26,0x89,0xC5,0xCD,0xD6,0x92,0xC6);


MIDL_DEFINE_GUID(IID, DIID__DAxTIF5,0x56EC6F0A,0x34F6,0x4D86,0x96,0x95,0x7B,0x1E,0x6F,0xB6,0x18,0x98);


MIDL_DEFINE_GUID(IID, DIID__DAxTIF5Events,0xCCBC4D8D,0x1D02,0x4CD2,0x9E,0xD0,0xF9,0x6B,0xDD,0x8C,0x4A,0x69);


MIDL_DEFINE_GUID(CLSID, CLSID_AxTIF5,0x05936E26,0x30E9,0x4210,0x84,0xA6,0xA0,0x59,0xB4,0x51,0x2D,0x14);

#undef MIDL_DEFINE_GUID

#ifdef __cplusplus
}
#endif



