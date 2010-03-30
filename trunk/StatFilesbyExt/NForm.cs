using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace StatFilesbyExt {
    public partial class NForm : Form {
        public NForm() {
            InitializeComponent();
        }

        private void buttonRef_Click(object sender, EventArgs e) {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "フォルダを選択してください。";
            fbd.SelectedPath = cbIn.Text;
            if (fbd.ShowDialog(this) == DialogResult.OK) {
                cbIn.Text = fbd.SelectedPath;
            }
        }

        private void buttonStat_Click(object sender, EventArgs e) {
            StatForm form = new StatForm(cbIn.Text);
            int i = cbIn.FindStringExact(cbIn.Text);
            if (i < 0) {
                cbIn.Items.Insert(0, cbIn.Text);
                List<string> al = new List<string>();
                foreach (object o in cbIn.Items) al.Add(o.ToString());
                RecentDirs = al.ToArray();
            }

            form.Show();
        }

        private void NForm_Load(object sender, EventArgs e) {
            cbIn.Items.Clear();
            cbIn.Items.AddRange(RecentDirs);
        }

        String fprd { get { return Path.Combine(Application.StartupPath, "RecentDirs.txt"); } }

        String[] RecentDirs {
            get {
                if (File.Exists(fprd))
                    return File.ReadAllLines(fprd, Encoding.UTF8);
                return new String[0];
            }
            set {
                File.WriteAllLines(fprd, value, Encoding.UTF8);
            }
        }
    }
}