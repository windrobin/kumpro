using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ofn {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AutoUpgradeEnabled = false;
            ofd.ShowDialog();
        }
    }
}