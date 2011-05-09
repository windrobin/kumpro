using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using NPOI.HSSF.UserModel;
using System.Threading;
using AxXlsVw.Properties;
using System.Runtime.InteropServices.ComTypes;

/*
 * NPOI
Apache License
Version 2.0, January 2004
http://www.apache.org/licenses/

 */

namespace AxXlsVw {
    [ComVisible(true), Guid("73ef63ff-c109-40a8-bc58-0bd79e719a38")]
    public partial class VwXls : UserControl, IObjectSafety {
        public VwXls() {
            InitializeComponent();

            {
                FlowLayoutPanel flp = flperr = new FlowLayoutPanel();
                flp.Hide();
                flp.Dock = DockStyle.Fill;
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = Resources.eventlogError.ToBitmap();
                    pb.SizeMode = PictureBoxSizeMode.AutoSize;
                    flp.Controls.Add(pb);
                }
                {
                    Label la = laerr1 = new Label();
                    la.AutoSize = true;
                    flp.Controls.Add(la);
                    flp.SetFlowBreak(la, true);
                }
                {
                    Label la = laerr2 = new Label();
                    la.AutoSize = true;
                    flp.Controls.Add(la);
                }
                flp.Parent = this;
            }

            {
                TableLayoutPanel p = flpwip = new TableLayoutPanel();
                p.Hide();
                p.Dock = DockStyle.Fill;
                p.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                p.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                {
                    FlowLayoutPanel flp = new FlowLayoutPanel();
                    flp.Anchor = AnchorStyles.None;
                    flp.AutoSize = true;
                    flp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    flp.BackColor = Color.WhiteSmoke;
                    flp.ForeColor = Color.Black;
                    {
                        Label la = lawip = new Label();
                        la.AutoSize = true;
                        flp.Controls.Add(la);
                        flp.SetFlowBreak(la, true);
                    }
                    {
                        ProgressBar pb = new ProgressBar();
                        pb.Style = ProgressBarStyle.Marquee;
                        flp.Controls.Add(pb);
                    }
                    p.Controls.Add(flp);
                    flp.Show();
                }
                p.Parent = this;
            }

            Sync = SynchronizationContext.Current;
        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e) {

        }

        String navi = "";

        public String Src {
            get { return navi; }
            set { LoadFile(value); }
        }

        String title = "";

        public void LoadFile(String s) {
            if (s == null || s.Trim().Length == 0) {
                navi = "";
            }
            else if (Site == null) {
                bwLoader.RunWorkerAsync(s);
            }
            else {
                IOleClientSite pClientSite = (IOleClientSite)Site.GetService(new AntiMoniker().GetType());
                if (pClientSite != null) {
                    IMoniker pimkDL = null;
                    try {
                        if (pimkDL == null) {
                            IBindHost pBindHost = (IBindHost)pClientSite;
                            pBindHost.CreateMoniker(s, null, out pimkDL, 0);
                        }
                        if (pimkDL == null) {
                            IMoniker pimkName = null;
                            pClientSite.GetMoniker(OLEGETMONIKER_FORCEASSIGN, OLEWHICHMK_CONTAINER, out pimkName);
                            try {
                                int chEaten;
                                pimkName.ParseDisplayName(null, pimkName, s, out chEaten, out pimkDL);
                            }
                            finally {
                                Marshal.ReleaseComObject(pimkName);
                            }
                        }
                        if (pimkDL != null) {
                            pimkDL.GetDisplayName(null, null, out title);
                            if (!String.IsNullOrEmpty(title)) s = title;
                        }
                    }
                    finally {
                        Marshal.ReleaseComObject(pimkDL);
                    }
                }

                bwLoader.RunWorkerAsync(s);
            }
        }

        const uint OLEGETMONIKER_FORCEASSIGN = 2;

        const uint OLEWHICHMK_CONTAINER = 1;

        [ComImport(), Guid("00000305-0000-0000-C000-000000000046")]
        class AntiMoniker {
        }

        void CloseWIP() {
            foreach (Control child in Controls) child.Hide();

            tsc.Show();
        }

        private void SetWIP(String text) {
            foreach (Control child in Controls) child.Hide();

            lawip.Text = text;

            flpwip.Show();
        }

