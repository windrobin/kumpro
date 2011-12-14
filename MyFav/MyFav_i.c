

/* this ALWAYS GENERATED file contains the IIDs and CLSIDs */

/* link this file in with the server and any clients */


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

MIDL_DEFINE_GUID(IID, IID_ISf,0xBD8F99EB,0xF753,0x42D8,0xA9,0x85,0x53,0x17,0x18,0x71,0x04,0x09);


MIDL_DEFINE_GUID(IID, IID_ISubf,0x56F18A76,0x3FE6,0x409B,0x93,0x64,0x91,0x38,0xC8,0xD6,0xFA,0x05);


MIDL_DEFINE_GUID(IID, LIBID_MyFavLib,0x44063A3D,0x1429,0x4580,0x94,0xA8,0x68,0xD6,0xB5,0x79,0x32,0x90);


MIDL_DEFINE_GUID(CLSID, CLSID_Sf,0x2BC11758,0xC9EF,0x4E3B,0x90,0x5C,0xF4,0x12,0xA6,0x52,0x7B,0xF5);


MIDL_DEFINE_GUID(CLSID, CLSID_Subf,0x3F8AA1BB,0xC52A,0x4BAE,0xA6,0x35,0x74,0x4D,0x7C,0xEC,0xF9,0xD4);

#undef MIDL_DEFINE_GUID

#ifdef __cplusplus
}
#endif



