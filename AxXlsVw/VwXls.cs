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
    public partial class VwXls : UserControl {
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

            //LoadFile(@"file:///C:/A/Book1.xls");
            //LoadFile(@"C:\A\検査項目2004とSQL-xls　00119532.xls");
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
    }
}