        private void SetErr(Exception exception) {
            foreach (Control child in Controls) child.Hide();

            String[] rows = exception.ToString().Replace("\r\n", "\n").Split('\n');

            laerr1.Text = rows[0];
            laerr2.Text = rows.Length >= 2 ? String.Join("\n", rows, 1, rows.Length - 1) : "";

            flperr.Show();
        }

        private void LoadIt(String fp, String src) {
            Sync.Send(delegate(object state) {
                SetWIP("読み込み中です。");
            }, null);

            using (FileStream fs = File.OpenRead(fp)) {
                HSSFWorkbook book = new HSSFWorkbook(fs);
                Sync.Send(delegate(object state) {
                    DisplayBook(book);
                    navi = src;
                }, null);
            }
        }

        void DisplayBook(HSSFWorkbook book) {
            ToolStripButton firstBtn = null;

            int cx = book.NumberOfSheets;
            for (int x = 0; x < cx; x++) {
                int si = x;
                ToolStripButton tsb = new ToolStripButton(book.GetSheetName(x));
                tsb.Click += delegate(object sender, EventArgs e) {
                    for (int t = 1; t < tss.Items.Count; t++)
                        ((ToolStripButton)tss.Items[t]).Checked = (tss.Items[t] == tsb);

                    ActivateSheet((NPOI.SS.UserModel.Sheet)book.GetSheetAt(si));
                };
                tss.Items.Add(tsb);

                firstBtn = firstBtn ?? tsb;
            }

            if (firstBtn != null)
                firstBtn.PerformClick();

            CloseWIP();
        }

        private void ActivateSheet(NPOI.SS.UserModel.Sheet sheet) {
            DataTable dt = new DataTable(sheet.SheetName);
            int maxCx = 0;
            int cy = sheet.PhysicalNumberOfRows;
            for (int y = 0; y < cy; y++) {
                NPOI.SS.UserModel.Row row = sheet.GetRow(y);
                if (row != null) {
                    int cx = row.PhysicalNumberOfCells;
                    maxCx = Math.Max(maxCx, row.FirstCellNum + cx);
                }
            }
            int maxCy = sheet.FirstRowNum + cy;

            for (int x = 0; x < maxCx; x++) {
                DataColumn col = dt.Columns.Add("C" + (1 + x), typeof(String));
            }
            for (int vy = 0; vy < maxCy; vy++) {
                DataRow dr = dt.NewRow();
                if (vy >= sheet.FirstRowNum) {
                    int y = vy - sheet.FirstRowNum;
                    NPOI.SS.UserModel.Row row = sheet.GetRow(y);
                    for (int vx = 0; vx < maxCx; vx++) {
                        dr[vx] = "";
                        if (row != null) {
                            if (vx >= row.FirstCellNum) {
                                int x = vx - row.FirstCellNum;
                                NPOI.SS.UserModel.Cell cell = row.GetCell(x);
                                dr[vx] = (cell != null) ? cell.ToString() : "";
                            }
                        }
                    }
                }
                dt.Rows.Add(dr);
            }

            gv.DataSource = dt;

            foreach (DataGridViewColumn col in gv.Columns) {
                col.ReadOnly = true;
            }

            gv.AutoResizeColumns();
            gv.AutoResizeRows();
        }

        TableLayoutPanel flpwip = null;
        Label lawip = null;
        FlowLayoutPanel flperr = null;
        Label laerr1 = null;
        Label laerr2 = null;

        private void VwXls_Load(object sender, EventArgs e) {
            Application.EnableVisualStyles();

#if DEBUG
            //LoadFile(@"file:///C:/A/Book1.xls");
            //LoadFile(@"C:\Proj\gc-chkiearea\trunk\chkiearea\f\動作チェック用テストデータ.xls");
            //LoadFile(@"http://www.google.co.jp/url?sa=t&source=web&cd=3&ved=0CEAQFjAC&url=http%3A%2F%2Fwww.eccj.or.jp%2Flaw06%2Fxls%2F03_00.xls&ei=Vn_HTZDGBYekuAOLt_CxAQ&usg=AFQjCNEpsAumW_rOJM-Vz38ha7lIZgvSsQ&sig2=e9Do4XcfPYtkrh2t2PB1zA");
#endif
        }

