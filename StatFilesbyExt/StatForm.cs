using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace StatFilesbyExt {
    public partial class StatForm : Form {
        String TargetDir;

        public StatForm(String dir) {
            this.TargetDir = dir;
            InitializeComponent();
        }

        Label la;

        private void StatForm_Load(object sender, EventArgs e) {
            bwComputer.RunWorkerAsync();

            vStatDataGridView.Sort(c容量の割合, ListSortDirection.Descending);

            la = new Label();
            la.Parent = this;
            la.Dock = DockStyle.Fill;
            la.Text = "走査・集計しています。御待ちください。";
            la.TextAlign = ContentAlignment.MiddleCenter;
            la.Show();
        }

        class G {
            public Int64 size = 0;
            public Int32 cnt = 0;
        }

        SortedDictionary<String, G> dict = new SortedDictionary<String, G>(StringComparer.CurrentCultureIgnoreCase);

        private void bwComputer_DoWork(object sender, DoWorkEventArgs e) {
            dict.Clear();
            Stack<string> dirs = new Stack<string>();
            dirs.Push(TargetDir);
            while (dirs.Count != 0) {
                String dir = dirs.Pop();
                try {
                    foreach (String fp in Directory.GetFiles(dir, "*.*")) {
                        G g = null;
                        String fext = Path.GetExtension(fp);
                        if (!dict.TryGetValue(fext, out g)) {
                            dict[fext] = g = new G();
                        }
                        g.cnt++;
                        g.size += new FileInfo(fp).Length;
                    }
                    foreach (String subdir in Directory.GetDirectories(dir)) dirs.Push(subdir);
                }
                catch (Exception) {

                }
            }

            dsVol.VStat.Rows.Clear();
            Int64 sizeTotal = 0;
            Int32 cntTotal = 0;
            foreach (G g in dict.Values) {
                sizeTotal += g.size;
                cntTotal += g.cnt;
            }
            foreach (KeyValuePair<String, G> kv in dict) {
                dsVol.VStat.AddVStatRow(
                    kv.Key,
                    kv.Value.cnt, (float)((double)kv.Value.cnt / cntTotal),
                    kv.Value.size, (float)((double)kv.Value.size / sizeTotal)
                    );
            }
        }

        private void StatForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (bwComputer.IsBusy)
                bwComputer.CancelAsync();
        }

        private void bwComputer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) {
                Close();
                return;
            }
            la.Dispose();
            tsc.Show();
        }

        private void tsbCopy_Click(object sender, EventArgs e) {
            StringWriter wr = new StringWriter();
            DataGridView gv = vStatDataGridView;
            {
                int x = 0;
                foreach (DataGridViewColumn col in gv.Columns) {
                    if (x != 0) {
                        wr.Write("\t");
                    }
                    x++;
                    wr.Write(col.HeaderText);
                }
                wr.WriteLine();
            }
            foreach (DataGridViewRow row in gv.Rows) {
                int x = 0;
                foreach (DataGridViewCell cell in row.Cells) {
                    if (x != 0) {
                        wr.Write("\t");
                    }
                    x++;
                    wr.Write(Convert.ToString(cell.Value));
                }
                wr.WriteLine();
            }

            Clipboard.SetText(wr.ToString());
            MessageBox.Show(this, "コピーしました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}