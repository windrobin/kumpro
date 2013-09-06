using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using AFPt2;
using System.Diagnostics;
using System.Globalization;

namespace FTP4AFP {
    public partial class Form1 : Form {
        public Form1(IList<string> als) {
            InitializeComponent();

            for (int x = 0; x < 3 && x < als.Count; x++) {
                al[x].CC.Setting = als[x];
            }
        }

        AFPServ[] al { get { return new AFPServ[] { s1, s2, s3, }; } }

        private void bStop_Click(object sender, EventArgs e) { al[0].Stop(); }

        private void bStart_Click(object sender, EventArgs e) { al[0].Start(); }

        private void Form1_Load(object sender, EventArgs e) {
            {
                tbFTPPort.DataBindings.Add("Text", al[0].CC, "FTPPort", false, DataSourceUpdateMode.OnPropertyChanged);
                tbAFPHost.DataBindings.Add("Text", al[0].CC, "AFPHost", false, DataSourceUpdateMode.OnPropertyChanged);
                tbAFPPort.DataBindings.Add("Text", al[0].CC, "AFPPort", false, DataSourceUpdateMode.OnPropertyChanged);
                al[0].RunningChanged += delegate { bStart.Enabled = !(bStop.Enabled = al[0].Running); };
            }
            {
                tbFTPPort2.DataBindings.Add("Text", al[1].CC, "FTPPort", false, DataSourceUpdateMode.OnPropertyChanged);
                tbAFPHost2.DataBindings.Add("Text", al[1].CC, "AFPHost", false, DataSourceUpdateMode.OnPropertyChanged);
                tbAFPPort2.DataBindings.Add("Text", al[1].CC, "AFPPort", false, DataSourceUpdateMode.OnPropertyChanged);
                al[1].RunningChanged += delegate { bStart2.Enabled = !(bStop2.Enabled = al[1].Running); };
            }
            {
                tbFTPPort3.DataBindings.Add("Text", al[2].CC, "FTPPort", false, DataSourceUpdateMode.OnPropertyChanged);
                tbAFPHost3.DataBindings.Add("Text", al[2].CC, "AFPHost", false, DataSourceUpdateMode.OnPropertyChanged);
                tbAFPPort3.DataBindings.Add("Text", al[2].CC, "AFPPort", false, DataSourceUpdateMode.OnPropertyChanged);
                al[2].RunningChanged += delegate { bStart3.Enabled = !(bStop3.Enabled = al[2].Running); };
            }

            new RadioButton[] { rbNo, rbAppend, rbPrepend }[Math.Max(0, Math.Min(2, al[0].CC.ForkMode))].Checked = true;
            new RadioButton[] { rbNo2, rbAppend2, rbPrepend2 }[Math.Max(0, Math.Min(2, al[1].CC.ForkMode))].Checked = true;
            new RadioButton[] { rbNo3, rbAppend3, rbPrepend3 }[Math.Max(0, Math.Min(2, al[2].CC.ForkMode))].Checked = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            foreach (AFPServ s in al) {
                s.Stop();
            }
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e) {
            if (rbNo.Checked) { al[0].CC.ForkMode = 0; }
            if (rbAppend.Checked) { al[0].CC.ForkMode = 1; }
            if (rbPrepend.Checked) { al[0].CC.ForkMode = 2; }
        }

        private void rbNo2_CheckedChanged(object sender, EventArgs e) {
            if (rbNo2.Checked) { al[1].CC.ForkMode = 0; }
            if (rbAppend2.Checked) { al[1].CC.ForkMode = 1; }
            if (rbPrepend2.Checked) { al[1].CC.ForkMode = 2; }
        }

        private void rbNo3_CheckedChanged(object sender, EventArgs e) {
            if (rbNo3.Checked) { al[2].CC.ForkMode = 0; }
            if (rbAppend3.Checked) { al[2].CC.ForkMode = 1; }
            if (rbPrepend3.Checked) { al[2].CC.ForkMode = 2; }
        }

        private void bStart2_Click(object sender, EventArgs e) { al[1].Start(); }

        private void bStop2_Click(object sender, EventArgs e) { al[1].Stop(); }

        private void bStart3_Click(object sender, EventArgs e) { al[2].Start(); }

        private void bStop3_Click(object sender, EventArgs e) { al[2].Stop(); }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            String s = "";
            foreach (AFPServ a in al) {
                s += " \"/s=" + a.CC.Setting + "\"";
            }
            tbCmdl.Text = s;
        }
    }

}
