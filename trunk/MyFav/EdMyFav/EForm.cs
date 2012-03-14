using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;

namespace EdMyFav {
    public partial class EForm : Form {
        public EForm() {
            InitializeComponent();
        }

        delegate int BrowseCallbackProc(IntPtr hwnd, int msg, IntPtr lp, IntPtr wp);

        [StructLayout(LayoutKind.Sequential)]
        struct BROWSEINFO {
            public IntPtr hwndOwner;
            public IntPtr pidlRoot;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszDisplayName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszTitle;
            public uint ulFlags;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public BrowseCallbackProc lpfn;
            public IntPtr lParam;
            public int iImage;
        }

        [DllImport("shell32.dll")]
        static extern IntPtr SHBrowseForFolder(ref BROWSEINFO lpbi);

        private void bNew_Click(object sender, EventArgs e) {
            BROWSEINFO bi = new BROWSEINFO();
            bi.hwndOwner = Handle;
            bi.pszDisplayName = "フォルダを選んでください：";

            IntPtr pidl = SHBrowseForFolder(ref bi);
        }

        private void EForm_Load(object sender, EventArgs e) {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\HIRAOKA HYPERS TOOLS, Inc.\MyFav\Favorites");
            foreach (String name in rk.GetValueNames()) {
                String dir = rk.GetValue(name) as String;
                if (dir != null) {
                    tbDirs.AppendText(dir + "\r\n");
                }
            }

            bwNSE.RunWorkerAsync();
            bwAD.RunWorkerAsync();
        }

        private void bSave_Click(object sender, EventArgs e) {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\HIRAOKA HYPERS TOOLS, Inc.\MyFav\Favorites");
            foreach (String name in rk.GetValueNames()) {
                rk.DeleteValue(name);
            }
            int i = 0;
            foreach (String dir in tbDirs.Lines) {
                if (dir.Trim().Length == 0) continue;
                i++;
                rk.SetValue("Dir" + i.ToString("000"), dir);
            }
            MessageBox.Show(this, "保存しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bwNSE_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Result is IEnumerable<string>) {
                AddPCs((IEnumerable<string>)e.Result);
            }

            flpwipNSE.Visible = bwNSE.IsBusy || bwAD.IsBusy;
        }

        private void AddPCs(IEnumerable<string> iEnumerable) {
            foreach (String pc in (IEnumerable<string>)iEnumerable) {
                if (lvPC.Items.IndexOfKey(pc) >= 0) continue;
                ListViewItem lvi = new ListViewItem(pc);
                lvi.ImageKey = "PC";
                lvi.Name = pc;
                lvPC.Items.Add(lvi);
            }
        }

        private void bwNSE_DoWork(object sender, DoWorkEventArgs e) {
            IntPtr res;
            int n = 0, m = 0;
            int r = NetServerEnum(null, 101, out res, MAX_PREFERRED_LENGTH, ref n, ref m, SV_101_TYPES.SV_TYPE_WORKSTATION | SV_101_TYPES.SV_TYPE_SERVER | SV_101_TYPES.SV_TYPE_DOMAIN_MASTER, null, IntPtr.Zero);
            if (r == 0) {
                List<string> pcs = new List<string>();
                try {
                    for (int x = 0; x < n; x++) {
                        SERVER_INFO_101 pc = (SERVER_INFO_101)Marshal.PtrToStructure(new IntPtr(res.ToInt64() + Marshal.SizeOf(typeof(SERVER_INFO_101)) * x), typeof(SERVER_INFO_101));
                        pcs.Add(pc.sv101_name);
                    }
                    e.Result = pcs;
                }
                finally {
                    NetApiBufferFree(res);
                }
            }
        }

        [DllImport("netapi32.dll", EntryPoint = "NetServerEnum")]
        public static extern int NetServerEnum(
            [MarshalAs(UnmanagedType.LPWStr)]string servername,
            int level,
            out IntPtr bufptr,
            int prefmaxlen,
            ref int entriesread,
            ref int totalentries,
            SV_101_TYPES servertype,
            [MarshalAs(UnmanagedType.LPWStr)]string domain,
            IntPtr resume_handle);

