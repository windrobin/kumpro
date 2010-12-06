using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tablega {
    public partial class TForm : Form {
        public TForm(String firstUrl) {
            this.firstUrl = firstUrl;

            InitializeComponent();
        }

        String firstUrl;

        private void wb_Navigated(object sender, WebBrowserNavigatedEventArgs e) {
            tbUrl.Text = wb.Url.ToString();
        }

        private void wb_LocationChanged(object sender, EventArgs e) { }

        private void bGo2Url_Click(object sender, EventArgs e) {
            wb.Navigate(tbUrl.Text);
        }

        private void wb_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e) { }

        private void TForm_Load(object sender, EventArgs e) {
            if (firstUrl != null) wb.Navigate(firstUrl);

            wb.StatusTextChanged += new EventHandler(wb_StatusTextChanged);
        }

        void wb_StatusTextChanged(object sender, EventArgs e) {
            lStat.Text = wb.StatusText;
        }

        private void bExport_Click(object sender, EventArgs e) {
            if (wb.Document == null) {
                MessageBox.Show(this, "失敗しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<HtmlElement> alTable = new List<HtmlElement>();
            foreach (HtmlElement el in wb.Document.All) {
                if (String.Compare(el.TagName, "table", true) == 0) {
                    alTable.Add(el);
                }
            }

            if (alTable.Count == 0) {
                MessageBox.Show(this, "Tableが見付かりませんでした。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            using (SelTblForm form = new SelTblForm(alTable)) {
                form.ShowDialog(this);
            }
        }

        private void bBack_Click(object sender, EventArgs e) {
            wb.GoBack();
        }

        private void bNext_Click(object sender, EventArgs e) {
            wb.GoForward();
        }
    }
}
