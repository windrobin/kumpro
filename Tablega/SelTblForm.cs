using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Tablega {
    public partial class SelTblForm : Form {
        List<HtmlElement> alTable;

        public SelTblForm(List<HtmlElement> alTable) {
            this.alTable = alTable;

            InitializeComponent();
        }

        private void SelTblForm_Load(object sender, EventArgs e) {
            int x = 0;
            foreach (HtmlElement el in alTable) {
                String name = el.Id;
                if (String.IsNullOrEmpty(name)) name = el.Name;
                if (String.IsNullOrEmpty(name)) name = x.ToString();
                ToolStripItem tsi = ts.Items.Add(name);
                tsi.Tag = el;
                tsi.Click += new EventHandler(tsi_Click);
                x++;
            }
        }

        enum TRowType {
            None, Head, Tail,
        }

        class TRow {
            public TRowType RowType = TRowType.None;
            public List<string> Cells = new List<string>();
        }

        class T2csv {
            public static List<TRow> Convert(HtmlElement table) {
                List<TRow> alRow = new List<TRow>();

                foreach (HtmlElement el in table.Children) {
                    if (String.Compare(el.TagName, "thead", true) == 0) {
                        foreach (HtmlElement elChild in el.Children) {
                            if (String.Compare(elChild.TagName, "tr", true) == 0) {
                                ReadTr(alRow, TRowType.Head, elChild);
                            }
                        }
                    }
                    if (String.Compare(el.TagName, "tfoot", true) == 0) {
                        foreach (HtmlElement elChild in el.Children) {
                            if (String.Compare(elChild.TagName, "tr", true) == 0) {
                                ReadTr(alRow, TRowType.Tail, elChild);
                            }
                        }
                    }
                    else if (String.Compare(el.TagName, "tbody", true) == 0) {
                        foreach (HtmlElement elChild in el.Children) {
                            if (String.Compare(elChild.TagName, "tr", true) == 0) {
                                ReadTr(alRow, TRowType.None, elChild);
                            }
                        }
                    }
                    else if (String.Compare(el.TagName, "tr", true) == 0) {
                        ReadTr(alRow, TRowType.None, el);
                    }
                }
                return alRow;
            }

            private static void ReadTr(List<TRow> alRow, TRowType rowType, HtmlElement elParent) {
                TRow row = new TRow();
                row.RowType = rowType;
                foreach (HtmlElement el in elParent.Children) {
                    if (String.Compare(el.TagName, "td", true) == 0 || String.Compare(el.TagName, "th", true) == 0) {
                        row.Cells.Add(el.InnerText);
                    }
                }
                alRow.Add(row);
            }
        }

        void tsi_Click(object sender, EventArgs e) {
            HtmlElement table = (HtmlElement)((ToolStripItem)sender).Tag;
            List<TRow> alRow = T2csv.Convert(table);
            int y = 0;
            DataTable dt = new DataTable();
            int maxcx = (alRow.Count == 0) ? 0 : alRow.Max(p => p.Cells.Count);

            foreach (TRow row in alRow) {
                if (y == 0) {
                    int x = 0;
                    foreach (String col in row.Cells) {
                        dt.Columns.Add("C" + x).Caption = col ?? "";
                        x++;
                    }
                    while (dt.Columns.Count < maxcx) {
                        dt.Columns.Add("C" + x).Caption = "";
                        x++;
                    }
                }
                if (y != 0 || (0 != (ModifierKeys & Keys.Shift))) {
                    dt.Rows.Add(row.Cells.ToArray());
                }
                y++;
            }
            gv.DataSource = null;
            gv.DataSource = dt;

            foreach (DataGridViewColumn col in gv.Columns) {
                col.HeaderText = dt.Columns[col.Index].Caption;
            }

            gv.AutoResizeColumns();
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
                if (s.Contains(camma) || s.IndexOfAny(new char[] { '\r', '\n' }) >= 0) {
                    wr.Write("\"" + s.Replace("\"", "\"\"") + "\"");
                }
                else {
                    wr.Write(s.Replace("\"", "\"\""));
                }
                x++;
            }
        }

        private void bCopy_Click(object sender, EventArgs e) {
            StringWriter os = new StringWriter();
            Writer wr = new Writer(os, (true) ? "\t" : ",");
            {
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

            String s = os.ToString();
            if (String.IsNullOrEmpty(s))
                Clipboard.Clear();
            else
                Clipboard.SetText(os.ToString());

            MessageBox.Show(this, "コピーしました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bConv2Date_Click(object sender, EventArgs e) {
            DateTime dt;
            foreach (DataGridViewRow row in gv.Rows) {
                if (row.IsNewRow) continue;
                foreach (DataGridViewCell cell in row.Cells) {
                    if (DateTime.TryParse(Convert.ToString(cell.Value), out dt)) {
                        cell.Value = (dt.TimeOfDay.TotalMilliseconds != 0)
                            ? dt.ToString("yyyy/MM/dd HH:mm:ss")
                            : dt.ToString("yyyy/MM/dd");
                    }
                }
            }

            MessageBox.Show(this, "しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