        [DllImport("netapi32.dll", EntryPoint = "NetApiBufferFree")]
        public static extern int NetApiBufferFree(IntPtr buffer);

        public enum SV_101_TYPES : uint {
            SV_TYPE_WORKSTATION = 0x00000001,
            SV_TYPE_SERVER = 0x00000002,
            SV_TYPE_SQLSERVER = 0x00000004,
            SV_TYPE_DOMAIN_CTRL = 0x00000008,
            SV_TYPE_DOMAIN_BAKCTRL = 0x00000010,
            SV_TYPE_TIME_SOURCE = 0x00000020,
            SV_TYPE_AFP = 0x00000040,
            SV_TYPE_NOVELL = 0x00000080,
            SV_TYPE_DOMAIN_MEMBER = 0x00000100,
            SV_TYPE_PRINTQ_SERVER = 0x00000200,
            SV_TYPE_DIALIN_SERVER = 0x00000400,
            SV_TYPE_XENIX_SERVER = 0x00000800,
            SV_TYPE_SERVER_UNIX = 0x00000800,
            SV_TYPE_NT = 0x00001000,
            SV_TYPE_WFW = 0x00002000,
            SV_TYPE_SERVER_MFPN = 0x00004000,
            SV_TYPE_SERVER_NT = 0x00008000,
            SV_TYPE_POTENTIAL_BROWSER = 0x00010000,
            SV_TYPE_BACKUP_BROWSER = 0x00020000,
            SV_TYPE_MASTER_BROWSER = 0x00040000,
            SV_TYPE_DOMAIN_MASTER = 0x00080000,
            SV_TYPE_SERVER_OSF = 0x00100000,
            SV_TYPE_SERVER_VMS = 0x00200000,
            SV_TYPE_WINDOWS = 0x00400000,
            SV_TYPE_DFS = 0x00800000,
            SV_TYPE_CLUSTER_NT = 0x01000000,
            SV_TYPE_TERMINALSERVER = 0x02000000,
            SV_TYPE_CLUSTER_VS_NT = 0x04000000,
            SV_TYPE_DCE = 0x10000000,
            SV_TYPE_ALTERNATE_XPORT = 0x20000000,
            SV_TYPE_LOCAL_LIST_ONLY = 0x40000000,
            SV_TYPE_DOMAIN_ENUM = 0x80000000,
            SV_TYPE_ALL = 0xFFFFFFFF
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct SERVER_INFO_101 {
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
            public UInt32 sv101_platform_id;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public string sv101_name;

            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
            public UInt32 sv101_version_major;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
            public UInt32 sv101_version_minor;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
            public UInt32 sv101_type;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public string sv101_comment;
        };
        public enum PLATFORM_ID {
            PLATFORM_ID_DOS = 300,
            PLATFORM_ID_OS2 = 400,
            PLATFORM_ID_NT = 500,
            PLATFORM_ID_OSF = 600,
            PLATFORM_ID_VMS = 700
        }

        private void lvPC_ItemActivate(object sender, EventArgs e) {
            if (!tbDirs.Text.EndsWith("\r\n")) tbDirs.AppendText("\r\n");
            foreach (ListViewItem lvi in lvPC.SelectedItems) {
                tbDirs.AppendText("\\\\" + lvi.Text + "\r\n");
                return;
            }

            MessageBox.Show(this, "コンピュータを選択してください。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bwAD_DoWork(object sender, DoWorkEventArgs e) {
            // http://channel9.msdn.com/Forums/TechOff/Computer-names-on-network-c
            List<String> _ComputerNames = new List<String>();
            String _ComputerSchema = "Computer";
            System.DirectoryServices.DirectoryEntry _WinNTDirectoryEntries = new System.DirectoryServices.DirectoryEntry("WinNT:");
            foreach (System.DirectoryServices.DirectoryEntry _AvailDomains in _WinNTDirectoryEntries.Children) {
                foreach (System.DirectoryServices.DirectoryEntry _PCNameEntry in _AvailDomains.Children) {
                    if (_PCNameEntry.SchemaClassName.ToLower().Contains(_ComputerSchema.ToLower())) {
                        _ComputerNames.Add(_PCNameEntry.Name);
                    }
                }
            }

            e.Result = _ComputerNames;
        }

        private void bwAD_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Result is IEnumerable<string>) {
                AddPCs((IEnumerable<string>)e.Result);
            }

