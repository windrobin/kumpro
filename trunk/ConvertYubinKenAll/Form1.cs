using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RidocHTMLImp;
using System.IO;
using Viewer;
using System.Net;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace ConvertYubinKenAll {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void bSel_Click(object sender, EventArgs e) {
            if (ofdcsv.ShowDialog(this) != DialogResult.OK) return;

            sfdcsv.FileName = Path.Combine(Path.GetDirectoryName(ofdcsv.FileName), "改" + Path.GetFileName(ofdcsv.FileName));

            if (sfdcsv.ShowDialog(this) == DialogResult.OK) {
                Csvr csv = new Csvr();
                csv.ReadAppended(File.ReadAllText(ofdcsv.FileName, Encoding.GetEncoding(932)), '"', ',');

                Conv(sfdcsv.FileName, csv, cbFmt.Text);

                MessageBox.Show(this, "変換しました。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void Conv(String fp2, Csvr csv, String fmt) {
            using (StreamWriter wr = new StreamWriter(fp2, false, Encoding.GetEncoding(932))) {
                for (int y = 0; y < csv.Rows.Count; y++) {
                    var cols = csv.Rows[y];
                    String code = cols[2];
                    String name = cols[6] + "" + cols[7] + "" + cols[8];
                    wr.WriteLine(fmt
                        .Replace("{郵便番号0000000}", code)
                        .Replace("{郵便番号000-0000}", code.Substring(0, 3) + "-" + code.Substring(3))
                        .Replace("{住所}", name.Split('（')[0])
                        .Replace("{住所空白有り}", cols[6] + " " + cols[7] + " " + cols[8].Split('（')[0])
                        );
                }
            }
        }

        WebClient wc = new WebClient();

        private void bFetch_Click(object sender, EventArgs e) {
            clb.Items.Clear();
            foreach (Match M in Regex.Matches(Encoding.UTF8.GetString(wc.DownloadData(tbURL.Text)), "\\<a\\s+href=\"(?<a>[^\"]+\\.zip)\">(?<s>[^\\>]+)\\</a\\>")) {
                clb.Items.Add(new Term { M = M, baseUrl = tbURL.Text });
            }
        }

        class Term {
            public Match M;
            public String baseUrl;

            public String Disp { get { return M.Groups["s"].Value; } }
            public String RelUrl { get { return M.Groups["a"].Value; } }
            public Uri Url { get { return new Uri(new Uri(baseUrl), RelUrl); } }

            public override string ToString() {
                return Disp;
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            wc.Headers[HttpRequestHeader.UserAgent] = "ConvertYubinKenAll/1.0";
        }

        Term[] selTerms;
        String cbFmt_Text;

        private void bConv_Click(object sender, EventArgs e) {
            if (sfdcsv.ShowDialog(this) == DialogResult.OK) {
                selTerms = clb.CheckedItems.Cast<Term>().ToArray();
                cbFmt_Text = cbFmt.Text;
                bwDLConv.RunWorkerAsync(sfdcsv.FileName);
                bConv.Enabled = false;
            }
        }

        private void bwDLConv_DoWork(object sender, DoWorkEventArgs e) {
            String dir2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "郵便番号データ");
            Directory.CreateDirectory(dir2);

            Csvr csv = new Csvr();
            foreach (Term t in selTerms) {
                String fpZip = Path.Combine(dir2, t.Disp + Path.GetExtension(t.RelUrl));
                bwDLConv.ReportProgress(0, "DL中\n" + t.Url);
                wc.DownloadFile(t.Url, fpZip);
                using (ZipFile zip = new ZipFile(fpZip)) {
                    String dirEx2 = Path.Combine(dir2, t.Disp);
                    Directory.CreateDirectory(dirEx2);
                    bwDLConv.ReportProgress(0, "展開中\n" + fpZip);
                    zip.ExtractAll(dirEx2, ExtractExistingFileAction.OverwriteSilently);
                    foreach (String fpcsv in Directory.GetFiles(dirEx2, "*.csv")) {
                        bwDLConv.ReportProgress(0, "処理中\n" + fpcsv);
                        csv.ReadAppended(File.ReadAllText(fpcsv, Encoding.GetEncoding(932)), '"', ',');
                    }
                }
            }

            bwDLConv.ReportProgress(0, "変換中\n" + sfdcsv.FileName);
            Conv(sfdcsv.FileName, csv, cbFmt_Text);

            bwDLConv.ReportProgress(0, "完了");
        }

        private void bwDLConv_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            bConv.Enabled = true;

            if (e.Error == null) {
                MessageBox.Show(this, "変換しました。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show(this, "失敗しました。" + e.Error, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void bwDLConv_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            lWip.Text = "" + e.UserState;
        }
    }
}
