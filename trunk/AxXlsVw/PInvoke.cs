using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

// Some interface definitions are taken from pinvoke.net

namespace AxXlsVw {
    [ComImport(), Guid("fc4801a1-2ba9-11cf-a229-00aa003d7352"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBindHost {
        void CreateMoniker(
            /* [in] */ String szName,
            /* [in] */ IBindCtx pBC,
            /* [out] */ out IMoniker ppmk,
            /* [in] */ UInt32 dwReserved);

        void MonikerBindToStorage(
            /* [in] */ IMoniker pMk,
            /* [in] */ IBindCtx pBC,
            /* [in] */ IBindStatusCallback pBSC,
            /* [in] */ Guid riid,
            /* [out] */ [Out, MarshalAs(UnmanagedType.IUnknown)] out Object ppvObj);

        void MonikerBindToObject(
            /* [in] */ IMoniker pMk,
            /* [in] */ IBindCtx pBC,
            /* [in] */ IBindStatusCallback pBSC,
            /* [in] */ Guid riid,
            /* [out] */ [Out, MarshalAs(UnmanagedType.IUnknown)] out Object ppvObj);
    }

    [ComImport()]
    [Guid("79eac9c1-baf9-11ce-8c82-00aa004ba90b")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBindStatusCallback {
    }

    [ComImport]
    [Guid("00000118-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleClientSite {
        void SaveObject();
        void GetMoniker(uint dwAssign, uint dwWhichMoniker, out IMoniker ppmk);
        void GetContainer(ref object ppContainer);
        void ShowObject();
        void OnShowWindow(bool fShow);
        void RequestNewObjectLayout();
    }
}
