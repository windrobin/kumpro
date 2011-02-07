using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace MacVlmAddExt {
    public partial class RunForm : Form {
        String dirIn;

        public RunForm(String dirIn) {
            this.dirIn = dirIn;

            InitializeComponent();
        }

        private void RunForm_Load(object sender, EventArgs e) {
            Sync = SynchronizationContext.Current;

            bwAddExt.RunWorkerAsync();
        }

        SynchronizationContext Sync;

        private void bwAddExt_DoWork(object sender, DoWorkEventArgs e) {
            Stack<string> alDir = new Stack<string>();
            alDir.Push(dirIn);
            int cnt = 0, cntWatched = 0;
            while (alDir.Count != 0) {
                String dir = alDir.Pop();
                foreach (String fp in Directory_GetFiles(dir)) {
                    try {
                        bool isIt = false;
                        using (FileStream fs = File.OpenRead(fp)) {
                            byte[] bin = new byte[4];
                            if (4 == fs.Read(bin, 0, 4)) {
                                if (bin[0] == 0x00 && bin[1] == 0xAB && bin[2] == 0xCD && bin[3] == 0xE1) {
                                    isIt = true;
                                }
                            }
                        }
                        cntWatched++;

                        if (isIt) {
                            if (String.Compare(".vlm", Path.GetExtension(fp), true) != 0) {
                                String fpVlm = fp + ".vlm";

                                File.Move(fp, fpVlm);
                                cnt++;

                                Sync.Send(delegate(object state) {
                                    lRepo.Text = String.Format("{0:#,##0}個、名前を変えました。({1:#,##0}個、ファイル有り)", cnt, cntWatched);
                                }, null);
                            }
                        }
                    }
                    catch (Exception) {
                    }
                }
                foreach (String dirSub in Directory_GetDirectories(dir)) {
                    alDir.Push(dirSub);
                }
            }
        }

        private IEnumerable<string> Directory_GetDirectories(string dir) {
            try {
                return Directory.GetDirectories(dir);
            }
            catch (Exception) {
                return new string[0];
            }
        }

        private IEnumerable<string> Directory_GetFiles(String dir) {
            try {
                return Directory.GetFiles(dir);
            }
            catch (Exception) {
                return new string[0];
            }
        }

        private void bwAddExt_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            bClose.Enabled = true;
            lDone.Show();
            lExecing.Hide();

            if (e.Error != null) {
                MessageBox.Show(this, "問題が有りました: " + e.Error, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bClose_Click(object sender, EventArgs e) {
            Close();
        }
    }
}