            flpwipNSE.Visible = bwNSE.IsBusy || bwAD.IsBusy;
        }

        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode)]
        private static extern int NetShareEnum(
             StringBuilder ServerName,
             int level,
             ref IntPtr bufPtr,
             int prefmaxlen,
             ref int entriesread,
             ref int totalentries,
             IntPtr resume_handle
             );

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHARE_INFO_0 {
            public string shi0_netname;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHARE_INFO_1 {
            public string shi1_netname;
            [MarshalAs(UnmanagedType.U4)]
            public SHARE_TYPE shi1_type;
            public string shi1_remark;
        }

        public enum SHARE_TYPE : uint {
            STYPE_DISKTREE = 0,
            STYPE_PRINTQ = 1,
            STYPE_DEVICE = 2,
            STYPE_IPC = 3,
            STYPE_SPECIAL = 0x80000000,
        }


        const int MAX_PREFERRED_LENGTH = -1;
        const int NERR_Success = 0;

        private void bAddPC_Click(object sender, EventArgs e) {
            lvPC_ItemActivate(sender, e);
        }

        private void bEnum_Click(object sender, EventArgs e) {
            if (bwShare.IsBusy) {
                MessageBox.Show(this, "前の検索が終わるまでお待ちください。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (ListViewItem lvi in lvPC.SelectedItems) {
                bwShare.RunWorkerAsync(lvi.Text);
                flpwipShare.Show();
                return;
            }
            MessageBox.Show(this, "コンピュータを選択してください。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bwShare_DoWork(object sender, DoWorkEventArgs e) {
            String pc = (String)e.Argument;

            IntPtr res = IntPtr.Zero;
            int n = 0, m = 0;
            int r = NetShareEnum(new StringBuilder("\\\\" + pc), 1, ref res, MAX_PREFERRED_LENGTH, ref n, ref m, IntPtr.Zero);
            if (r == 0) {
                try {
                    List<string> shares = new List<string>();
                    for (int x = 0; x < n; x++) {
                        SHARE_INFO_1 share = (SHARE_INFO_1)Marshal.PtrToStructure(new IntPtr(res.ToInt64() + Marshal.SizeOf(typeof(SHARE_INFO_1)) * x), typeof(SHARE_INFO_1));
                        if (share.shi1_type == SHARE_TYPE.STYPE_DISKTREE)
                            shares.Add("\\\\" + pc + "\\" + share.shi1_netname);
                    }
                    e.Result = shares;
                }
                finally {
                    NetApiBufferFree(res);
                }
            }
        }

        private void bwShare_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            flpwipShare.Hide();

            lvDir.Items.Clear();

            IEnumerable<string> shares = e.Result as IEnumerable<string>;
            if (shares != null) {
                foreach (string s in shares) {
                    ListViewItem lvi = new ListViewItem(s);
                    lvi.ImageKey = "Dir";
                    lvDir.Items.Add(lvi);
                }
            }
        }

        private void lvDir_ItemActivate(object sender, EventArgs e) {
            foreach (ListViewItem lvi in lvDir.SelectedItems) {
                tbDirs.AppendText(lvi.Text + "\r\n");
            }
        }

        private void tbDirs_DragEnter(object sender, DragEventArgs e) {
            e.Effect = e.AllowedEffect & (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None);
        }

        private void tbDirs_DragDrop(object sender, DragEventArgs e) {
            String[] alfp = e.Data.GetData(DataFormats.FileDrop) as String[];
            if (!tbDirs.Text.EndsWith("\r\n")) tbDirs.AppendText("\r\n");
            foreach (String fp in alfp) {
                if (Directory.Exists(fp))
                    tbDirs.AppendText(fp + "\r\n");
            }
        }
    }
}