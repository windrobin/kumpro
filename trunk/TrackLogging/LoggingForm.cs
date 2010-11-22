using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using HDD.LibShellLink;
using System.Diagnostics;

namespace TrackLogging {
    public partial class LoggingForm : Form {
        public LoggingForm() {
            InitializeComponent();
        }
        public LoggingForm(String fp) {
            this.fp = fp;
            InitializeComponent();
        }

        private void sf1_Click(object sender, EventArgs e) {

        }

        String fp = @"C:\php.error.txt";
        Encoding enc = Encoding.Default;

        FileStream fs = null;
        StreamReader rr = null;

        private void LoggingForm_Load(object sender, EventArgs e) {
            fs = File.Open(fp, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            fs.Seek(0, SeekOrigin.End);
            rr = new StreamReader(fs, enc);
            this.Text = Path.GetFileName(fp) + " - " + this.Text;
            Fresh();
            timer1.Start();
        }

        class FWUt {
            // http://www.ecoop.net/memo/cat_2enet.html

            [DllImport("user32.dll")]
            private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

            public static void FlashWindow(System.Windows.Forms.Form window) {
                FlashWindow(window.Handle, false);
            }
        }

        int cx = 0;

        private void Fresh() {
            String row;
            int cx0 = tb1.TextLength;
            while (null != (row = rr.ReadLine())) {
                if (row != "")
                    tb1.AppendText(row.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n") + "\r\n");
            }
            int cx1 = tb1.TextLength;
            if (cx0 != cx1) {
                cx = Math.Min(cx, cx1);
                tb1.Select(cx, cx1 - cx);
                FWUt.FlashWindow(this);
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            try {
                Fresh();
            }
            catch (Exception) {
                timer1.Stop();
                throw;
            }
        }

        private void bClr_Click(object sender, EventArgs e) {
            tb1.Select(cx = tb1.TextLength, 0);
        }

        private void bErase_Click(object sender, EventArgs e) {
            cx = 0;
            tb1.Clear();
        }

        private void bCSC_Click(object sender, EventArgs e) {
            ShellLinkW o = new ShellLinkW();
            String exe = Path.GetFullPath(Environment.GetCommandLineArgs()[0]);
            o.path = exe;
            o.arguments = " \"" + fp + "\" \"" + enc.EncodingName + "\"";
            o.showCmd = SW.SHOWNORMAL;
            o.setIconLocation(exe, 0);
            String flnk = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "TL" + Path.GetFileNameWithoutExtension(fp) + ".lnk");
            o.save(flnk);
            Process.Start("explorer.exe", " /select,\"" + flnk + "\"");
        }
    }
}