using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MacVlmAddExt {
    public partial class EForm : Form {
        public EForm() {
            InitializeComponent();
        }

        private void bRef_Click(object sender, EventArgs e) {
            fbd.SelectedPath = tbDir.Text;
            if (fbd.ShowDialog(this) == DialogResult.OK)
                tbDir.Text = fbd.SelectedPath;
        }

        private void bRun_Click(object sender, EventArgs e) {
            if (!Directory.Exists(tbDir.Text)) {
                tbDir.Select();
                return;
            }
            new RunForm(tbDir.Text).Show();
        }
    }
}