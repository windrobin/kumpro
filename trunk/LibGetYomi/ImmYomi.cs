using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace LibGetYomi {
    [ComVisible(true)]
    [ClassInterface( ClassInterfaceType.AutoDual)]
    [Guid("be2a76c1-6d2b-4ced-85f0-b2732d8b3ee4")]
    public class ImmYomi {
        [DllImport("Imm32.dll")]
        static extern IntPtr ImmGetContext(IntPtr hWnd);
        [DllImport("Imm32.dll")]
        static extern IntPtr ImmReleaseContext(IntPtr hWnd, IntPtr context);

        [DllImport("user32.dll")]
        static extern IntPtr GetKeyboardLayout(uint idThread);
        [DllImport("Imm32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern int ImmGetConversionListW(
            IntPtr hKL,
            IntPtr hIMC,
            String lpSrc,
            IntPtr lpDst,
            int dwBufLen,
            int uFlag
            );

        class Win : IDisposable {
            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            static extern IntPtr CreateWindowEx(
               uint dwExStyle,
               string lpClassName,
               string lpWindowName,
               uint dwStyle,
               int x,
               int y,
               int nWidth,
               int nHeight,
               IntPtr hWndParent,
               IntPtr hMenu,
               IntPtr hInstance,
               IntPtr lpParam);

            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool DestroyWindow(IntPtr hwnd);

            IntPtr hwnd;

            public IntPtr Handle { get { return hwnd; } }

            public Win() {
                hwnd = CreateWindowEx(0, "EDIT", "LibGetYomi.ImmYomi.cs", 0, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            }

            ~Win() {
                Dispose();
            }

            #region IDisposable ƒƒ“ƒo

            public void Dispose() {
                if (hwnd != IntPtr.Zero) {
                    DestroyWindow(hwnd);
                    hwnd = IntPtr.Zero;
                }
            }

            #endregion
        }

        const int GCL_REVERSECONVERSION = 0x0002;

        public int BufferSize = 5000;

        public String[] GetYomi(String src) {
            using (Win win = new Win()) {
                IntPtr hIMC = ImmGetContext(win.Handle);
                try {
                    if (hIMC != IntPtr.Zero) {
                        IntPtr hKL = GetKeyboardLayout(0);
                        IntPtr pBuff = Marshal.AllocHGlobal(BufferSize);
                        try {
                            int cb = ImmGetConversionListW(hKL, hIMC, src, pBuff, BufferSize, GCL_REVERSECONVERSION);
                            if (cb > 0) {
                                byte[] buff = new byte[cb];
                                Marshal.Copy(pBuff, buff, 0, cb);
                                MemoryStream si = new MemoryStream(buff, false);
                                BinaryReader rr = new BinaryReader(si);
                                uint dwSize = rr.ReadUInt32();
                                uint dwStyle = rr.ReadUInt32();
                                uint dwCount = rr.ReadUInt32();
                                uint dwSelection = rr.ReadUInt32();
                                uint dwPageStart = rr.ReadUInt32();
                                uint dwPageSize = rr.ReadUInt32();
                                List<String> al = new List<string>();
                                for (uint x = 0; x < dwCount; x++) {
                                    int dwOffset = Convert.ToInt32(rr.ReadUInt32());
                                    int cx = 0;
                                    while (buff[dwOffset + cx] != 0 || buff[dwOffset + cx + 1] != 0) cx += 2;
                                    String s = Encoding.Unicode.GetString(buff, dwOffset, cx);
                                    al.Add(s);
                                }
                                return al.ToArray();
                            }
                        }
                        finally {
                            Marshal.FreeHGlobal(pBuff);
                        }
                    }
                    return new String[] { };
                }
                finally {
                    ImmReleaseContext(win.Handle, hIMC);
                }
            }
        }

    }
}