        private void bwLoader_DoWork(object sender, DoWorkEventArgs e) {
            String s = (String)e.Argument;

            if (File.Exists(s)) {
                LoadIt(s, s);
            }
            else {
                Sync.Send(delegate(object state) {
                    SetWIP("ダウンロード中です。しばらく、お待ちください。");
                }, null);
                WebClient wc = new WebClient();
                String fp = Path.GetTempFileName();
                try {
                    wc.DownloadFile(s, fp);
                }
                catch (Exception err) { throw new Exception("読み込みに失敗しました：" + s, err); }
                LoadIt(fp, s);
            }
        }

        SynchronizationContext Sync;

        private void bwLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Error != null) {
                SetErr(e.Error);
            }
        }

        #region IObjectSafety メンバ

        private const int INTERFACESAFE_FOR_UNTRUSTED_CALLER = 0x00000001;
        private const int INTERFACESAFE_FOR_UNTRUSTED_DATA = 0x00000002;
        private const int S_OK = 0;
        private const int E_FAIL = unchecked((int)0x80004005);
        private const int E_NOINTERFACE = unchecked((int)0x80004002);

        private readonly Guid IID_IDispatch = new Guid("{00020400-0000-0000-C000-000000000046}");
        private readonly Guid IID_IDispatchEx = new Guid("{a6ef9860-c720-11d0-9337-00a0c90dcaa9}");
        private readonly Guid IID_IPersistStorage = new Guid("{0000010A-0000-0000-C000-000000000046}");
        private readonly Guid IID_IPersistStream = new Guid("{00000109-0000-0000-C000-000000000046}");
        private readonly Guid IID_IPersistPropertyBag = new Guid("{37D84F60-42CB-11CE-8135-00AA004BB851}");

        private bool _fSafeForScripting = true;
        private bool _fSafeForInitializing = true;

        public int GetInterfaceSafetyOptions(ref Guid riid,
                             ref int pdwSupportedOptions,
                             ref int pdwEnabledOptions) {
            int Rslt = E_FAIL;

            pdwSupportedOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER | INTERFACESAFE_FOR_UNTRUSTED_DATA;
            if (riid == IID_IDispatch || riid == IID_IDispatchEx) {
                Rslt = S_OK;
                pdwEnabledOptions = 0;
                if (_fSafeForScripting == true)
                    pdwEnabledOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER;
            }
            else if (riid == IID_IPersistStorage || riid == IID_IPersistStream || riid == IID_IPersistPropertyBag) {
                Rslt = S_OK;
                pdwEnabledOptions = 0;
                if (_fSafeForInitializing == true)
                    pdwEnabledOptions = INTERFACESAFE_FOR_UNTRUSTED_DATA;
            }
            else {
                Rslt = E_NOINTERFACE;
            }

            return Rslt;
        }

        public int SetInterfaceSafetyOptions(ref Guid riid,
                             int dwOptionSetMask,
                             int dwEnabledOptions) {
            int Rslt = E_FAIL;

            if (riid == IID_IDispatch || riid == IID_IDispatchEx) {
                if (((dwEnabledOptions & dwOptionSetMask) == INTERFACESAFE_FOR_UNTRUSTED_CALLER) &&
                     (_fSafeForScripting == true))
                    Rslt = S_OK;
            }
            else if (riid == IID_IPersistStorage || riid == IID_IPersistStream || riid == IID_IPersistPropertyBag) {
                if (((dwEnabledOptions & dwOptionSetMask) == INTERFACESAFE_FOR_UNTRUSTED_DATA) &&
                     (_fSafeForInitializing == true))
                    Rslt = S_OK;
            }
            else {
                Rslt = E_NOINTERFACE;
            }

            return Rslt;
        }


        #endregion
    }

    [ComImport, GuidAttribute("CB5BDC81-93C1-11CF-8F20-00805F2CD064")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IObjectSafety {
        [PreserveSig]
        int GetInterfaceSafetyOptions(ref Guid riid,
                      [MarshalAs(UnmanagedType.U4)] ref int pdwSupportedOptions,
                      [MarshalAs(UnmanagedType.U4)] ref int pdwEnabledOptions);

        [PreserveSig()]
        int SetInterfaceSafetyOptions(ref Guid riid,
                      [MarshalAs(UnmanagedType.U4)] int dwOptionSetMask,
                      [MarshalAs(UnmanagedType.U4)] int dwEnabledOptions);
    }
}