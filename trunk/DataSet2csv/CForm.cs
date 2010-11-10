using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace DataSet2csv {
    public partial class CForm : Form {
        public CForm() {
            InitializeComponent();
        }

        private void bRead_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.xml|*.xml||";
            if (ofd.ShowDialog(this) == DialogResult.OK) {
                dataSet1.Reset();
                dataSet1.ReadXml(ofd.FileName);

                cbTable.Items.Clear();

                foreach (DataTable dt in dataSet1.Tables) {
                    cbTable.Items.Add(dt.TableName);
                }

                SynchronizationContext.Current.Post(delegate(object state) {
                    if (cbTable.Items.Count != 0) {
                        cbTable.SelectedIndex = 0;
                    }
                }, null);
            }
        }

        private void cbTable_Click(object sender, EventArgs e) {

        }

        private void cbTable_SelectedIndexChanged(object sender, EventArgs e) {
            String name = cbTable.SelectedItem as String;
            if (!String.IsNullOrEmpty(name)) {
                gv.DataSource = null;
                bindingSource1.DataMember = null;
                bindingSource1.DataMember = name;
                gv.DataSource = bindingSource1;
            }
        }

        private void bTabsv_Click(object sender, EventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.csv|*.csv||";
            if (sfd.ShowDialog(this) == DialogResult.OK) {
                using (StreamWriter os = new StreamWriter(sfd.FileName, false, Encoding.Default)) {
                    Writer wr = new Writer(os, (sender == bTabsv) ? "\t" : ",");
                    if (bIncludeHeader.Checked) {
                        foreach (DataGridViewColumn col in gv.Columns) {
                            wr.Write(col.HeaderText);
                        }
                        wr.NextLine();
                    }
                    foreach (DataGridViewRow row in gv.Rows) {
                        if (row.IsNewRow) continue;
                        foreach (DataGridViewCell cell in row.Cells) {
                            wr.Write(Convert.ToString(cell.Value));
                        }
                        wr.NextLine();
                    }
                }
                MessageBox.Show(this, "保存しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        class Writer {
            TextWriter wr;
            String camma;
            int x = 0, y = 0;

            public Writer(TextWriter wr, String camma) {
                this.wr = wr;
                this.camma = camma;
            }

            public void NextLine() {
                x = 0;
                y++;
            }

            public void Write(String s) {
                if (s == null)
                    s = "";
                if (x == 0 && y != 0) {
                    wr.WriteLine();
                }
                if (x != 0) {
                    wr.Write(camma);
                }
                if (s.Contains(camma)) {
                    wr.Write("\"" + s.Replace("\"", "\"\"") + "\"");
                }
                else {
                    wr.Write(s.Replace("\"", "\"\""));
                }
                x++;
            }
        }

        private void bCopySel_Click(object sender, EventArgs e) {
            StringWriter text = new StringWriter();
            Writer wr = new Writer(text, ",");
            foreach (DataGridViewRow row in gv.Rows) {
                if (row.IsNewRow) continue;
                int cnt = 0;
                foreach (DataGridViewCell cell in row.Cells) {
                    if (!cell.Selected) continue;

                    wr.Write(Convert.ToString(cell.Value));
                    cnt++;
                }
                if (cnt == 0) continue;
                wr.NextLine();
            }

            String s = text.ToString();
            if (String.IsNullOrEmpty(s))
                Clipboard.Clear();
            else
                Clipboard.SetText(text.ToString());

            MessageBox.Show(this, "選択部分をコピーしました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}