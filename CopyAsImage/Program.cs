using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace CopyAsImage {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length != 0) {
                try {
                    using (Bitmap pic = new Bitmap(args[0])) {
                        Clipboard.SetImage(pic);
                    }
                    MessageBox.Show("コピーしました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception err) {
                    MessageBox.Show("失敗しました。\n\n" + err.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}