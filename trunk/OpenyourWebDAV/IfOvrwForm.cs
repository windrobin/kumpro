using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenyourWebDAV {
    public partial class IfOvrwForm : Form {
        String fp;

        public IfOvrwForm(String fp) {
            this.fp = fp;

            InitializeComponent();
        }

        private void IfOvrwForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.DialogResult == DialogResult.None)
                this.DialogResult = DialogResult.Abort;
        }

        private void IfOvrwForm_Load(object sender, EventArgs e) {
            lfp.Text = fp;
        }

        private void bOk_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
