using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LibGetYomi;

namespace Test {
    public partial class EForm : Form {
        public EForm() {
            InitializeComponent();
        }

        private void bImm_Click(object sender, EventArgs e) {
            tbRes.Text = String.Join("\r\n", new ImmYomi().GetYomi(tbIn.Text));
        }

        private void bTsf_Click(object sender, EventArgs e) {
            tbRes.Text = new TSFYomi().GetYomi(tbIn.Text);
        }
    }
}