using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using ShachoUndelivered;
using System.Threading;
using HDD.LibEML;
using System.IO;
using System.Text.RegularExpressions;

namespace PeekPOP3 {
    public partial class PForm : Form {
        public PForm() {
            InitializeComponent();
        }

        POP3 pop3 = null;

        private void bConn_Click(object sender, EventArgs e) {
            using (ConnForm form = new ConnForm()) {
                if (form.ShowDialog(this) == DialogResult.OK) {
                    using (this.pop3) { }
                    this.pop3 = form.pop3;
                    UpdateLvm();
                }
            }
        }

        private void UpdateLvm() {
            lvm.Items.Clear();
            foreach (MailItem mail in pop3.GetList()) {
                ListViewItem lvi = lvm.Items.Add(mail.ToString(), 0);
                lvi.Tag = mail;
                lvi.SubItems.Add(mail.size.ToString("#,##0"));
            }
        }

        private void lvm_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (ListViewItem lvi in lvm.SelectedItems) {
                tbEML.Enabled = false; Update();
                tbEML.Text = pop3.Retrieve(((MailItem)lvi.Tag).i);
                lSize.Text = ((MailItem)lvi.Tag).size.ToString("#,##0");
                tbEML.Enabled = true; Update();
                break;
            }
        }

        private void bResolver_Click(object sender, EventArgs e) {
            if (bwResolver.IsBusy)
                return;

            bwResolver.RunWorkerAsync();
        }

        SynchronizationContext Sync;

        private void bwResolver_DoWork(object sender, DoWorkEventArgs e) {
            POP3 pop3 = this.pop3;
            if (pop3 == null)
                return;
            while (true) {
                MailItem next = null;
                ListViewItem lviIt = null;
                Sync.Send(delegate(object state) {
                    foreach (ListViewItem lvi in lvm.Items) {
                        if (lvi.ImageIndex == 0) {
                            next = (MailItem)lvi.Tag;
                            lviIt = lvi;
                            break;
                        }
                    }
                }, null);
                if (next == null || lviIt == null)
                    break;
                String rows = pop3.Top(next.i, 1);
                String text = "?";
                try {
                    EML_Reader er = new EML_Reader();
                    er.read(new StringReader(rows));
                    text = er.main.mlSubject;
                }
                catch (Exception) {

                }
                Sync.Send(delegate(object state) {
                    lviIt.ImageIndex = 1;
                    lviIt.Text = text;
                }, null);
            }
        }

        private void PForm_Load(object sender, EventArgs e) {
            Sync = SynchronizationContext.Current;
        }

        private void cms_Opening(object sender, CancelEventArgs e) {
            bSaveTo.Enabled = lvm.SelectedIndices.Count == 1;
        }

        private void bSaveTo_Click(object sender, EventArgs e) {
            foreach (ListViewItem lvi in lvm.SelectedItems) {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = Regex.Replace(lvi.Text, "[\\\\\\/\\:\\?\"\\|\\<\\>\\*]", "_") + ".eml";
                sfd.Filter = "*.eml|*.eml||";
                if (sfd.ShowDialog(this) == DialogResult.OK) {
                    MemoryStream os = new MemoryStream();
                    try {
                        lvm.Enabled = false; Update();
                        foreach (char c in pop3.Retrieve(((MailItem)lvi.Tag).i)) os.WriteByte((byte)c);
                        File.WriteAllBytes(sfd.FileName, os.ToArray());
                    }
                    finally {
                        lvm.Enabled = true; Update();
                    }
                }
                break;
            }
        }
    }
}