using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QueryMbcsChars {
    public partial class QForm : Form {
        public QForm() {
            InitializeComponent();
        }

        private void tb1_TextChanged(object sender, EventArgs e) {
            if (tb1.TextLength < 1) return;
            byte b = Encoding.Default.GetBytes(tb1.Text)[0];

            lCode.Text = b.ToString("x2");

            String s = "";

            // http://charset.7jp.net/sjis.html
            for (int y = 0x81; y <= 0x9f; y++) {
                for (int x = 0x40; x <= 0xfc; x++) {
                    if (x == b) {
                        Label l = new Label();
                        s += Encoding.Default.GetString(new byte[] { (byte)y, (byte)x });
                    }
                }
            }
            for (int y = 0xe0; y <= 0xef; y++) {
                for (int x = 0x40; x <= 0xfc; x++) {
                    if (x == b) {
                        Label l = new Label();
                        s += Encoding.Default.GetString(new byte[] { (byte)y, (byte)x });
                    }
                }
            }

            tbRes.Text = s;
        }
    }
}