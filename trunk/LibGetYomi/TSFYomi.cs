using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LibGetYomi {
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [Guid("a3811b44-8c5d-42a8-bbe0-c5ad0268cc14")]
    public class TSFYomi {
        [Guid("019F7152-E6DB-11D0-83C3-00C04FDDB82E")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IFELanguage {
            void Open();
            void Close();
            void GetJMorphResult();
            void GetConversionModeCaps();
            void GetPhonetic(
                [MarshalAs(UnmanagedType.BStr)] String str,
                [MarshalAs(UnmanagedType.I4)] int start,
                [MarshalAs(UnmanagedType.I4)] int length,
                out IntPtr phonetic
                );
        }

        public String ProgID = "MSIME.Japan";

        public String GetYomi(String src) {
            IFELanguage fel = (IFELanguage)Activator.CreateInstance(Type.GetTypeFromProgID(ProgID));
            fel.Open();
            try {
                IntPtr phonetic;
                fel.GetPhonetic(src, 1, -1, out phonetic);
                try {
                    return Marshal.PtrToStringBSTR(phonetic);
                }
                finally {
                    Marshal.FreeBSTR(phonetic);
                }
            }
            finally {
                fel.Close();
            }
        }
    }
}
