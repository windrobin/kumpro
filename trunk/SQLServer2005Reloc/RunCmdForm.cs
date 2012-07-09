using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace SQLServer2005Reloc {
    public partial class RunCmdForm : Form {
        public RunCmdForm(String cmd) {
            InitializeComponent();

            tbCmd.Text = cmd;
        }

        private void bExec_Click(object sender, EventArgs e) {
            String fp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N")) + ".bat";
            File.WriteAllText(fp, tbCmd.Text, Encoding.Default);
            Process p = Process.Start(fp);
            Close();
        }
    }
}