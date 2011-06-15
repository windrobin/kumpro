using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Check64BITS {
    public partial class BForm : Form {
        public BForm() {
            InitializeComponent();
        }

        private void BForm_Load(object sender, EventArgs e) {
#if AnyCPU
            this.Text += "Any CPU";
#elif x64
            this.Text += "x64";
#elif x86
            this.Text += "x86";
#endif
            Test();
        }

        private void Test() {
            label16.Text = Environment.OSVersion.ToString();

            {
                Label la = label2;
                EP.SetError(la, null);
                try {
                    la.Text = new UseIsWow64Process().Is64BitOS() ? "64" : "32";
                }
                catch (Exception err) { EP.SetError(la, "失敗: " + err); }
            }

            {
                Label la = label4;
                EP.SetError(la, null);
                try {
                    la.Text = new UsePROCESSOR_ARCHITECTURE().IsOS64() ? "64" : "32";
                }
                catch (Exception err) { EP.SetError(la, "失敗: " + err); }
            }

            {
                Label la = label6;
                EP.SetError(la, null);
                try {
                    la.Text = new UseWMI().GetOSBits();
                }
                catch (Exception err) { EP.SetError(la, "失敗: " + err); }
            }

            {
                Label la = label9;
                EP.SetError(la, null);
                try {
                    la.Text = new UseGetSystemWow64Directory().Is64OS() ? "64" : "32";
                }
                catch (Exception err) { EP.SetError(la, "失敗: " + err); }
            }

            {
                Label la = label11;
                EP.SetError(la, null);
                try {
                    la.Text = new UseGetNativeSystemInfo().GetOSBits();
                }
                catch (Exception err) { EP.SetError(la, "失敗: " + err); }
            }

            {
                Label la = label13;
                EP.SetError(la, null);
                try {
                    la.Text = new UseIntPtr().GetOSBits();
                }
                catch (Exception err) { EP.SetError(la, "失敗: " + err); }
            }
        }

        class UseIntPtr {
            public string GetOSBits() {
                if (IntPtr.Size == 4) {
                    return "32";
                }
                else if (IntPtr.Size == 8) {
                    return "64";
                }
                return "" + (IntPtr.Size / 8.0);
            }
        }

        class UseGetNativeSystemInfo {
            [StructLayout(LayoutKind.Sequential)]
            internal struct SYSTEM_INFO {
                public ushort wProcessorArchitecture;
                public ushort wReserved;
                public uint dwPageSize;
                public IntPtr lpMinimumApplicationAddress;
                public IntPtr lpMaximumApplicationAddress;
                public UIntPtr dwActiveProcessorMask;
                public uint dwNumberOfProcessors;
                public uint dwProcessorType;
                public uint dwAllocationGranularity;
                public ushort wProcessorLevel;
                public ushort wProcessorRevision;
            };

            [DllImport("kernel32.dll")]
            internal static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);

            public String GetOSBits() {
                SYSTEM_INFO sysInfo = new SYSTEM_INFO();

                GetNativeSystemInfo(ref sysInfo);

                switch (sysInfo.wProcessorArchitecture) {
                    case 9:
                        return "64";
                    case 6:
                        return "ia64";
                    case 0:
                        return "32";
                }

                return "?";
            }
        }

        class UseGetSystemWow64Directory {
            [DllImport("kernel32.dll", SetLastError = true)]
            static extern int GetSystemWow64Directory([In, Out] char[] lpBuffer, [MarshalAs(UnmanagedType.U4)] uint size);

            public bool Is64OS() {
                try {
                    char[] path = new char[256];
                    GetSystemWow64Directory(path, 256u);
                    if (Marshal.GetLastWin32Error() != 0)
                        return false;
                    return true;
                }
                catch (Exception) {
                    return false;
                }
            }
        }

        class UseWMI {
            public String GetOSBits() {
                using (System.Management.ManagementObject mo =
                    new System.Management.ManagementObject("Win32_Processor.DeviceID='CPU0'")) {
                    ushort procArch = (ushort)mo["Architecture"];
                    if (procArch == 0) {
                        //x86
                        return "32";
                    }
                    else if (procArch == 9) {
                        //x64
                        return "64";
                    }
                    else if (procArch == 6) {
                        return "ia64";
                    }
                    return "?";
                }
            }
        }

        class UsePROCESSOR_ARCHITECTURE {
            public bool IsOS64() {
                //現在のプロセスにおける環境変数"PROCESSOR_ARCHITECTURE"の値を取得
                string procArch = System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");

                if (procArch == null || procArch == "x86") {
                    //環境変数"PROCESSOR_ARCHITEW6432"の値を取得
                    string pa6432 = System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432");
                    if (pa6432 == null || pa6432 == "x86") {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else {
                    return true;
                }
            }

        }

        class UseIsWow64Process {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr GetModuleHandle(string lpModuleName);

            [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true)]
            private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

            [DllImport("kernel32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool lpSystemInfo);

            //現在のプロセスがWOW32上で動作しているか調べる
            public bool IsWow64() {
                //IsWow64Processが使えるか調べる
                IntPtr wow64Proc = GetProcAddress(GetModuleHandle("Kernel32.dll"), "IsWow64Process");
                if (wow64Proc != IntPtr.Zero) {
                    //IsWow64Processを呼び出す
                    bool ret;
                    if (IsWow64Process(System.Diagnostics.Process.GetCurrentProcess().Handle, out ret)) {
                        return ret;
                    }
                }

                return false;
            }

            //OSが64ビットか調べる
            public bool Is64BitOS() {
                if (IntPtr.Size == 4) {
                    if (IsWow64()) {
                        //OSは64ビットです
                        return true;
                    }
                    else {
                        //OSは32ビットです
                        return false;
                    }
                }
                else if (IntPtr.Size == 8) {
                    //OSは64ビットです
                    return true;
                }

                return false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            String url = ttLink.GetToolTip((Control)sender);
            if (url != null) Process.Start(url);
        }
    }
}