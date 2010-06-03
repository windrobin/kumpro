using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using KU;

namespace HelpInstAlternatiff {
    public partial class AForm : Form {
        public AForm() {
            InitializeComponent();
        }

        private void bInst_Click(object sender, EventArgs e) {
            wb.Navigate("http://www.alternatiff.com/install-ie/");
        }

        private void bTest_Click(object sender, EventArgs e) {
            wb.Navigate("http://www.alternatiff.com/testpage.html");
        }

        private void bReact_Click(object sender, EventArgs e) {
            wb.Navigate("http://www.alternatiff.com/install-ie/reinstall.html");
        }

        // http://blogs.msdn.com/b/tsmatsuz/archive/2007/01/25/windows-vista-uac-part-2.aspx
        private bool IsAdmin() {
            WindowsIdentity usrId = WindowsIdentity.GetCurrent();
            WindowsPrincipal p = new WindowsPrincipal(usrId);
            return p.IsInRole(@"BUILTIN\Administrators");
        }

        private void AForm_Load(object sender, EventArgs e) {
            try {
                tate();
            }
            catch (Exception) {
            }

            try {
                tsslUACRaised.Text = UtVistaToken.IsElevated() ? "è∏äiçœÇ›" : "è∏äiÇµÇƒÇ¢Ç»Ç¢";
            }
            catch (NotSupportedException) {
                tsslUACRaised.Text = "ñ¢ëŒâû";
            }

            try {
                tsslIL.Text = UtVistaToken.GetIntegrityLevel().ToString();
            }
            catch (NotSupportedException) {
                tsslIL.Text = "ñ¢ëŒâû";
            }

            try {
                tsslIsAdmin.Text = IsAdmin() ? "ÇÕÇ¢" : "Ç¢Ç¢Ç¶";
            }
            catch (NotSupportedException) {
                tsslIsAdmin.Text = "ñ¢ëŒâû";
            }
        }

        class IUt {
            public static Bitmap Thumbnail(Image sizeSrc, Icon o) {
                int cx = sizeSrc.Width;
                int cy = sizeSrc.Height;
                Bitmap p = new Bitmap(cx, cy);
                using (Graphics cv = Graphics.FromImage(p)) {
                    cv.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    cv.DrawIcon(o, new Rectangle(0, 0, cx, cy));
                }
                return p;
            }
        }

        private void tate() {
            //bRaise.Image = IUt.Thumbnail(bRaise.Image, SystemIcons.Shield);
            bRaise.Image = new Icon(SystemIcons.Shield, bRaise.Image.Size).ToBitmap();
        }

        private void bRaise_Click(object sender, EventArgs e) {
            Process p = new Process();
            p.StartInfo.FileName = Assembly.GetExecutingAssembly().CodeBase;
            p.StartInfo.Verb = "runas";
            p.StartInfo.UseShellExecute = true;
            p.Start();
            Application.Exit();
        }


    }
}