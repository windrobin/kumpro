using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sfgaof.Properties;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Sfgaof {
    public partial class FForm : Form {
        public FForm() {
            InitializeComponent();
        }

        private void FForm_Load(object sender, EventArgs e) {
            //#define SFGAO_CAPABILITYMASK    0x00000177L
            //#define SFGAO_ENCRYPTED         0x00002000L     // object is 
            foreach (Match M in Regex.Matches(Resources.Flags, "^#define\\s+(?<n>[^ ]+)\\s+0x(?<v>[0-9a-fA-F]+)L(\\s+//(?<c>.+)$)?", RegexOptions.Multiline)) {
                ListViewItem lvi = lvf.Items.Add(M.Groups["n"].Value);
                lvi.SubItems.Add(M.Groups["v"].Value);
                lvi.SubItems.Add(M.Groups["c"].Value);
                lvi.Tag = Convert.ToUInt32(M.Groups["v"].Value, 16);
            }
        }

        class Bit {
            public uint v;
            public string s, c;

            public Bit(uint v, string s, string c) {
                this.v = v; this.s = s; this.c = c;
            }

            public override string ToString() {
                return string.Format("{0} ({1:x8}) {2}", s, v, c);
            }
        }

        private void lvf_ItemChecked(object sender, ItemCheckedEventArgs e) {
            uint val = 0;

            foreach (ListViewItem lvi in lvf.Items) {
                val |= lvi.Checked ? (uint)lvi.Tag : 0;
            }

            tbOut10.Text = val.ToString();
            tbOut16.Text = val.ToString("x8");
        }

        private void bIn10_Click(object sender, EventArgs e) {
            if (tt.GetToolTip(tbInput) != null) { tt.SetToolTip(tbInput, null); tt.Hide(tbInput); }

            try {
                bool is10 = sender == bIn10;

                uint val = is10 ? uint.Parse(tbInput.Text) : Convert.ToUInt32(tbInput.Text, 16);

                foreach (ListViewItem lvi in lvf.Items) {
                    lvi.Checked = (val & (uint)lvi.Tag) == (uint)lvi.Tag;
                }
            }
            catch (Exception err) {
                tt.SetToolTip(tbInput, err.Message);
                tt.Show(err.Message, tbInput);
            }
        }

        private void llGo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(((LinkLabel)sender).Text);
        }

        private void tt_Popup(object sender, PopupEventArgs e) {

        }

        private void tbInput_TextChanged(object sender, EventArgs e) {
            if (tt.GetToolTip(tbInput) != null) { tt.SetToolTip(tbInput, null); tt.Hide(tbInput); }
        }

    }
}